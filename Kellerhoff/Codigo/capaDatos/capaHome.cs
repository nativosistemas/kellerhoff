using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Kellerhoff.Codigo.capaDatos
{

    public class capaHome
    {
        //

        public static DataTable RecuperarHomeSlidePorId(int pIdHomeSlide)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("spRecuperarHomeSlidePorId", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            SqlParameter paIdHomeSlide = cmdComandoInicio.Parameters.Add("@hsl_idHomeSlide", SqlDbType.Int);
            paIdHomeSlide.Value = pIdHomeSlide;

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

        //
        //public static DataTable RecuperarOfertaPorId(int pIdOferta)
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("spRecuperarOfertaPorId", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;
        //    SqlParameter paIdOferta = cmdComandoInicio.Parameters.Add("@ofe_idOferta", SqlDbType.Int);
        //    paIdOferta.Value = pIdOferta;

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
        public static bool EliminarOfertaHome(int pIdOfertaHome)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("dbo.spEliminarOfertaHome", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            SqlParameter paIdOfertaHome = cmdComandoInicio.Parameters.Add("@ofh_idOfertaHome", SqlDbType.Int);
            paIdOfertaHome.Value = pIdOfertaHome;
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
        public static bool InsertarActualizarOfertaHome(int ofh_idOfertaHome, int ofh_orden, int ofh_idOferta)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("dbo.spInsertarActualizarOfertaHome", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            SqlParameter paofh_idOfertaHome = cmdComandoInicio.Parameters.Add("@ofh_idOfertaHome", SqlDbType.Int);
            SqlParameter paofh_orden = cmdComandoInicio.Parameters.Add("@ofh_orden", SqlDbType.Int);
            SqlParameter paofh_idOferta = cmdComandoInicio.Parameters.Add("@ofh_idOferta", SqlDbType.Int);
            paofh_idOfertaHome.Value = ofh_idOfertaHome;
            paofh_orden.Value = ofh_orden;
            paofh_idOferta.Value = ofh_idOferta;

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
        public static DataTable RecuperarTodasOfertaHome()
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("spRecuperarTodasOfertaHome", Conn);
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
        public static bool InsertarActualizarOfertaDetalle(int pIdOfertaDetalle, int pOfd_idOferta, string pProductoCodigo, string pProductoNombre)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("dbo.spInsertarActualizarOfertaDetalle", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            SqlParameter paIdOfertaDetalle = cmdComandoInicio.Parameters.Add("@ofd_idOfertaDetalle", SqlDbType.Int);
            SqlParameter paOfd_idOferta = cmdComandoInicio.Parameters.Add("@ofd_idOferta", SqlDbType.Int);
            SqlParameter paProductoCodigo = cmdComandoInicio.Parameters.Add("@ofd_productoCodigo", SqlDbType.NVarChar, 50);
            SqlParameter paProductoNombre = cmdComandoInicio.Parameters.Add("@ofd_productoNombre", SqlDbType.NVarChar, 75);
            paIdOfertaDetalle.Value = pIdOfertaDetalle;
            paOfd_idOferta.Value = pOfd_idOferta;
            if (pProductoCodigo == null)
                paProductoCodigo.Value = DBNull.Value;
            else
                paProductoCodigo.Value = pProductoCodigo;
            if (pProductoNombre == null)
                paProductoNombre.Value = DBNull.Value;
            else
                paProductoNombre.Value = pProductoNombre;

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
        public static bool ElimimarOfertaDetallePorId(int pIdOfertaDetalle)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("dbo.spEliminarOfertaDetalle", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            SqlParameter paIdOfertaDetalle = cmdComandoInicio.Parameters.Add("@ofd_idOfertaDetalle", SqlDbType.Int);
            paIdOfertaDetalle.Value = pIdOfertaDetalle;
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
        public static bool InsertarActualizarOferta(int pOfe_idOferta, string pOfe_titulo, string pOfe_descr, string pOfe_descuento, string pOfe_etiqueta, string pOfe_etiquetaColor, int pOfe_tipo, string ofe_nombreTransfer, DateTime? ofe_fechaFinOferta, bool ofe_nuevosLanzamiento,string ofe_descrHtml)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("dbo.spInsertarActualizarOferta", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            SqlParameter paIdOferta = cmdComandoInicio.Parameters.Add("@ofe_idOferta", SqlDbType.Int);
            SqlParameter paOfe_titulo = cmdComandoInicio.Parameters.Add("@ofe_titulo", SqlDbType.NVarChar, 500);
            SqlParameter paOfe_descr = cmdComandoInicio.Parameters.Add("@ofe_descr", SqlDbType.NVarChar, -1);
            SqlParameter paOfe_tipo = cmdComandoInicio.Parameters.Add("@ofe_tipo", SqlDbType.Int);
            SqlParameter paOfe_nombreTransfer = cmdComandoInicio.Parameters.Add("@ofe_nombreTransfer", SqlDbType.NVarChar, 75);
            SqlParameter paOfe_descuento = cmdComandoInicio.Parameters.Add("@ofe_descuento", SqlDbType.NVarChar, 50);
            SqlParameter paOfe_etiqueta = cmdComandoInicio.Parameters.Add("@ofe_etiqueta", SqlDbType.NVarChar, 50);
            SqlParameter paOfe_etiquetaColor = cmdComandoInicio.Parameters.Add("@ofe_etiquetaColor", SqlDbType.NVarChar, 10);
            SqlParameter paOfe_fechaFinOferta = cmdComandoInicio.Parameters.Add("@ofe_fechaFinOferta", SqlDbType.DateTime);
            SqlParameter paOfe_nuevosLanzamiento = cmdComandoInicio.Parameters.Add("@ofe_nuevosLanzamiento", SqlDbType.Bit);
            SqlParameter paOfe_descrHtml = cmdComandoInicio.Parameters.Add("@ofe_descrHtml", SqlDbType.NVarChar,-1);

            paIdOferta.Value = pOfe_idOferta;
            paOfe_titulo.Value = pOfe_titulo;
            paOfe_descr.Value = pOfe_descr;
            paOfe_descuento.Value = pOfe_descuento;
            paOfe_tipo.Value = pOfe_tipo;
            if (ofe_nombreTransfer == null || ofe_nombreTransfer == "-1")
                paOfe_nombreTransfer.Value = DBNull.Value;
            else
                paOfe_nombreTransfer.Value = ofe_nombreTransfer;

            //paIsPublicar.Value = pOfe_publicar;
            //paOfe_activo.Value = pOfe_activo;
            if (pOfe_etiqueta == null)
                paOfe_etiqueta.Value = DBNull.Value;
            else
                paOfe_etiqueta.Value = pOfe_etiqueta;

            if (pOfe_etiquetaColor == null)
                paOfe_etiquetaColor.Value = DBNull.Value;
            else
                paOfe_etiquetaColor.Value = pOfe_etiquetaColor;

            if (ofe_fechaFinOferta == null)
                paOfe_fechaFinOferta.Value = DBNull.Value;
            else
                paOfe_fechaFinOferta.Value = ofe_fechaFinOferta;

            if (ofe_descrHtml == null)
                paOfe_descrHtml.Value = DBNull.Value;
            else
                paOfe_descrHtml.Value = ofe_descrHtml;

            paOfe_nuevosLanzamiento.Value = ofe_nuevosLanzamiento;
            try
            {
                Conn.Open();
                cmdComandoInicio.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
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
        public static bool CambiarEstadoPublicarOferta(int pIdOferta)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("dbo.spCambiarEstadoPublicarOferta", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            SqlParameter paIdOferta = cmdComandoInicio.Parameters.Add("@ofe_idOferta", SqlDbType.Int);
            //  SqlParameter paIsPublicar = cmdComandoInicio.Parameters.Add("@ofe_publicar", SqlDbType.Bit);
            paIdOferta.Value = pIdOferta;
            //  paIsPublicar.Value = pIsPublicar;
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
        public static bool ElimimarOfertaPorId(int pIdOferta)
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("dbo.spEliminarOferta", Conn);
            cmdComandoInicio.CommandType = CommandType.StoredProcedure;
            SqlParameter paIdOferta = cmdComandoInicio.Parameters.Add("@ofe_idOferta", SqlDbType.Int);
            paIdOferta.Value = pIdOferta;
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

        //public static DataTable RecuperarTodasOfertas()
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("spRecuperarTodasOferta", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;
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
        //public static DataTable RecuperarTodasOfertaPublicar()
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("spRecuperarTodasOfertaPublicar", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;
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
        public static DataTable RecuperarTodasOfertaDetalles()
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("spRecuperarTodasOfertaDetalles", Conn);
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
    }
}