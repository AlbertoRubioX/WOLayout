using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Datos
{
    public class ConexionCRH
    {
        public static string cadenaConexion = ConfigurationManager.ConnectionStrings["RH_Connection"].ToString();

        private static void Cadena()
        {
            if (string.IsNullOrEmpty(cadenaConexion))
                cadenaConexion = "Data Source=MXCPRDTRESSDB01;Initial Catalog=Tress_MedlineMXL;User ID=tress_readonly;Password=medlinetress";
        }
        public static string CadenaConexion()
        {
            Cadena();
            return cadenaConexion;
        }
    }
}
