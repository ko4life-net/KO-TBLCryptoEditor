using System;
using System.Windows.Forms;

using KO.TBLCryptoEditor.Views;

namespace KO.TBLCryptoEditor
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
