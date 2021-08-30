using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class DhTracdetaLogica
    {
        public long Folio { get; set; }
        public int Consec { get; set; }
        public int Accion { get; set; }
        public string Nota { get; set; }
        public string Inspector { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(DhTracdetaLogica dhr)
        {
            string[] parametros = {"@Folio","@Consec", "@Accion", "@Notas","@Inspector", "@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_dhtracdeta", parametros, dhr.Folio, dhr.Consec, dhr.Accion, dhr.Nota, dhr.Inspector, dhr.Usuario);

        }

        public static DataTable ConsultarOrden(DhTracdetaLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtrackera WHERE folio = '"+dhr.Folio+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarAccionView(DhTracdetaLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT d.consec,d.accion,f.descrip as [Acción Inmediata],d.notas as Descripción,inspector as Inspector FROM t_dhtracdeta d inner join t_dhfallas f on d.accion = f.falla and f.tipo = 'A' where d.folio=" + dhr.Folio + " order by f.descrip");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarFallas(DhTracdetaLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracdeta where folio="+dhr.Folio+" order by descrip ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarFallasView(DhTracdetaLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT d.consec,d.falla,f.descrip as Defecto,d.notas as Comentarios,d.estatus FROM t_dhtracdeta d inner join t_dhfallas f on d.falla = f.falla where d.folio="+dhr.Folio+" order by f.descrip");
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

        public static int Eliminar(DhTracdetaLogica det)
        {
            try
            {
                return AccesoDatos.Borrar("DELETE FROM t_dhtracdeta where folio = " + det.Folio+" and consec = "+det.Consec+" ");
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }

       


    }
}
