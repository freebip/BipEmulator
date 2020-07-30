using System.Runtime.InteropServices;

namespace BipEmulator.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct ResParam
    {
        public short Width;
        public short Height;
    }
}
