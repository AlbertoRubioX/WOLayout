using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class LineaHoraDetLogica
    {
        public string CN { get; set; }
        public long Folio { get; set; }
        public int Consec { get; set; }
        public string Hora { get; set; }
        public string Linea { get; set; }
        public string Lider { get; set; }
        public string Turno { get; set; }
        public int Meta { get; set; }
        public int MetaAc { get; set; }
        public int Actual { get; set; }
        public int ActualAc { get; set; }
        public string Cumple { get; set; }
        public int Defectos { get; set; }
        public string Orden { get; set; }
        public string Nota { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(LineaHoraDetLogica line)
        {
            string[] parametros = { "@Folio", "@Consec", "@Hora","@Meta","@MetaAc", "@Actual", "@ActualAc","@Cumple", "@Defectos", "@Orden", "@Nota", "@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_linehrdet", parametros,line.Folio,line.Consec,line.Hora, line.Meta, line.MetaAc, line.Actual, line.ActualAc, line.Cumple, line.Defectos, line.Orden, line.Nota ,line.Usuario);
        }

        public static DataTable ConsultarLinea(LineaHoraDetLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_linehrdet WHERE linea = '"+line.Linea+"' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        
        public static DataTable ConsultarFolio(LineaHoraDetLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_linehrdet WHERE folio = " + line.Folio + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable VistaHorario(LineaHoraDetLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT consec,hora as Hora,meta as Meta,meta_ac as Acum,actual as Actual,actual_ac as [Actual Acum],defectos as Defectos,orden as [Work Order],nota as Comentarios,cumple_meta FROM t_linehrdet WHERE folio = "+line.Folio+" order by consec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static int Eliminar(LineaHoraDetLogica line)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_linehrdet WHERE folio = "+line.Folio+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

    }
}
