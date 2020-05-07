using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class ItemSugdetLogica
    {
        public string Item { get; set; }
        public string Mesa { get; set; }
        public int Consec { get; set; }
        public string Codigo { get; set; }
        public string Descrip { get; set; }
        public decimal Cant { get; set; }
        public string UM { get; set; }
        public string Tipo { get; set; }

        public static int GuardarSP(ItemSugdetLogica item)
        {
            string[] parametros = { "@Item", "@Mesa", "@Consec", "@Code", "@Descrip", "@Cant", "@Um", "@Tipo" };
            return AccesoDatos.Actualizar("sp_mant_itemsugdet", parametros, item.Item, item.Mesa, item.Consec, item.Codigo, item.Descrip, item.Cant, item.UM, item.Tipo);
        }

        public static DataTable ConsultarItem(ItemSugdetLogica item)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_itemsugd WHERE item = '" + item.Item + "'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarVista(ItemSugdetLogica Item)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT item,mesa as MESA,consec as #,code as [NO. PARTE],descrip as DESCRIPCION,cant as CANT,um as UM FROM t_itemsugd WHERE item = '" + Item.Item + "' ORDER BY tipo,mesa,consec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarVistaMesa(ItemSugdetLogica Item)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT mesa as MESA,consec as #,code as [NO. PARTE],descrip as DESCRIPCION,cant as CANT,um as UM FROM t_itemsugd WHERE item = '" + Item.Item + "' and mesa = '" + Item.Mesa + "' and tipo = 'C' ORDER BY consec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ListarPS(ItemSugdetLogica Item)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "select distinct mesa from t_itemsugd where item = '" + Item.Item + "' and mesa like'PRE%' and mesa in (select code from t_itemsugd where item = '" + Item.Item + "' and tipo = 'C' and mesa = '" + Item.Mesa + "') ";
                datos = AccesoDatos.Consultar(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarVistaPS(ItemSugdetLogica Item)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT mesa as MESA,consec as #,code as [NO. PARTE],descrip as DESCRIPCION,cant as CANT,um as UM FROM t_itemsugd " +
                "WHERE item = '" + Item.Item + "' and mesa = '" + Item.Codigo + "' " +
                "ORDER BY consec";
                //"and mesa in (select code from t_itemsugd where item = '" + Item.Item + "' and tipo = 'C' and mesa = '" + Item.Mesa + "') " +

                datos = AccesoDatos.Consultar(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarVistaSug(ItemSugdetLogica Item)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT mesa as MESA,consec as #,code as [NO. PARTE],descrip as DESCRIPCION,cant as CANT,um as UM FROM t_itemsugd WHERE item = '" + Item.Item + "' and tipo = 'C' ORDER BY mesa, consec");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ConsultarVistaSugPS(ItemSugdetLogica Item)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT mesa as MESA,consec as #,code as [NO. PARTE],descrip as DESCRIPCION,cant as CANT,um as UM FROM t_itemsugd " +
                "WHERE item = '" + Item.Item + "' and mesa = '"+Item.Mesa+"' and tipo = 'S' ORDER BY consec";
                datos = AccesoDatos.Consultar(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static DataTable ListarSugePS(ItemSugdetLogica Item)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "select distinct mesa from t_itemsugd where item = '" + Item.Item + "' and mesa like'PRE%' ";
                datos = AccesoDatos.Consultar(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static int Eliminar(ItemSugdetLogica item)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_itemsugd WHERE item = '" + item.Item + "' and mesa = '"+item.Mesa+"' and consec = "+item.Consec+" ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

        public static int EliminarSP(ItemSugdetLogica item)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("DELETE FROM t_itemsugd WHERE item = '" + item.Item + "' and mesa = '" + item.Mesa + "' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }

        public static int UpdateRow(ItemSugdetLogica item)
        {
            int iRes = 0;
            try
            {
                iRes = AccesoDatos.Borrar("UPDATE t_itemsugd set code = null,descrip = null,cant = null,um=null WHERE item = '" + item.Item + "' and mesa = '" + item.Mesa + "' and consec = " + item.Consec + " ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }
    }
}
