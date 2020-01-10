using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Kellerhoff.Codigo.clases;

namespace Kellerhoff.Codigo.capaDatos
{
    public class capaWebServiceDLL
    {
        private static ServiceReferenceDLL.ServiceSoapClient _objServicio;
        private static ServiceReferenceDLL.ServiceSoapClient Instacia()
        {
            //if (_objServicio == null)
            //{
            _objServicio = new ServiceReferenceDLL.ServiceSoapClient();
            _objServicio.Login(System.Configuration.ConfigurationManager.AppSettings["ws_dll_usu"], System.Configuration.ConfigurationManager.AppSettings["ws_dll_psw"]);
            //}
            return _objServicio;
        }
        public capaWebServiceDLL()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static List<ServiceReferenceDLL.cFichaCtaCte> ObtenerMovimientosDeFichaCtaCte(string pLoginWeb, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                return objServicio.ObtenerMovimientosDeFichaCtaCte(pLoginWeb, pFechaDesde, pFechaHasta);
            }
            catch (Exception ex)
            {
                //FuncionesPersonalizadas.grabarLog("ObtenerMovimientosDeFichaCtaCte", ex, DateTime.Now,null);
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFechaDesde, pFechaHasta);
                return null;
            }
        }
        public static ServiceReferenceDLL.cDllPedido TomarPedido(string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<ServiceReferenceDLL.cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.ArrayOfCDllProductosAndCantidad listaArray = new ServiceReferenceDLL.ArrayOfCDllProductosAndCantidad();
                foreach (var item in pListaProducto)
                {
                    listaArray.Add(item);
                }
                ServiceReferenceDLL.cDllPedido objResultado = objServicio.TomarPedido(pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, listaArray, pIsUrgente);
                //ServiceReferenceDLL.cDllPedido objResultado = null;

                //FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), new Exception("eeee"), DateTime.Now, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);

                return objResultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);
                return null;
            }
        }
        public static ServiceReferenceDLL.cDllRespuestaResumenAbierto ObtenerResumenAbierto(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.cDllRespuestaResumenAbierto resultado = objServicio.ObtenerResumenAbierto(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static ServiceReferenceDLL.cFactura ObtenerFactura(string pNroFactura, string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.cFactura resultado = objServicio.ObtenerFactura(pNroFactura, pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNroFactura, pLoginWeb);
                return null;
            }
        }
        public static ServiceReferenceDLL.cNotaDeCredito ObtenerNotaDeCredito(string pNroNotaDeCredito, string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.cNotaDeCredito resultado = objServicio.ObtenerNotaDeCredito(pNroNotaDeCredito, pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNroNotaDeCredito, pLoginWeb);
                return null;
            }
        }
        public static ServiceReferenceDLL.cNotaDeDebito ObtenerNotaDeDebito(string pNroNotaDeDebito, string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.cNotaDeDebito resultado = objServicio.ObtenerNotaDeDebito(pNroNotaDeDebito, pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNroNotaDeDebito, pLoginWeb);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cComprobanteDiscriminado> ObtenerComprobantesEntreFecha(string pTipoComprobante, DateTime pFechaDesde, DateTime pFechaHasta, string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cComprobanteDiscriminado> resultado = objServicio.ObtenerComprobantesEntreFechas(pTipoComprobante, pFechaDesde, pFechaHasta, pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pTipoComprobante, pFechaDesde, pFechaHasta, pLoginWeb);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cCtaCteMovimiento> ObtenerMovimientosDeCuentaCorriente(bool pIsIncluyeCancelado, DateTime pFechaDesde, DateTime pFechaHasta, string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cCtaCteMovimiento> resultado = objServicio.ObtenerMovimientosDeCuentaCorriente(pIsIncluyeCancelado, pFechaDesde, pFechaHasta, pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIsIncluyeCancelado, pFechaDesde, pFechaHasta, pLoginWeb);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cDllPedido> ObtenerPedidosEntreFechas(DateTime pFechaDesde, DateTime pFechaHasta, string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cDllPedido> resultado = objServicio.ObtenerPedidosEntreFechas(pFechaDesde, pFechaHasta, pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pFechaDesde, pFechaHasta, pLoginWeb);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cDllChequeRecibido> ObtenerChequesEnCartera(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cDllChequeRecibido> resultado = objServicio.ObtenerChequesEnCartera(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static decimal? ObtenerSaldoCtaCteAFecha(string pLoginWeb, DateTime pFecha)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                decimal? resultado = objServicio.ObtenerSaldoCtaCteAFecha(pLoginWeb, pFecha);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFecha);
                return null;
            }
        }
        public static decimal? ObtenerSaldoResumenAbierto(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                decimal? resultado = objServicio.ObtenerSaldoResumenAbierto(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static decimal? ObtenerSaldoChequesEnCartera(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                decimal? resultado = objServicio.ObtenerSaldoChequesEnCartera(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static ServiceReferenceDLL.cDllSaldosComposicion ObtenerSaldosPresentacionParaComposicion(string pLoginWeb, DateTime pFecha)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.cDllSaldosComposicion resultado = objServicio.ObtenerSaldosPresentacionParaComposicion(pLoginWeb, pFecha);
                //FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), new Exception("ggg"), DateTime.Now, pLoginWeb, pFecha);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFecha);
                return null;
            }
        }
        public static ServiceReferenceDLL.cResumen ObtenerResumenCerrado(string pNroResumen, string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.cResumen resultado = objServicio.ObtenerResumenCerrado(pNroResumen, pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNroResumen, pLoginWeb);
                return null;
            }
        }
        public static string ObtenerTipoDeComprobanteAMostrar(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                string resultado = objServicio.ObtenerTipoDeComprobanteAMostrar(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static List<string> ObtenerTiposDeComprobantesAMostrar(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<string> resultado = objServicio.ObtenerTiposDeComprobantesAMostrar(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cDllPedidoTransfer> TomarPedidoTransfer(string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<ServiceReferenceDLL.cDllProductosAndCantidad> pListaProducto)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.ArrayOfCDllProductosAndCantidad listaArray = new ServiceReferenceDLL.ArrayOfCDllProductosAndCantidad();
                foreach (var item in pListaProducto)
                {
                    listaArray.Add(item);
                }
                List<ServiceReferenceDLL.cDllPedidoTransfer> listaResultado = objServicio.TomarPedidoDeTransfers(pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, listaArray);
                return listaResultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto);
                return null;
            }
        }
        public static void ModificarPasswordWEB(string pLoginWeb, string pPassActual, string pPassNueva)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                objServicio.ModificarPasswordWEB(pLoginWeb, pPassActual, pPassNueva);
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pPassActual, pPassNueva);
            }
        }
        public static List<ServiceReferenceDLL.cComprobantesDiscriminadosDePuntoDeVenta> ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(string pLoginWeb, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cComprobantesDiscriminadosDePuntoDeVenta> resultado = objServicio.ObtenerComprobantesDiscriminadosDePuntoDeVentaEntreFechas(pLoginWeb, pFechaDesde, pFechaHasta);
                //ServiceReferenceDLL.cComprobantesDiscriminadosDePuntoDeVenta ob = new ServiceReferenceDLL.cComprobantesDiscriminadosDePuntoDeVenta();
                //ob.MontoTotal
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb, pFechaDesde, pFechaHasta);
                return null;
            }
        }
        public static bool ImprimirComprobante(string pTipoComprobante, string pNroComprobante)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                objServicio.ImprimirComprobante(pTipoComprobante, pNroComprobante);
                return true;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pTipoComprobante, pNroComprobante);
                return false;
            }
        }
        public static List<ServiceReferenceDLL.cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(string pNombrePlan, string pLoginWeb, int pAnio, int pMes)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cPlanillaObSoc> resultado = objServicio.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMes(pNombrePlan, pLoginWeb, pAnio, pMes);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNombrePlan, pLoginWeb, pAnio, pMes);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena(string pNombrePlan, string pLoginWeb, int pAnio, int pMes, int pQuincena)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cPlanillaObSoc> resultado = objServicio.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioMesQuincena(pNombrePlan, pLoginWeb, pAnio, pMes, pQuincena);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNombrePlan, pLoginWeb, pAnio, pMes, pQuincena);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cPlanillaObSoc> ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(string pNombrePlan, string pLoginWeb, int pAnio, int pSemana)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cPlanillaObSoc> resultado = objServicio.ObtenerPlanillasObraSocialClientesDeObraSocialPorAnioSemana(pNombrePlan, pLoginWeb, pAnio, pSemana);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNombrePlan, pLoginWeb, pAnio, pSemana);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cPlan> ObtenerPlanesDeObrasSociales()
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cPlan> resultado = objServicio.ObtenerPlanesDeObrasSociales();
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now);
                return null;
            }
        }
        public static ServiceReferenceDLL.cObraSocialCliente ObtenerObraSocialCliente(string pNumeroObraSocialCliente, string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.cObraSocialCliente resultado = objServicio.ObtenerObraSocialCliente(pNumeroObraSocialCliente, pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroObraSocialCliente, pLoginWeb);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cResumen> ObtenerUltimos10ResumenesDePuntoDeVenta(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cResumen> resultado = objServicio.ObtenerUltimos10ResumenesDePuntoDeVenta(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cCbteParaImprimir> ObtenerComprobantesAImprimirEnBaseAResumen(string pNumeroResumen)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cCbteParaImprimir> resultado = objServicio.ObtenerComprobantesAImprimirEnBaseAResumen(pNumeroResumen);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pNumeroResumen);
                return null;
            }
        }
        public static decimal? ObtenerCreditoDisponibleSemanal(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                decimal? resultado = objServicio.ObtenerCreditoDisponibleSemanal(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static decimal? ObtenerCreditoDisponibleTotal(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                decimal? resultado = objServicio.ObtenerCreditoDisponibleTotal(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static List<ServiceReferenceDLL.cConsObraSocial> ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(string pLoginWeb, string pPlan, DateTime pFechaDesde, DateTime pFechaHasta)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                List<ServiceReferenceDLL.cConsObraSocial> resultado = objServicio.ObtenerComprobantesObrasSocialesDePuntoDeVentaEntreFechas(pLoginWeb, pPlan, pFechaDesde, pFechaHasta);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
        public static ServiceReferenceDLL.cDllPedido TomarPedidoConIdCarrito(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<ServiceReferenceDLL.cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
        {
            try
            {
                capaCAR.InicioCarritoEnProceso(pIdCarrito, Constantes.cAccionCarrito_TOMAR);
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.ArrayOfCDllProductosAndCantidad listaArray = new ServiceReferenceDLL.ArrayOfCDllProductosAndCantidad();
                foreach (var item in pListaProducto)
                {
                    listaArray.Add(item);
                }
                ServiceReferenceDLL.cDllPedido objResultado = objServicio.TomarPedidoConIdCarrito(pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, listaArray, pIsUrgente);
                return objResultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);
                return null;
            }
            finally
            {
                capaCAR.EndCarritoEnProceso(pIdCarrito);
            }
        }
        public static List<ServiceReferenceDLL.cDllPedidoTransfer> TomarPedidoDeTransfersConIdCarrito(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<ServiceReferenceDLL.cDllProductosAndCantidad> pListaProducto)
        {
            try
            {
                capaCAR.InicioCarritoEnProceso(pIdCarrito, Constantes.cAccionCarrito_TOMAR);
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                ServiceReferenceDLL.ArrayOfCDllProductosAndCantidad listaArray = new ServiceReferenceDLL.ArrayOfCDllProductosAndCantidad();
                foreach (var item in pListaProducto)
                {
                    listaArray.Add(item);
                }
                List<ServiceReferenceDLL.cDllPedidoTransfer> listaResultado = objServicio.TomarPedidoDeTransfersConIdCarrito(pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, listaArray);
                return listaResultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto);
                return null;
            }
            finally
            {
                capaCAR.EndCarritoEnProceso(pIdCarrito);
            }
        }
        public static bool ValidarExistenciaDeCarritoWebPasado(int pIdCarrito)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                return objServicio.ValidarExistenciaDeCarritoWebPasado(pIdCarrito);
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito);
                return false;
            }
        }
        public static double? ObtenerSaldoFinalADiciembrePorCliente(string pLoginWeb)
        {
            try
            {
                ServiceReferenceDLL.ServiceSoapClient objServicio = Instacia();
                double resultado = objServicio.ObtenerSaldoFinalADiciembrePorCliente(pLoginWeb);
                return resultado;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pLoginWeb);
                return null;
            }
        }
    }


}