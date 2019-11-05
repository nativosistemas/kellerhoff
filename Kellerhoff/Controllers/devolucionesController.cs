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
            return View();
        }
        public ActionResult NuevaDevolucion()
        {
            if (Session["clientesDefault_Cliente"] != null)
            {
                List<string> lista = new List<string>();
                lista = WebService.ObtenerTiposDeComprobantesAMostrar(((cClientes)Session["clientesDefault_Cliente"]).cli_login);
                Session["ConsultaDeComprobantes_tipoComprobante"] = lista;
            }
            return View();
        }
        public ActionResult NuevaDevolucionVencidos()
        {
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
    }
}