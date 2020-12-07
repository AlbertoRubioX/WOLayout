using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class TressLogica
    {
        public int Empleado { get; set; }
        public decimal WrapNA { get; set; }
        public string Acceso { get; set; }
        public string Usuario { get; set; }

        

        public static DataTable Consultar()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_usuario");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarEmpleado(TressLogica user)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.ConsultarCRH("SELECT * FROM COLABORA where CB_CODIGO = "+user.Empleado+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static bool ValidaAcceso(TressLogica user)
        {
            
            try
            {
                string sColumn = string.Empty;
                switch(user.Acceso)
                {
                    case "CONF":
                        sColumn = "ind_conf";
                        break;
                    case "EXPF":
                        sColumn = "ind_export";
                        break;
                    case "PRO040":
                        sColumn = "ind_tablesetup";
                        break;
                }
                    
                DataTable datos = new DataTable();
                datos = AccesoDatos.Consultar("SELECT * FROM t_usuario where usuario = '" + user.Usuario + "' and "+sColumn+" >= '1'");
                if (datos.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }
         
        public static string GetCompany(TressLogica user)
        {
            string sPlanta = string.Empty;
            try
            {
                DataTable datos = new DataTable();
                datos = AccesoDatos.Consultar("SELECT * FROM t_usuario where usuario = '" + user.Usuario + "'");
                if (datos.Rows.Count > 0)
                    sPlanta = datos.Rows[0]["planta"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sPlanta;
        }
    }
}
