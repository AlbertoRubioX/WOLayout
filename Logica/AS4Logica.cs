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
        public string Comp { get; set; }
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
                "ROUND(" + con.Takt + "*(S.WOQTY  * K.IMSTCK)/60,2)/60 AS duration_min, " +
                "'0' as qa_alert, " +
                "K.IMCONV ,S.WOLOT " +
                "FROM KBM400MFG.FMWOSUM S " +
                "INNER JOIN KBM400MFG.FKITMSTR K ON S.WOPN = K.IMPN AND S.WOCO = K.IMCO " +
                "WHERE S.WOCO = '" +con.CN+"' AND S.WOWONO = '"+con.WO+"'";
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
        public static DataTable WorkOrderData(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql =
                "SELECT F.* " +
                "FROM KBM400MFG.FMWOSUM F " +
                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '" + con.CN + "' ";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable FKITMSTRData(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT FKITMSTR.IMCO, FKITMSTR.IMPN, FKITMSTR.IMDSC " +
                "FROM B20E386T.KBM400MFG.FKITMSTR FKITMSTR " +
                "WHERE(FKITMSTR.IMCO = '" + con.CN + "') AND(FKITMSTR.IMPN = '" + con.Comp + "')";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public static DataTable FMALOCATData(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT SUM( L.ALQTY) AS QTY, MIN( L.ALLOC ) AS ALLOC "+
                "FROM KBM400MFG.FMALOCAT L "+
                "WHERE L.WOCO = '" + con.CN + "' AND L.WOWONO = '" + con.WO + "' AND L.PSCMRN = '" + con.Comp + "'";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable FKITSAVEData(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT  FKITSAVE.TSDATE, TSLOCF, FKITSAVE.USRPRF "+
                "FROM B20E386T.KBM400MFG.FKITSAVE FKITSAVE "+
                "WHERE(FKITSAVE.TSCO = '"+con.CN+"')  AND(FKITSAVE.TSREFN = '" + con.WO + "')  AND(FKITSAVE.TSCODE = '03')  AND FKITSAVE.TSPN = '" + con.Comp + "' " +
                "ORDER BY TSDATE DESC";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable FKLOCMSTData(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT FKLOCMST.LMCO, FKLOCMST.LMPN, FKLOCMST.LMCODE, FKLOCMST.LMTQTY, FKLOCMST.LMSEQ ,FKLOCMST.LMLOT " +
                "FROM KBM400MFG.FKLOCMST FKLOCMST " +
                "WHERE (FKLOCMST.LMCO='"+con.CN+"') AND (FKLOCMST.LMSEQ<>900 AND FKLOCMST.LMSEQ<>0 AND FKLOCMST.LMSEQ<>90000 AND FKLOCMST.LMSEQ<>50000 AND FKLOCMST.LMSEQ<>50 AND FKLOCMST.LMSEQ<>5000 AND FKLOCMST.LMSEQ<>3500 AND FKLOCMST.LMSEQ<>99999) AND (FKLOCMST.LMTQTY<>0) AND (FKLOCMST.LMPN = '"+con.Comp+"') " +
                "ORDER BY LMCODE ASC";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable FKLOTESData(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT DISTINCT FKLOCMST.LMLOT AS LMLOT " +
                "FROM KBM400MFG.FKLOCMST FKLOCMST " +
                "WHERE (FKLOCMST.LMCO='" + con.CN + "') AND (FKLOCMST.LMSEQ<>900 AND FKLOCMST.LMSEQ<>0 AND FKLOCMST.LMSEQ<>90000 AND FKLOCMST.LMSEQ<>50000 AND FKLOCMST.LMSEQ<>50 AND FKLOCMST.LMSEQ<>5000 AND FKLOCMST.LMSEQ<>3500 AND FKLOCMST.LMSEQ<>99999) AND (FKLOCMST.LMTQTY<>0) AND (FKLOCMST.LMPN = '" + con.Comp + "') " +
                "ORDER BY 1";
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
                string sSql = "SELECT IMSTCK,IMDSC FROM KBM400MFG.FKITMSTR WHERE IMCO = '" + con.CN+"' AND IMPN = '"+con.Item+"'";
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
        public static DataTable PartKitsCont(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT P.DMRID , COUNT( P.NODE) ,0 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' GROUP BY P.DMRID ,0 " +
                                "UNION " + 
                                "SELECT P.DMRID , COUNT( P.NODE) ,1 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%TOWEL%' GROUP BY P.DMRID ,1 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) ,2 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%DRAPE%' GROUP BY P.DMRID ,2 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) ,3 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%GAUZE%' GROUP BY P.DMRID ,3 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 4 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%BAG%' GROUP BY P.DMRID ,4 " +
                                "UNION " +
                                "SELECT P.DMRID, COUNT(P.NODE), 5 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%MAYO%' GROUP BY P.DMRID ,5 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 6 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%SYR%' GROUP BY P.DMRID ,6 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 7 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%NDL%' GROUP BY P.DMRID ,7 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 8 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%CUP%' GROUP BY P.DMRID ,8 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 9 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%LBL%' GROUP BY P.DMRID ,9 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 10 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%BLADE%' GROUP BY P.DMRID ,10 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 11 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%MARKER%' GROUP BY P.DMRID ,11 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 12 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%RULER%' GROUP BY P.DMRID ,12 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 13 FROM KBM400MFG.FMWOSUM F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.WOPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.WOPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.WOWONO = '" + con.WO + "' AND F.WOCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%BOWL%' GROUP BY P.DMRID ,13 ORDER BY 3";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }
        public static DataTable PartKitsContOld(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT P.DMRID , COUNT( P.NODE) ,0 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN WHERE F.SKWONO = '0130310' AND F.SKCO = '686' AND K.IMCO = '686'  GROUP BY P.DMRID ,0 UNION " +
                                "SELECT P.DMRID , COUNT( P.NODE) ,1 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN "+
                                "WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%TOWEL%' GROUP BY P.DMRID ,1 "+
                                "UNION "+
                                "SELECT P.DMRID , COUNT(P.NODE) ,2 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%DRAPE%' GROUP BY P.DMRID ,2 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) ,3 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%GAUZE%' GROUP BY P.DMRID ,3 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 4 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%BAG%' GROUP BY P.DMRID ,4 " +
                                "UNION " +
                                "SELECT P.DMRID, COUNT(P.NODE), 5 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%MAYO%' GROUP BY P.DMRID ,5 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 6 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%SYR%' GROUP BY P.DMRID ,6 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 7 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%NDL%' GROUP BY P.DMRID ,7 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 8 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%CUP%' GROUP BY P.DMRID ,8 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 9 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%LBL%' GROUP BY P.DMRID ,9 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 10 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN) INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                " WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%BLADE%' GROUP BY P.DMRID ,10 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 11 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " +
                                "WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%MARKER%' GROUP BY P.DMRID ,11 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 12 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN " + 
                                " WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%RULER%' GROUP BY P.DMRID ,12 " +
                                "UNION " +
                                "SELECT P.DMRID , COUNT(P.NODE) , 13 FROM KBM400MFG.FQSPCHST F INNER JOIN KBM400SQL.PACKDETAIL P ON P.DMRID = F.SKPN AND P.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = F.SKPN)  INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN "+
                                "WHERE F.SKWONO = '"+con.WO+"' AND F.SKCO = '686' AND K.IMCO = '686' AND K.IMDSC LIKE '%BOWL%' GROUP BY P.DMRID ,13 ORDER BY 3";
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

        public static DataTable SpecialNotes(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT  DISTINCT(MSG)   FROM KBM400SQL.PACKDETAIL P INNER JOIN KBM400MFG.FKITMSTR K ON P.DMRID = K.IMPN "+
                "WHERE DMRID = '"+con.Item+ "' AND PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = '" + con.Item + "') " +
                "AND(P.MSG IS NOT NULL AND P.MSG > '')";
                datos = AccesoDatos.ConsultarAS4(sSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return datos;
        }

        public static DataTable PackLookup(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                //CH CODES & DP
                /*
                 * SELECT 
                    F.PSCMRN, K.IMDSC 
                    FROM KBM400MFG.FKPSTRUC F
                    INNER JOIN KBM400MFG.FKITMSTR K ON F.PSCMRN = K.IMPN AND K.IMCO = '686' 
                   WHERE PSPMRN = '112849' AND SUBSTR(PSCMRN,1,2) = 'CH'
                */
                string sSql = "SELECT P.QTY,P.UM, K.IMDSC,P.NODE, K.IMVDPN , F.PSCMRN  , K2.IMDSC "+
                "FROM KBM400SQL.PACKDETAIL P " +
                "INNER JOIN KBM400MFG.FKITMSTR K ON P.NODE = K.IMPN AND K.IMCO = '686' " +
                "LEFT OUTER JOIN KBM400MFG.FKPSTRUC F ON P.NODE = F.PSPMRN AND SUBSTR(F.PSCMRN, 1, 2) = 'CH' " +
                "LEFT OUTER JOIN KBM400MFG.FKITMSTR K2 ON F.PSCMRN = K2.IMPN AND K2.IMCO = '686' " +
                "WHERE P.DMRID = '" + con.Item + "' AND P.PACKDRAW_REV = " +
                "(SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = '" + con.Item + "') " +
                "AND P.LEVEL_CODE > '' AND(SUBSTR(P.NODE, 1, 2) != 'S0' AND SUBSTR(P.NODE, 1, 2) != 'B0' AND SUBSTR(P.NODE, 1, 2) != 'F0') ";
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

        public static DataTable ComponentsTable(AS4Logica con)
        {
            DataTable datos = new DataTable();
            try
            {
                string sSql = "SELECT D.LEVEL_CODE as NIVEL, D.NODE as NO_PARTE, K.IMDSC as DESCRIPCION ,D.QTY as CANT,D.UM as UM,D.PACKDRAW_REV " +
                "FROM KBM400SQL.PACKDETAIL D INNER JOIN KBM400MFG.FKITMSTR K ON D.NODE = K.IMPN AND K.IMCO = '"+con.CN+"' " +
                "WHERE D.DMRID = '" + con.Item + "' " +
                "AND SUBSTR(D.LEVEL_CODE, 1, 1) <> 'Z' AND SUBSTR(D.LEVEL_CODE, 1, 1) <> 'W' " +
                "AND D.PACKDRAW_REV = (SELECT  MAX(PACKDRAW_REV) FROM KBM400SQL.PACKMASTER WHERE DMRID = '"+con.Item+"') " +
                "ORDER BY D.LEVEL_CODE, D.SEQUENCE_NUM";
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
