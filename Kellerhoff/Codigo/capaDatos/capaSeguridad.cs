using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Kellerhoff.Codigo.clases;

namespace Kellerhoff.Codigo.capaDatos
{
    //public class cRegla : DKbase.web.cRegla
    //{
    //    public cRegla()
    //    {

    //    }

    //}
    //public class cRol : DKbase.web.cRol
    //{

    //}
    //public class AcccionesRol : DKbase.web.AcccionesRol
    //{


    //}
 
    [DataContract]
    public class ListaCheck
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string palabra { get; set; }
        [DataMember]
        public int checkAgregar { get; set; }
        [DataMember]
        public int checkEditar { get; set; }
        [DataMember]
        public int checkEliminar { get; set; }
        [DataMember]
        public int? idPadreRegla { get; set; }
        [DataMember]
        public List<int> listaIdPadre { get; set; }
        [DataMember]
        public List<int> listaIdHijas { get; set; }
        [DataMember]
        public int Nivel { get; set; }
        [DataMember]
        public bool isGraficada { get; set; }
    }
    [DataContract]
    public class ReglaAGrabar
    {
        public int idRelacionReglaRol { get; set; }
        public int idRegla { get; set; }
        public bool isActivo { get; set; }
        public bool? isAgregado { get; set; }
        public bool? isEditado { get; set; }
        public bool? isEliminado { get; set; }
    }

    public class capaSeguridad
    {
        //public static DataSet Login(string pNombreLogin, string pPassword, string pIp, string pHostName, string pUserAgent)
        //{

        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spInicioSession", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paLogin = cmdComandoInicio.Parameters.Add("@login", SqlDbType.NVarChar, 255);
        //    SqlParameter paPassword = cmdComandoInicio.Parameters.Add("@Password", SqlDbType.NVarChar, 255);
        //    SqlParameter paIp = cmdComandoInicio.Parameters.Add("@Ip", SqlDbType.NVarChar, 255);
        //    SqlParameter paHost = cmdComandoInicio.Parameters.Add("@Host", SqlDbType.NVarChar, 255);
        //    SqlParameter paUserName = cmdComandoInicio.Parameters.Add("@UserName", SqlDbType.NVarChar, 255);

        //    paLogin.Value = pNombreLogin;
        //    paPassword.Value = pPassword;
        //    paIp.Value = pIp;
        //    paHost.Value = pHostName;
        //    paUserName.Value = pUserAgent;

        //    try
        //    {
        //        Conn.Open();
        //        DataSet dsResultado = new DataSet();
        //        SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
        //        da.Fill(dsResultado, "Login");
        //        return dsResultado;
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
        //public static void CerrarSession(int pIdUsuarioLog)
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spCerrarSession", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paIdUsuarioLog = cmdComandoInicio.Parameters.Add("@IdUsuarioLog", SqlDbType.Int);
        //    paIdUsuarioLog.Value = pIdUsuarioLog;

        //    try
        //    {
        //        Conn.Open();
        //        int count = cmdComandoInicio.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        if (Conn.State == ConnectionState.Open)
        //        {
        //            Conn.Close();
        //        }
        //    }

        //}
        //public static DataSet GestiónRol(int? rol_codRol, string rol_Nombre, string filtro, string accion)
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spGestionRol", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paRol_codRol = cmdComandoInicio.Parameters.Add("@rol_codRol", SqlDbType.Int);
        //    SqlParameter paRol_Nombre = cmdComandoInicio.Parameters.Add("@rol_Nombre", SqlDbType.NVarChar, 80);
        //    SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
        //    SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

        //    if (rol_codRol == null)
        //    {
        //        paRol_codRol.Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        paRol_codRol.Value = rol_codRol;
        //    }
        //    if (rol_Nombre == null)
        //    {
        //        paRol_Nombre.Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        paRol_Nombre.Value = rol_Nombre;
        //    }
        //    if (filtro == null)
        //    {
        //        paFiltro.Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        paFiltro.Value = filtro;
        //    }
        //    paAccion.Value = accion;

        //    try
        //    {
        //        Conn.Open();
        //        DataSet dsResultado = new DataSet();
        //        SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
        //        da.Fill(dsResultado, "Rol");
        //        return dsResultado;
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
        public static DataTable RecuperarTodasReglasPorNivel()
        {
            SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
            SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spRecuperarTodasReglasPorNiveles", Conn);
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
        //public static DataTable RecuperarTodasAccionesRol(int pIdRol)
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spRecuperarAccionesUsuario", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paIdRol = cmdComandoInicio.Parameters.Add("@IdRol", SqlDbType.Int);
        //    paIdRol.Direction = ParameterDirection.Input;

        //    paIdRol.Value = pIdRol;

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
        //public static DataSet GestiónRoleRegla(int? rrr_codRol, int? rrr_codRegla, string pXML, string accion)
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spGestionRelacionRoleRegla", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paCodRol = cmdComandoInicio.Parameters.Add("@rrr_codRol", SqlDbType.Int);
        //    SqlParameter paCodRegla = cmdComandoInicio.Parameters.Add("@rrr_codRegla", SqlDbType.Int);
        //    SqlParameter paStrXML = cmdComandoInicio.Parameters.Add("@strXML", SqlDbType.Xml);
        //    //SqlParameter paFiltro = cmdComandoInicio.Parameters.Add("@filtro", SqlDbType.NVarChar, 50);
        //    SqlParameter paAccion = cmdComandoInicio.Parameters.Add("@accion", SqlDbType.NVarChar, 50);

        //    if (rrr_codRol == null)
        //    {
        //        paCodRol.Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        paCodRol.Value = rrr_codRol;
        //    }
        //    if (rrr_codRegla == null)
        //    {
        //        paCodRegla.Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        paCodRegla.Value = rrr_codRegla;
        //    }
        //    if (pXML == null)
        //    {
        //        paStrXML.Value = DBNull.Value;
        //    }
        //    else
        //    {
        //        paStrXML.Value = pXML;
        //    }
        //    //if (filtro == null)
        //    //{
        //    //    paFiltro.Value = DBNull.Value;
        //    //}
        //    //else
        //    //{
        //    //    paFiltro.Value = filtro;
        //    //}
        //    paAccion.Value = accion;

        //    try
        //    {
        //        Conn.Open();
        //        DataSet dsResultado = new DataSet();
        //        SqlDataAdapter da = new SqlDataAdapter(cmdComandoInicio);
        //        da.Fill(dsResultado, "RelacionRoleRegla");
        //        return dsResultado;
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
   
        public static string obtenerStringEstado(int pIdEstado)
        {
            return DKbase.web.capaDatos.capaSeguridad_base.obtenerStringEstado(pIdEstado);
        }
   

      

        //public static DataTable RecuperarSinPermisoUsuarioIntranetPorIdUsuario(int pIdUsuario)
        //{
        //    SqlConnection Conn = new SqlConnection(accesoBD.ObtenerConexión());
        //    SqlCommand cmdComandoInicio = new SqlCommand("Seguridad.spRecuperarSinPermisoUsuarioIntranetPorIdUsuario", Conn);
        //    cmdComandoInicio.CommandType = CommandType.StoredProcedure;

        //    SqlParameter paIdUsuario = cmdComandoInicio.Parameters.Add("@idUsuario", SqlDbType.Int);
        //    paIdUsuario.Direction = ParameterDirection.Input;

        //    paIdUsuario.Value = pIdUsuario;

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
    
    }

}