using System;
using System.Windows.Forms;

namespace StreetBridgeWindowsApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Form1.Instance);  // Запуск головної форми додатку
        }
    }
}
