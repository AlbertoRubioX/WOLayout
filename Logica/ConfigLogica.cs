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
        public string Usuario { get; set; }

        public static int GuardarSP(ConfigLogica config)
        {
            string[] parametros = { "@Jornada", "@HrsDisp", "@SegDisp", "@CajasLinea", "@KitsCaja", "@KitsLinea", "@TakTime", "@Tak80", "@AssyTime", "@MaxComp", "@MesaEns", "@MesaWrap", "@MesaSub", "@OperNA", "@Surtidor", "@InspSella", "@Sellador", "@Inspeccion", "@Usuario", "@Horiz", "@Vertical", "@Sobre", "@TuckTape", "@WrapNA" };
            return AccesoDatos.Actualizar("sp_mant_config", parametros, config.Jornada, config.HorasDisp, config.SegDisp, config.Cajas, config.Kits, config.KitLinea, config.Tak, config.Tak80,config.Assy, config.MaxComp, config.Mesas, config.MesaWrap, config.MesaSub, config.OperNA, config.Surtidor, config.InspSella, config.Selladora, config.Inspeccion, config.Usuario,config.Horizontal,config.Vertical,config.Sobre,config.Tape,config.WrapNA);
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
    }
}
