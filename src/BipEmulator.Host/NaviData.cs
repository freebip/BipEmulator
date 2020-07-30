using System.Runtime.InteropServices;

namespace BipEmulator.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct NaviData
    {
        public int ready; // Готовность данных: bit 0: давление ; bit 1: высота  ; bit 2: широта  ; bit 3: долгота
        public uint pressure; // значение давления в Паскалях
        public float altitude; // значение высоты в метрах
        public int latitude; // модуль значения долготы в градусах, умноженное на 3000000
        public int ns; // ns: 0-северное полушарие; 1-южное
        public int longitude; // модуль знаения долготы в градусах, умноженное на 3000000
        public int ew; // ew: 2-западное полушарие; 3-восточное; 
    }
}
