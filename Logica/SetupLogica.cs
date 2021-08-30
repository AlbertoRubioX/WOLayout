using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class SetupLogica
    {
        public string WorkOrder { get; set; }
        public string Part { get; set; }
        public decimal Takt { get; set; }
        public static int EjecutaSP(SetupLogica config)
        {
            //string[] parametros = { "@Jornada", "@HrsDisp", "@SegDisp", "@CajasLinea", "@KitsCaja", "@KitsLinea", "@TakTime", "@Tak80", "@AssyTime", "@MaxComp", "@MesaEns", "@MesaWrap", "@MesaSub", "@OperNA", "@Surtidor", "@InspSella", "@Sellador", "@Inspeccion", "@Usuario", "@Horiz", "@Vertical", "@Sobre", "@TuckTape", "@WrapNA" };
            //return AccesoDatos.Actualizar("sp_mant_config", parametros, config.Jornada, config.HorasDisp, config.SegDisp, config.Cajas, config.Kits, config.KitLinea, config.Tak, config.Tak80,config.Assy, config.MaxComp, config.Mesas, config.MesaWrap, config.MesaSub, config.OperNA, config.Surtidor, config.InspSella, config.Selladora, config.Inspeccion, config.Usuario,config.Horizontal,config.Vertical,config.Sobre,config.Tape,config.WrapNA);
            return 0;
        }

        public static DataTable ConsultarWO(SetupLogica set)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT Product_Name as product,Product_Description AS name,Original_Work_Order_Quantity AS box,Product_Kits_per_Case AS kits,"+
                "In_Kits_Work_Order_Quantity total_kits, ((" + set.Takt + " * In_Kits_Work_Order_Quantity) / 60)/60 as duration,'' as boxhr,'' as duration_min " +
                "FROM Work_Order_Details where  Work_Order_Number = '"+set.WorkOrder+"'"; //Company_Number = 686 and
                datos = AccesoDatos.ConsultarSetup(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarEDL()
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT componente FROM t_setup_edl"; 
                datos = AccesoDatos.Consultar(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static bool getEDL(SetupLogica sp)
        {
            bool bReturn = false;
            try
            {
                string sSql = "SELECT * FROM t_setup_edl where componente = '"+sp.Part+"'";
                DataTable datos = AccesoDatos.Consultar(sSql);
                if (datos.Rows.Count > 0)
                {
                    bReturn = true;
                }
                else
                    bReturn = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;
        }
    }
}
