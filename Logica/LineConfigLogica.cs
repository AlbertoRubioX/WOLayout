using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class LineaConfigLogica
    {
        public string CN { get; set; }
        public string Clave { get; set; }
        public int Consec { get; set; }
        public string Descrip { get; set; }
        public string Turno { get; set; }
        public decimal Meta { get; set; }
        public string MetaUm { get; set; }
        public string Horario { get; set; }
        public string Linea { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(LineaConfigLogica cve)
        {
            string[] parametros = {"@Clave", "Descrip", "@Meta", "@MetaUm", "@Turno", "@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_lineconf", parametros,cve.Clave, cve.Descrip, cve.Meta, cve.MetaUm, cve.Turno, cve.Usuario);
        }
        public static DataTable Consultar()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineconf order by clave");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarClave(LineaConfigLogica cve)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_lineconf WHERE clave = '"+cve.Clave+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarVistaHorario(LineaConfigLogica cve)
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
        public static DataTable ConsultarClaveHr(LineaConfigLogica cve)
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
        public static DataTable ConsultarHorario(LineaConfigLogica cve)
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
        public static DataTable VistaHorarioLinea(LineaConfigLogica cve)
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

        //conf lineas
        public static DataTable ConsultarClaveLine(LineaConfigLogica cve)
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
        public static DataTable VistaClaveLine(LineaConfigLogica cve)
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

        public static DataTable ConsultarConfLinea(LineaConfigLogica cve)
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
 
        public static int Eliminar(LineaConfigLogica cve)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_lineconf WHERE clave = "+cve.Clave+"");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

        public static DataTable ConsultarTurnos()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * from t_turno");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarTurnoHr(LineaConfigLogica con)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * from t_turnohr where turno = '"+con.Turno+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable VistaTurnoHr(LineaConfigLogica con)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT 0 as consec,horario as Horario,0 as Meta,0 as [Meta Acumulada] from t_turnohr where turno = '" + con.Turno + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

    }
}
