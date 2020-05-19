using System;
using System.Windows.Forms;

using KO.TBLCryptoEditor.Utils;
using KO.TBLCryptoEditor.Views;

namespace KO.TBLCryptoEditor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Logger.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
