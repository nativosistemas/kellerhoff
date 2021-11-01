using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Kellerhoff.Codigo.capaDatos
{
    public class capaMensajeNew
    {
        public static void ElimimarMensajeNewPorId(int pIdMensaje)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spElimimarMensajeNewPorId", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@codigo", SqlDbType.Int);
            paIdMensaje.Value = pIdMensaje;

            try
            {
                Conn.Open();
                cmdComandoInicio.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static DataTable RecuperarMensajeNewPorId(int pIdMensaje)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperarMensajeNewPorId", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@codigo", SqlDbType.Int);
            paIdMensaje.Value = pIdMensaje;

            try
            {
                Conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
                dt.Load(LectorSQLdata);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
        public static DataTable RecuperartTodosMensajes()
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spRecuperartTodosMensajesNew", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            try
            {
                Conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
                dt.Load(LectorSQLdata);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        public static int ActualizarInsertarMensajeNew(int pIdMensaje, string pAsunto, string pMensaje, DateTime? pFechaDesde, DateTime? pFechaHasta, bool pImportante, string pSucursales, string pRepartos, string pTipo)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("LogRegistro.spActualizarInsertarMensajeNew", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paIdMensaje = cmdComandoInicio.Parameters.Add("@codigo", SqlDbType.Int);
            paIdMensaje.Value = pIdMensaje;
            SqlParameter paAsunto = cmdComandoInicio.Parameters.Add("@asunto", SqlDbType.NVarChar, 300);
            paAsunto.Value = pAsunto;
            SqlParameter paMensaje = cmdComandoInicio.Parameters.Add("@mensaje", SqlDbType.NVarChar, -1);
            paMensaje.Value = pMensaje;

            SqlParameter paFechaDesde = cmdComandoInicio.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
            SqlParameter paFechaHasta = cmdComandoInicio.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
            SqlParameter paImportante = cmdComandoInicio.Parameters.Add("@importante", SqlDbType.Bit);
            SqlParameter paTmn_todosSucursales = cmdComandoInicio.Parameters.Add("@todosSucursales", SqlDbType.NVarChar, 500);
            SqlParameter paTmn_todosRepartos = cmdComandoInicio.Parameters.Add("@todosRepartos", SqlDbType.NVarChar, 500);
            SqlParameter paTmn_tipo = cmdComandoInicio.Parameters.Add("@tipo", SqlDbType.NVarChar, 100);
            if (pFechaDesde == null)
            {
                paFechaDesde.Value = DBNull.Value;
            }
            else
            {
                paFechaDesde.Value = pFechaDesde;
            }
            if (pFechaHasta == null)
            {
                paFechaHasta.Value = DBNull.Value;
            }
            else
            {
                paFechaHasta.Value = pFechaHasta;
            }
            paImportante.Value = pImportante;
            if (string.IsNullOrEmpty(pSucursales))
            {
                paTmn_todosSucursales.Value = DBNull.Value;
            }
            else
            {
                paTmn_todosSucursales.Value = pSucursales;
            }
            if (string.IsNullOrEmpty(pRepartos))
            {
                paTmn_todosRepartos.Value = DBNull.Value;
            }
            else
            {
                paTmn_todosRepartos.Value = pRepartos;
            }
            if (string.IsNullOrEmpty(pTipo))
            {
                paTmn_tipo.Value = DBNull.Value;
            }
            else
            {
                paTmn_tipo.Value = pTipo;
            }

            try
            {
                Conn.Open();
                return Convert.ToInt32(cmdComandoInicio.ExecuteScalar());
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }
    }
}