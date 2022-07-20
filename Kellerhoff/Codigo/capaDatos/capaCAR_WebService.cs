using DKbase.web;
using DKbase.web.capaDatos;
using Kellerhoff.Codigo.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Kellerhoff.Codigo.capaDatos
{
    public class capaCAR_WebService
    {
        public static List<cCarrito> RecuperarCarritosPorSucursalYProductos(int pIdCliente)
        {
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                DKbase.web.capaDatos.cClientes cliente = (DKbase.web.capaDatos.cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"];
                List<cCarrito> l = DKbase.web.capaDatos.capaCAR_WebService_base.RecuperarCarritosPorSucursalYProductos_generica(cliente, Constantes.cTipo_Carrito);
                foreach (var item in l)
                {
                    item.proximoHorarioEntrega = FuncionesPersonalizadas.getObtenerHorarioCierre(item.codSucursal);
                }
                return l;
            }
            return null;
        }
        public static List<cCarrito> RecuperarCarritosDiferidosPorCliente(int pIdCliente)
        {
            if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                DKbase.web.capaDatos.cClientes cliente = (DKbase.web.capaDatos.cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"];
                List<cCarrito> l = DKbase.web.capaDatos.capaCAR_WebService_base.RecuperarCarritosPorSucursalYProductos_generica(cliente, Constantes.cTipo_CarritoDiferido);
                foreach (var item in l)
                {
                    item.proximoHorarioEntrega = FuncionesPersonalizadas.getObtenerHorarioCierre(item.codSucursal);
                }
                return l;
            }
            return null;
        }

        public static int BorrarCarritoTransfer(int pIdCliente, string pSucursal, string pAccion)
        {
            if (WebService.VerificarPermisos(WebService.CredencialAutenticacion))
            {
                return capaCAR_base.BorrarCarrito(pIdCliente, pSucursal, Constantes.cTipo_CarritoTransfers, pAccion);
            }
            return -1;
        }
        public static int BorrarCarrito(int pIdCliente, string pSucursal, string pAccion)
        {
            if (WebService.VerificarPermisos(WebService.CredencialAutenticacion))
            {
                return capaCAR_base.BorrarCarrito(pIdCliente, pSucursal, Constantes.cTipo_Carrito, pAccion);
            }
            return -1;
        }
        public static int BorrarCarritosDiferidos(int pIdCliente, string pSucursal, string pAccion)
        {
            if (WebService.VerificarPermisos(WebService.CredencialAutenticacion))
            {
                return capaCAR_base.BorrarCarrito(pIdCliente, pSucursal, Constantes.cTipo_CarritoDiferido, pAccion);
            }
            return -1;
        }
        //public static List<cSucursalCarritoTransfer> RecuperarCarritosTransferPorIdClienteOrdenadosPorSucursal(cClientes pCliente, string pTipo)
        //{
        //    if (WebService.VerificarPermisos(WebService.CredencialAutenticacion))
        //    {

        //        DataSet dsProductoCarrito = capaCAR_base.RecuperarCarritoTransferPorIdCliente(pCliente.cli_codigo, pTipo);
        //        return acceso.convertDataSetToSucursalCarritoTransfer(pCliente, dsProductoCarrito);
        //    }
        //    // sin no valida la credencial
        //    return null;
        //}
        //public static bool ActualizarProductoCarritoSubirArchivo(List<cProductosAndCantidad> pListaValor, int pIdCliente, int pIdUsuario)
        //{
        //    string strXML = string.Empty;
        //    strXML += "<Root>";
        //    foreach (cProductosAndCantidad item in pListaValor)
        //    {
        //        List<XAttribute> listaAtributos = new List<XAttribute>();

        //        listaAtributos.Add(new XAttribute("lcp_cantidad", item.cantidad));
        //        listaAtributos.Add(new XAttribute("codigo", item.codProducto));
        //        listaAtributos.Add(new XAttribute("nombre", item.codProductoNombre));
        //        listaAtributos.Add(new XAttribute("codTransfer", item.tde_codtfr));
        //        listaAtributos.Add(new XAttribute("isTransferFacturacionDirecta", item.isTransferFacturacionDirecta));
        //        listaAtributos.Add(new XAttribute("codSucursal", item.codSucursal));
        //        XElement nodo = new XElement("DetallePedido", listaAtributos);
        //        strXML += nodo.ToString();
        //    }
        //    strXML += "</Root>";
        //    return capaCAR_base.SubirPedido(strXML, pIdCliente, pIdUsuario, Constantes.cTipo_Carrito, Constantes.cTipo_CarritoTransfers);
        //}
    }
}