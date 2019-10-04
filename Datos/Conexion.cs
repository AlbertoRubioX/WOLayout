using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Datos
{
    public class Conexion
    {
        public static string cadenaConexion = ConfigurationManager.ConnectionStrings["WOLayout_Connection"].ToString();

        private static void Cadena()
        {
            if (string.IsNullOrEmpty(cadenaConexion))
                cadenaConexion = "Data Source=MXCPRDLOCALDB01;Initial Catalog=LayoutSystem;User ID=mxcprd;Password=Admin10";
        }
        public static string CadenaConexion()
        {
            Cadena();
            return cadenaConexion;
        }
    }
}
