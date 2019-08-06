using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Datos
{
    public class ConexionAS400
    {
        public static string cadenaConexion = ConfigurationManager.ConnectionStrings["AS400SYS_Connection"].ToString();

        private static void Cadena()
        {
            if (string.IsNullOrEmpty(cadenaConexion))
                cadenaConexion = "Driver=iSeries Access ODBC Driver;System=AS400SYS;uid=inqmex;pwd=inqmex;";
        }
        public static string CadenaConexion()
        {
            Cadena();
            return cadenaConexion;
        }
    }
}
