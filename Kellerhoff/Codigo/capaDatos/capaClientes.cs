using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Kellerhoff.Codigo.capaDatos
{
    /// <summary>
    /// Summary description for capaClientes
    /// </summary>
    public class capaClientes
    {
        public static DataTable RecuperarClienteAdministradorPorIdUsuarios(int usu_codigo)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Recursos.spRecuperarClienteAdministradorPorIdUsuarios", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paUsu_codigo = cmdComandoInicio.Parameters.Add("@usu_codigo", SqlDbType.Int);
            paUsu_codigo.Value = usu_codigo;

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

        public static DataSet RecuperarUsuariosDeCliente(int usu_codRol, int usu_codCliente, string filtro)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Recursos.spRecuperarUsuariosPorIdCliente", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
            SqlParameter paUsu_codRol = cmdComandoInicio.Parameters.Add("@usu_codRol", SqlDbType.Int);
            SqlParameter paUsu_codCliente = cmdComandoInicio.Parameters.Add("@usu_codCliente", SqlDbType.Int);
            paUsu_codRol.Value = usu_codRol;
            paUsu_codCliente.Value = usu_codCliente;


            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "UsuariosCliente");
                return dsResultado;

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
        public static DataSet MostrarProvincia(string filtro)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spProvincias", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);


            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Clientes");
                return dsResultado;

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
        public static DataTable RecuperarTodosCodigoReparto()
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarTodosCodigoReparto", Conn);
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
        public static DataTable RecuperarTodosClientes()
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarTodosClientes", Conn);
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
        public static DataTable RecuperarTodosClientesBySucursal(string pSucursal)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarTodosClientesBySucursal", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paSucursal = cmdComandoInicio.Parameters.Add("@cli_sucursal", SqlDbType.NVarChar, 2);
            paSucursal.Value = pSucursal;
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
        //public static DataTable RecuperarTodosClientesByGrupoCliente(string pGC)
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarTodosClientesByGrupoCliente", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paGrupoCliente = cmdComandoInicio.Parameters.Add("@cli_GrupoCliente", SqlDbType.NVarChar, 75);
        //    paGrupoCliente.Value = pGC;
        //    try
        //    {
        //        Conn.Open();
        //        DataTable dt = new DataTable();
        //        SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
        //        dt.Load(LectorSQLdata);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if (Conn.State == ConnectionState.Open)
        //        {
        //            Conn.Close();
        //        }
        //    }
        //}
        //public static DataTable spRecuperarTodosClientesByPromotor(string pPromotor)
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarTodosClientesByPromotor", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paPromotor = cmdComandoInicio.Parameters.Add("@cli_promotor", SqlDbType.NVarChar, 75);
        //    paPromotor.Value = pPromotor;
        //    try
        //    {
        //        Conn.Open();
        //        DataTable dt = new DataTable();
        //        SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
        //        dt.Load(LectorSQLdata);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        if (Conn.State == ConnectionState.Open)
        //        {
        //            Conn.Close();
        //        }
        //    }
        //}
        public static DataTable RecuperarClientePorId(int pIdCliente)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarClientePorId", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paIdCliente = cmdComandoInicio.Parameters.Add("@cli_codigo", SqlDbType.Int);
            paIdCliente.Value = pIdCliente;

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
        public static DataSet GestiónSucursal(int? sde_codigo, string sde_sucursal, string sde_sucursalDependiente, string accion)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spGestionSucursal", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paSde_codigo = cmdComandoInicio.Parameters.Add("@sde_codigo", SqlDbType.Int);
            SqlParameter paSde_sucursal = cmdComandoInicio.Parameters.Add("@sde_sucursal", SqlDbType.NVarChar, 2);
            SqlParameter paSde_sucursalDependiente = cmdComandoInicio.Parameters.Add("@sde_sucursalDependiente", SqlDbType.NVarChar, 2);
            SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

            if (sde_codigo == null)
            {
                paSde_codigo.Value = DBNull.Value;
            }
            else
            {
                paSde_codigo.Value = sde_codigo;
            }

            if (sde_sucursal == null)
            {
                paSde_sucursal.Value = DBNull.Value;
            }
            else
            {
                paSde_sucursal.Value = sde_sucursal;
            }

            if (sde_sucursalDependiente == null)
            {
                paSde_sucursalDependiente.Value = DBNull.Value;
            }
            else
            {
                paSde_sucursalDependiente.Value = sde_sucursalDependiente;
            }
            paAccion.Value = accion;

            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Sucursal");
                return dsResultado;
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

        public static DataSet MostrarClientes(int cli_codigo, string tipo, string filtro)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spClientes", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter pacli_codigo = cmdComandoInicio.Parameters.Add("@cli_codigo", SqlDbType.Int);
            SqlParameter paTipo = cmdComandoInicio.Parameters.Add("@tipo", SqlDbType.NVarChar, 50);
            SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);

            if (cli_codigo == null)
            {
                pacli_codigo.Value = DBNull.Value;
            }
            else
            {
                pacli_codigo.Value = cli_codigo;
            }

            if (filtro == null)
            {
                paFiltro.Value = DBNull.Value;
            }
            else
            {
                paFiltro.Value = filtro;
            }


            paTipo.Value = tipo;
            try
            {
                Conn.Open();
                DataSet dsResultado = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
                da.Fill(dsResultado, "Clientes");
                return dsResultado;

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
        public static decimal? RecuperarLimiteSaldo()
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spRecuperarLimiteSaldo", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            try
            {
                Conn.Open();
                DataTable dt = new DataTable();
                SqlDataReader LectorSQLdata = cmdComandoInicio.ExecuteReader();
                dt.Load(LectorSQLdata);
                decimal? resultado = null;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Contains("tls_limiteSaldo"))
                    {
                        resultado = Convert.ToDecimal(dt.Rows[0]["tls_limiteSaldo"]);
                    }
                }

                return resultado;
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
  
        public static bool AgregarMontoMinimo(string suc_codigo, decimal suc_montoMinimo)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Clientes.spAgregarMontoMinimo", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;

            SqlParameter paSuc_codigo = cmdComandoInicio.Parameters.Add("@suc_codigo", SqlDbType.NVarChar, 2);
            SqlParameter paSuc_montoMinimo = cmdComandoInicio.Parameters.Add("@suc_montoMinimo", SqlDbType.Decimal, 9);

            paSuc_codigo.Value = suc_codigo;
            paSuc_montoMinimo.Value = suc_montoMinimo;


            try
            {
                Conn.Open();
                cmdComandoInicio.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
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