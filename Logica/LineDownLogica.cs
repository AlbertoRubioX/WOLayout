using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class LineaDownLogica
    {
        public string CN { get; set; }
        public long Folio { get; set; }
        public DateTime Fecha { get; set; }
        public string Linea { get; set; }
        public string Turno { get; set; }
        public decimal Duracion { get; set; }
        public string Hora { get; set; }
        public string Motivo { get; set; }
        public string Notas { get; set; }

        public static int GuardarSP(LineaDownLogica line)
        {
            string[] parametros = {"@Folio","@Fecha", "@Turno", "@Linea", "@Hora", "@Duracion", "@Motivo", "@Notas" };
            return AccesoDatos.Actualizar("sp_mant_linedown", parametros,line.Folio,line.Fecha,line.Turno, line.Linea, line.Hora, line.Duracion, line.Motivo, line.Notas);
        }

        public static DataTable ConsultarLinea(LineaDownLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_linedown WHERE linea = '"+line.Linea+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        
        public static DataTable ConsultarFolio(LineaDownLogica line)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_linedown WHERE folio = " + line.Folio + "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

   
        public static int Eliminar(LineaDownLogica line)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_linedown WHERE folio = "+line.Folio+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

    }
}
