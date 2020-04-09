using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logica;

namespace PlaybookSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Globals._gsStation = Environment.MachineName.ToUpper();

            LineaRampeoLogica line = new LineaRampeoLogica();
            line.Estacion = Globals._gsStation;
            if (LineaRampeoLogica.VerificaCapturaHora(line))
                Application.Run(new wfLineHour());
            else
                Application.Run(new wfLayout());
        }
    }
}
