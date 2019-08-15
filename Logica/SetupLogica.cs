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
                string sSql = "SELECT Product_Name as PRODUCTO,Product_Description AS NAME,Original_Work_Order_Quantity AS CAJAS,Product_Kits_per_Case AS KITS,"+
                "In_Kits_Work_Order_Quantity [TOTAL EN KITS], ((20 * In_Kits_Work_Order_Quantity) / 60) as [W.O. DURACION (min)] "+
                "FROM Work_Order_Details where Company_Number = 686 and Work_Order_Number = '"+set.WorkOrder+"'";
                datos = AccesoDatos.ConsultarSetup(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
    }
}
