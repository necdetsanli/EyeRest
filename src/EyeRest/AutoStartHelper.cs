using System;
using Microsoft.Win32;

namespace EyeRest
{
    public static class AutoStartHelper
    {
        const string RunKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        const string ValueName = "EyeRest";

        public static void SetAutoStart(bool enabled, string exePath)
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(RunKey, true) ?? Registry.CurrentUser.CreateSubKey(RunKey))
                {
                    if (key == null) return;
                    if (enabled)
                    {
                        key.SetValue(ValueName, exePath);
                    }
                    else
                    {
                        try { key.DeleteValue(ValueName); } catch { }
                    }
                }
            }
            catch
            {
                // swallow errors to avoid crashing
            }
        }
    }
}
