using FastBitmapLib;
using System;
using System.Drawing;

namespace BipEmulator.Host
{
    public class FtMaker
    {
        const int EMPTY_LETTER_WIDTH = 9;
        const int LETTER_SPACING = 2;

        private FtFile _ftFile;
        public FtMaker(FtFile ftFile)
        {
            _ftFile = ftFile;
        }

        public Bitmap GetImage(string text)
        {
            var bmp = new Bitmap(GetTextWidth(text), GetTextHeight());
            TextOutCenter(bmp, text, 0, 0, Color.White, Color.Black);
            return bmp;
        }

        private void SetPixel(FastBitmap fbmp, int x, int y, Color color)
        {
            if (x < 0 || y < 0 || x >= fbmp.Width || y >= fbmp.Height)
                return;
            fbmp.SetPixel(x, y, color);
        }

        public void TextOutCenter(Bitmap bmp, string text, int x, int y, Color fgColor, Color bgColor, bool bgTransparent = false)
        {
            var w = GetTextWidth(text);
            var h = GetTextHeight();
            TextOut(bmp, text, x - w / 2, y, fgColor, bgColor, bgTransparent);
        }


        public void TextOut(Bitmap bmp, string text, int x, int y, Color fgColor, Color bgColor, bool bgTransparent = false)
        {
            var fbmp = new FastBitmap(bmp);
            fbmp.Lock();
            var xCurrent = x;

            for (var i = 0; i < text.Length; i++)
            {
                var key = (int)text[i];
                var letterWidth = GetLetterWidth(text[i]);

                if (_ftFile.Letters.ContainsKey(key))
                {
                    var data = (byte[])_ftFile.Letters[key];
                    for (var j = 0; j < data.Length / 2; j++)
                    {
                        var line = Swap(BitConverter.ToUInt16(data, 2 * j));

                        for (var k = 0; k < letterWidth; k++)
                        {
                            if ((line & 0x8000) != 0)
                                SetPixel(fbmp, xCurrent + k, y + j, fgColor);
                            else if (!bgTransparent)
                                SetPixel(fbmp, xCurrent + k, y + j, bgColor);
                            line = (ushort)(line << 1);
                        }

                        // для пропорционального шрифта добавляем межбуквенный интервал
                        if (!bgTransparent && _ftFile.Version == 8)
                        {
                            for (var k = 0; k < LETTER_SPACING; k++)
                            {
                                SetPixel(fbmp, xCurrent + letterWidth + k, y + j, bgColor);
                            }
                        }
                    }
                }
                else
                {
                    if (!bgTransparent)
                    {
                        for (var j = 0; j < GetTextHeight(); j++)
                            for (var k = 0; k < EMPTY_LETTER_WIDTH + LETTER_SPACING; k++)
                                SetPixel(fbmp, xCurrent + k, y + j, bgColor);
                    }
                }
                xCurrent += letterWidth + (_ftFile.Version == 8 ? LETTER_SPACING : 0);
            }
            fbmp.Unlock();
        }

        private ushort Swap(ushort value)
        {
            return (ushort)((value >> 8) | (value << 8));
        }

        private int GetLetterWidth(char letter)
        {
            int width = 0;
            if (_ftFile.Letters.ContainsKey((int)letter))
            {
                var data = (byte[])_ftFile.Letters[(int)letter];
                var maxLen = 0;
                for (var j = 0; j < data.Length / 2; j++)
                {
                    var line = Swap(BitConverter.ToUInt16(data, 2 * j));
                    var count = 16;
                    while (count > 0)
                    {
                        if ((line & 1) != 0)
                            break;

                        line = (ushort)(line >> 1);
                        count--;

                    }
                    maxLen = maxLen > count ? maxLen : count;
                }
                width = maxLen == 0 ? EMPTY_LETTER_WIDTH : maxLen;
            }
            else
            {
                width = EMPTY_LETTER_WIDTH;
            }
            return width;
        }

        public int GetTextWidth(string text)
        {
            if (_ftFile.Version == 9)
                return text.Length * 9;

            var width = 0;
            for (var i = 0; i < text.Length; i++)
            {
                width += GetLetterWidth(text[i]) + LETTER_SPACING;
            }

            return width;
        }

        public int GetTextHeight()
        {
            return _ftFile.Version == 8 ? 16 : 15;
        }
    }
}
