using DKbase.web;
using DKbase.web.capaDatos;
using Kellerhoff.Codigo.capaDatos;
using Kellerhoff.Codigo.clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Kellerhoff.Codigo.capaDatos
{
    public class capaCAR_decision
    {
        public static bool isCAR = true;

        public static bool isDiferido_CAR()
        {
            if (isCAR)
            {
                return true;
            }
            else
            {
                return !Kellerhoff.Controllers.mvcController.isDiferido();
            }
        }

        public static int BorrarCarrito(int lrc_id, string lrc_codSucursal)
        {
            if (isCAR)
            {
                return capaCAR_WebService.BorrarCarrito((int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente, lrc_codSucursal, Constantes.cAccionCarrito_VACIAR);
            }
            else
            {
                if (lrc_id == -1)
                {
                    if (System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] != null)
                    {
                        List<cCarrito> listaCarrito = WebService.RecuperarCarritosPorSucursalYProductos((int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente);
                        foreach (cCarrito item in listaCarrito)
                        {
                            if (item.codSucursal == lrc_codSucursal)
                            {
                                lrc_id = item.lrc_id;
                                break;
                            }
                        }
                    }
                }
                return WebService.BorrarCarrito(lrc_id) ? 0 : -1;
            }
        }
        public static int BorrarCarritosDiferidos(int lrc_id, string lrc_codSucursal)
        {
            if (isCAR)
            {
                return capaCAR_WebService.BorrarCarritosDiferidos((int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente, lrc_codSucursal, Constantes.cAccionCarrito_VACIAR);
            }
            else
            {
                if (lrc_id == -1)
                {
                    if (System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] != null)
                    {
                        List<cCarrito> listaCarrito = WebService.RecuperarCarritosDiferidosPorCliente((int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente);
                        foreach (cCarrito item in listaCarrito)
                        {
                            if (item.codSucursal == lrc_codSucursal)
                            {
                                lrc_id = item.lrc_id;
                                break;
                            }
                        }
                    }
                }
                WebService.BorrarCarritosDiferidos(lrc_id);
                return 0;
            }
        }
        public static int BorrarCarritoTransfer(string pSucursal)
        {
            if (isCAR)
            {
                return capaCAR_WebService.BorrarCarritoTransfer((int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente, pSucursal, Constantes.cAccionCarrito_VACIAR);
            }
            else
            {
                bool Resultado = false;

                if (System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
                {
                    Resultado = WebService.BorrarCarritoTransfer(((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo, pSucursal);
                }

                return Resultado ? 0 : -1;
            }
        }
        //public static void GuardarPedidoBorrarCarrito(List<cProductosGenerico> pListaProductos, int car_id, string pSucursal, string pTipo, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, bool pIsUrgente)
        //{
        //    if (isCAR)
        //    {
        //        //int idCarrito = capaCAR.BorrarCarrito((int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente, pSucursal, pTipo, Constantes.cAccionCarrito_TOMAR);
        //        capaCAR_base.BorrarCarritoPorId_SleepTimer(car_id, Constantes.cAccionCarrito_TOMAR);
        //        capaCAR_base.guardarPedido(pListaProductos, car_id, pSucursal, pTipo, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pIsUrgente);
        //    }
        //    else
        //    {
        //        WebService.BorrarCarritoTransfer(((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codigo, pSucursal);
        //    }
        //}
        //public static void GuardarPedidoBorrarCarrito(cCarrito pCarrito, string pTipo, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, bool pIsUrgente)
        //{
        //    if (isCAR)
        //    {
        //        // capaCAR.BorrarCarrito((int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente, pCarrito.codSucursal, pTipo, Constantes.cAccionCarrito_TOMAR);
        //        capaCAR_base.BorrarCarritoPorId_SleepTimer(pCarrito.car_id, Constantes.cAccionCarrito_TOMAR);
        //        capaCAR_base.guardarPedido(pCarrito, pTipo, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pIsUrgente);
        //    }
        //    else if (pTipo == Constantes.cTipo_Carrito)
        //    {
        //        capaAuditoria.guardarCarrito(pCarrito.lrc_id);
        //        capaLogRegistro.GuardarHistorialIdCarrito(pCarrito.lrc_id);
        //        capaAuditoria.guardarPedido(pCarrito, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pIsUrgente);
        //    }
        //    else if (pTipo == Constantes.cTipo_CarritoDiferido)
        //    {
        //        WebService.BorrarCarritosDiferidos(pCarrito.lrc_id);
        //    }
        //}
        public static cSucursalCarritoTransfer RecuperarCarritosTransferPorIdClienteIdSucursal(cClientes pCliente, string pCodSucursal, string pTipo)
        {
            if (isCAR)
            {
                return RecuperarCarritosTransferPorCliente_generico(pCliente, pTipo, pCodSucursal);
            }
            else
            {
                return WebService.RecuperarCarritosTransferPorIdClienteIdSucursal(pCliente, pCodSucursal);
            }
        }
        public static bool AgregarProductosTransfersAlCarrito(List<cProductosAndCantidad> pListaProductosMasCantidad, int pIdCliente, int pIdUsuario, int pIdTransfers, string pCodSucursal, string pTipo)
        {
            if (isCAR)
            {
                if (WebService.VerificarPermisos(WebService.CredencialAutenticacion))
                {
                    DataTable pTablaDetalle = DKbase.web.FuncionesPersonalizadas_base.ConvertProductosAndCantidadToDataTable(pListaProductosMasCantidad);
                    return capaCAR_base.AgregarProductosTransfersAlCarrito(pTablaDetalle, pIdCliente, pIdUsuario, pIdTransfers, pCodSucursal, pTipo);
                }
                return false;
            }
            else
            {
                return WebService.AgregarProductosTransfersAlCarrito(pListaProductosMasCantidad, pIdCliente, pIdUsuario, pIdTransfers, pCodSucursal);
            }
        }
        //public static cCarrito RecuperarCarritoPorIdClienteIdSucursal(int pIdCliente, string pIdSucursal)
        //{
        //    if (isCAR)
        //    {
        //        return capaCAR_WebService.RecuperarCarritoPorIdClienteIdSucursal(pIdCliente, pIdSucursal);
        //    }
        //    else
        //    {
        //        return WebService.RecuperarCarritoPorIdClienteIdSucursal(pIdCliente, pIdSucursal);
        //    }
        //}
        public static bool CargarCarritoDiferido(string pSucursal, string pIdProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
        {
            if (isCAR)
            {
                return capaCAR_base.CargarCarritoDiferido(pSucursal, pIdProducto, pCantidadProducto, pIdCliente, pIdUsuario);
            }
            else
            {
                return WebService.CargarCarritoDiferido(pSucursal, pIdProducto, pCantidadProducto, pIdCliente, pIdUsuario);
            }
        }
        public static bool AgregarProductoAlCarrito(string pSucursal, string pIdProducto, int pCantidadProducto, int pIdCliente, int? pIdUsuario)
        {
            if (isCAR)
            {
                return capaCAR_base.AgregarProductoAlCarrito(pSucursal, pIdProducto, pCantidadProducto, pIdCliente, pIdUsuario);
            }
            else
            {
                return WebService.AgregarProductoAlCarrito(pSucursal, pIdProducto, pCantidadProducto, pIdCliente, pIdUsuario);
            }
        }
        public static List<cCarrito> RecuperarCarritosDiferidosPorCliente(int pIdCliente)
        {
            return RecuperarCarritosPorSucursalYProductos_generica(Constantes.cTipo_CarritoDiferido);
            //if (isCAR)
            //{
            //    return capaCAR_WebService.RecuperarCarritosDiferidosPorCliente(pIdCliente);
            //}
            //else
            //{
            //    return WebService.RecuperarCarritosDiferidosPorCliente(pIdCliente);
            //}
        }
        public static List<cCarrito> RecuperarCarritosPorSucursalYProductos_generica(string pTipo)
        {
            DKbase.web.capaDatos.cClientes cliente = (DKbase.web.capaDatos.cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"];
            List<cCarrito> l = DKbase.web.capaDatos.capaCAR_WebService_base.RecuperarCarritosPorSucursalYProductos_generica(cliente, pTipo);
            return l;
        }
        public static List<cCarrito> RecuperarCarritosPorSucursalYProductos(int pIdCliente)
        {
            return RecuperarCarritosPorSucursalYProductos_generica(Constantes.cTipo_Carrito);
            //if (isCAR)
            //{
            //    return capaCAR_WebService.RecuperarCarritosPorSucursalYProductos(pIdCliente);
            //}
            //else
            //{
            //    return WebService.RecuperarCarritosPorSucursalYProductos(pIdCliente);
            //}
        }
        public static List<cSucursalCarritoTransfer> RecuperarCarritosTransferPorIdClienteOrdenadosPorSucursal(cClientes pCliente, string pTipo)
        {
            if (isCAR)
            {
                return RecuperarCarritosTransfer_generico(pCliente, pTipo);// capaCAR_WebService_base.RecuperarCarritosTransferPorIdClienteOrdenadosPorSucursal(pCliente, pTipo);
            }
            else
            {
                return WebService.RecuperarCarritosTransferPorIdClienteOrdenadosPorSucursal(pCliente);
            }
        }
        public static List<cSucursalCarritoTransfer> RecuperarCarritosTransfer_generico(cClientes pCliente, string pTipo)
        {
            List<cSucursalCarritoTransfer> result = null;
            result = capaCAR_WebService_base.RecuperarCarritosTransferPorIdClienteOrdenadosPorSucursal(pCliente, pTipo);
            if (result != null)
            {
                foreach (var item in result)
                {
                    item.proximoHorarioEntrega = FuncionesPersonalizadas.getObtenerHorarioCierre(item.Sucursal);
                }
            }
            return result;
        }
        public static cSucursalCarritoTransfer RecuperarCarritosTransferPorCliente_generico(cClientes pCliente, string pTipo, string pIdSucursal)
        {
            cSucursalCarritoTransfer o = RecuperarCarritosTransfer_generico(pCliente, pTipo).Where(x => x.Sucursal == pIdSucursal).FirstOrDefault();
            return o;
        }
        public static List<cCarritoTransfer> RecuperarCarritosTransferPorIdCliente(cClientes pCliente, string pTipo, string pIdSucursal)
        {
            if (isCAR)
            {
                cSucursalCarritoTransfer o = RecuperarCarritosTransferPorCliente_generico(pCliente, pTipo, pIdSucursal);
                return o == null ? null : o.listaTransfer;
            }
            else
            {
                return WebService.RecuperarCarritosTransferPorIdCliente(pCliente);
            }
        }
        public static bool ActualizarProductoCarritoSubirArchivo(List<cProductosAndCantidad> pListaValor)
        {
            if (isCAR)
            {
                return capaCAR_WebService_base.ActualizarProductoCarritoSubirArchivo(pListaValor, (int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente, ((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["clientesDefault_Usuario"] != null && pListaValor != null)
                {
                    foreach (cProductosAndCantidad item in pListaValor)
                    {
                        if (item.isTransferFacturacionDirecta)
                        {
                            List<cProductosAndCantidad> listaTransfer = new List<cProductosAndCantidad>();
                            listaTransfer.Add(item);
                            WebService.AgregarProductosTransfersAlCarrito_TempSubirArchivo(listaTransfer, (int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente, ((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id, item.tde_codtfr, item.codSucursal);
                        }
                        else
                        {
                            WebService.AgregarProductoAlCarrito_TempSubirArchivo(item.codSucursal, item.codProducto, item.cantidad, (int)((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).usu_codCliente, ((Usuario)System.Web.HttpContext.Current.Session["clientesDefault_Usuario"]).id);
                        }
                    }
                }
                return true;
            }
        }

    }

}
