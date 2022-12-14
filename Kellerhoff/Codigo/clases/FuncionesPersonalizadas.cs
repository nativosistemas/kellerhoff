﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Kellerhoff.Codigo.capaDatos;
using System.Reflection;
using DKbase.web.capaDatos;
using DKbase.web;

namespace Kellerhoff.Codigo.clases
{
    public class FuncionesPersonalizadas
    {
        public static List<cProductosGenerico> cargarProductosBuscadorArchivos(DataTable tablaProductos, DataTable tablaSucursalStocks, List<cTransferDetalle> listaTransferDetalle, DKbase.generales.Constantes.CargarProductosBuscador pCargarProductosBuscador, string pSucursalElejida)
        {
            cClientes oClientes = null;
            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                oClientes = (cClientes)HttpContext.Current.Session["clientesDefault_Cliente"];
            }
            return DKbase.web.acceso.cargarProductosBuscadorArchivos(oClientes, tablaProductos, tablaSucursalStocks, listaTransferDetalle, pCargarProductosBuscador, pSucursalElejida);
        }
        public static void grabarLog(MethodBase method, Exception pException, DateTime pFechaActual, params object[] values)
        {
            DKbase.generales.Log.LogError(method, pException, pFechaActual, values);
        }
        public static List<cClientes> RecuperarTodosClientesActivosYSinClientes()
        {
            List<cClientes> resultado = new List<cClientes>();
            resultado.Add(new cClientes(0, "<< Sin clientes >>"));
            resultado.AddRange(WebService.RecuperarTodosClientes());
            return resultado;
        }
        public static List<cClientes> RecuperarTodosClientesParaCombo(bool pIsTodos)
        {
            List<cClientes> resultado = new List<cClientes>();
            //if (pIsTodos)
            //{
            //    resultado.Add(new cClientes(0, "<< Todos >>"));
            //}
            resultado.Add(new cClientes(0, "<< Todos >>"));
            resultado.AddRange(WebService.RecuperarTodosClientes());
            return resultado;
        }

        public static List<string> RecuperarPalabrasYaBuscadaSinRepetir(int? pIdUsuario, string pNombreTabla)
        {
            var listaPalabras = WebService.RecuperarTodasPalabrasYaBuscada(pIdUsuario, pNombreTabla);
            var listaStrPalabrasSinRepetir = (from ob in listaPalabras
                                              select ob.hbp_Palabra.ToUpper()).Distinct().ToList();
            return listaStrPalabrasSinRepetir;
        }
        public static decimal ObtenerPrecioFinalTransfer(cClientes pClientes, cTransfer pTransfer, cTransferDetalle pTransferDetalle)
        {
            return DKbase.web.FuncionesPersonalizadas_base.ObtenerPrecioFinalTransferBase(pClientes, pTransfer.tfr_deshab, pTransfer.tfr_pordesadi, pTransferDetalle.pro_neto, pTransferDetalle.pro_codtpopro, pTransferDetalle.pro_descuentoweb, (decimal)pTransferDetalle.tde_predescuento, pTransferDetalle.tde_PrecioConDescuentoDirecto, pTransferDetalle.tde_PorcARestarDelDtoDeCliente);
        }

        public static DataTable ConvertNombresSeccionToDataTable(List<string> pListaNombreSeccion)
        {
            return DKbase.web.FuncionesPersonalizadas_base.ConvertNombresSeccionToDataTable(pListaNombreSeccion);
        }

        //public static DataRow ConvertProductosCarritoArchivosPedidosToDataRow(DataTable pTabla, string pCodProducto, int pCantidad, string pCodigoBarra, string pCodigoAlfaBeta, string pTroquel, string pTipoArchivoPedidos)
        //{
        //    DataRow fila = pTabla.NewRow();
        //    if (pTipoArchivoPedidos == "F")
        //    {
        //        fila["codProducto"] = pCodProducto;
        //        fila["cantidad"] = pCantidad;
        //    }
        //    else if (pTipoArchivoPedidos == "S")
        //    {
        //        fila["codigobarra"] = pCodigoBarra;
        //        fila["codigoalfabeta"] = pCodigoAlfaBeta;
        //        fila["troquel"] = pTroquel;//.Trim();
        //        fila["cantidad"] = pCantidad;
        //    }
        //    fila["nroordenamiento"] = pTabla.Rows.Count + 1;
        //    return fila;
        //}

