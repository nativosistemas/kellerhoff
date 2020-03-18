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
    public class devolucionesController : Controller
    {
        // GET: devoluciones
        public ActionResult Devoluciones()
        {
            if (Session["clientesDefault_Cliente"] == null)
            {
                Response.Redirect("~/home/index.aspx");
            }
                return View();
        }
        public ActionResult NuevaDevolucion()
        {
            if (Session["clientesDefault_Cliente"] != null)
            {
                List<string> lista = new List<string>();
                lista = WebService.ObtenerTiposDeComprobantesAMostrar(((cClientes)Session["clientesDefault_Cliente"]).cli_login);
                Session["ConsultaDeComprobantes_tipoComprobante"] = lista;
            } else
            {
                Response.Redirect("~/home/index.aspx");
            }
            return View();
        }
        public ActionResult NuevaDevolucionFacturaCompleta()
        {
            if (Session["clientesDefault_Cliente"] != null)
            {
                List<string> lista = new List<string>();
                lista = WebService.ObtenerTiposDeComprobantesAMostrar(((cClientes)Session["clientesDefault_Cliente"]).cli_login);
                Session["ConsultaDeComprobantes_tipoComprobante"] = lista;
            }
            else
            {
                Response.Redirect("~/home/index.aspx");
            }
            return View();
        }
        public ActionResult NuevaDevolucionVencidos()
        {
            if (Session["clientesDefault_Cliente"] == null)
            {
                Response.Redirect("~/home/index.aspx");
            }
            return View();
        }
        public ActionResult NotaDevolucion(string nrodev, string imprimir)
        {
            if (Session["clientesDefault_Cliente"] == null)
            {
                Response.Redirect("~/home/index.aspx");
            }
            if (nrodev != null)
            {
                Session["Cliente_NumeroDevolucion"] = nrodev;
            }
            if (imprimir != null)
            {
                Session["Cliente_CartelImprimir"] = 1;
            } else
            {
                Session["Cliente_CartelImprimir"] = 0;
            }
            return View();
        }
        public string ObtenerFacturaCliente(string pNroFactura)
        {
            object resultadoObj = null;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.ObtenerFactura(pNroFactura, ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
            }
            if (resultadoObj != null)
                return Serializador.SerializarAJson(resultadoObj);
            else
                return null;
        }

        public string ObtenerNumerosLoteDeProductoDeFacturaProveedorLogLotesConCadena(string pNombreProducto, string pNumeroLote)
        {
            object resultadoObj = null;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.ObtenerNumerosLoteDeProductoDeFacturaProveedorLogLotesConCadena(pNombreProducto,pNumeroLote, ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
            }
            if (resultadoObj != null)
                return Serializador.SerializarAJson(resultadoObj);
            else
                return null;
        }

        public string RecuperarItemsDevolucionPrecargaPorCliente()
        {
            object resultadoObj = null;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.RecuperarItemsDevolucionPrecargaPorCliente(((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo);
            }
            if (resultadoObj != null)
                return Serializador.SerializarAJson(resultadoObj);
            else
                return null;
        }

        public string RecuperarItemsDevolucionVencidosPrecargaPorCliente()
        {
            object resultadoObj = null;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.RecuperarItemsDevolucionPrecargaVencidosPorCliente(((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo);
            }
            if (resultadoObj != null)
                return Serializador.SerializarAJson(resultadoObj);
            else
                return null;
        }

        public string RecuperarItemsDevolucionFacturaCompletaPrecargaPorCliente()
        {
            object resultadoObj = null;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.RecuperarItemsDevolucionPrecargaFacturaCompletaPorCliente(((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo);
            }
            if (resultadoObj != null)
                return Serializador.SerializarAJson(resultadoObj);
            else
                return null;
        }

        public bool AgregarDevolucionItemPrecarga( cDevolucionItemPrecarga Item )
        {
            bool resultadoObj = false;

            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.AgregarDevolucionItemPrecarga(Item);
            }

            return resultadoObj;
        }

        public bool EliminarDevolucionItemPrecarga(int NumeroItem)
        {
            bool resultadoObj = false;

            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.EliminarDevolucionItemPrecarga(NumeroItem);
            }

            return resultadoObj;
        }

        public bool EliminarPrecargaDevolucionPorCliente(int NumeroCliente)
        {
            bool resultadoObj = false;

            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.EliminarPrecargaDevolucionPorCliente(NumeroCliente);
            }

            return resultadoObj;
        }

        public bool EliminarPrecargaDevolucionVencidosPorCliente(int NumeroCliente)
        {
            bool resultadoObj = false;

            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.EliminarPrecargaDevolucionVencidosPorCliente(NumeroCliente);
            }

            return resultadoObj;
        }

        public bool EliminarPrecargaDevolucionFacturaCompletaPorCliente(int NumeroCliente)
        {
            bool resultadoObj = false;

            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.EliminarPrecargaDevolucionFacturaCompletaPorCliente(NumeroCliente);
            }

            return resultadoObj;
        }

        public string RecuperarDevolucionesPorCliente()
        {
            object resultadoObj = null;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.RecuperarDevolucionesPorCliente(((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
            }
            if (resultadoObj != null)
                return Serializador.SerializarAJson(resultadoObj);
            else
                return null;
        }

        public string RecuperarDevolucionesPorClientePorNumero(string NumeroDevolucion)
        {
            object resultadoObj = null;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.RecuperarDevolucionesPorClientePorNumero(NumeroDevolucion,((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
            }
            if (resultadoObj != null)
                return Serializador.SerializarAJson(resultadoObj);
            else
                return null;
        }

        public string AgregarSolicitudDevolucionCliente(List<cDevolucionItemPrecarga> Item)
        {
            string resultado = null;

            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultado = WebService.AgregarSolicitudDevolucionCliente(Item, ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
            }

            return resultado;
        }

        public long? ObtenerCantidadSolicitadaDevolucionPorProductoFacturaYCliente(string NombreProducto, string NumeroFactura)
        {
            long? resultado = null;

            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultado = WebService.ObtenerCantidadSolicitadaDevolucionPorProductoFacturaYCliente( NombreProducto,NumeroFactura, ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
            }

            return resultado;
        }

        public bool EsFacturaConDevolucionesEnProceso(string pNroFactura)
        {
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                return WebService.EsFacturaConDevolucionesEnProceso(pNroFactura, ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
            }

            return false;
        }

        public string ObtenerFacturasPorUltimosNumeros(string Cbte)
        {
            object resultadoObj = null;
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                resultadoObj = WebService.ObtenerFacturasPorUltimosNumeros(Cbte, ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
            }
            if (resultadoObj != null)
                return Serializador.SerializarAJson(resultadoObj);
            else
                return null;
        }

        public int enviarConsultaReclamos(string pMail, string pComentario, string pNombreProducto)
        {
            return WebService.enviarConsultaReclamos(pMail, pComentario,pNombreProducto);
        }

        public int enviarConsultaValePsicotropico(string pMail, string pComentario, string pNombreProducto)
        {
            return WebService.enviarConsultaValePsicotropico(pMail, pComentario,pNombreProducto);
        }

        public bool contralarSesion()
        {
            if (Session["clientesDefault_Cliente"] == null)
            {
                return false;
            }
            return true;
        }

    }

}