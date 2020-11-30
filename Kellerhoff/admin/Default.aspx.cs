using Kellerhoff.Codigo.capaDatos;
using Kellerhoff.Codigo.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kellerhoff.admin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("http://www.kellerhoff.com.ar:84/admin/Default.aspx");
            //if (!IsPostBack)
            //{
            //    txtUsuario.Focus();
            //    string isCerrarSesion = Request.QueryString["c"];
            //    if (isCerrarSesion != string.Empty)
            //    {
            //        if (Session["BaseAdmin_Usuario"] != null)
            //        {
            //            Codigo.clases.Seguridad.CerrarSession(((Codigo.capaDatos.Usuario)Session["BaseAdmin_Usuario"]).idUsuarioLog);
            //        }
            //        lblMensaje.Text = string.Empty;
            //        txtPassword.Text = string.Empty;
            //        txtUsuario.Text = string.Empty;
            //        Session["BaseAdmin_Usuario"] = null;
            //    }
            //}
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string userAgent = Request.UserAgent;
            string ip = Server.HtmlEncode(Request.UserHostAddress);
            string hostName = Request.UserHostName;
            Usuario user = Seguridad.Login(txtUsuario.Text, txtPassword.Text, ip, hostName, userAgent);
            if (user != null)
            {
                if (user.id != -1)
                {
                    if (user.usu_estado == Constantes.cESTADO_ACTIVO)
                    {
                        if (user.idRol != Constantes.cROL_ADMINISTRADORCLIENTE && user.idRol != Constantes.cROL_OPERADORCLIENTE)
                        {
                            Session["BaseAdmin_Usuario"] = user;
                            Autenticacion objAutenticacion = new Autenticacion();
                            objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                            objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                            WebService.CredencialAutenticacion = objAutenticacion;
                            cBaseAdmin.CargarAccionesEnVariableSession();
                            Response.Redirect("~/admin/pages/MenuPrincipal.aspx");
                        }
                        else
                        {
                            lblMensaje.Text = "Usuario con rol sin permiso";
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "Usuario inactivo";
                    }
                }
                else
                {
                    lblMensaje.Text = "Usuario o contraseña erróneo";
                }
            }
            else
            {
                lblMensaje.Text = "Error en el servidor";
            }
        }
    }
}