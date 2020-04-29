using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class ItemSugLogica
    {
        public string Item { get; set; }        
        public string Descrip { get; set; }
        public int Layout { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(ItemSugLogica item)
        {
            string[] parametros = {"@Item","@Descrip", "@Layout", "@Usuario"};
            return AccesoDatos.Actualizar("sp_mant_itemsug", parametros, item.Item, item.Descrip, item.Layout, item.Usuario);

        }

        public static DataTable ConsultarItem(ItemSugLogica item)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_itemsug WHERE item = '"+item.Item+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        /*
        public static DataTable ConsultarFolio(ItemSugLogica line)
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

   
        public static int Eliminar(ItemSugLogica line)
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
        */
    }
}
