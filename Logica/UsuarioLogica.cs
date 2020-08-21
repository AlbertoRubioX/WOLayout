using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class UsuarioLogica
    {
        public decimal Tape { get; set; }
        public decimal WrapNA { get; set; }
        public string Acceso { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(UsuarioLogica user)
        {
            //string[] parametros = { "@Jornada", "@HrsDisp", "@SegDisp", "@CajasLinea", "@KitsCaja", "@KitsLinea", "@TakTime", "@Tak80", "@AssyTime", "@MaxComp", "@MesaEns", "@MesaWrap", "@MesaSub", "@OperNA", "@Surtidor", "@InspSella", "@Sellador", "@Inspeccion", "@Usuario", "@Horiz", "@Vertical", "@Sobre", "@TuckTape", "@WrapNA" };
            //return AccesoDatos.Actualizar("sp_mant_config", parametros, config.Jornada, config.HorasDisp, config.SegDisp, config.Cajas, config.Kits, config.KitLinea, config.Tak, config.Tak80,config.Assy, config.MaxComp, config.Mesas, config.MesaWrap, config.MesaSub, config.OperNA, config.Surtidor, config.InspSella, config.Selladora, config.Inspeccion, config.Usuario,config.Horizontal,config.Vertical,config.Sobre,config.Tape,config.WrapNA);
            return 0;
        }

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

        public static DataTable ConsultarUser(UsuarioLogica user)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_usuario where usuario = '"+user.Usuario+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static bool ValidaAcceso(UsuarioLogica user)
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
        public static bool ValidaAcceso2(UsuarioLogica user)
        {

            try
            {
                
                DataTable datos = new DataTable();
                datos = AccesoDatos.Consultar("SELECT * FROM t_usuario where usuario = '" + user.Usuario + "' and ind_conf = '"+user.Acceso+"'");
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

        public static string GetCompany(UsuarioLogica user)
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
