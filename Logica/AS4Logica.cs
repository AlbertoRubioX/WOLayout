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
        public string Item { get; set; }

        public static DataTable WorkOrder(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT DISTINCT "+
                "FMWOSUM.WOPN AS JOB,'' as NAME, FMWOSUM.WOQTY AS BOXES, " +
                "FKITMSTR.IMSTCK AS KITS,  " +
                "FMWOSUM.WOQTY * FKITMSTR.IMSTCK AS TOTAL_KITS, " +
                "ROUND(20*(FMWOSUM.WOQTY  * FKITMSTR.IMSTCK)/60,0) AS DURATION " +
                "FROM B20E386T.KBM400MFG.FMWOSUM FMWOSUM, B20E386T.KBM400MFG.FKITMSTR FKITMSTR " +
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

        public static DataTable ComponentsLayer(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT CASE WHEN COUNT( DMRID) > 7 THEN 'YES' ELSE 'NO' END AS SUBASSY,"+
                        "COUNT(DMRID) AS COMPONENTS  "+
                        "FROM KBM400SQL.PACKDETAIL " +
                        "WHERE DMRID = '" + con.Item + "' AND PACKDRAW_REV = " +
                        "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER " +
                        "WHERE DMRID = '" + con.Item + "') AND LEVEL_CODE > '' AND ( SUBSTR(NODE,1,2) != 'S0' AND SUBSTR(NODE,1,2) != 'B0' ) " +
                        "UNION "+
                        "SELECT SUBSTR(LEVEL_CODE,1, 1) AS LEVEL, FOLD "+
                        "FROM KBM400SQL.PACKDETAIL " +
                        "WHERE DMRID = '" + con.Item + "' AND PACKDRAW_REV = " +
                        "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER " +
                        "WHERE DMRID = '" + con.Item + "') AND LEVEL_CODE > '' AND FOLD > 0 AND FOLD <> 7";
                
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
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
                "AND(SUBSTR(NODE, 1, 2) != 'S0' AND SUBSTR(NODE, 1, 2) != 'B0') " +
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
