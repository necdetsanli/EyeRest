// TaskTrayApplication\Program.cs
using System;
using System.Threading;
using System.Windows.Forms;

namespace EyeRest
{
    static class Program
    {
        // Keep the mutex reference for the app lifetime to hold ownership.
        private static Mutex instanceMutex;

        [STAThread]
        static void Main()
        {
            bool createdNew;
            instanceMutex = SingleInstance.AcquireMutex("EyeRestSingletonMutex", out createdNew);
            if (instanceMutex == null || !createdNew)
            {
                // Another instance is running; exit silently (or show a brief notification).
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EyeRestApplicationContext());

            // When exiting, release/close the mutex.
            try { instanceMutex.ReleaseMutex(); } catch { }
            try { instanceMutex.Close(); } catch { }
            instanceMutex = null;
        }
    }
}