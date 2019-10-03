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
        public string Linea { get; set; }
        public string Estacion { get; set; }
        public decimal Factor { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(LineaRampeoLogica line)
        {
            string[] parametros = { "@Station","@Line","@Factor","@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_lineramp", parametros, line.Estacion,line.Linea,line.Factor,line.Usuario);
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
        public static DataTable Vista()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT station,line,factor FROM t_lineramp order by line");
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
                iRes = AccesoDatos.Borrar("DELETE FROM t_lineramp WHERE station = '"+line.Estacion+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

    }
}
