using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data;

namespace Logica
{
    public class AS4Logica
    {
        public string WO { get; set; }
        public decimal Takt { get; set; }
        public decimal MaxComp { get; set; }
        public string Item { get; set; }

        public static DataTable WorkOrder(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT DISTINCT "+
                "FMWOSUM.WOPN AS product,SF58041W.IMDSC as name, FMWOSUM.WOQTY AS box, " +
                "FKITMSTR.IMSTCK AS kits,  " +
                @"FMWOSUM.WOQTY * FKITMSTR.IMSTCK AS ""total_kits"", " +
                "ROUND("+con.Takt+"*(FMWOSUM.WOQTY  * FKITMSTR.IMSTCK)/60,0) AS duration " +
                "FROM B20E386T.KBM400MFG.FMWOSUM FMWOSUM LEFT OUTER JOIN B20E386T.MRC400WEB.SF58041W SF58041W ON FMWOSUM.WOPN = SF58041W.IMPN, B20E386T.KBM400MFG.FKITMSTR FKITMSTR " +
                "WHERE FMWOSUM.WOPN = FKITMSTR.IMPN " +
                "AND FMWOSUM.WOWONO = '"+con.WO+"'";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable TableDescrip(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT * FROM SYSIBM.COLUMNS WHERE TABLE_NAME = 'FMWOSUM'";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public static DataTable Components(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT CASE WHEN COUNT( DMRID) > "+con.MaxComp+" THEN 'Si' ELSE 'No' END AS SUBASSY," +
                        "COUNT(DMRID) AS COMPONENTS  " +
                        "FROM KBM400SQL.PACKDETAIL " +
                        "WHERE DMRID = '" + con.Item + "' AND PACKDRAW_REV = " +
                        "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER " +
                        "WHERE DMRID = '" + con.Item + "') AND LEVEL_CODE > '' AND ( SUBSTR(NODE,1,2) != 'S0' AND SUBSTR(NODE,1,2) != 'B0' AND SUBSTR(NODE,1,2) != 'F0' )";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public static DataTable ComponentsLayer(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT 0,CASE WHEN COUNT( DMRID) > " + con.MaxComp + " THEN 'Si' ELSE 'No' END AS SUBASSY," +
                        "COUNT(DMRID) AS COMPONENTS  " +
                        "FROM KBM400SQL.PACKDETAIL " +
                        "WHERE DMRID = '" + con.Item + "' AND PACKDRAW_REV = " +
                        "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER " +
                        "WHERE DMRID = '" + con.Item + "') AND LEVEL_CODE > '' AND ( SUBSTR(NODE,1,2) != 'S0' AND SUBSTR(NODE,1,2) != 'B0' AND SUBSTR(NODE,1,2) != 'F0'  ) " +
                        "UNION "+
                        "SELECT 1,SUBSTR(LEVEL_CODE,1, 1) AS LEVEL, FOLD " +
                        "FROM KBM400SQL.PACKDETAIL " +
                        "WHERE DMRID = '" + con.Item + "' AND PACKDRAW_REV = " +
                        "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER " +
                        "WHERE DMRID = '" + con.Item + "') AND (SUBSTR(LEVEL_CODE,1, 1) = 'W' OR SUBSTR(LEVEL_CODE,1, 1) = 'M' OR SUBSTR(LEVEL_CODE,1, 1) = 'L'  OR SUBSTR(LEVEL_CODE,1, 1) = 'S') " +
                        "AND LEVEL_CODE > '' AND FOLD > 0 AND FOLD <> 7 ORDER BY 1";
                
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public static bool ComponentsLayerFold(AS4Logica con)
        {
            try
            {
                string sSql = "SELECT FOLD FROM KBM400SQL.PACKDETAIL " +
                        "WHERE DMRID = '" + con.Item + "' AND PACKDRAW_REV = " +
                        "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER " +
                        "WHERE DMRID = '" + con.Item + "') AND SUBSTR(LEVEL_CODE,1, 1) = 'F' AND FOLD > 0 AND FOLD <> 7";
                DataTable datos = AccesoDatos.ConsultarAS4(sSql);
                if(datos.Rows.Count > 0)
                {
                    string sValue = datos.Rows[0][0].ToString();
                    if (!string.IsNullOrEmpty(sValue))
                        return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        public static DataTable LineLayout(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT SUBSTR(LEVEL_CODE,1,1) AS LEVEL, " +
                "COUNT(DMRID) AS COMPONENTS " +
                "FROM KBM400SQL.PACKDETAIL " +
                "WHERE DMRID = '" + con.Item + "' AND PACKDRAW_REV = " +
                "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER " +
                "WHERE DMRID = '" + con.Item + "') AND LEVEL_CODE > '' "+
                "AND(SUBSTR(NODE, 1, 2) != 'S0' AND SUBSTR(NODE, 1, 2) != 'B0' AND SUBSTR(NODE, 1, 2) != 'F0') " +
                "GROUP BY SUBSTR(LEVEL_CODE, 1, 1)";

                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }


    }
}
