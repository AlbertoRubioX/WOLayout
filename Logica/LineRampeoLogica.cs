using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class LineaRampeoLogica
    {
        public string CN { get; set; }
        public string Linea { get; set; }
        public string Estacion { get; set; }
        public decimal Factor { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(LineaRampeoLogica line)
        {
            string[] parametros = {"@Company", "@Station","@Line","@Factor","@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_lineramp", parametros,line.CN, line.Estacion,line.Linea,line.Factor,line.Usuario);
        }

        public static DataTable ConsultarLinea(LineaRampeoLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineramp WHERE line = '"+line.Linea+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarEstacion(LineaRampeoLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineramp WHERE station = '"+line.Estacion+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable Listar()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineramp");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ListarDrop()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineramp where linehr is not null order by linehr");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable Vista(LineaRampeoLogica lin)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT station,line,factor FROM t_lineramp WHERE company='"+lin.CN+"' order by line");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static int Eliminar(LineaRampeoLogica line)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_lineramp WHERE company ='"+line.CN+"' and station = '"+line.Estacion+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

        public static string GetCompany(LineaRampeoLogica line)
        {
            string sPlanta = string.Empty;
            try
            {
                string sQuery = "SELECT * FROM t_lineramp where station = '"+line.Estacion+"'";
                DataTable datos = AccesoDatos.Consultar(sQuery);
                if (datos.Rows.Count > 0)
                    sPlanta = datos.Rows[0]["company"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sPlanta;
        }
        public static bool VerificaCapturaHora(LineaRampeoLogica line)
        {
            try
            {
                string sQuery = "SELECT c.line FROM t_lineconfd c inner join t_lineramp r on c.line = r.linehr " +
                "WHERE R.station = '" + line.Estacion + "' AND(c.clave = 'LMBA01' OR c.clave = 'LMBA02')  group by c.line HAVING COUNT(c.line)	 > 0";
                DataTable datos = AccesoDatos.Consultar(sQuery);
                if (datos.Rows.Count != 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
