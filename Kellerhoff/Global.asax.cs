﻿using DKbase.generales;
using DKbase.web.capaDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Kellerhoff
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DKbase.Helper.getConnectionStringSQL = ConfigurationManager.ConnectionStrings["db_conexion"].ConnectionString;
            DKbase.Helper.getTipoApp = "WEB";
            DKbase.Helper.getFolder = Kellerhoff.Codigo.clases.Constantes.cRaizArchivos;// @"C:\ArchivosSitioWEB";
        }
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/config/Error");
        //}
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

           // int idUsuarioLog = 0;
           // int codigoCliente = 0;
            //string login = string.Empty;
            string Parameters = string.Empty;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                Parameters += "<" + "login" + ">";
                Parameters += ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login;
                Parameters += "</" + "login" + ">";
                Parameters += "<" + "codigoCliente" + ">";
                Parameters += ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo;
                Parameters += "</" + "codigoCliente" + ">";
           
            }
            if (System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] != null)
            {
                Parameters += "<" + "idUsuarioLog" + ">";
                Parameters += ((Codigo.capaDatos.Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).idUsuarioLog;
                Parameters += "</" + "idUsuarioLog" + ">";
            }
            // Codigo.clases.FuncionesPersonalizadas.grabarLog(System.Reflection.MethodBase.GetCurrentMethod(), exception, DateTime.Now, login, codigoCliente, idUsuarioLog);
            DKbase.generales.Log.grabarLog_generico(System.Reflection.MethodBase.GetCurrentMethod().Name, exception, DateTime.Now,  Parameters, "WEB");
            Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action = string.Empty;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "HttpError404";
                        Response.Redirect("~/home/index.aspx");
                        return;
                    //break;
                    case 500:
                        // server error
                        action = "HttpError500";
                        break;
                    default:
                        action = "General";
                        break;
                }

                // clear error on server
                Server.ClearError();

                //Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, exception.Message));
            }
            //Codigo.clases.FuncionesPersonalizadas.grabarLog(System.Reflection.MethodBase.GetCurrentMethod(), exception, DateTime.Now);
            Response.Redirect("~/config/Error");
        }
    }
}