using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class LineaHoraLogica
    {
        public string CN { get; set; }
        public long Folio { get; set; }
        public DateTime Fecha { get; set; }
        public string Linea { get; set; }
        public string Turno { get; set; }
        public decimal Meta { get; set; }
        public string MetaUm { get; set; }
        public decimal Real { get; set; }
        public decimal Defectos { get; set; }
        public string Estacion { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(LineaHoraLogica line)
        {
            string[] parametros = {"@Folio","@Fecha", "@Linea","@Turno", "@Meta", "@MetaUm", "@Total", "@Defectos", "@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_linehr", parametros,line.Folio,line.Fecha,line.Linea, line.Turno, line.Meta, line.MetaUm, line.Real, line.Defectos ,line.Usuario);
        }

        public static DataTable ConsultarLinea(LineaHoraLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_linehr WHERE linea = '"+line.Linea+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarActual(LineaHoraLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("select * from t_linehr where folio = (select max(folio) from t_linehr where linea = '"+line.Linea+"' and cast(fecha as date) = cast(getdate() as date))");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarFolio(LineaHoraLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_linehr WHERE folio = " + line.Folio + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static bool AlertaCapturaHora(LineaHoraLogica line)
        {
            try
            {
                string sQuery = "SELECT  d.* FROM t_linehrdet d inner join t_linehr l on d.folio = l.folio " +
                "WHERE l.linea = '"+line.Linea+"' and cast(l.fecha as date) = cast(getdate() as date) and  DATEPART(hour, l.fecha) < DATEPART(hour, getdate())";
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

        public static int Eliminar(LineaHoraLogica line)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_linehr WHERE folio = "+line.Folio+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

    }
}
