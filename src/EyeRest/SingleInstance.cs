using System;
using System.Threading;

namespace EyeRest
{
    // Simple single-instance guard using a named mutex.
    // Returns the mutex when ownership is acquired or null when another instance holds it.
    // This helper class is used to ensure that only one instance of the application
    // is running at the same time. It utilizes a system-wide mutex identified by a
    // unique name to provide the single-instance functionality.
    public static class SingleInstance
    {
        public static Mutex AcquireMutex(string name, out bool createdNew)
        {
            createdNew = false;
            try
            {
                var mutex = new Mutex(true, name, out createdNew);
                if (!createdNew)
                {
                    // Another instance already holds the mutex
                    mutex.Close();
                    return null;
                }
                return mutex;
            }
            catch
            {
                createdNew = false;
                return null;
            }
        }
    }
}