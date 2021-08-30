using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class DhTrackerLogica
    {
        public long Folio { get; set; }
        public string Orden { get; set; }
        public string Linea { get; set; }
        public string Inspector { get; set; }
        public string Nombre { get; set; }
        public string Parte { get; set; }
        public string Descrip { get; set; }
        public string Division { get; set; }
        public int Estatus { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCR { get; set; }
        public DateTime FechaBO { get; set; }
        public DateTime FechaDH { get; set; }
        public DateTime FechaSC { get; set; }
        public int Detenido { get; set; }
        public string Lote { get; set; }
        public string TipoFecha { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public string TipoLinea { get; set; }
        public string TipoEstatus { get; set; }
        public string TipoDivision { get; set; }
        public string TipoDetenido { get; set; }
        public string IndNC { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(DhTrackerLogica dhr)
        {
            string[] parametros = {"@Folio","@Orden", "@Linea", "@Inspector", "@Nombre", "@Parte", "@Descrip", "@Division", "@Estatus", "@Detenido", "@Lote","@IndNC", "@Fecha", "@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_dhtracker", parametros, dhr.Folio, dhr.Orden, dhr.Linea, dhr.Inspector, dhr.Nombre, dhr.Parte, dhr.Descrip, dhr.Division, dhr.Estatus, dhr.Detenido,dhr.Lote,dhr.IndNC, dhr.Fecha, dhr.Usuario);
        }

        public static DataTable ConsultarOrden(DhTrackerLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracker WHERE orden = '"+dhr.Orden+"'");
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
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracker WHERE estatus = 3 and detenido=1 and (DATEDIFF(hour,f_dhr,getdate()) >= (select dh_horas from t_config where clave='686') ) order by f_dhr desc");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable VistaReporteDet(DhTrackerLogica dh)
        {
            DataTable datos = new DataTable();
            try
            {
                string[] parametros = { "@Detenido" };
                datos = AccesoDatos.ConsultaSP("sp_rep_dhtracker_det", parametros, dh.Detenido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable VistaReporteLocal(DhTrackerLogica dh)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT  orden as [Work Order],linea as Linea,parte + ' ' + descrip as Descripción, " +
                "case estatus when 1 then 'Clean Room' when 2 then 'Boxing' when 3 then 'DHR' when 4 then 'Escaneo' else '' end as Estatus," +
                "f_clean as [Fecha C.R.],f_escaneo as [Fecha Escaneo],DATEDIFF(hour, f_clean, f_escaneo) as [Duracion HRs]," +
                "inspector as Inspector,detenido as Detenido  FROM t_dhtracker " +
                "WHERE " +
                "('"+dh.TipoFecha+ "' = '0' or ( '" + dh.TipoFecha + "' = '1' and cast(f_clean as date) between cast('" + dh.FechaIni + "' as date) and cast('" + dh.FechaFin + "' as date) ) ) " +
                "and ('" + dh.TipoLinea + "' = '0' or('" + dh.TipoLinea + "' = '1' and linea = '" + dh.Linea + "')) and ('" + dh.TipoEstatus + "' = '0' or('" + dh.TipoEstatus + "' = '1' and estatus = " + dh.Estatus + ")) "+
                "and ('" + dh.TipoDivision + "' = '0' or ('" + dh.TipoDivision + "' = '1' and division = '"+dh.Division+"')) and ('" + dh.TipoDetenido+"' = '0' or ('"+dh.TipoDetenido+"' = '1' and detenido = "+dh.Detenido+")) ";
                datos = AccesoDatos.Consultar(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable VistaReporte(DhTrackerLogica dh)
        {
            DataTable datos = new DataTable();
            try
            {
                string[] parametros = { "@TipoFecha", "@FechaIni", "@FechaFin", "@TipoLinea", "@Linea", "@TipoEstatus", "@Estatus", "@TipoDivision", "@Division", "@TipoDetenido", "@Detenido" };
                datos = AccesoDatos.ConsultaSP("sp_rep_dhtracker", parametros, dh.TipoFecha, dh.FechaIni, dh.FechaFin,dh.TipoLinea,dh.Linea,dh.TipoEstatus,dh.Estatus,dh.TipoDivision,dh.Division,dh.TipoDetenido,dh.Detenido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable VistaReporteExp(DhTrackerLogica dh)
        {
            DataTable datos = new DataTable();
            try
            {
                string[] parametros = { "@TipoFecha", "@FechaIni", "@FechaFin", "@TipoLinea", "@Linea", "@TipoEstatus", "@Estatus", "@TipoDivision", "@Division", "@TipoDetenido", "@Detenido" };
                datos = AccesoDatos.ConsultaSP("sp_rep_dhtracker_exp", parametros, dh.TipoFecha, dh.FechaIni, dh.FechaFin, dh.TipoLinea, dh.Linea, dh.TipoEstatus, dh.Estatus, dh.TipoDivision, dh.Division, dh.TipoDetenido, dh.Detenido);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable VistaInspectores(DhTrackerLogica dh)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT  orden ,empleado as Inspector, nombre as Nombre  FROM t_dhtrackins WHERE = order = '"+dh.Orden+"' ORDER BY nombre ";
                datos = AccesoDatos.Consultar(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static int Update(DhTrackerLogica dhr)
        {
            int iRes = 0;
            try
            {
                string sSql = "";
                if (dhr.Estatus == 1)
                    sSql = "UPDATE t_dhtracker set estatus = "+dhr.Estatus+",f_clean = '"+dhr.FechaCR+"' f_id = getdate(),u_id = '" + dhr.Usuario + "' WHERE folio = '" + dhr.Folio + "'";
                if (dhr.Estatus == 2)
                    sSql = "UPDATE t_dhtracker set estatus = " + dhr.Estatus + ",f_boxing = '" + dhr.FechaCR + "' f_id = getdate(),u_id = '" + dhr.Usuario + "' WHERE folio = '" + dhr.Folio + "'";
                if (dhr.Estatus == 3)
                    sSql = "UPDATE t_dhtracker set estatus = " + dhr.Estatus + ",f_dhr = '" + dhr.FechaCR + "' f_id = getdate(),u_id = '" + dhr.Usuario + "' WHERE folio = '" + dhr.Folio + "'";
                if (dhr.Estatus == 4)
                    sSql = "UPDATE t_dhtracker set estatus = " + dhr.Estatus + ",f_escaneo = '" + dhr.FechaCR + "' f_id = getdate(),u_id = '" + dhr.Usuario + "' WHERE folio = '" + dhr.Folio + "'";

                iRes = AccesoDatos.Borrar(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

        public static DataTable ConsultarFallas(DhTrackerLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracdet where folio="+dhr.Folio+" order by folio desc ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarFallasView(DhTrackerLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT d.consec,d.falla,f.descrip as Defecto,d.notas as Comentarios,'0' as Inspector,'0' as Corregido,d.estatus,d.ind_inspector FROM t_dhtracdet d inner join t_dhfallas f on d.falla = f.falla  and f.tipo = 'F' where d.folio=" + dhr.Folio+" order by f.descrip");
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
                datos = AccesoDatos.Consultar("SELECT falla,descrip FROM t_dhfallas where tipo = 'F' and activa='1' order by descrip ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable DhrAccion()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT falla,descrip FROM t_dhfallas where tipo = 'A' and activa='1' order by descrip ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }


        public static int UpdateCleanOut(DhTrackerLogica dh)
        {
            try
            {
                return AccesoDatos.Borrar("UPDATE t_dhtracker set f_clean_out = getdate() where folio = " + dh.Folio + " ");
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }

    }
}
