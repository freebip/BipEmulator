using System;

namespace BipEmulator.Host
{
    [AttributeUsage(AttributeTargets.All)]
    public class DisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
    {
        public DisplayNameAttribute(string name)
        {
            DisplayNameValue = name;
        }

        public string Name { get { return DisplayName; } }

        public static string GetName(Object o)
        {
            if (o == null) return String.Empty;

            var t = o.GetType();
            if (t.IsEnum)
            {
                var fi = t.GetFields();
                foreach (var fieldInfo in fi)
                {
                    if (fieldInfo.Name == o.ToString())
                    {
                        var a = (DisplayNameAttribute)GetCustomAttribute(fieldInfo, typeof(DisplayNameAttribute));
                        return a == null ? fieldInfo.Name : a.Name;
                    }
                }
            }
            else
            {
                var a = (DisplayNameAttribute)GetCustomAttribute(t, typeof(DisplayNameAttribute));
                if (a != null)
                    return a.Name;

            }
            return String.Empty;
        }

        public static string GetName(Type t)
        {
            if (t == null) return String.Empty;

            var a = (DisplayNameAttribute)GetCustomAttribute(t, typeof(DisplayNameAttribute));
            return a != null ? a.Name : String.Empty;
        }
    }
}