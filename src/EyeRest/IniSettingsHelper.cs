using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace EyeRest
{
    public static class IniSettingsHelper
    {
        const string IniFileName = "EyeRest.ini";

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern uint GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        // Check directory writable by attempting to create and delete a temp file.
        public static bool IsDirectoryWritable(string dir)
        {
            try
            {
                if (string.IsNullOrEmpty(dir)) return false;
                if (!Directory.Exists(dir)) return false;
                string testFile = Path.Combine(dir, Path.GetRandomFileName());
                using (var fs = new FileStream(testFile, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    byte[] data = new byte[] { 0 };
                    fs.Write(data, 0, 1);
                }
                try { File.Delete(testFile); } catch { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetSettingsFilePath()
        {
            try
            {
                // Determine exe folder
                string exePath = AppDomain.CurrentDomain.BaseDirectory;
                if (!string.IsNullOrEmpty(exePath) && Directory.Exists(exePath) && IsDirectoryWritable(exePath))
                {
                    return Path.Combine(exePath, IniFileName);
                }

                // Fallback to AppData
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string folder = Path.Combine(appData, "EyeRest");
                try { Directory.CreateDirectory(folder); } catch { }
                return Path.Combine(folder, IniFileName);
            }
            catch
            {
                // last resort: current directory
                return Path.Combine(Directory.GetCurrentDirectory(), IniFileName);
            }
        }

        public static int ReadInt(string section, string key, int defaultValue)
        {
            try
            {
                var sb = new StringBuilder(512);
                int read = GetPrivateProfileString(section, key, defaultValue.ToString(), sb, sb.Capacity, GetSettingsFilePath());
                if (read > 0 && int.TryParse(sb.ToString(), out int v)) return v;
            }
            catch { }
            return defaultValue;
        }

        public static string ReadString(string section, string key, string defaultValue)
        {
            try
            {
                var sb = new StringBuilder(1024);
                int read = GetPrivateProfileString(section, key, defaultValue ?? string.Empty, sb, sb.Capacity, GetSettingsFilePath());
                if (read >= 0) return sb.ToString();
            }
            catch { }
            return defaultValue;
        }

        public static bool ReadBool(string section, string key, bool defaultValue)
        {
            try
            {
                string s = ReadString(section, key, defaultValue ? "1" : "0");
                if (int.TryParse(s, out int iv)) return iv != 0;
                if (bool.TryParse(s, out bool bv)) return bv;
            }
            catch { }
            return defaultValue;
        }

        public static void WriteString(string section, string key, string value)
        {
            try
            {
                WritePrivateProfileString(section, key, value, GetSettingsFilePath());
            }
            catch { }
        }

        public static void WriteInt(string section, string key, int value)
        {
            WriteString(section, key, value.ToString());
        }

        public static void WriteBool(string section, string key, bool value)
        {
            WriteString(section, key, value ? "1" : "0");
        }
    }
}
