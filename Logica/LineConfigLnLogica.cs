using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class LineaConfigLnLogica
    {
        public string CN { get; set; }
        public string Clave { get; set; }
        public int Consec { get; set; }
        public string Linea { get; set; }
        

        public static int GuardarSP(LineaConfigLnLogica cve)
        {
            string[] parametros = {"@Clave", "@Consec", "@Linea"};
            return AccesoDatos.Actualizar("sp_mant_lineconfd", parametros,cve.Clave, cve.Consec, cve.Linea);
        }
        
        //conf lineas
        public static DataTable ConsultarClaveLine(LineaConfigLnLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineconfd WHERE clave = '" + cve.Clave + "' order by consec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ValidaClaveLinea(LineaConfigLnLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineconfd WHERE line = '"+cve.Linea+"' and clave != '" + cve.Clave + "' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable VistaClaveLine(LineaConfigLnLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT consec,line as Linea FROM t_lineconfd WHERE clave = '" + cve.Clave + "' order by consec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarConfLinea(LineaConfigLnLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                string sQuery = "SELECT f.* FROM t_lineconfd d inner join t_lineconf f on d.clave = f.clave where d.line = '"+cve.Linea+"'";
                datos = AccesoDatos.Consultar(sQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
 
        public static int Eliminar(LineaConfigLnLogica cve)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_lineconfd WHERE clave = '"+cve.Clave+"' and consec = "+cve.Consec+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }
        

    }
}
