using System;
using System.Windows.Forms;

namespace Rocio.Classes
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0)
            {
                Application.Run(new Forms.MainForm(null));
            }
            else
            {
                Application.Run(new Forms.MainForm(args[0]));
            }
        }
    }
}
