using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeonProject
{
    

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [DllImport(@"C:\Users\Maja\Documents\Studia\JA\ProjektWindowsForms\NeonProjekt\Asm\x64\Debug\Asm.dll")]
        static extern int MyProc1(int a, int b);
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}


