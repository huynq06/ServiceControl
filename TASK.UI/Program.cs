using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TASK.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool ownsmutex = false;
            using (Mutex mutex = new Mutex(true, "SAMI.FTP.UI.DC2DDE1C-A3B2-4771-A04A-28B8B2E9E5DD", out ownsmutex))
            {
                if (ownsmutex)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FrmMain());
                }
            }
        }
    }
}
