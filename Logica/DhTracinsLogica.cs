﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Datos;

namespace Logica
{
    public class DhTracinsLogica
    {
        public long Folio { get; set; }
        public int Falla { get; set; }
        public int Inspector { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }

        public static int GuardarSP(DhTracinsLogica dhr)
        {
            string[] parametros = {"@Folio", "@Falla", "@Empleado", "@Nombre", "@Usuario" };
            return AccesoDatos.Actualizar("sp_mant_dhtracinsp", parametros, dhr.Folio, dhr.Falla, dhr.Inspector, dhr.Nombre,dhr.Usuario);

        }

        public static DataTable ConsultarOrden(DhTracinsLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracker WHERE folio = '"+dhr.Folio+"'");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

         
        public static DataTable ConsultarInsp(DhTracinsLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT * FROM t_dhtracinsp where folio="+dhr.Folio+" and falla = "+dhr.Falla+" order by nombre ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }

        public static DataTable ConsultarInspView(DhTracinsLogica dhr)
        {
            DataTable datos = new DataTable();
            try
            {
                datos = AccesoDatos.Consultar("SELECT folio,falla,empleado as Inspector,nombre as Nombre FROM t_dhtracinsp where folio="+dhr.Folio+" and falla = "+dhr.Falla+" order by nombre");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public static int Eliminar(DhTracinsLogica det)
        {
            try
            {
                return AccesoDatos.Borrar("DELETE FROM t_dhtracinsp where folio = " + det.Folio + " and falla = " + det.Falla + " ");
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }

    }
}