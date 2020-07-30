using Newtonsoft.Json;
using System.Drawing;
using System.IO;

namespace BipEmulator.Host
{
    [System.Serializable]
    public class LocalSettings
    {
        const string SettingsFilename = "settings.json";

        public int HeartRate = 70;
        public bool HeartRateMeasurementCompleted = false;
        public double Latitude = 37.401583;
        public double Longitude = -116.867808;
        public double Altitude = 1592.12;
        public bool GeoLocationMeasurementCompleted = false;
        public int Pressure = 760;
        public bool PressureMeasurementCompleted = false;
        public LocaleEnum Locale = LocaleEnum.ru_RU;
        public int MaxDistanceClick = 5;

        public ColorArray Colors = new ColorArray
        {
            Black = Color.FromArgb(56, 56, 56),
            Red = Color.FromArgb(187, 113, 136),
            Green = Color.FromArgb(89, 181, 132),
            Blue = Color.FromArgb(28, 110, 194),
            Yellow = Color.FromArgb(221, 214, 133),
            Aqua = Color.FromArgb(70, 198, 207),
            Purple = Color.FromArgb(173, 147, 208),
            White = Color.FromArgb(220, 220, 220)
        };

        public string FontFilename = "Presets//Mili_chaohu.ft.latin";
        public string SystemResourceFilename = "Presets//Mili_chaohu.res.latin";
        public string UserResourceFilename = "assert.res";

        public static LocalSettings Load()
        {
            try
            {
                return JsonConvert.DeserializeObject<LocalSettings>(File.ReadAllText(SettingsFilename));
            }
            catch
            {
                return new LocalSettings();
            }
        }

        public void Save()
        {
            try
            {
                File.WriteAllText(SettingsFilename, JsonConvert.SerializeObject(this));
            }
            catch
            {
            }
        }

    }
}
