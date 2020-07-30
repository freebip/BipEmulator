namespace BipEmulator.Host
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbPressureMeasurementComplited = new System.Windows.Forms.CheckBox();
            this.nudPressure = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbGeoLocationMeasurementComplited = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAltitude = new System.Windows.Forms.TextBox();
            this.tbLongitude = new System.Windows.Forms.TextBox();
            this.tbLatitude = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbHeartRateMeasurementComplited = new System.Windows.Forms.CheckBox();
            this.nudHeartRate = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSwipeUp = new System.Windows.Forms.Button();
            this.btnLongKeypress = new System.Windows.Forms.Button();
            this.btnShortKeypress = new System.Windows.Forms.Button();
            this.btnSwipeDown = new System.Windows.Forms.Button();
            this.btnSwipeLeft = new System.Windows.Forms.Button();
            this.btnClick = new System.Windows.Forms.Button();
            this.btnSwipeRight = new System.Windows.Forms.Button();
            this.gbWatchScreen = new System.Windows.Forms.GroupBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblMouseLocation = new System.Windows.Forms.Label();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnUserResFileSelect = new System.Windows.Forms.Button();
            this.tbUserResFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSystemResFileSelect = new System.Windows.Forms.Button();
            this.tbSystemResFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnColorsSelect = new System.Windows.Forms.Button();
            this.tbFontFilename = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLocale = new System.Windows.Forms.ComboBox();
            this.btnFontSelect = new System.Windows.Forms.Button();
            this.tpDebug = new System.Windows.Forms.TabPage();
            this.rtbDebugLog = new System.Windows.Forms.RichTextBox();
            this.btnCLose = new System.Windows.Forms.Button();
            this.ucScreen = new BipEmulator.Host.WatchScreen();
            this.tcMain.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPressure)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeartRate)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.gbWatchScreen.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpMain);
            this.tcMain.Controls.Add(this.tpOptions);
            this.tcMain.Controls.Add(this.tpDebug);
            this.tcMain.Location = new System.Drawing.Point(12, 12);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(565, 398);
            this.tcMain.TabIndex = 0;
            this.tcMain.SelectedIndexChanged += new System.EventHandler(this.tcMain_SelectedIndexChanged);
            // 
            // tpMain
            // 
            this.tpMain.Controls.Add(this.groupBox6);
            this.tpMain.Controls.Add(this.groupBox5);
            this.tpMain.Controls.Add(this.groupBox4);
            this.tpMain.Controls.Add(this.groupBox2);
            this.tpMain.Controls.Add(this.gbWatchScreen);
            this.tpMain.Location = new System.Drawing.Point(4, 24);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(557, 370);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main page";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbPressureMeasurementComplited);
            this.groupBox6.Controls.Add(this.nudPressure);
            this.groupBox6.Location = new System.Drawing.Point(348, 98);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(191, 83);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Pressure, mmHg";
            // 
            // cbPressureMeasurementComplited
            // 
            this.cbPressureMeasurementComplited.AutoSize = true;
            this.cbPressureMeasurementComplited.Location = new System.Drawing.Point(9, 51);
            this.cbPressureMeasurementComplited.Name = "cbPressureMeasurementComplited";
            this.cbPressureMeasurementComplited.Size = new System.Drawing.Size(159, 19);
            this.cbPressureMeasurementComplited.TabIndex = 16;
            this.cbPressureMeasurementComplited.Text = "Measurement completed";
            this.cbPressureMeasurementComplited.UseVisualStyleBackColor = true;
            // 
            // nudPressure
            // 
            this.nudPressure.Location = new System.Drawing.Point(9, 22);
            this.nudPressure.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPressure.Name = "nudPressure";
            this.nudPressure.Size = new System.Drawing.Size(89, 23);
            this.nudPressure.TabIndex = 15;
            this.nudPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbGeoLocationMeasurementComplited);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.tbAltitude);
            this.groupBox5.Controls.Add(this.tbLongitude);
            this.groupBox5.Controls.Add(this.tbLatitude);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(348, 187);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(191, 169);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "GeoLocation";
            // 
            // cbGeoLocationMeasurementComplited
            // 
            this.cbGeoLocationMeasurementComplited.AutoSize = true;
            this.cbGeoLocationMeasurementComplited.Location = new System.Drawing.Point(9, 113);
            this.cbGeoLocationMeasurementComplited.Name = "cbGeoLocationMeasurementComplited";
            this.cbGeoLocationMeasurementComplited.Size = new System.Drawing.Size(159, 19);
            this.cbGeoLocationMeasurementComplited.TabIndex = 17;
            this.cbGeoLocationMeasurementComplited.Text = "Measurement completed";
            this.cbGeoLocationMeasurementComplited.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "Altitude:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Longitude:";
            // 
            // tbAltitude
            // 
            this.tbAltitude.Location = new System.Drawing.Point(76, 84);
            this.tbAltitude.Name = "tbAltitude";
            this.tbAltitude.Size = new System.Drawing.Size(100, 23);
            this.tbAltitude.TabIndex = 20;
            // 
            // tbLongitude
            // 
            this.tbLongitude.Location = new System.Drawing.Point(76, 55);
            this.tbLongitude.Name = "tbLongitude";
            this.tbLongitude.Size = new System.Drawing.Size(100, 23);
            this.tbLongitude.TabIndex = 19;
            // 
            // tbLatitude
            // 
            this.tbLatitude.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbLatitude.Location = new System.Drawing.Point(76, 26);
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(100, 23);
            this.tbLatitude.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "Latitude:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbHeartRateMeasurementComplited);
            this.groupBox4.Controls.Add(this.nudHeartRate);
            this.groupBox4.Location = new System.Drawing.Point(348, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(191, 83);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Heart rate";
            // 
            // cbHeartRateMeasurementComplited
            // 
            this.cbHeartRateMeasurementComplited.AutoSize = true;
            this.cbHeartRateMeasurementComplited.Location = new System.Drawing.Point(9, 51);
            this.cbHeartRateMeasurementComplited.Name = "cbHeartRateMeasurementComplited";
            this.cbHeartRateMeasurementComplited.Size = new System.Drawing.Size(159, 19);
            this.cbHeartRateMeasurementComplited.TabIndex = 16;
            this.cbHeartRateMeasurementComplited.Text = "Measurement completed";
            this.cbHeartRateMeasurementComplited.UseVisualStyleBackColor = true;
            // 
            // nudHeartRate
            // 
            this.nudHeartRate.Location = new System.Drawing.Point(9, 22);
            this.nudHeartRate.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudHeartRate.Name = "nudHeartRate";
            this.nudHeartRate.Size = new System.Drawing.Size(89, 23);
            this.nudHeartRate.TabIndex = 15;
            this.nudHeartRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSwipeUp);
            this.groupBox2.Controls.Add(this.btnLongKeypress);
            this.groupBox2.Controls.Add(this.btnShortKeypress);
            this.groupBox2.Controls.Add(this.btnSwipeDown);
            this.groupBox2.Controls.Add(this.btnSwipeLeft);
            this.groupBox2.Controls.Add(this.btnClick);
            this.groupBox2.Controls.Add(this.btnSwipeRight);
            this.groupBox2.Location = new System.Drawing.Point(9, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(330, 116);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Controls";
            // 
            // btnSwipeUp
            // 
            this.btnSwipeUp.Location = new System.Drawing.Point(119, 22);
            this.btnSwipeUp.Name = "btnSwipeUp";
            this.btnSwipeUp.Size = new System.Drawing.Size(96, 23);
            this.btnSwipeUp.TabIndex = 1;
            this.btnSwipeUp.Text = "Swipe Up";
            this.btnSwipeUp.UseVisualStyleBackColor = true;
            this.btnSwipeUp.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnLongKeypress
            // 
            this.btnLongKeypress.Location = new System.Drawing.Point(221, 51);
            this.btnLongKeypress.Name = "btnLongKeypress";
            this.btnLongKeypress.Size = new System.Drawing.Size(96, 23);
            this.btnLongKeypress.TabIndex = 2;
            this.btnLongKeypress.Text = "Long Keypress";
            this.btnLongKeypress.UseVisualStyleBackColor = true;
            this.btnLongKeypress.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnShortKeypress
            // 
            this.btnShortKeypress.Location = new System.Drawing.Point(221, 22);
            this.btnShortKeypress.Name = "btnShortKeypress";
            this.btnShortKeypress.Size = new System.Drawing.Size(96, 23);
            this.btnShortKeypress.TabIndex = 0;
            this.btnShortKeypress.Text = "Short Keypress";
            this.btnShortKeypress.UseVisualStyleBackColor = true;
            this.btnShortKeypress.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnSwipeDown
            // 
            this.btnSwipeDown.Location = new System.Drawing.Point(119, 80);
            this.btnSwipeDown.Name = "btnSwipeDown";
            this.btnSwipeDown.Size = new System.Drawing.Size(96, 23);
            this.btnSwipeDown.TabIndex = 6;
            this.btnSwipeDown.Text = "Swipe Down";
            this.btnSwipeDown.UseVisualStyleBackColor = true;
            this.btnSwipeDown.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnSwipeLeft
            // 
            this.btnSwipeLeft.Location = new System.Drawing.Point(17, 80);
            this.btnSwipeLeft.Name = "btnSwipeLeft";
            this.btnSwipeLeft.Size = new System.Drawing.Size(96, 23);
            this.btnSwipeLeft.TabIndex = 1;
            this.btnSwipeLeft.Text = "Swipe Left";
            this.btnSwipeLeft.UseVisualStyleBackColor = true;
            this.btnSwipeLeft.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnClick
            // 
            this.btnClick.Location = new System.Drawing.Point(119, 51);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(96, 23);
            this.btnClick.TabIndex = 5;
            this.btnClick.Text = "Center Click";
            this.btnClick.UseVisualStyleBackColor = true;
            this.btnClick.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // btnSwipeRight
            // 
            this.btnSwipeRight.Location = new System.Drawing.Point(221, 80);
            this.btnSwipeRight.Name = "btnSwipeRight";
            this.btnSwipeRight.Size = new System.Drawing.Size(96, 23);
            this.btnSwipeRight.TabIndex = 4;
            this.btnSwipeRight.Text = "Swipe Right";
            this.btnSwipeRight.UseVisualStyleBackColor = true;
            this.btnSwipeRight.Click += new System.EventHandler(this.btnControl_Click);
            // 
            // gbWatchScreen
            // 
            this.gbWatchScreen.Controls.Add(this.lblAction);
            this.gbWatchScreen.Controls.Add(this.lblMouseLocation);
            this.gbWatchScreen.Controls.Add(this.ucScreen);
            this.gbWatchScreen.Location = new System.Drawing.Point(9, 9);
            this.gbWatchScreen.Margin = new System.Windows.Forms.Padding(6);
            this.gbWatchScreen.Name = "gbWatchScreen";
            this.gbWatchScreen.Padding = new System.Windows.Forms.Padding(6);
            this.gbWatchScreen.Size = new System.Drawing.Size(330, 222);
            this.gbWatchScreen.TabIndex = 20;
            this.gbWatchScreen.TabStop = false;
            this.gbWatchScreen.Text = "Watch Screen";
            // 
            // lblAction
            // 
            this.lblAction.Location = new System.Drawing.Point(9, 155);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(68, 46);
            this.lblAction.TabIndex = 2;
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblMouseLocation
            // 
            this.lblMouseLocation.Location = new System.Drawing.Point(265, 155);
            this.lblMouseLocation.Name = "lblMouseLocation";
            this.lblMouseLocation.Size = new System.Drawing.Size(56, 46);
            this.lblMouseLocation.TabIndex = 1;
            this.lblMouseLocation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.groupBox3);
            this.tpOptions.Location = new System.Drawing.Point(4, 24);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(6);
            this.tpOptions.Size = new System.Drawing.Size(557, 370);
            this.tpOptions.TabIndex = 2;
            this.tpOptions.Text = "Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnUserResFileSelect);
            this.groupBox3.Controls.Add(this.tbUserResFile);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.btnSystemResFileSelect);
            this.groupBox3.Controls.Add(this.tbSystemResFile);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btnColorsSelect);
            this.groupBox3.Controls.Add(this.tbFontFilename);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cbLocale);
            this.groupBox3.Controls.Add(this.btnFontSelect);
            this.groupBox3.Location = new System.Drawing.Point(9, 9);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(533, 349);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            // 
            // btnUserResFileSelect
            // 
            this.btnUserResFileSelect.Location = new System.Drawing.Point(498, 313);
            this.btnUserResFileSelect.Name = "btnUserResFileSelect";
            this.btnUserResFileSelect.Size = new System.Drawing.Size(26, 23);
            this.btnUserResFileSelect.TabIndex = 24;
            this.btnUserResFileSelect.Text = "...";
            this.btnUserResFileSelect.UseVisualStyleBackColor = true;
            this.btnUserResFileSelect.Click += new System.EventHandler(this.btnUserResFileSelect_Click);
            // 
            // tbUserResFile
            // 
            this.tbUserResFile.Location = new System.Drawing.Point(12, 313);
            this.tbUserResFile.Name = "tbUserResFile";
            this.tbUserResFile.ReadOnly = true;
            this.tbUserResFile.Size = new System.Drawing.Size(480, 23);
            this.tbUserResFile.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.TabIndex = 22;
            this.label7.Text = "User resource file:";
            // 
            // btnSystemResFileSelect
            // 
            this.btnSystemResFileSelect.Location = new System.Drawing.Point(498, 268);
            this.btnSystemResFileSelect.Name = "btnSystemResFileSelect";
            this.btnSystemResFileSelect.Size = new System.Drawing.Size(26, 23);
            this.btnSystemResFileSelect.TabIndex = 21;
            this.btnSystemResFileSelect.Text = "...";
            this.btnSystemResFileSelect.UseVisualStyleBackColor = true;
            this.btnSystemResFileSelect.Click += new System.EventHandler(this.btnSystemResFileSelect_Click);
            // 
            // tbSystemResFile
            // 
            this.tbSystemResFile.Location = new System.Drawing.Point(12, 269);
            this.tbSystemResFile.Name = "tbSystemResFile";
            this.tbSystemResFile.ReadOnly = true;
            this.tbSystemResFile.Size = new System.Drawing.Size(480, 23);
            this.tbSystemResFile.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 19;
            this.label2.Text = "System resource file:";
            // 
            // btnColorsSelect
            // 
            this.btnColorsSelect.Location = new System.Drawing.Point(391, 40);
            this.btnColorsSelect.Name = "btnColorsSelect";
            this.btnColorsSelect.Size = new System.Drawing.Size(133, 23);
            this.btnColorsSelect.TabIndex = 18;
            this.btnColorsSelect.Text = "Colors";
            this.btnColorsSelect.UseVisualStyleBackColor = true;
            this.btnColorsSelect.Click += new System.EventHandler(this.btnColorsSelect_Click);
            // 
            // tbFontFilename
            // 
            this.tbFontFilename.Location = new System.Drawing.Point(12, 225);
            this.tbFontFilename.Name = "tbFontFilename";
            this.tbFontFilename.ReadOnly = true;
            this.tbFontFilename.Size = new System.Drawing.Size(480, 23);
            this.tbFontFilename.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Locale:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Font file:";
            // 
            // cbLocale
            // 
            this.cbLocale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocale.FormattingEnabled = true;
            this.cbLocale.Location = new System.Drawing.Point(9, 40);
            this.cbLocale.Name = "cbLocale";
            this.cbLocale.Size = new System.Drawing.Size(133, 23);
            this.cbLocale.TabIndex = 12;
            // 
            // btnFontSelect
            // 
            this.btnFontSelect.Location = new System.Drawing.Point(498, 224);
            this.btnFontSelect.Name = "btnFontSelect";
            this.btnFontSelect.Size = new System.Drawing.Size(26, 23);
            this.btnFontSelect.TabIndex = 10;
            this.btnFontSelect.Text = "...";
            this.btnFontSelect.UseVisualStyleBackColor = true;
            this.btnFontSelect.Click += new System.EventHandler(this.btnFontSelect_Click);
            // 
            // tpDebug
            // 
            this.tpDebug.Controls.Add(this.rtbDebugLog);
            this.tpDebug.Location = new System.Drawing.Point(4, 24);
            this.tpDebug.Margin = new System.Windows.Forms.Padding(6);
            this.tpDebug.Name = "tpDebug";
            this.tpDebug.Padding = new System.Windows.Forms.Padding(6);
            this.tpDebug.Size = new System.Drawing.Size(557, 370);
            this.tpDebug.TabIndex = 1;
            this.tpDebug.Text = "Debug output";
            this.tpDebug.UseVisualStyleBackColor = true;
            // 
            // rtbDebugLog
            // 
            this.rtbDebugLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDebugLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbDebugLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbDebugLog.Location = new System.Drawing.Point(9, 9);
            this.rtbDebugLog.Name = "rtbDebugLog";
            this.rtbDebugLog.ReadOnly = true;
            this.rtbDebugLog.Size = new System.Drawing.Size(539, 352);
            this.rtbDebugLog.TabIndex = 0;
            this.rtbDebugLog.Text = "";
            // 
            // btnCLose
            // 
            this.btnCLose.Location = new System.Drawing.Point(12, 416);
            this.btnCLose.Name = "btnCLose";
            this.btnCLose.Size = new System.Drawing.Size(96, 23);
            this.btnCLose.TabIndex = 1;
            this.btnCLose.Text = "Close";
            this.btnCLose.UseVisualStyleBackColor = true;
            this.btnCLose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucScreen
            // 
            this.ucScreen.BackColor = System.Drawing.Color.LightGray;
            this.ucScreen.BackgroundColor = System.Drawing.Color.Empty;
            this.ucScreen.ForegroundColor = System.Drawing.Color.Empty;
            this.ucScreen.Location = new System.Drawing.Point(83, 25);
            this.ucScreen.MaximumSize = new System.Drawing.Size(176, 176);
            this.ucScreen.MinimumSize = new System.Drawing.Size(176, 176);
            this.ucScreen.Name = "ucScreen";
            this.ucScreen.Size = new System.Drawing.Size(176, 176);
            this.ucScreen.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 448);
            this.Controls.Add(this.btnCLose);
            this.Controls.Add(this.tcMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcMain.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPressure)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeartRate)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.gbWatchScreen.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tpDebug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox cbPressureMeasurementComplited;
        private System.Windows.Forms.NumericUpDown nudPressure;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cbGeoLocationMeasurementComplited;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbAltitude;
        private System.Windows.Forms.TextBox tbLongitude;
        private System.Windows.Forms.TextBox tbLatitude;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cbHeartRateMeasurementComplited;
        private System.Windows.Forms.NumericUpDown nudHeartRate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSwipeUp;
        private System.Windows.Forms.Button btnLongKeypress;
        private System.Windows.Forms.Button btnShortKeypress;
        private System.Windows.Forms.Button btnSwipeDown;
        private System.Windows.Forms.Button btnSwipeLeft;
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.Button btnSwipeRight;
        private System.Windows.Forms.GroupBox gbWatchScreen;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnColorsSelect;
        private System.Windows.Forms.TextBox tbFontFilename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLocale;
        private System.Windows.Forms.Button btnFontSelect;
        private System.Windows.Forms.TabPage tpDebug;
        private System.Windows.Forms.Button btnCLose;
        private System.Windows.Forms.RichTextBox rtbDebugLog;
        private WatchScreen ucScreen;
        private System.Windows.Forms.Button btnUserResFileSelect;
        private System.Windows.Forms.TextBox tbUserResFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSystemResFileSelect;
        private System.Windows.Forms.TextBox tbSystemResFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMouseLocation;
        private System.Windows.Forms.Label lblAction;
    }
}

