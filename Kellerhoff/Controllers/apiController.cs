using Kellerhoff.Codigo;
using Kellerhoff.Codigo.capaDatos;
using Kellerhoff.Codigo.clases;
using Kellerhoff.Codigo.clases.Generales;
using Kellerhoff.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kellerhoff.Controllers
{
    public class apiController : Controller
    {
        public string loginApi( string login, string password)
        {
            string resultado = null;
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            string ip = System.Web.HttpContext.Current.Server.HtmlEncode(System.Web.HttpContext.Current.Request.UserHostAddress);
            string hostName = System.Web.HttpContext.Current.Request.UserHostName;
            Codigo.capaDatos.Usuario user = Codigo.clases.Seguridad.Login(login, password, ip, hostName, userAgent);
            bool ok = true;
            if (user != null)
            {
                if (user.id != -1)
                {
                    if (user.usu_estado == Constantes.cESTADO_ACTIVO && user.usu_codCliente != null)
                    {
                        Autenticacion objAutenticacion = new Autenticacion();
                        objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                        objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                        WebService.CredencialAutenticacion = objAutenticacion;
                        cClientes oCliente = WebService.RecuperarClientePorId((int)user.usu_codCliente);
                        List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(user.id);
                        string apiKey = Codigo.clases.Seguridad.getApiKey((int)user.id);
                        resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
                        resultado += ",\"usuario\": " + Serializador.SerializarAJson(user);
                        resultado += ",\"cliente\": " + Serializador.SerializarAJson(oCliente);
                        resultado += ",\"permisosDenegados\": " + Serializador.SerializarAJson(listaPermisoDenegados);
                        resultado += ",\"apikey\": \"" + apiKey + "\"";
                        resultado += "}";

                        //if (user.idRol == Constantes.cROL_ADMINISTRADORCLIENTE || user.idRol == Constantes.cROL_OPERADORCLIENTE)
                        //{
                        //    Autenticacion objAutenticacion = new Autenticacion();
                        //    objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                        //    objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                        //    WebService.CredencialAutenticacion = objAutenticacion;
                        //    System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)user.usu_codCliente);

                        //    if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                        //    {
                        //        System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                        //        List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Codigo.capaDatos.Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                        //        System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                        //        System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                        //        System.Web.HttpContext.Current.Session["isMostrarOferesultado"] = false;
                        //        resultado = "Ok";
                        //    }
                        //    else
                        //    {
                        //        resultado = "Error al recuperar el cliente";
                        //    }
                        //}
                        //else
                        //{
                        //    if (user.idRol == Constantes.cROL_PROMOTOR)
                        //    {
                        //        // resultado = "Es Promotor";
                        //        //resultado = user.NombreYApellido;
                        //        Autenticacion objAutenticacion = new Autenticacion();
                        //        objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                        //        objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                        //        WebService.CredencialAutenticacion = objAutenticacion;

                        //        List<cClientes> clientes = WebService.spRecuperarTodosClientesByPromotor(user.ApNombre);

                        //        System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)clientes[0].cli_codigo);

                        //        if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                        //        {
                        //            System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                        //            List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Codigo.capaDatos.Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                        //            System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                        //            System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                        //            System.Web.HttpContext.Current.Session["isMostrarOferesultado"] = false;
                        //            resultado = "OkPromotor";
                        //        }
                        //    }
                        //    else if (user.idRol == Constantes.cROL_ENCGRAL)
                        //    {
                        //        // resultado = "Es Promotor";
                        //        //resultado = user.NombreYApellido;
                        //        Autenticacion objAutenticacion = new Autenticacion();
                        //        objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                        //        objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                        //        WebService.CredencialAutenticacion = objAutenticacion;

                        //        List<cClientes> clientes = WebService.RecuperarTodosClientes();

                        //        System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)clientes[0].cli_codigo);

                        //        if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                        //        {
                        //            System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                        //            List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Codigo.capaDatos.Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                        //            System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                        //            System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                        //            System.Web.HttpContext.Current.Session["isMostrarOferesultado"] = false;
                        //            resultado = "OkPromotor";
                        //        }
                        //    }
                        //    else if (user.idRol == Constantes.cROL_ENCSUCURSAL)
                        //    {
                        //        // resultado = "Es Promotor";
                        //        //resultado = user.NombreYApellido;
                        //        Autenticacion objAutenticacion = new Autenticacion();
                        //        objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                        //        objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                        //        WebService.CredencialAutenticacion = objAutenticacion;

                        //        IdSuc = pName.Substring(3, 2);

                        //        List<cClientes> clientes = WebService.RecuperarTodosClientesBySucursal(IdSuc);

                        //        System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)clientes[0].cli_codigo);

                        //        if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                        //        {
                        //            System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                        //            List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Codigo.capaDatos.Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                        //            System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                        //            CargarAccionesEnVariableSession();
                        //            System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                        //            System.Web.HttpContext.Current.Session["isMostrarOferesultado"] = false;
                        //            resultado = "OkPromotor";
                        //        }
                        //    }
                        //    else if (user.idRol == Constantes.cROL_GRUPOCLIENTE)
                        //    {
                        //        // resultado = "Es Promotor";
                        //        //resultado = user.NombreYApellido;
                        //        Autenticacion objAutenticacion = new Autenticacion();
                        //        objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                        //        objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                        //        WebService.CredencialAutenticacion = objAutenticacion;

                        //        GrupoCliente = pName;

                        //        List<cClientes> clientes = WebService.RecuperarTodosClientesByGrupoCliente(GrupoCliente);

                        //        System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)clientes[0].cli_codigo);

                        //        if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                        //        {
                        //            System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                        //            List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Codigo.capaDatos.Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                        //            System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                        //            CargarAccionesEnVariableSession();
                        //            System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                        //            System.Web.HttpContext.Current.Session["isMostrarOferesultado"] = false;
                        //            resultado = "OkPromotor";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        resultado = "Usuario con rol sin permiso.";
                        //    }
                        //}
                    }
                    else
                    {
                        if (user.usu_codCliente == null)
                        {
                            ok = false;
                            resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
                            resultado += ",\"mensaje\": \"Usuario sin cliente asociado\"";
                            resultado += "}";
                            return resultado;
                        }
                        else
                        {
                            resultado = "Usuario inactivo";
                        }
                    }
                }
                else
                {
                    resultado = "Mail o contraseña erróneo";
                }
            }
            else
            {
                resultado = "Error en el servidor";
            }


            return resultado;
        }
        public string validarUsuario(string apikey)
        {
            string resultado = Codigo.clases.Seguridad.validarUsuarioByApikey(apikey);
            return resultado;
        }

        public string ObtenerSaldoFinalADiciembrePorCliente(string apikey)
        {
            bool ok = true;
            string resultado = null;
            string idUser = validarUsuario(apikey);
            if ( idUser == null )
            {
                ok = false;
                resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
                resultado += ",\"mensaje\": \"Token invalido\"";
                resultado += "}";
                return resultado;
            }

            Autenticacion objAutenticacion = new Autenticacion();
            objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
            objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
            WebService.CredencialAutenticacion = objAutenticacion;
            cUsuario oUser = Seguridad.RecuperarUsuarioPorId(Int32.Parse(idUser));
            double? SaldoDic = capaWebServiceDLL.ObtenerSaldoFinalADiciembrePorCliente(oUser.usu_login);
            resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
            //resultado += ",\"usuario\": " + Serializador.SerializarAJson(oUser);
            resultado += ",\"SaldoDiciembre\": " + Serializador.SerializarAJson(SaldoDic);
            resultado += "}";
            return resultado;
        }

        public string DeudaVencidaApi(string apikey)
        {
            bool ok = true;
            string resultado = null;
            string idUser = validarUsuario(apikey);
            if (idUser == null)
            {
                ok = false;
                resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
                resultado += ",\"mensaje\": \"Token invalido\"";
                resultado += "}";
                return resultado;
            }
            Autenticacion objAutenticacion = new Autenticacion();
            objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
            objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
            WebService.CredencialAutenticacion = objAutenticacion;
            cUsuario oUser = Seguridad.RecuperarUsuarioPorId(Int32.Parse(idUser));
            int pDia = 7;
            int pPendiente = 1;
            int pCancelado = 0;
            DateTime fechaDesde = DateTime.Now.AddDays(pDia * -1);
            DateTime fechaHasta = DateTime.Now;
            List<ServiceReferenceDLL.cCtaCteMovimiento> l = ctacteController.AgregarVariableSessionComposicionSaldo(fechaDesde, fechaHasta, pPendiente, pCancelado, oUser.usu_login);
            resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
            resultado += ",\"DeudaVencida\": " + Serializador.SerializarAJson(l);
            resultado += "}";
            return resultado;
        }

        public string ObtenerComprobantesDiscriminadosEntreFechasApi(string desde, string hasta, string apikey)
        {
            bool ok = true;
            string resultado = null;
            string idUser = validarUsuario(apikey);
            if (idUser == null)
            {
                ok = false;
                resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
                resultado += ",\"mensaje\": \"Token invalido\"";
                resultado += "}";
                return resultado;
            }
            Autenticacion objAutenticacion = new Autenticacion();
            objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
            objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
            WebService.CredencialAutenticacion = objAutenticacion;
            cUsuario oUser = Seguridad.RecuperarUsuarioPorId(Int32.Parse(idUser));

            DateTime fechaDesde = Convert.ToDateTime(desde);//, 0, 0, 0
            DateTime fechaHasta = Convert.ToDateTime(hasta + " 23:59:59");//, 23, 59, 59
            List<ServiceReferenceDLL.cComprobantesDiscriminadosDePuntoDeVenta> resultadoObj = WebService.ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(oUser.usu_login, fechaDesde, fechaHasta);
            resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
            resultado += ",\"ComprobantesDiscriminados\": " + Serializador.SerializarAJson(resultadoObj);
            resultado += "}";

            return resultado;
        }
        public string ComposicionSaldoApi(int pDia, int pPendiente, int pCancelado, string apikey)
        {
            bool ok = true;
            string resultado = null;
            string idUser = validarUsuario(apikey);
            if (idUser == null)
            {
                ok = false;
                resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
                resultado += ",\"mensaje\": \"Token invalido\"";
                resultado += "}";
                return resultado;
            }
            Autenticacion objAutenticacion = new Autenticacion();
            objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
            objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
            WebService.CredencialAutenticacion = objAutenticacion;
            cUsuario oUser = Seguridad.RecuperarUsuarioPorId(Int32.Parse(idUser));
            DateTime fechaDesde = DateTime.Now.AddDays(pDia * -1);
            DateTime fechaHasta = DateTime.Now;
            List<ServiceReferenceDLL.cCtaCteMovimiento> l = ctacteController.AgregarVariableSessionComposicionSaldo(fechaDesde, fechaHasta, pPendiente, pCancelado, oUser.usu_login);
            resultado += "{\"ok\": " + Serializador.SerializarAJson(ok);
            resultado += ",\"ComposicionSaldo\": " + Serializador.SerializarAJson(l);
            resultado += "}";
            return resultado;
        }

    }

}