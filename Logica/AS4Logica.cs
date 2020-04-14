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
        public string CN { get; set; }
        public string WO { get; set; }
        public decimal Takt { get; set; }
        public decimal MaxComp { get; set; }
        public string Item { get; set; }
        public string Layer { get; set; }

        public static DataTable WorkOrder(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT "+
                "S.WOPN AS product,"+
                "K.IMDSC AS name," +
                "S.WOQTY AS box,"+
                "K.IMSTCK AS kits," + 
                "S.WOQTY * K.IMSTCK AS total_kits," +
                "ROUND(("+con.Takt+"*(S.WOQTY  * K.IMSTCK)/60),2)/60 AS duration,"+
                "'' as boxhr, " +
                "ROUND(" + con.Takt + "*(S.WOQTY  * K.IMSTCK)/60,2)/60 AS duration_min " +
                "FROM KBM400MFG.FMWOSUM S "+
                "INNER JOIN KBM400MFG.FKITMSTR K ON S.WOPN = K.IMPN AND S.WOCO = K.IMCO " +
                "WHERE S.WOCO = '"+con.CN+"' AND S.WOWONO = '"+con.WO+"'";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public static DataTable WorkOrderOld(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql =
                "SELECT F.SKPN,P.FOLD " +
                "FROM KBM400MFG.FQSPCHST F " +
                "LEFT OUTER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.FOLD > 0 AND P.FOLD <> 7 AND P.PACKDRAW_REV = " +
                "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) AND SUBSTR(LEVEL_CODE,1, 1) = 'W' " +
                "WHERE F.SKWONO = '" + con.WO + "' AND F.SKCO = '" + con.CN + "' ";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable WorkOrderNew(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql =
                "SELECT F.WOPN,P.FOLD " +
                "FROM KBM400MFG.FMWOSUM F " +
                "LEFT OUTER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.FOLD > 0 AND P.FOLD <> 7 AND P.PACKDRAW_REV = " +
                "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) AND SUBSTR(LEVEL_CODE,1, 1) = 'W' " +
                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '" + con.CN + "' ";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable PartKits(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT IMSTCK FROM KBM400MFG.FKITMSTR WHERE IMCO = '"+con.CN+"' AND IMPN = '"+con.Item+"'";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable PartKitsComp(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT F.IMSTCK ,(SELECT QTY FROM  KBM400SQL.PACKDETAIL WHERE DMRID='"+con.Item+ "' AND NODE='03883' AND PACKDRAW_REV = (SELECT MAX(PACKDRAW_REV) FROM KBM400SQL.PACKDETAIL WHERE DMRID='"+con.Item+"')) AS QTY " +
                "FROM B20E386T.KBM400MFG.FKITMSTR F " +
                "INNER JOIN  KBM400SQL.PACKDETAIL P ON F.IMPN = P.DMRID " +
                "WHERE F.IMPN = '"+con.Item+"' AND F.IMCO = '"+con.CN+"' " +
                "GROUP BY F.IMSTCK ";
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
        public static DataTable ComponentsLayerDetail(AS4Logica con)
        {

            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT NODE,LEVEL_CODE,FOLD FROM KBM400SQL.PACKDETAIL " +
                        "WHERE DMRID = '" + con.Item + "' AND PACKDRAW_REV = " +
                        "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER " +
                        "WHERE DMRID = '" + con.Item + "') AND SUBSTR(LEVEL_CODE,1, 1) = '"+con.Layer+"' ORDER BY LEVEL_CODE";
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
