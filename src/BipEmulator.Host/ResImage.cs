using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace BipEmulator.Host
{
    public class ResImage
    {
        private Bitmap _bitmap = null;

        private ushort _bitsPerPixel;
        private ushort _height;
        private Color[] _palette;
        private ushort _paletteColors;
        private ushort _rowLengthInBytes;
        private bool _transparency;
        private ushort _width;

        public Bitmap Bitmap
        {
            get
            {
                if (_bitmap == null)
                    Read();
                return _bitmap;
            }
        }

        private byte[] _data;

        public ResImage(byte[] data)
        {
            _data = data;
        }

        public void Read()
        {
            using (var ms = new MemoryStream(_data))
            using (var br = new BinaryReader(ms))
            {
                var signature = br.ReadChars(4);
                if (signature[0] != 'B' || signature[1] != 'M')
                    throw new ArgumentException("Image signature doesn't match.");

                ReadHeader(br);
                if (_paletteColors > 256)
                    throw new ArgumentException("Too many palette colors.");

                if (_paletteColors > 0)
                    ReadPalette(br);
                else if (_bitsPerPixel == 8 || _bitsPerPixel == 16 || _bitsPerPixel == 24 || _bitsPerPixel == 32)
                    Debug.WriteLine("The image doesn't use a palette.");
                else
                    throw new ArgumentException(
                        "The image format is not supported. Please report the issue on https://bitbucket.org/valeronm/amazfitbiptools");
                _bitmap = ReadImage(br);
            }
        }

        private void ReadHeader(BinaryReader br)
        {
            Debug.WriteLine("Reading image header...");
            _width = br.ReadUInt16();
            _height = br.ReadUInt16();
            _rowLengthInBytes = br.ReadUInt16();
            _bitsPerPixel = br.ReadUInt16();
            _paletteColors = br.ReadUInt16();
            _transparency = br.ReadUInt16() > 0;
            Debug.WriteLine("Image header was read:");
            Debug.WriteLine("Width: {0}, Height: {1}, RowLength: {2}", _width, _height, _rowLengthInBytes);
            Debug.WriteLine("BPP: {0}, PaletteColors: {1}, Transaparency: {2}", _bitsPerPixel, _paletteColors, _transparency);
        }

        private void ReadPalette(BinaryReader br)
        {
            Debug.WriteLine("Reading palette...");
            _palette = new Color[_paletteColors];
            for (var i = 0; i < _paletteColors; i++)
            {
                var r = br.ReadByte();
                var g = br.ReadByte();
                var b = br.ReadByte();
                var padding = br.ReadByte(); // always 0 maybe padding

                if (padding != 0) Debug.WriteLine("Palette item {0} last byte is not zero: {1:X2}", i, padding);

                var isColorValid = (r == 0 || r == 0xff) && (g == 0 || g == 0xff) && (b == 0 || b == 0xff);
                if (isColorValid)
                    Debug.WriteLine("Palette item {0}: R {1:X2}, G {2:X2}, B {3:X2}", i, r, g, b);
                else
                    Debug.WriteLine("Palette item {0}: R {1:X2}, G {2:X2}, B {3:X2}, color isn't supported!", i, r, g, b);

                var alpha = _transparency && i == 0 ? 0x00 : 0xff;
                _palette[i] = Color.FromArgb(alpha, r, g, b);
            }
        }

        private Bitmap ReadImage(BinaryReader br)
        {
            if (_paletteColors > 0) return ReadPaletteImage(br);
            if (_bitsPerPixel == 8) return Read8BitImage(br);
            if (_bitsPerPixel == 16) return Read16BitImage(br);
            if (_bitsPerPixel == 24) return Read24BitImage(br);
            if (_bitsPerPixel == 32) return Read32BitImage(br);
            throw new ArgumentException($"Unsupported bits per pixel value: {_bitsPerPixel}");
        }

        private Bitmap ReadPaletteImage(BinaryReader br)
        {
            var image = new Bitmap(_width, _height);
            using (var context = image.CreateUnsafeContext())
            {
                for (var y = 0; y < _height; y++)
                {
                    var rowBytes = br.ReadBytes(_rowLengthInBytes);
                    var bitReader = new BitReader(rowBytes);
                    for (var x = 0; x < _width; x++)
                    {
                        var pixelColorIndex = bitReader.ReadBits(_bitsPerPixel);
                        var color = _palette[(int)pixelColorIndex];
                        context.SetPixel(x, y, color);
                    }
                }
            }

            return image;
        }

        private Bitmap Read8BitImage(BinaryReader br)
        {
            var image = new Bitmap(_width, _height);
            using (var context = image.CreateUnsafeContext())
            {
                for (var y = 0; y < _height; y++)
                {
                    var rowBytes = br.ReadBytes(_rowLengthInBytes);
                    for (var x = 0; x < _width; x++)
                    {
                        var data = rowBytes[x];
                        var color = Color.FromArgb(0xff, data, data, data);
                        context.SetPixel(x, y, color);
                    }
                }
            }

            return image;
        }

        private Bitmap Read16BitImage(BinaryReader br)
        {
            var image = new Bitmap(_width, _height);
            using (var context = image.CreateUnsafeContext())
            {
                for (var y = 0; y < _height; y++)
                {
                    var rowBytes = br.ReadBytes(_rowLengthInBytes);
                    var bitReader = new BitReader(rowBytes);
                    for (var x = 0; x < _width; x++)
                    {
                        var firstByte = (int)bitReader.ReadByte();
                        var secondByte = (int)bitReader.ReadByte();
                        var b = (byte)((secondByte >> 3) & 0x1f) << 3;
                        var g = (byte)(((firstByte >> 5) & 0x7) | ((secondByte & 0x07) << 3)) << 2;
                        var r = (byte)(firstByte & 0x1f) << 3;
                        var color = Color.FromArgb(0xff, r, g, b);
                        context.SetPixel(x, y, color);
                    }
                }
            }

            return image;
        }

        private Bitmap Read24BitImage(BinaryReader br)
        {
            var image = new Bitmap(_width, _height);
            using (var context = image.CreateUnsafeContext())
            {
                for (var y = 0; y < _height; y++)
                {
                    var rowBytes = br.ReadBytes(_rowLengthInBytes);
                    var bitReader = new BitReader(rowBytes);
                    for (var x = 0; x < _width; x++)
                    {
                        var alpha = (int)bitReader.ReadByte();
                        var b = (int)(bitReader.ReadBits(5) << 3);
                        var g = (int)(bitReader.ReadBits(6) << 2);
                        var r = (int)(bitReader.ReadBits(5) << 3);
                        var color = Color.FromArgb(0xff - alpha, r, g, b);
                        context.SetPixel(x, y, color);
                    }
                }
            }

            return image;
        }

        private Bitmap Read32BitImage(BinaryReader br)
        {
            var image = new Bitmap(_width, _height);
            using (var context = image.CreateUnsafeContext())
            {
                for (var y = 0; y < _height; y++)
                {
                    var rowBytes = br.ReadBytes(_rowLengthInBytes);
                    for (var x = 0; x < _width; x++)
                    {
                        var r = rowBytes[x * 4];
                        var g = rowBytes[x * 4 + 1];
                        var b = rowBytes[x * 4 + 2];
                        var alpha = rowBytes[x * 4 + 3];
                        var color = Color.FromArgb(0xff - alpha, r, g, b);
                        context.SetPixel(x, y, color);
                    }
                }
            }

            return image;
        }
    }
}
