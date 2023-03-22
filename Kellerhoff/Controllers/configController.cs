using DKbase.web;
using DKbase.web.capaDatos;
using Kellerhoff.Codigo.capaDatos;
using Kellerhoff.Codigo.clases;
using Kellerhoff.Codigo.clases.Generales;
using Kellerhoff.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kellerhoff.Controllers
{
    public class configController : Controller
    {
        public ActionResult loginbot()
        {
            return View();
        }
        public ActionResult action(int id)
        {


            System.Web.HttpContext.Current.Session["action_id"] = id;
            if (Session["clientesDefault_Cliente"] == null)
            {
                return RedirectToAction("loginbot");//?id=" + id
            }
            else
            {

                switch (id)
                {
                    case 1:
                        return RedirectToAction("descargaResumenes", "ctacte");//Descargas de Resumen	https://www.kellerhoff.com.ar/ctacte/descargaResumenes
                    case 2:
                        return RedirectToAction("mediosdepago1", "config");//Formas De Pago	https://www.kellerhoff.com.ar/config/mediosdepago1
                    case 3:
                        return RedirectToAction("ConsultaDeComprobantes", "ctacte");//Consulta de Comprobantes	https://www.kellerhoff.com.ar/ctacte/ConsultaDeComprobantes
                    case 4:
                        return RedirectToAction("ConsultaDeComprobantesObraSocial", "ctacte");//Consulta NC Obras Sociales	https://www.kellerhoff.com.ar/ctacte/ConsultaDeComprobantesObraSocial
                    case 5:
                        return RedirectToAction("composicionsaldo", "ctacte");//Envíos Remesas de Pago
                    case 6:
                        return RedirectToAction("NuevaDevolucion", "devoluciones");// Devolución por Reclamo https://www.kellerhoff.com.ar/devoluciones/NuevaDevolucion
                    case 7:
                        return RedirectToAction("ReclamoFacturadoNoEnviado", "devoluciones");//Facturado No Enviado	https://www.kellerhoff.com.ar/devoluciones/DevolucionesFacturadoNoEnviado
                    default:
                        break;
                }
            }
            return View();
        }
        public string RecuperarOferta(int pId)
        {
            int? id = pId;
            List<cOferta> resultado = new List<cOferta>();
            cOferta o = WebService.RecuperarTodasOfertas_generico().FirstOrDefault(x => x.ofe_idOferta == id);
            if (o != null)
                resultado.Add(o);
            foreach (var item in resultado)
            {
                List<cArchivo> listaArchivo = WebService.RecuperarTodosArchivos(item.ofe_idOferta, "ofertas", string.Empty);
                if (listaArchivo != null)
                {
                    if (listaArchivo.Count > 0)
                    {
                        item.nameImagen = listaArchivo[0].arc_nombre;
                    }
                }
            }
            return resultado == null ? string.Empty : Serializador.SerializarAJson(resultado);
        }
        public string loginCarrito(string pName, string pPass, int pIdOferta)
        {
            string resultado = null;
            resultado = login(pName, pPass);
            if (resultado == "Ok" && Session["clientesDefault_Cliente"] != null)
            {
                cClientes oCliente = (cClientes)Session["clientesDefault_Cliente"];
                WebService.InsertarOfertaRating(pIdOferta, oCliente.cli_codigo, true);
                cOferta o = WebService.RecuperarOfertaPorId(pIdOferta);
                if (o != null)
                {
                    Session["home_Tipo"] = o.ofe_tipo;
                    if (o.tfr_codigo != null)
                        Session["home_IdTransfer"] = o.tfr_codigo;
                }
                Session["home_IdOferta"] = pIdOferta;
            }
            return resultado;
        }
        public ActionResult SignOff()
        {
            FuncionesPersonalizadas.clearHorarioCierre();
            System.Web.HttpContext.Current.Session["main_ListaOferta"] = null;
            System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = null;
            System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = null;
            System.Web.HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] = null;
            System.Web.HttpContext.Current.Session["isMostrarOferta"] = null;
            System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = null;
            System.Web.HttpContext.Current.Session["BaseAdmin_PermisosRol"] = null;
            System.Web.HttpContext.Current.Session["sucursalesDelCliente"] = null;
            System.Web.HttpContext.Current.Session["todasSucursalesDependientes"] = null;
            System.Web.HttpContext.Current.Session["todasSucursales"] = null;
            System.Web.HttpContext.Current.Session["TodosSucursalDependienteTipoEnvioCliente"] = null;
            System.Web.HttpContext.Current.Session["RecuperarTiposDeEnvios"] = null;
            System.Web.HttpContext.Current.Session["RecuperarTodosCadeteriaRestricciones"] = null;

            return Content("Ok");
        }
        //public ActionResult loginTest(string pName, string pPass)
        //{
        //    return Content(login( pName,  pPass));
        //}
        public string logJS(List<string> pList)
        {

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
                Parameters += ((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).idUsuarioLog;
                Parameters += "</" + "idUsuarioLog" + ">";
            }
            string funFlecha = string.Empty;
            if (pList != null)
                funFlecha = string.Join(" | ", pList);
            bool isNotGeneroError = capaLogRegistro.spError("JavaScript", Parameters, funFlecha, null, null, null, null, null, DateTime.Now, "JAVASCRIPT");
            return "Ok";
        }
        public string login(string pName, string pPass)
        {
            string IdSuc = "CC";
            string GrupoCliente = "";
            string resultado = null;
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            string ip = System.Web.HttpContext.Current.Server.HtmlEncode(System.Web.HttpContext.Current.Request.UserHostAddress);
            string hostName = System.Web.HttpContext.Current.Request.UserHostName;
            Usuario user = Codigo.clases.Seguridad.Login(pName, pPass, ip, hostName, userAgent);
            if (user != null)
            {
                if (user.id != -1)
                {
                    if (user.usu_estado == Constantes.cESTADO_ACTIVO && user.usu_codCliente != null)
                    {
                        if (user.idRol == Constantes.cROL_ADMINISTRADORCLIENTE || user.idRol == Constantes.cROL_OPERADORCLIENTE)
                        {
                            Autenticacion objAutenticacion = new Autenticacion();
                            objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                            objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                            WebService.CredencialAutenticacion = objAutenticacion;
                            System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)user.usu_codCliente);

                            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                            {
                                cClientes oCliente = (cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"];
                                FuncionesPersonalizadas.CargarMensajeActualizado(oCliente.cli_codigo);
                                System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                                List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                                System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                                CargarAccionesEnVariableSession();
                                System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                                System.Web.HttpContext.Current.Session["isMostrarOferta"] = false;
                                resultado = "Ok";
                            }
                            else
                            {
                                resultado = "Error al recuperar el cliente";
                            }
                        }
                        else
                        {
                            if (user.idRol == Constantes.cROL_PROMOTOR)
                            {
                                // resultado = "Es Promotor";
                                //resultado = user.NombreYApellido;
                                Autenticacion objAutenticacion = new Autenticacion();
                                objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                                objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                                WebService.CredencialAutenticacion = objAutenticacion;

                                List<cClientes> clientes = WebService.spRecuperarTodosClientesByPromotor(user.ApNombre);

                                System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)clientes[0].cli_codigo);

                                if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                                {
                                    System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                                    List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                                    System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                                    CargarAccionesEnVariableSession();
                                    System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                                    System.Web.HttpContext.Current.Session["isMostrarOferta"] = false;
                                    resultado = "OkPromotor";
                                }
                            }
                            else if (user.idRol == Constantes.cROL_ENCGRAL)
                            {
                                // resultado = "Es Promotor";
                                //resultado = user.NombreYApellido;
                                Autenticacion objAutenticacion = new Autenticacion();
                                objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                                objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                                WebService.CredencialAutenticacion = objAutenticacion;

                                List<cClientes> clientes = WebService.RecuperarTodosClientes();

                                System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)clientes[0].cli_codigo);

                                if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                                {
                                    System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                                    List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                                    System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                                    CargarAccionesEnVariableSession();
                                    System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                                    System.Web.HttpContext.Current.Session["isMostrarOferta"] = false;
                                    resultado = "OkPromotor";
                                }
                            }
                            else if (user.idRol == Constantes.cROL_ENCSUCURSAL)
                            {
                                // resultado = "Es Promotor";
                                //resultado = user.NombreYApellido;
                                Autenticacion objAutenticacion = new Autenticacion();
                                objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                                objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                                WebService.CredencialAutenticacion = objAutenticacion;

                                IdSuc = pName.Substring(3, 2);

                                List<cClientes> clientes = WebService.RecuperarTodosClientesBySucursal(IdSuc);

                                System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)clientes[0].cli_codigo);

                                if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                                {
                                    System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                                    List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                                    System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                                    CargarAccionesEnVariableSession();
                                    System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                                    System.Web.HttpContext.Current.Session["isMostrarOferta"] = false;
                                    resultado = "OkPromotor";
                                }
                            }
                            else if (user.idRol == Constantes.cROL_GRUPOCLIENTE)
                            {
                                // resultado = "Es Promotor";
                                //resultado = user.NombreYApellido;
                                Autenticacion objAutenticacion = new Autenticacion();
                                objAutenticacion.UsuarioNombre = System.Configuration.ConfigurationManager.AppSettings["ws_usu"];
                                objAutenticacion.UsuarioClave = System.Configuration.ConfigurationManager.AppSettings["ws_psw"];
                                WebService.CredencialAutenticacion = objAutenticacion;

                                GrupoCliente = pName;

                                List<cClientes> clientes = WebService.RecuperarTodosClientesByGrupoCliente(GrupoCliente);

                                System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] = WebService.RecuperarClientePorId((int)clientes[0].cli_codigo);

                                if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                                {
                                    System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] = user;
                                    List<string> listaPermisoDenegados = FuncionesPersonalizadas.RecuperarSinPermisosSecciones(((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                                    System.Web.HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = listaPermisoDenegados;
                                    CargarAccionesEnVariableSession();
                                    System.Web.HttpContext.Current.Session["ClientesBase_isLogeo"] = true;
                                    System.Web.HttpContext.Current.Session["isMostrarOferta"] = false;
                                    resultado = "OkPromotor";
                                }
                            }
                            else
                            {
                                resultado = "Usuario con rol sin permiso.";
                            }
                        }
                    }
                    else
                    {
                        if (user.usu_codCliente == null)
                        {
                            resultado = "Usuario no asigando cliente";
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
        public static void CargarAccionesEnVariableSession()
        {
            if (System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] != null)
            {
                ListaAcccionesRol listaAcciones = Codigo.clases.Seguridad.RecuperarTodasAccionesPorIdRol((((Usuario)(System.Web.HttpContext.Current.Session["clientesDefault_Usuario"])).idRol));
                System.Web.HttpContext.Current.Session["BaseAdmin_PermisosRol"] = listaAcciones;
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult inhabilitado()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult sinpermiso()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult sinusodll()
        {
            return View();
        }
        public static int ObtenerCantidadMensaje()
        {
            int resultado = 0;
            //if ((System.Web.HttpContext.Current.Session["clientesDefault_CantListaMensaje"] == null ||
            //    System.Web.HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] == null)
            //    && System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            //    Codigo.clases.FuncionesPersonalizadas.CargarMensajeActualizado(((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo);
            if (System.Web.HttpContext.Current.Session["clientesDefault_CantListaMensaje"] != null)
                resultado = Convert.ToInt32(System.Web.HttpContext.Current.Session["clientesDefault_CantListaMensaje"]);
            return resultado;
        }
        // GET: config
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult mensajes()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public bool CambiarEstadoMensaje(int pIdMensaje, int pIdEstado)
        {
            WebService.CambiarEstadoMensajePorId(pIdMensaje, pIdEstado);
            System.Web.HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] = null;
            return true;
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult perfil()
        {
            ViewBag.ContraseniaNueva = null;
            ViewBag.ContraseniaVieja = null;
            ViewBag.ContraseniaNuevaRepetir = null;
            if (System.Web.HttpContext.Current.Session["perfil_CambiarContraseña"] != null &&
                Convert.ToInt32(System.Web.HttpContext.Current.Session["perfil_CambiarContraseña"]) == 0 &&
                System.Web.HttpContext.Current.Session["perfil_idContraseniaVieja"] != null &&
                System.Web.HttpContext.Current.Session["perfil_idContraseniaNueva"] != null)
            {
                ViewBag.ContraseniaVieja = System.Web.HttpContext.Current.Session["perfil_idContraseniaVieja"].ToString();
                ViewBag.ContraseniaNueva = System.Web.HttpContext.Current.Session["perfil_idContraseniaNueva"].ToString();
                ViewBag.ContraseniaNuevaRepetir = System.Web.HttpContext.Current.Session["perfil_idContraseniaNueva"].ToString();
            }
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador", isCheckOPERADORCLIENTE = true)]
        public ActionResult usuarios()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        [HttpPost]
        public ActionResult ActionCambiarContrasenia(string idContraseniaVieja, string idContraseniaNueva)
        {
            System.Web.HttpContext.Current.Session["perfil_idContraseniaVieja"] = idContraseniaVieja;
            System.Web.HttpContext.Current.Session["perfil_idContraseniaNueva"] = idContraseniaNueva;
            System.Web.HttpContext.Current.Session["perfil_CambiarContraseña"] = CambiarContraseñaPersonal(idContraseniaVieja, idContraseniaNueva);
            return RedirectToAction("perfil");
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult mediosdepago1()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult mediosdepago2()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult mediosdepago3()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult mediosdepago4()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public ActionResult mediosdepago5()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "DESCARGAS")]
        public ActionResult catalogo()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "DESCARGAS")]
        public ActionResult descarga()
        {
            return View();
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public int CambiarContraseñaPersonal(string pContraseñaVieja, string pContraseñaNueva)
        {
            int id = -1;

            if (System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] != null && System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes cliente = (cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"];
                Usuario usu = (Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"];
                cUsuario objUsuario = null;
                objUsuario = DKbase.Util.RecuperarUsuarioPorId(usu.id);
                if (pContraseñaVieja == objUsuario.usu_pswDesencriptado)
                {
                    id = capaSeguridad_base.CambiarContraseñaPersonal(usu.id, pContraseñaVieja, pContraseñaNueva);
                    if (usu.idRol == Constantes.cROL_ADMINISTRADORCLIENTE)
                    {
                        WebService.ModificarPasswordWEB(cliente.cli_login, objUsuario.usu_pswDesencriptado, pContraseñaNueva);
                    }
                }
                else { id = 0; }
            }
            return id;
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public bool IsBanderaUsarDll()
        {
            bool resultado = capaCAR_WebService_base.IsBanderaCodigo(DKbase.generales.Constantes.cBAN_SERVIDORDLL);
            return resultado;
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public bool IsBanderaUsarDllSucursal(string pSucursal)
        {
            bool resultado = capaCAR_WebService_base.IsBanderaCodigo(pSucursal);
            return resultado;
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public int IsHacerPedidos(string pSucursal)
        {
            int resultado = 0;
            if (!IsBanderaUsarDllSucursal(pSucursal))
            {
                resultado = 1;
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                {
                    cClientes objCliente = WebService.RecuperarClientePorId(((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo);
                    if (objCliente != null)
                    {
                        ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_estado = objCliente.cli_estado;
                    }
                    if (((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_estado.ToUpper() == Constantes.cESTADO_INH)
                    {
                        resultado = 2;
                    }
                }
            }
            return resultado;
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public int GuardarUsuario(int pIdUsuario, string pNombre, string pApellido, string pMail, string pLogin, string pContraseña, string pObservaciones1, List<string> pListaPermisos)
        {
            if (Session["clientesDefault_Cliente"] == null)
                return -1;
            return DKbase.Util.GuardarUsuario((cClientes)Session["clientesDefault_Cliente"], pIdUsuario, pNombre, pApellido, pMail, pLogin, pContraseña, pObservaciones1, pListaPermisos);
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public int CambiarEstadoUsuario(int pIdUsuario)
        {
            if (Session["clientesDefault_Usuario"] == null)
                return -1;
            return DKbase.Util.CambiarEstadoUsuario((Usuario)Session["clientesDefault_Usuario"], pIdUsuario);
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public int EliminarUsuario(int pIdUsuario)
        {
            DKbase.Util.EliminarUsuario(pIdUsuario);
            return 0;
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public int CambiarContraseñaUsuario(int pIdUsuario, string pPass)
        {
            if (Session["clientesDefault_Usuario"] == null)
                return -1;
            return DKbase.Util.CambiarContraseñaUsuario((Usuario)Session["clientesDefault_Usuario"], pIdUsuario, pPass);
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public string ObtenerUsuarios()
        {
            if (Session["clientesDefault_Cliente"] == null)
                return null;
            List<DKbase.web.cUsuario> lista = DKbase.web.AccesoGrilla_base.GetUsuariosDeCliente("usu_codigo", ((DKbase.web.capaDatos.cClientes)Session["clientesDefault_Cliente"]).cli_codigo, null);
            return Codigo.clases.Generales.Serializador.SerializarAJson(lista);
        }
        [AuthorizePermisoAttribute(Permiso = "mvc_Buscador")]
        public FileContentResult GenerateDocument(int id)
        {
            if (Session["clientesDefault_Cliente"] == null)
                return null;
            cClientes oCliente = (cClientes)Session["clientesDefault_Cliente"];
            var nameFile = DKbase.Util.GenerateDocument_getNameFile(id);
            var fileStream = DKbase.Util.GenerateDocument(id, oCliente);
            String f = DKbase.Util.GenerateDocument_getPathFile(id);
            return File(fileStream, MimeMapping.GetMimeMapping(f), nameFile);
        }
    }
}