using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Datos
{
    public class ConexionSetup
    {
        public static string cadenaConexion = ConfigurationManager.ConnectionStrings["Setup_Connection"].ToString();

        private static void Cadena()
        {
            if (string.IsNullOrEmpty(cadenaConexion))
                cadenaConexion = "Data Source=NLPRDSSS1;Initial Catalog=SetupScanningSystem;User ID=nldprod;Password=nldprod";
        }
        public static string CadenaConexion()
        {
            Cadena();
            return cadenaConexion;
        }
    }
}
