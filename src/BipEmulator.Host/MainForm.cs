using BipEmulatorProxy;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BipEmulator.Host
{
    public partial class MainForm : Form, IMessageFilter
    {
        const int VIDEO_MEMORY_SIZE = 15488;
        /// <summary>
        /// Функция обратного вызова
        /// </summary>
        private static ProxyLib.Callback _callback;

        Point _mouseDownPoint;

        private ResFile _systemResFile = null;
        private ResFile _userResFile = null;

        private LocalSettings _settings;

        private int _newDebugOutputLines = 0;

        private IntPtr _hrmDataPointer = IntPtr.Zero;

        private bool _useSharedVideoMemory;
        MemoryMappedFile _sharedMemoryFile;
        MemoryMappedViewStream _sharedMemoryViewStream;
        private byte[] _videoMemory = new byte[VIDEO_MEMORY_SIZE];

        public MainForm()
        {
            InitializeComponent();
            // установим фильтр для отлова клавиатурных сообщений
            Application.AddMessageFilter(this);

            // locale
            foreach (var locale in Enum.GetValues(typeof(LocaleEnum)))
            {
                var name = DisplayNameAttribute.GetName(locale);
                cbLocale.Items.Add(name);
            }

            _settings = LocalSettings.Load();
            FillFormValuesFromSettings();

            // устанавливаем обработчики на мышь для
            // создания событий по свайпам
            ucScreen.MouseDown += WatchScreen_MouseDown;
            ucScreen.MouseUp += WatchScreen_MouseUp;
            ucScreen.MouseMove += WatchScreen_MouseMove;
            ucScreen.MouseLeave += WatchScreen_MouseLeave;

            // информация о версии
            var version = Assembly.GetEntryAssembly()?.GetName().Version;
            base.Text = $"Bip Emulator v{version.Major}.{version.Minor}";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // пробуем загрузить файл шрифтов
            if (string.IsNullOrEmpty(tbFontFilename.Text) || !File.Exists(tbFontFilename.Text))
            {
                MessageBox.Show("Font file don't exist or not selected!" + Environment.NewLine + "Check Options Tab!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // регистрируем функцию обратного вызова
            _callback = new ProxyLib.Callback(CallbackParser);
            ProxyLib.RegisterCallback(_callback);

            // запускаем на выполнение программу, которая эмулируется
            ProxyLib.MainRun();
        }

        /// <summary>
        /// Разгребатель прилетевших от прокси обратных вызовов функций
        /// </summary>
        /// <param name="args">аргументы функций</param>
        /// <returns></returns>
        private unsafe object CallbackParser(object[] args)
        {
            if (args == null || args.Length == 0 || args[0] == null)
                return null;

            switch(args[0])
            {
                // Отрабатываем все функции графического отображения и относящиеся к ним

                case FunctionNames.DRAW_FILLED_RECT:
                    ucScreen.DrawFilledRect((int)args[1], (int)args[2], (int)args[3], (int)args[4]);
                    break;

                case FunctionNames.DRAW_FILLED_RECT_BG:
                    ucScreen.DrawFilledRectBg((int)args[1], (int)args[2], (int)args[3], (int)args[4]);
                    break;

                case FunctionNames.DRAW_RECT:
                    ucScreen.DrawRect((int)args[1], (int)args[2], (int)args[3], (int)args[4]);
                    break;

                case FunctionNames.DRAW_HORIZONTAL_LINE:
                    ucScreen.DrawLine((int)args[2], (int)args[1], (int)args[3], (int)args[1]);
                    break;

                case FunctionNames.DRAW_VERTICAL_LINE:
                    ucScreen.DrawLine((int)args[1], (int)args[2], (int)args[1], (int)args[3]);
                    break;

                case FunctionNames.FILL_SCREEN_BG:
                    ucScreen.FillScreenBg();
                    break;

                case FunctionNames.TEXT_OUT:
                    ucScreen.TextOut(args[1].ToString(), (int)args[2], (int)args[3]);
                    break;

                case FunctionNames.TEXT_OUT_CENTER:
                    ucScreen.TextOutCenter(args[1].ToString(), (int)args[2], (int)args[3]);
                    break;

                case FunctionNames.SET_FG_COLOR:
                    ucScreen.SetFgColor(WatchScreen.GetColorFromGRBInt((int)args[1]));
                    break;

                case FunctionNames.SET_BG_COLOR:
                    ucScreen.SetBgColor(WatchScreen.GetColorFromGRBInt((int)args[1]));
                    break;

                case FunctionNames.GET_TEXT_HEIGHT:
                    return ucScreen.GetTextHeight();

                case FunctionNames.TEXT_WIDTH:
                    return ucScreen.GetTextWidth(args[1].ToString());

                case FunctionNames.REPAINT_SCREEN_LINES:

                    if (_useSharedVideoMemory)
                    {
                        _sharedMemoryViewStream.Position = 0;
                        _sharedMemoryViewStream.Read(_videoMemory, 0, 15488);
                        ucScreen.SetVideoData(_videoMemory);
                    }
                    ucScreen.RepaintScreenLines((int)args[1], (int)args[2]);

                    if (ucScreen.InvokeRequired)
                    {
                        try
                        {
                            Invoke(new Action(() =>
                            {
                                ucScreen.Refresh();
                            }));
                        }
                        catch
                        {
                            Debug.WriteLine("Exception CallbackParser REPAINT_SCREEN_LINES");
                        }
                    }
                    else
                        ucScreen.Refresh();
                    break;

                case FunctionNames.SHOW_ELF_RES_BY_ID:
                    try
                    {
                        if (_userResFile == null)
                        {
                            _userResFile = ResFile.Load(tbUserResFile.Text);
                        }

                        var resImage = new ResImage(_userResFile.Resources[(int)args[2]]);
                        ucScreen.DrawImage(resImage.Bitmap, (int)args[3], (int)args[4]);
                    }
                    catch
                    {
                        Debug.WriteLine("Exception CallbackParser SHOW_ELF_RES_BY_ID");
                    }

                    return 0;

                case FunctionNames.SHOW_RES_BY_ID:
                    ShowResById((int)args[1], (int)args[2], (int)args[3]);
                    return 0;

                case FunctionNames.GET_RES_PARAMS:
                    var res = GetUserResImage((int)args[2]);
                    if (res == null)
                        return -1;

                    var bmp = res.Bitmap;

                    var param = new ResParam { Width=(short)bmp.Width, Height=(short)bmp.Height };
                    CopyStructToUnmanagedMemory(param, (IntPtr)args[3]);
                    return 0;

                case FunctionNames.SHOW_BIG_DIGITS:
                    ShowBigDigit((int)args[1], args[2].ToString(), (int)args[3], (int)args[4], (int)args[5]);
                    break;

                case FunctionNames.LOG_PRINTF:
                    DebugWriteLine(args[1].ToString());
                    return 0;

                case FunctionNames.GET_LAST_HEARTRATE:
                    int heartRate = 0;
                    if (nudHeartRate.InvokeRequired)
                        Invoke(new Action(() =>
                        {
                            heartRate = (int)nudHeartRate.Value;
                        }));
                    else
                        heartRate = (int)nudHeartRate.Value;
                    return heartRate;

                case FunctionNames.GET_HRM_STRUCT:
                    HrmData hrmData = new HrmData();
                    if (nudHeartRate.InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            hrmData = new HrmData 
                            { 
                                heart_rate = (byte)nudHeartRate.Value,  
                                last_hr = (byte)nudHeartRate.Value, 
                                ret_code = (byte)(cbHeartRateMeasurementComplited.Checked ? 0 : 5)
                            };
                        }));
                    }
                    else
                        hrmData = new HrmData
                        {
                            heart_rate = (byte)nudHeartRate.Value,
                            last_hr = (byte)nudHeartRate.Value,
                            ret_code = (byte)(cbHeartRateMeasurementComplited.Checked ? 0 : 5)
                        };


                    if (_hrmDataPointer == IntPtr.Zero)
                        _hrmDataPointer = Marshal.AllocHGlobal(Marshal.SizeOf(hrmData));
                    Marshal.StructureToPtr(hrmData, _hrmDataPointer, false);
                    return _hrmDataPointer.ToInt32();

                case FunctionNames.GET_NAVI_DATA:
                    NaviData naviData = new NaviData();
                    if (cbGeoLocationMeasurementComplited.InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            var alt = GetFloatFromText(tbAltitude.Text);
                            var lat = GetFloatFromText(tbLatitude.Text);
                            var lon = GetFloatFromText(tbLongitude.Text);

                            naviData = new NaviData
                            {
                                ready = (cbPressureMeasurementComplited.Checked ? 1 : 0) | (cbGeoLocationMeasurementComplited.Checked ? 0x0e : 0),
                                pressure = (uint)((float)nudPressure.Value * 133.322f),
                                altitude = alt,
                                latitude = (int)(3e6 * Math.Abs(lat)),
                                ns = lat < 0 ? 1 : 0,
                                longitude = (int)(3e6 * Math.Abs(lon)),
                                ew = lon < 0 ? 2 : 3
                            };
                        }));
                    }
                    else
                    {
                        var alt = GetFloatFromText(tbAltitude.Text);
                        var lat = GetFloatFromText(tbLatitude.Text);
                        var lon = GetFloatFromText(tbLongitude.Text);

                        naviData = new NaviData
                        {
                            ready = (cbPressureMeasurementComplited.Checked ? 1 : 0) | (cbGeoLocationMeasurementComplited.Checked ? 0x0e : 0),
                            pressure = (uint)((float)nudPressure.Value * 133.322f),
                            altitude = alt,
                            latitude = (int)(3e6 * Math.Abs(lat)),
                            ns = lat < 0 ? 1 : 0,
                            longitude = (int)(3e6 * Math.Abs(lon)),
                            ew = lon < 0 ? 2 : 3
                        };
                    }
                    CopyStructToUnmanagedMemory(naviData, (IntPtr)args[1]);
                    break;

                case FunctionNames.IS_GPS_FIXED:
                    int isGpsFixed = 0;
                    if (cbGeoLocationMeasurementComplited.InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            isGpsFixed = cbGeoLocationMeasurementComplited.Checked ? 1 : 0;
                        }));
                    }
                    else
                        isGpsFixed = cbGeoLocationMeasurementComplited.Checked ? 1 : 0;
                    return isGpsFixed;

                case FunctionNames.GET_SELECTED_LOCALE:
                    LocaleEnum locale = LocaleEnum.ru_RU;
                    if (cbLocale.InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            locale = GetCurrentLocale();
                        }));
                    }
                    else
                    {
                        locale = GetCurrentLocale();
                    }
                    return (int)locale;

                case FunctionNames.SHARED_MEMORY_ENABLED:
                    _useSharedVideoMemory = OpenFileMapping();
                    return _useSharedVideoMemory ? 1 : 0;

                // заглушки

                case FunctionNames.VIBRATE:
                case FunctionNames.SET_UPDATE_PERIOD:
                    return 0;

                case FunctionNames.LOAD_FONT:
                case FunctionNames.REG_MENU:
                    break;

                default:
                    Debug.WriteLine($"UNKNOWN FUNCTION: {args[0]}");
                    break;

            }
            return null;
        }

        private bool OpenFileMapping()
        {
            try
            {
                _sharedMemoryFile = MemoryMappedFile.OpenExisting("meme", MemoryMappedFileRights.ReadWrite);
                _sharedMemoryViewStream = _sharedMemoryFile.CreateViewStream();
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        private LocaleEnum GetCurrentLocale()
        {
            foreach (LocaleEnum locale in Enum.GetValues(typeof(LocaleEnum)))
            {
                if (DisplayNameAttribute.GetName(locale).Equals(cbLocale.SelectedItem))
                {
                    return locale;
                }
            }
            return LocaleEnum.ru_RU;
        }

        private float GetFloatFromText(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0f;
            
            float res;
            if (float.TryParse(str, out res))
                return res;

            if (float.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out res))
                return res;

            return 0f;
        }

        private unsafe void CopyStructToUnmanagedMemory<T>(T t, IntPtr dst)  where T : struct
        {
            IntPtr pnt = Marshal.AllocHGlobal(Marshal.SizeOf(t));
            Marshal.StructureToPtr(t, pnt, false);
            Buffer.MemoryCopy(pnt.ToPointer(), dst.ToPointer(), Marshal.SizeOf(t), Marshal.SizeOf(t));
            Marshal.FreeHGlobal(pnt);
        }

        private ResImage GetUserResImage(int resId)
        {
            try
            {
                if (_userResFile == null)
                {
                    _userResFile = ResFile.Load(tbUserResFile.Text);
                }

                return new ResImage(_userResFile.Resources[resId]);
            }
            catch
            {
                Debug.WriteLine("Exception GetUserResImage");
            }
            return null;
        }

        private ResImage GetSystemResImage(int resId)
        {
            try
            {
                if (_systemResFile == null)
                {
                    _systemResFile = ResFile.Load(tbSystemResFile.Text);
                }

                return new ResImage(_systemResFile.Resources[resId]);
            }
            catch
            {
                Debug.WriteLine("Exception GetSystemResImage");
            }
            return null;
        }

        private void ShowResById(int resId, int x, int y)
        {
            var resImage = GetSystemResImage(resId);
            if (resImage == null)
                return;

            ucScreen.DrawImage(resImage.Bitmap, x, y);
        }

        private void ShowBigDigit(int symbolSet, string value, int x, int y, int space)
        {
            for(var i=0; i<value.Length; i++)
            {
               (int resId, int width) = SymbolSet.GetResource(symbolSet, value[i]);

                if (resId == -1)
                    continue;

                ShowResById(resId, x, y);
                x += width + space;
            }
        }


        /// <summary>
        /// Перехватываем сообщения с клавиатуры и передаем их прокси
        /// </summary>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != 0x0100)
                return false;

            switch ((Keys)m.WParam.ToInt32())
            {
                case Keys.Up:
                    lblAction.Text = "Swipe Up";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_SWIPE_UP, 0, 0);
                    btnSwipeUp.Focus();
                    break;
                case Keys.Down:
                    lblAction.Text = "Swipe Down";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_SWIPE_DOWN, 0, 0);
                    btnSwipeDown.Focus();
                    break;
                case Keys.Left:
                    lblAction.Text = "Swipe Left";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_SWIPE_LEFT, 0, 0);
                    btnSwipeLeft.Focus();
                    break;
                case Keys.Right:
                    lblAction.Text = "Swipe Right";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_SWIPE_RIGHT, 0, 0);
                    btnSwipeRight.Focus();
                    break;
                default:
                    return false;
            }

            return true;
        }

        private void WatchScreen_MouseUp(object sender, MouseEventArgs e)
        {
            var dx = e.X - _mouseDownPoint.X;
            var dy = e.Y - _mouseDownPoint.Y;

            if (Math.Abs(dx) < _settings.MaxDistanceClick && Math.Abs(dy) < _settings.MaxDistanceClick)
            {
                lblAction.Text = "Click";
                ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_CLICK, _mouseDownPoint.X, _mouseDownPoint.Y);
                btnClick.Focus();
            }
            else if (Math.Abs(dx) > Math.Abs(dy))
            {
                lblAction.Text = dx < 0 ? "Swipe Left" : "Swipe Right";
                ProxyLib.RegMenuDispatchScreen(dx < 0 ? (byte)GestureEnum.GESTURE_SWIPE_LEFT : (byte)GestureEnum.GESTURE_SWIPE_RIGHT, e.X, e.Y);
                if (dx < 0)
                    btnSwipeLeft.Focus();
                else
                    btnSwipeRight.Focus();
            }
            else
            {
                lblAction.Text = dy < 0 ? "Swipe Up" : "Swipe Down";
                ProxyLib.RegMenuDispatchScreen(dy < 0 ? (byte)GestureEnum.GESTURE_SWIPE_UP : (byte)GestureEnum.GESTURE_SWIPE_DOWN, e.X, e.Y);
                if (dy < 0)
                    btnSwipeUp.Focus();
                else
                    btnSwipeDown.Focus();
            }
        }

        private void WatchScreen_MouseMove(object sender, MouseEventArgs e)
        {
            lblMouseLocation.Text = $"X: {e.X}\r\nY: {e.Y}";
        }

        private void WatchScreen_MouseLeave(object sender, EventArgs e)
        {
            lblMouseLocation.Text = string.Empty;
        }

        private void WatchScreen_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDownPoint = e.Location;
        }

        /// <summary>
        /// При закрытии формы все очищаем и сохраняем настройки
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProxyLib.UnregisterCallback();

            FillSettingsFromFormValues();
            _settings.Save();
        }

        /// <summary>
        /// Заполняем значения элементов формы параметрами из сохраненных настроек
        /// </summary>
        private void FillFormValuesFromSettings()
        {
            nudHeartRate.Value = _settings.HeartRate;
            cbHeartRateMeasurementComplited.Checked = _settings.HeartRateMeasurementCompleted;
            tbLatitude.Text = _settings.Latitude.ToString("F6");
            tbLongitude.Text = _settings.Longitude.ToString("F6");
            tbAltitude.Text = _settings.Altitude.ToString("F2");
            cbGeoLocationMeasurementComplited.Checked = _settings.GeoLocationMeasurementCompleted;
            nudPressure.Value = _settings.Pressure;
            cbPressureMeasurementComplited.Checked = _settings.PressureMeasurementCompleted;
            cbLocale.SelectedItem = DisplayNameAttribute.GetName(_settings.Locale);
            tbFontFilename.Text = _settings.FontFilename;
            tbSystemResFile.Text = _settings.SystemResourceFilename;
            tbUserResFile.Text = _settings.UserResourceFilename;
            ucScreen.Colors = _settings.Colors;

            cbLocale.SelectedItem = DisplayNameAttribute.GetName(_settings.Locale);

            if (File.Exists(tbFontFilename.Text))
                ucScreen.SetFontFile(tbFontFilename.Text);
        }

        /// <summary>
        /// Заполняем настройки для сохранения параметрами из элементов формы
        /// </summary>
        private void FillSettingsFromFormValues()
        {
            _settings.HeartRate = (int)nudHeartRate.Value;
            _settings.HeartRateMeasurementCompleted = cbHeartRateMeasurementComplited.Checked;
            _settings.Latitude = double.Parse(tbLatitude.Text);
            _settings.Longitude = double.Parse(tbLongitude.Text);
            _settings.Altitude = double.Parse(tbAltitude.Text);
            _settings.GeoLocationMeasurementCompleted = cbGeoLocationMeasurementComplited.Checked;
            _settings.Pressure = (int)nudPressure.Value;
            _settings.PressureMeasurementCompleted = cbPressureMeasurementComplited.Checked;
            _settings.Locale = GetCurrentLocale();
            _settings.FontFilename = tbFontFilename.Text;
            _settings.SystemResourceFilename = tbSystemResFile.Text;
            _settings.UserResourceFilename = tbUserResFile.Text;
            _settings.Colors = ucScreen.Colors;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Настройка цветовых параметров
        /// </summary>
        private void btnColorsSelect_Click(object sender, EventArgs e)
        {
            var frm = new ColorSelectionForm(ucScreen.Colors);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ucScreen.Colors = frm.Colors;
            }
        }

        /// <summary>
        /// Выбо файла шрифта
        /// </summary>
        private void btnFontSelect_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = @"Font Files (*.ft)|*.ft|" + @"All files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false,
                RestoreDirectory = true,
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbFontFilename.Text = ofd.FileName;
                ucScreen.SetFontFile(ofd.FileName);
            }
        }

        /// <summary>
        /// Выбор системного файла ресурсов
        /// </summary>
        private void btnSystemResFileSelect_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = @"Res Files (*.res)|*.res|" + @"All files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false,
                RestoreDirectory = true,
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _systemResFile = null;
                tbSystemResFile.Text = ofd.FileName;
            }
        }

        /// <summary>
        /// Выбор пользовательского файла ресурсов
        /// относящего к эмулируемой программе
        /// </summary>
        private void btnUserResFileSelect_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = @"Res Files (*.res)|*.res|" + @"All files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false,
                RestoreDirectory = true,
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _userResFile = null;
                tbUserResFile.Text = ofd.FileName;
            }
        }

        /// <summary>
        /// Отладочный вывод эмулятора
        /// </summary>
        private void DebugWriteLine(string text)
        {
            if (rtbDebugLog.InvokeRequired)
                Invoke(new Action(() =>
                {
                    rtbDebugLog.Text += text + Environment.NewLine;
                }));
            else
                rtbDebugLog.Text += text + Environment.NewLine;

            if (tpDebug.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    if (tcMain.SelectedTab != tpDebug)
                        _newDebugOutputLines++;
                }));
            }
            else
            {
                if (tcMain.SelectedTab != tpDebug)
                    _newDebugOutputLines++;
            }

            ShowDebugTabPageTitle();
        }

        private void ShowDebugTabPageTitle()
        {
            var title = "Debug Output";
            if (_newDebugOutputLines > 0)
                title += $" ({_newDebugOutputLines})";

            if (tpDebug.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    tpDebug.Text = title;
                }));
            }
            else
                tpDebug.Text = title;
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (btn == null)
                return;

            // bad bad bad practice
            switch(btn.Name)
            {
                case "btnSwipeUp":
                    lblAction.Text = "Swipe Up";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_SWIPE_UP, 0, 0);
                    btnSwipeUp.Focus();
                    break;
                case "btnSwipeDown":
                    lblAction.Text = "Swipe Down";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_SWIPE_DOWN, 0, 0);
                    btnSwipeDown.Focus();
                    break;
                case "btnSwipeLeft":
                    lblAction.Text = "Swipe Left";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_SWIPE_LEFT, 0, 0);
                    btnSwipeLeft.Focus();
                    break;
                case "btnSwipeRight":
                    lblAction.Text = "Swipe Right";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_SWIPE_RIGHT, 0, 0);
                    btnSwipeRight.Focus();
                    break;
                case "btnClick":
                    lblAction.Text = "Click";
                    ProxyLib.RegMenuDispatchScreen((byte)GestureEnum.GESTURE_CLICK, 176 / 2, 176 / 2);
                    btnClick.Focus();
                    break;
                case "btnShortKeypress":
                    lblAction.Text = "Short Keypress";
                    ProxyLib.RegMenuKeyPress();
                    btnShortKeypress.Focus();
                    break;
                case "btnLongKeypress":
                    lblAction.Text = "Long Keypress";
                    ProxyLib.RegMenuLongKeyPress();
                    btnLongKeypress.Focus();
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"UNKNOWN BUTTON: {btn.Name} IN btnControl_Click");
                    break;
            }
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedTab == tpDebug)
                _newDebugOutputLines = 0;
            ShowDebugTabPageTitle();
        }
    }
}
