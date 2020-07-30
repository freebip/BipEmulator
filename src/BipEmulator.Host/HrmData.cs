using System.Runtime.InteropServices;

namespace BipEmulator.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct HrmData
    {
        int t_start;
        public short last_hr;
        byte field_2;
        byte field_3;
        byte field_4;
        public byte heart_rate;
        public byte ret_code;
        byte field_5;
    }
}
