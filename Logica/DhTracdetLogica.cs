using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class DhTracdetLogica
    {
        public long Folio { get; set; }
        public int Consec { get; set; }
        public int Estatus { get; set; }
        public int Falla { get; set; }
        public string Nota { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(DhTracdetLogica dhr)
        {
            string[] parametros = {"@Folio","@Consec", "@Falla", "@Notas", "@Estatus", "@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_dhtracdet", parametros, dhr.Folio, dhr.Consec, dhr.Falla, dhr.Nota, dhr.Estatus, dhr.Usuario);

        }

        public static DataTable ConsultarOrden(DhTracdetLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracker WHERE folio = '"+dhr.Folio+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarPending()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracker WHERE where estatus = 3 and detenido=1 order by f_dhr desc");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
 

        public static DataTable ConsultarFallas(DhTracdetLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracdet where folio="+dhr.Folio+" order by descrip ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarFallasView(DhTracdetLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT d.consec,d.falla,f.descrip as Defecto,d.notas as Comentarios,d.estatus FROM t_dhtracdet d inner join t_dhfallas f on d.falla = f.falla where d.folio="+dhr.Folio+" order by f.descrip");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable DhrFallas()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT falla,descrip FROM t_dhfallas where activa='1' order by descrip ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }


    }
}
