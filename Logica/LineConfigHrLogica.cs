using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class LineaConfigHrLogica
    {
        public string CN { get; set; }
        public string Clave { get; set; }
        public int Consec { get; set; }
        public int Meta { get; set; }
        public int MetaAc { get; set; }
        public string Horario { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(LineaConfigHrLogica cve)
        {
            string[] parametros = {"@Clave", "@Consec", "@Hora", "@Meta", "@MetaAc"};
            return AccesoDatos.Actualizar("sp_mant_lineconfh", parametros,cve.Clave, cve.Consec, cve.Horario, cve.Meta, cve.MetaAc);
        }
        public static DataTable Consultar()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineconfh order by clave");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
    
        public static DataTable ConsultarVistaHorario(LineaConfigHrLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT consec,hora as Horario,meta as Meta,meta_ac as [Meta Acumulada] FROM t_lineconfh WHERE clave = '" + cve.Clave + "' order by hora");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        //conf horas
        public static DataTable ConsultarClaveHr(LineaConfigHrLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineconfh WHERE clave = '" + cve.Clave + "' order by consec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarHorario(LineaConfigHrLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineconfh WHERE clave = '" + cve.Clave + "' and hora  = '"+cve.Horario+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable VistaHorarioLinea(LineaConfigHrLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT 0 as consec, hora as Hora,meta as Meta,meta_ac as [Meta Acum],0 as Actual,0 as [Actual Acu],0 as Defectos,'' as [Work Order],'' as Comentarios,'1' as cumple_meta FROM t_lineconfh WHERE clave = '"+cve.Clave+"' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
         
     
  
        public static int Eliminar(LineaConfigHrLogica cve)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_lineconfh WHERE clave = "+cve.Clave+" and consec = "+cve.Consec+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

      

    }
}
