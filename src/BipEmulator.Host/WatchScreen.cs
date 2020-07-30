using System;
using System.Drawing;
using System.Windows.Forms;
using FastBitmapLib;

namespace BipEmulator.Host
{
    public partial class WatchScreen : UserControl
    {
        const int SIZE = 176;
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }

        private Bitmap _shadowImage = new Bitmap(SIZE, SIZE);
        private Bitmap _primImage = new Bitmap(SIZE, SIZE);

        private Brush _foreBrush;
        private Brush _backBrush;
        private Pen _forePen;

        private FtFile _ftFile;
        private FtMaker _ftMaker;

        public ColorArray Colors { get; set; }

        public WatchScreen()
        {
            InitializeComponent();
            DoubleBuffered = true;
    }

    public void SetFontFile(string fontFilename)
        {
            _ftFile = FtFile.Load(fontFilename);
            _ftMaker = new FtMaker(_ftFile);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Width = SIZE;
            Height = SIZE;
        }

        public void RepaintScreenLines(int from, int to)
        {
            var r = new Rectangle(0, from, SIZE, to - from);
            FastBitmap.CopyRegion(_shadowImage, _primImage, r, r);
        }

        public int GetTextHeight()
        {
            if (_ftMaker == null)
                return 0;
            return _ftMaker.GetTextHeight();
        }

        public int GetTextWidth(string text)
        {
            if (_ftMaker == null)
                return 0;
            return _ftMaker.GetTextWidth(text);
        }

        public void TextOutCenter(string text, int x, int y)
        {
            if (_ftMaker == null)
                return;

            _ftMaker.TextOutCenter(_shadowImage, text, x, y, ForegroundColor, BackgroundColor);
        }

        public void TextOut(string text, int x, int y)
        {
            if (_ftMaker == null)
                return;
            _ftMaker.TextOut(_shadowImage, text, x, y, ForegroundColor, BackgroundColor);
        }

        public void FillScreenBg()
        {
            FastBitmap.ClearBitmap(_shadowImage, BackgroundColor);
        }
        public void FillScreenFg()
        {
            FastBitmap.ClearBitmap(_shadowImage, ForegroundColor);
        }

        public void SetFgColor(Color color)
        {
            ForegroundColor = GetRealColor(color);
            _foreBrush = new SolidBrush(ForegroundColor);
            _forePen = new Pen(ForegroundColor);
        }
        public void SetBgColor(Color color)
        {
            BackgroundColor = GetRealColor(color);
            _backBrush = new SolidBrush(BackgroundColor);
        }

        public void DrawLine(int x0, int y0, int x1, int y1)
        {
            var g = Graphics.FromImage(_shadowImage);
            g.DrawLine(_forePen, x0, y0, x1, y1);
        }

        public void DrawFilledRectBg(int x0, int y0, int x1, int y1)
        {
            var g = Graphics.FromImage(_shadowImage);
            g.FillRectangle(_backBrush, x0, y0, x1 - x0, y1 - y0);
        }

        public void DrawFilledRect(int x0, int y0, int x1, int y1)
        {
            var g = Graphics.FromImage(_shadowImage);
            g.FillRectangle(_foreBrush, x0, y0, x1 - x0, y1 - y0);
        }

        public void DrawRect(int x0, int y0, int x1, int y1)
        {
            var g = Graphics.FromImage(_shadowImage);
            g.DrawRectangle(_forePen, x0, y0, x1 - x0, y1 - y0);
        }

        public void DrawImage(Bitmap image, int x, int y)
        {
            var g = Graphics.FromImage(_shadowImage);

            var fastImage = new FastBitmap(image);
            fastImage.Lock();
            for (var i = 0; i < image.Width; i++)
                for (var j = 0; j < image.Height; j++)
                    fastImage.SetPixel(i, j, GetRealColor(fastImage.GetPixel(i, j)));
            fastImage.Unlock();

            g.DrawImage(image, x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(_primImage, 0, 0);
        }

        private Color GetRealColor(Color color)
        {
            switch ((uint)color.ToArgb())
            {
                // red
                case 0xFFFF0000:
                    return Colors.Red; // (187, 113, 136)
                // green
                case 0xFF00FF00:
                    return Colors.Green; // (89, 181, 132)
                // blue
                case 0xFF0000FF:
                    return Colors.Blue; // (28, 110, 194)
                // yellow
                case 0xFFFFFF00:
                    return Colors.Yellow; // (221, 214, 133)
                // aqua
                case 0xFF00FFFF:
                    return Colors.Aqua; // (70, 198, 207)
                // purple
                case 0xFFFF00FF:
                    return Colors.Purple; // (173, 147, 208)
                // white
                case 0xFFFFFFFF:
                    return Colors.White; // (220, 220, 220)
                // black
                case 0xFF000000:
                    return Colors.Black; //  (56, 56, 56)
            }
            return color;
        }

        public static Color GetColorFromGRBInt(int value)
        {
            return Color.FromArgb(value & 0xff, (value >> 8) & 0xff, (value >> 16) & 0xff);
        }
    }
}
