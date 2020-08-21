using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
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
                
            string sUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            sUser = sUser.Substring(sUser.IndexOf("\\") + 1).ToUpper();
            Globals._gsUser = sUser;
            Globals._gsStation = Environment.MachineName.ToUpper();

            UsuarioLogica user = new UsuarioLogica();
            user.Usuario = sUser;
            user.Acceso = "PRO040";
            
            //GET CN & line from pc station
            LineaRampeoLogica line = new LineaRampeoLogica();
            line.Estacion = Globals._gsStation;

            //obtener planta del usuario de windows
            string sPlanta = UsuarioLogica.GetCompany(user);
            if (string.IsNullOrEmpty(sPlanta))
                sPlanta = LineaRampeoLogica.GetCompany(line);

            if (string.IsNullOrEmpty(sPlanta))
            {
                MessageBox.Show("The user "+sUser+" was not found in the system." + Environment.NewLine + "Please, contact the system administrator.", "Playbook System Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
            else
            {
                Globals._gsCompany = sPlanta;

                //valida parametros cargados para la planta
                ConfigLogica conf = new ConfigLogica();
                conf.CN = sPlanta;
                DataTable dtC = ConfigLogica.Consultar(conf);
                if (dtC.Rows.Count == 0)
                {
                    MessageBox.Show("System Configuration is not setup for this site." + Environment.NewLine + "Please, contact the system administrator.","Playbook System Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Application.Exit();
                }
                else
                {
                    if (UsuarioLogica.ValidaAcceso(user))
                        Application.Run(new wfTableSetup());
                    else
                    {
                        if (LineaRampeoLogica.VerificaCapturaHora(line))
                            Application.Run(new wfLineHour());
                        else
                            Application.Run(new wfLayout());
                    }
                }
            }
        }
    }
}
