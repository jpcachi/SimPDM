using PDMv4.Vistas;
using System;
using System.Windows.Forms;

namespace PDMv4
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string parameterFile = args.Length > 0 ? args[0] : null;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SimPDM(parameterFile));
        }
    }
}