        public static void CargarMensajeActualizado(int pIdCliente)
        {
            //bool isAgregar = true;
            //if (HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] != null)
            //{
            //    if (((DateTime)HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"]) < DateTime.Now.AddMinutes(-5))
            //    {
            //        isAgregar = false;
            //    }
            //}
            //if (isAgregar)
            //{
            List<cMensaje> listaMensaje = WebService.RecuperarTodosMensajesPorIdCliente(pIdCliente);
            if (listaMensaje != null)
            {
                HttpContext.Current.Session["clientesDefault_CantListaMensaje"] = listaMensaje.Where(x => (x.tme_estado == Convert.ToInt32(Constantes.cESTADO_SINLEER) && !x.tme_importante)).ToList().Count;
                HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] = DateTime.Now;
            }
            //}
        }
        public static void CargarRecuperadorFaltaActualizado(int pIdCliente)
        {
            bool isAgregar = true;
            if (HttpContext.Current.Session["clientesDefault_CantRecuperadorFaltaFechaHora"] != null)
            {
                if (((DateTime)HttpContext.Current.Session["clientesDefault_CantRecuperadorFaltaFechaHora"]) < DateTime.Now.AddMinutes(-5))
                {
                    isAgregar = false;
                }
            }
            if (isAgregar && System.Web.HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes oCliente = (cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"];
                List<cFaltantesConProblemasCrediticiosPadre> listaRecuperador = DKbase.Util.RecuperarFaltasProblemasCrediticios(oCliente, 1, 14, ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc); ;
                if (listaRecuperador != null)
                {
                    HttpContext.Current.Session["clientesDefault_CantRecuperadorFalta"] = listaRecuperador.Count;
                    HttpContext.Current.Session["clientesDefault_CantRecuperadorFaltaFechaHora"] = DateTime.Now;
                }
            }
        }
        public static cjSonBuscadorProductos RecuperarProductosGeneral_OfertaTransfer(bool pIsOrfeta, bool pIsTransfer)
        {
            cjSonBuscadorProductos resultado = null;
            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes oClientes = (cClientes)HttpContext.Current.Session["clientesDefault_Cliente"];
                List<string> l_Sucursales = RecuperarSucursalesDelCliente();
                resultado = DKbase.web.FuncionesPersonalizadas_base.RecuperarProductosGeneral_OfertaTransfer(l_Sucursales, oClientes, pIsOrfeta, pIsTransfer);
            }
            return resultado;
        }
        public static List<string> RecuperarSucursalesDelCliente()
        {
            List<string> result = null;
            if (HttpContext.Current.Session["sucursalesDelCliente"] != null)
            {
                result = (List<string>)HttpContext.Current.Session["sucursalesDelCliente"];
            }
            else if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes oClientes = (cClientes)HttpContext.Current.Session["clientesDefault_Cliente"];
                result = DKbase.web.FuncionesPersonalizadas_base.RecuperarSucursalesParaBuscadorDeCliente(oClientes);
                HttpContext.Current.Session["sucursalesDelCliente"] = result;
            }
            return result;
        }
        public static List<cProductosGenerico> ActualizarStockListaProductos_SubirArchico(List<string> pListaSucursal, List<cProductosGenerico> pListaProductos, string pSucursalElegida)
        {
            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes oClientes = (cClientes)HttpContext.Current.Session["clientesDefault_Cliente"];
                return DKbase.web.FuncionesPersonalizadas_base.ActualizarStockListaProductos_SubirArchico(oClientes, pListaSucursal, pListaProductos, pSucursalElegida);
            }
            return pListaProductos;
        }
        public static List<cSucursalStocks> ActualizarStockListaProductos_Transfer(string pro_codigo, List<cSucursalStocks> pSucursalStocks)
        {
            cClientes oClientes = (cClientes)HttpContext.Current.Session["clientesDefault_Cliente"];
            List<cSucursalStocks> result = pSucursalStocks;
            bool isActualizar = false;
            if (oClientes.cli_codrep == "S7")
                isActualizar = true;
            //else if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc == "CD" && ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codprov != "ENTRE RIOS")// cli_codsuc == "CD" concordia y de la provincia de entre rios        
            //    isActualizar = true;
            else if (oClientes.cli_IdSucursalAlternativa != null)
                isActualizar = true;
            if (isActualizar)
            {
                List<cProductosAndCantidad> listaProductos = new List<cProductosAndCantidad>();

                listaProductos.Add(new cProductosAndCantidad { codProductoNombre = pro_codigo });
                List<string> ListaSucursal = RecuperarSucursalesDelCliente();
                DataTable table = capaProductos.RecuperarStockPorProductosAndSucursal(ConvertNombresSeccionToDataTable(ListaSucursal), DKbase.web.FuncionesPersonalizadas_base.ConvertProductosAndCantidadToDataTable(listaProductos));
                if (table != null)
                    result = (from r in table.Select("stk_codpro = '" + pro_codigo + "'").AsEnumerable()
                              select new cSucursalStocks { stk_codpro = r["stk_codpro"].ToString(), stk_codsuc = r["stk_codsuc"].ToString(), stk_stock = r["stk_stock"].ToString() }).ToList();

            }
            return result;
        }

        //
        //


        public static cjSonBuscadorProductos RecuperarProductosGeneralSubirPedidos(List<cProductosGenerico> pListaProveedor)
        {
            cjSonBuscadorProductos resultado = null;
            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes oClientes = (cClientes)HttpContext.Current.Session["clientesDefault_Cliente"];
                List<cProductosGenerico> listaProductosBuscador = pListaProveedor;
                List<string> ListaSucursal = RecuperarSucursalesDelCliente();
                string SucursalEleginda = HttpContext.Current.Session["subirpedido_SucursalEleginda"].ToString();
                resultado = DKbase.Util.RecuperarProductosGeneralSubirPedidos(oClientes, SucursalEleginda, ListaSucursal, pListaProveedor);
            }
            return resultado;
        }

        public static cjSonBuscadorProductos RecuperarProductosGeneral_V3(int? pIdOferta, string pTxtBuscador, List<string> pListaColumna, bool pIsOrfeta, bool pIsTransfer)
        {
            cjSonBuscadorProductos resultado = null;
            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes oClientes = (cClientes)HttpContext.Current.Session["clientesDefault_Cliente"];
                List<string> l_Sucursales = RecuperarSucursalesDelCliente();
                resultado = DKbase.web.FuncionesPersonalizadas_base.RecuperarProductosGeneral_V3(l_Sucursales, oClientes, pIdOferta, pTxtBuscador, pListaColumna, pIsOrfeta, pIsTransfer);
            }
            return resultado;
        }
        public static cjSonBuscadorProductos RecuperarProductosBase_V3(int? pIdOferta, string pTxtBuscador, List<string> pListaColumna, bool pIsBuscarConOferta, bool pIsBuscarConTransfer)
        {
            cjSonBuscadorProductos resultado = null;
            if (!string.IsNullOrEmpty(pTxtBuscador) || pIdOferta != null)
            {
                if (!string.IsNullOrEmpty(pTxtBuscador) && pTxtBuscador.Trim() != string.Empty && HttpContext.Current.Session["clientesDefault_Usuario"] != null)
                    HttpContext.Current.Session["clientesPages_Buscador"] = DKbase.Util.InsertarPalabraBuscada(pTxtBuscador.ToUpper(), ((Usuario)HttpContext.Current.Session["clientesDefault_Usuario"]).id, Constantes.cTABLA_PRODUCTO);

                resultado = FuncionesPersonalizadas.RecuperarProductosGeneral_V3(pIdOferta, pTxtBuscador, pListaColumna, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaOfertas, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_tomaTransfers);
                if (pIsBuscarConOferta || pIsBuscarConTransfer)
                {
                    if (resultado != null)
                    {
                        if (pIsBuscarConOferta && pIsBuscarConTransfer)
                        {
                            resultado.listaProductos = resultado.listaProductos.Where(x => x.pro_ofeporcentaje > 0 || x.isTieneTransfer || x.isProductoFacturacionDirecta).ToList();
                        }
                        else
                        {
                            if (pIsBuscarConOferta)
                            {
                                resultado.listaProductos = resultado.listaProductos.Where(x => x.pro_ofeporcentaje > 0).ToList();
                            }
                            else if (pIsBuscarConTransfer)
                            {
                                resultado.listaProductos = resultado.listaProductos.Where(x => x.isTieneTransfer || x.isProductoFacturacionDirecta).ToList();
                            }
                        }
                    }
                }

            }
            return resultado;
        }
        //public static cjSonBuscadorProductos RecuperarProductosBase(string pTxtBuscador, List<string> pListaColumna, bool pIsBuscarConOferta, bool pIsBuscarConTransfer)
        //{
        //    cjSonBuscadorProductos resultado = null;
        //    if (!string.IsNullOrEmpty(pTxtBuscador))
        //    {
        //        if (pTxtBuscador.Trim() != string.Empty)
        //        {
        //            if (HttpContext.Current.Session["clientesDefault_Usuario"] != null)
        //            {
        //                HttpContext.Current.Session["clientesPages_Buscador"] = WebService.InsertarPalabraBuscada(pTxtBuscador.ToUpper(), ((capaDatos.Usuario)HttpContext.Current.Session["clientesDefault_Usuario"]).id, Constantes.cTABLA_PRODUCTO);
        //            }
        //            resultado = FuncionesPersonalizadas.RecuperarProductosGeneral(pTxtBuscador, pListaColumna, false, false);
        //            if (pIsBuscarConOferta || pIsBuscarConTransfer)
        //            {
        //                if (resultado != null)
        //                {
        //                    if (pIsBuscarConOferta && pIsBuscarConTransfer)
        //                    {
        //                        resultado.listaProductos = resultado.listaProductos.Where(x => x.pro_ofeporcentaje > 0 || x.isTieneTransfer).ToList();
        //                    }
        //                    else
        //                    {
        //                        if (pIsBuscarConOferta)
        //                        {
        //                            resultado.listaProductos = resultado.listaProductos.Where(x => x.pro_ofeporcentaje > 0).ToList();
        //                        }
        //                        else if (pIsBuscarConTransfer)
        //                        {
        //                            resultado.listaProductos = resultado.listaProductos.Where(x => x.isTieneTransfer).ToList();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return resultado;
        //}
        public static string ObtenerHorarioCierreAnterior(string pSucursal, string pSucursalDependiente, string pCodigoReparto, string pHorarioCierre)
        {//ObtenerHorarioCierreAnterior
            string result = string.Empty;
            cClientes oCliente = ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]);
            if (HttpContext.Current.Session["horario_siguiente" + pSucursal] == null)
            {
                HttpContext.Current.Session["horario_siguiente" + pSucursal] = DKbase.web.FuncionesPersonalizadas_base.ObtenerHorarioCierreAnterior(oCliente, pSucursalDependiente, pHorarioCierre);
            }
            if (HttpContext.Current.Session["horario_siguiente" + pSucursal] != null)
            {
                result = HttpContext.Current.Session["horario_siguiente" + pSucursal].ToString();
                DateTime? fechaHorarioCierre = FuncionesPersonalizadas_base.getFecha_Horario(getObtenerHorarioCierre(pSucursal));
                DateTime? fechaGuarda = FuncionesPersonalizadas_base.getFecha_Horario(result);
                if (fechaHorarioCierre != null && fechaGuarda != null && fechaGuarda.Value < fechaHorarioCierre.Value)
                {
                    HttpContext.Current.Session["horario_siguiente" + pSucursal] = DKbase.web.FuncionesPersonalizadas_base.ObtenerHorarioCierreAnterior(oCliente, pSucursalDependiente, pHorarioCierre);
                    result = HttpContext.Current.Session["horario_siguiente" + pSucursal].ToString();
                }

                //

            }
            return result;
        }
        public static string getObtenerHorarioCierre(string pSucursal)
        {
            string result = string.Empty;
            cClientes oCliente = ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]);
            if (HttpContext.Current.Session["horario_" + pSucursal] == null)
            {

                HttpContext.Current.Session["horario_" + pSucursal] = DKbase.web.FuncionesPersonalizadas_base.ObtenerHorarioCierre(oCliente, oCliente.cli_codsuc, pSucursal, oCliente.cli_codrep);

            }
            if (HttpContext.Current.Session["horario_" + pSucursal] != null)
            {
                result = HttpContext.Current.Session["horario_" + pSucursal].ToString();
                //
                DateTime? fechaGuarda = FuncionesPersonalizadas_base.getFecha_Horario(result);
                if (fechaGuarda != null && fechaGuarda.Value < DateTime.Now)
                {
                    HttpContext.Current.Session["horario_" + pSucursal] = DKbase.web.FuncionesPersonalizadas_base.ObtenerHorarioCierre(oCliente, oCliente.cli_codsuc, pSucursal, oCliente.cli_codrep);
                    result = HttpContext.Current.Session["horario_" + pSucursal].ToString();
                }

                //

            }
            return result;
        }
        public static void clearHorarioCierre()
        {
            if (HttpContext.Current.Session["sucursalesDelCliente"] != null)
            {
                List<string> l = (List<string>)HttpContext.Current.Session["sucursalesDelCliente"];
                foreach (var itemSucursal in l)
                {
                    HttpContext.Current.Session["horario_" + itemSucursal] = null;
                    HttpContext.Current.Session["horario_siguiente" + itemSucursal] = null;
                }
            }
        }
        public static string ObtenerHorarioCierre(string pSucursal, string pSucursalDependiente, string pCodigoReparto)
        {
            return getObtenerHorarioCierre(pSucursalDependiente);
        }
        //public static string ObtenerHorarioCierreAnterior(string pSucursal, string pSucursalDependiente, string pCodigoReparto, string pHorarioCierre)
        //{
        //    cClientes oCliente = ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]);
        //    return DKbase.web.FuncionesPersonalizadas_base.ObtenerHorarioCierreAnterior(oCliente, pSucursalDependiente, pHorarioCierre);
        //}
        public static void GenerarCSV()
        {
            List<string> lista = new List<string>();
            //nombreArchivoTXT = "Kell" + fechaArchivoTXT + "-" + pFactura + ".txt";
            //resultado = nombreArchivoTXT;
            System.IO.StreamWriter FAC_txt = new System.IO.StreamWriter("pRuta + nombreArchivoTXT", false, System.Text.Encoding.UTF8);
            // encabezado
            string encabezado = string.Empty;
            //Fecha;Comprobante;Numero Comprobante;Monto Exento;Monto Gravado;Monto Iva Inscipto;Monto Iva No Inscripto;Monto Percepciones DGR;Monto Total
            encabezado += "Fecha";
            encabezado += ";";
            encabezado += "Comprobante";
            encabezado += ";";
            encabezado += "Numero Comprobante";
            encabezado += ";";
            encabezado += "Monto Exento";
            encabezado += ";";
            encabezado += "Monto Gravado";
            encabezado += ";";
            encabezado += "Monto Iva Inscipto";
            encabezado += ";";
            encabezado += "Monto Iva No Inscripto";
            encabezado += ";";
            encabezado += "Monto Percepciones DGR";
            encabezado += ";";
            encabezado += "Monto Total";

            FAC_txt.WriteLine(encabezado);
            // fin encabezado


            foreach (string item in lista)
            {
                string fila = string.Empty;
                fila += DateTime.Now.ToString();
                fila += ";";
                fila += item;
                fila += ";";
                fila += item;
                FAC_txt.WriteLine(fila);
            }
            FAC_txt.Close();
        }
        public static List<string> RecuperarSinPermisosSecciones(int pIdUsuario)
        {
            return DKbase.Util.RecuperarSinPermisosSecciones(pIdUsuario);
        }
        public static bool IsPermisoSeccion(string pNombreSeccion)
        {
            // falta analizar la variable de session: "master_ListaSinPermisoSecciones"
            //
            bool resultado = true;
            if (HttpContext.Current.Session["master_ListaSinPermisoSecciones"] != null)
            {
                List<string> listaSinPermiso = (List<string>)HttpContext.Current.Session["master_ListaSinPermisoSecciones"];
                foreach (string item in listaSinPermiso)
                {
                    if (item == pNombreSeccion)
                    {
                        resultado = false;
                        break;
                    }
                }
            }
            return resultado;
        }
        public static string getHrefRevista()
        {
            string resultado = "href=\"" + "#" + "\""; ;
            if (HttpContext.Current.Session["href_Revista"] != null)
            {
                resultado = (string)HttpContext.Current.Session["href_Revista"];
            }
            return resultado;
        }
        public static List<cTipoEnvioClienteFront> RecuperarTiposDeEnvios()
        {
            List<cTipoEnvioClienteFront> resultado = null;
            if (HttpContext.Current.Session["RecuperarTiposDeEnvios"] != null)
            {
                resultado = (List<cTipoEnvioClienteFront>)HttpContext.Current.Session["RecuperarTiposDeEnvios"];
            }
            else if (HttpContext.Current.Session["clientesDefault_Cliente"] != null)
            {
                cClientes objCliente = ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]);
                resultado = DKbase.Util.RecuperarTiposDeEnvios(objCliente);
                HttpContext.Current.Session["RecuperarTiposDeEnvios"] = resultado;
            }
            return resultado;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////

        public static string obtenerNombreMes(int pValue)
        {
            string result = string.Empty;
            switch (pValue)
            {
                case 1:
                    result = "Enero";
                    break;
                case 2:
                    result = "Febrero";
                    break;
                case 3:
                    result = "Marzo";
                    break;
                case 4:
                    result = "Abril";
                    break;
                case 5:
                    result = "Mayo";
                    break;
                case 6:
                    result = "Junio";
                    break;
                case 7:
                    result = "Julio";
                    break;
                case 8:
                    result = "Agosto";
                    break;
                case 9:
                    result = "Septiembre";
                    break;
                case 10:
                    result = "Octubre";
                    break;
                case 11:
                    result = "Noviembre";
                    break;
                case 12:
                    result = "Diciembre";
                    break;
                default:
                    break;
            }
            return result;
        }
    }

}