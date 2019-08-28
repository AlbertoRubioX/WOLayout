using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class ConfigLogica
    {
        public decimal Jornada { get; set; }
        public decimal HorasDisp { get; set; }
        public decimal SegDisp { get; set; }
        public decimal Cajas { get; set; }
        public decimal Kits { get; set; }
        public decimal KitLinea { get; set; }
        public decimal Tak { get; set; }
        public decimal Tak80 { get; set; }
        public decimal Assy { get; set; }
        public decimal MaxComp { get; set; }
        public decimal Mesas { get; set; }
        public decimal MesaWrap { get; set; }
        public decimal MesaSub { get; set; }
        public decimal OperNA { get; set; }
        public decimal Surtidor { get; set; }
        public decimal InspSella { get; set; }
        public decimal Selladora { get; set; }
        public decimal Inspeccion { get; set; }
        public decimal Horizontal { get; set; }
        public decimal Vertical { get; set; }
        public decimal Sobre { get; set; }
        public decimal Tape { get; set; }
        public decimal WrapNA { get; set; }
        public decimal Detroit { get; set; }
        public string Language { get; set; }
        public int MaxHC { get; set; }
        public int MinHC { get; set; }
        public string Form { get; set; }
        public string Control { get; set; }
        public string SubControl { get; set; }
        public int Columna { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(ConfigLogica config)
        {
            string[] parametros = { "@Jornada", "@HrsDisp", "@SegDisp", "@CajasLinea", "@KitsCaja", "@KitsLinea", "@TakTime", "@Tak80", "@AssyTime", "@MaxComp", "@MesaEns", "@MesaWrap", "@MesaSub", "@OperNA", "@Surtidor", "@InspSella", "@Sellador", "@Inspeccion", "@Usuario", "@Horiz", "@Vertical", "@Sobre", "@TuckTape", "@WrapNA","@Detroit","@Language" ,"@MaxHC", "@MinHC"};
            return AccesoDatos.Actualizar("sp_mant_config", parametros, config.Jornada, config.HorasDisp, config.SegDisp, config.Cajas, config.Kits, config.KitLinea, config.Tak, config.Tak80,config.Assy, config.MaxComp, config.Mesas, config.MesaWrap, config.MesaSub, config.OperNA, config.Surtidor, config.InspSella, config.Selladora, config.Inspeccion, config.Usuario,config.Horizontal,config.Vertical,config.Sobre,config.Tape,config.WrapNA,config.Detroit,config.Language,config.MaxHC,config.MinHC);
        }

        public static DataTable Consultar()
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_config WHERE clave = '01'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ChangeLanguage(ConfigLogica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sColumn = "descripcion";
                if (con.Language == "EN")
                    sColumn = "description";
                else
                datos = AccesoDatos.Consultar("SELECT "+sColumn+" FROM t_sysleng WHERE form = '"+con.Form+"' and control = '"+con.Control+"' and subcontrol = '"+con.SubControl+"' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static string ChangeLanguageCont(ConfigLogica con)
        {
            string sValue = string.Empty;
            
            try
            {
                string sColumn = "descripcion";
                if (con.Language == "EN")
                    sColumn = "description";

                string sSql = "SELECT " + sColumn + " FROM t_sysleng WHERE form = '" + con.Form + "' and control = '" + con.Control + "' ";
                DataTable datos = AccesoDatos.Consultar(sSql);
                if (datos.Rows.Count > 0)
                    sValue = datos.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sValue;
        }
        public static string ChangeLanguageGrid(ConfigLogica con)
        {
            string sValue = string.Empty;

            try
            {
                string sColumn = "descripcion";
                if (con.Language == "EN")
                    sColumn = "description";

                string sSql = "SELECT " + sColumn + " FROM t_sysleng WHERE form = '" + con.Form + "' and control = '" + con.Control + "' and subcontrol = '"+con.SubControl+"' ";
                DataTable datos = AccesoDatos.Consultar(sSql);
                if (datos.Rows.Count > 0)
                    sValue = datos.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sValue;
        }
        public static string ChangeLanguageGridRow(ConfigLogica con)
        {
            string sValue = string.Empty;

            try
            {
                string sColumn = "descripcion";
                if (con.Language == "EN")
                    sColumn = "description";

                string sSql = "SELECT " + sColumn + " FROM t_sysleng WHERE form = '" + con.Form + "' and control = '" + con.Control + "' and subcontrol = '" + con.SubControl + "' and columna = "+con.Columna+" ";
                DataTable datos = AccesoDatos.Consultar(sSql);
                if (datos.Rows.Count > 0)
                    sValue = datos.Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sValue;
        }
    }
}
