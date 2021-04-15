using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class PickingLogica
    {
        public string CN { get; set; }
        public long Folio { get; set; }
        public DateTime Fecha { get; set; }
        public string Area { get; set; }
        public string Turno { get; set; }
        public string Linea { get; set; }
        public string WO { get; set; }
        public double QtyWO { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string Motivo { get; set; }
        public string Concepto { get; set; }
        public string Causa { get; set; }
        public string CausaDesc { get; set; }
        public string Comentario { get; set; }
        public string Componente { get; set; }
        public string CompDesc { get; set; }
        public double Surtido { get; set; }
        public double Faltante { get; set; }
        public double Requerido { get; set; }
        public double Discrep { get; set; }
        public string Hora { get; set; }
        public string FechaD { get; set; }
        public string Locacion { get; set; }
        public string Autor { get; set; }
        public string Realizado { get; set; }
        public string Lote { get; set; }
        public decimal Factor { get; set; }
        public string Usuario { get; set; }
        public string TipoFecha { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }

        public static int GuardarSP(PickingLogica pick)
        {
            string[] parametros = {"@Folio", "@Fecha","@Area","@Orden", "@Turno", "@Linea", "@Parte", "@Comp", "@CompDesc","@CantWO", "@CantR", "@CantS", "@CantF", "@CantD", "@Hora", "@FechaD", "@Realizado", "@Autor", "@Locacion", "@Motivo", "@Concepto","@Causa","@CausaDesc", "@Notas","@Lote", "@Uid" };
            return AccesoDatos.Actualizar("sp_mant_pickingpr", parametros, pick.Folio, pick.Fecha, pick.Area,pick.WO, pick.Turno, 
                pick.Linea, pick.Item, pick.Componente, pick.CompDesc,pick.QtyWO, pick.Requerido, pick.Surtido,pick.Faltante, pick.Discrep, 
                pick.Hora, pick.FechaD, pick.Realizado, pick.Autor, pick.Locacion, pick.Motivo, pick.Concepto, pick.Causa, pick.CausaDesc, pick.Comentario,pick.Lote, pick.Usuario);
        }

        public static DataTable Consultar(PickingLogica pick)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_pickingpr WHERE folio = "+pick.Folio+"");
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
                datos = AccesoDatos.Consultar("SELECT * FROM t_pickingpr");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        
        public static DataTable VistaReporte(PickingLogica lin)
        {
            DataTable datos = new DataTable();
            try
            {
                string[] parametros = { "@TipoFecha", "@FechaIni", "@FechaFin"};
                datos = AccesoDatos.ConsultaSP("sp_pickingpr_report",parametros,lin.TipoFecha,lin.FechaIni,lin.FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static int Eliminar(PickingLogica line)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE  ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

        
    }
}
