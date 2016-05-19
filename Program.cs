using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiboClientWatchdog
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "Watchdog");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run();

                // Release when we've stopped
                mutex.ReleaseMutex();
            }
        }
    }
}
