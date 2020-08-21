using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class ConfigWrapLogica
    {
        public string CN { get; set; }
        public string Wrap { get; set; }
        public string Size { get; set; }
        public string Fold { get; set; }
        public decimal Duration { get; set; }
        public string User { get; set; }

        public static int GuardarSP(ConfigWrapLogica data)
        {
            string[] parametros = { "@Clave", "@Wrap", "@Size", "@Fold", "@Duration", "@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_configw", parametros, data.CN, data.Wrap, data.Size, data.Fold, data.Duration, data.User);
        }

        public static DataTable ConsultarWrap(ConfigWrapLogica data)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_config_w WHERE clave = '"+data.CN+"' and wrap = '"+data.Wrap+"' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarSize(ConfigWrapLogica data)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_config_w WHERE clave = '" + data.CN + "' and wrap = '" + data.Wrap + "' and size = '" + data.Size + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable VistaSize(ConfigWrapLogica data)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT fold,duration FROM t_config_w WHERE clave = '" + data.CN + "' and wrap = '" + data.Wrap + "' and size = '" + data.Size + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultaFold(ConfigWrapLogica data)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT duration FROM t_config_w WHERE clave = '" + data.CN + "' and wrap = '" + data.Wrap + "' and size = '" + data.Size + "' and fold = '"+data.Fold+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }


        public static DataTable VistaWrap(ConfigWrapLogica data)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT fold AS [FOLD TYPE],duration as DURATION FROM t_config_w WHERE clave = '" + data.CN + "' and wrap = '" + data.Wrap + "' and size = '" + data.Size + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static int Eliminar(ConfigWrapLogica data)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_config_w WHERE clave = '"+data.CN+"' and wrap = '"+data.Wrap+"' and size = '"+data.Size+"' and fold = '"+data.Fold+"' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

    }
}
