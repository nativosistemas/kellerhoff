using System;
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
        public static string LimpiarStringErrorPedido(string pValor)
        {
            string resultado = pValor;
            resultado = resultado.Replace("-", string.Empty);
            resultado = resultado.Replace("0", string.Empty);
            resultado = resultado.Replace("1", string.Empty);
            resultado = resultado.Replace("2", string.Empty);
            resultado = resultado.Replace("3", string.Empty);
            resultado = resultado.Replace("4", string.Empty);
            resultado = resultado.Replace("5", string.Empty);
            resultado = resultado.Replace("6", string.Empty);
            resultado = resultado.Replace("7", string.Empty);
            resultado = resultado.Replace("8", string.Empty);
            resultado = resultado.Replace("9", string.Empty);
            return resultado;
        }
        public static DataTable ConvertNombresSeccionToDataTable(List<string> pListaNombreSeccion)
        {
            return DKbase.web.FuncionesPersonalizadas_base.ConvertNombresSeccionToDataTable(pListaNombreSeccion);
        }

        public static DataRow ConvertProductosCarritoArchivosPedidosToDataRow(DataTable pTabla, string pCodProducto, int pCantidad, string pCodigoBarra, string pCodigoAlfaBeta, string pTroquel, string pTipoArchivoPedidos)
        {
            DataRow fila = pTabla.NewRow();
            if (pTipoArchivoPedidos == "F")
            {
                fila["codProducto"] = pCodProducto;
                fila["cantidad"] = pCantidad;
            }
            else if (pTipoArchivoPedidos == "S")
            {
                fila["codigobarra"] = pCodigoBarra;
                fila["codigoalfabeta"] = pCodigoAlfaBeta;
                fila["troquel"] = pTroquel;//.Trim();
                fila["cantidad"] = pCantidad;
            }
            fila["nroordenamiento"] = pTabla.Rows.Count + 1;
            return fila;
        }
        public static ServiceReferenceDLL.cDllProductosAndCantidad ProductosEnCarrito_ToConvert_DllProductosAndCantidad(cProductosGenerico pValor)
        {
            ServiceReferenceDLL.cDllProductosAndCantidad resultado = new ServiceReferenceDLL.cDllProductosAndCantidad();
            resultado.cantidad = pValor.cantidad;
            resultado.codProductoNombre = pValor.pro_nombre;
            resultado.isOferta = (pValor.pro_ofeunidades == 0 && pValor.pro_ofeporcentaje == 0) ? false : true;
            return resultado;
        }
        public static void CargarMensajeActualizado(int pIdCliente)
        {
            bool isAgregar = true;
            if (HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] != null)
            {
                if (((DateTime)HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"]) < DateTime.Now.AddMinutes(-5))
                {
                    isAgregar = false;
                }
            }
            if (isAgregar)
            {
                List<cMensaje> listaMensaje = WebService.RecuperarTodosMensajesPorIdCliente(pIdCliente);
                if (listaMensaje != null)
                {
                    //List<cMensaje> lista = ((List<cMensaje>)(Session["clientesDefault_ListaMensaje"])).Where(x => x.tme_estado == Convert.ToInt32(Constantes.cESTADO_SINLEER)).ToList();
                    HttpContext.Current.Session["clientesDefault_CantListaMensaje"] = listaMensaje.Where(x => (x.tme_estado == Convert.ToInt32(Constantes.cESTADO_SINLEER) && !x.tme_importante)).ToList().Count;
                    HttpContext.Current.Session["clientesDefault_CantListaMensajeFechaHora"] = DateTime.Now;
                }
            }
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
                List<cFaltantesConProblemasCrediticiosPadre> listaRecuperador = WebService.RecuperarFaltasProblemasCrediticios(pIdCliente, 1, 14, ((cClientes)System.Web.HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codsuc); ;
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
                // fin Si el cliente no toma perfumeria
                //for (int iPrecioFinal = 0; iPrecioFinal < listaProductosBuscador.Count; iPrecioFinal++)
                //{
                //    listaProductosBuscador[iPrecioFinal].PrecioFinal = DKbase.web.FuncionesPersonalizadas_base.ObtenerPrecioFinal(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]), listaProductosBuscador[iPrecioFinal]);
                //    listaProductosBuscador[iPrecioFinal].PrecioConDescuentoOferta = DKbase.web.FuncionesPersonalizadas_base.ObtenerPrecioUnitarioConDescuentoOferta(listaProductosBuscador[iPrecioFinal].PrecioFinal, listaProductosBuscador[iPrecioFinal]);
                //}
                // Inicio 17/02/2016
                List<string> ListaSucursal = RecuperarSucursalesDelCliente();// DKbase.web.FuncionesPersonalizadas_base.RecuperarSucursalesParaBuscadorDeCliente(oClientes);
                listaProductosBuscador = ActualizarStockListaProductos_SubirArchico(ListaSucursal, listaProductosBuscador, HttpContext.Current.Session["subirpedido_SucursalEleginda"].ToString());
                // Fin 17/02/2016
                cjSonBuscadorProductos ResultadoObj = new cjSonBuscadorProductos();
                ResultadoObj.listaSucursal = ListaSucursal;
                ResultadoObj.listaProductos = listaProductosBuscador;
                resultado = ResultadoObj;
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
                    HttpContext.Current.Session["clientesPages_Buscador"] = WebService.InsertarPalabraBuscada(pTxtBuscador.ToUpper(), ((Usuario)HttpContext.Current.Session["clientesDefault_Usuario"]).id, Constantes.cTABLA_PRODUCTO);

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
            List<string> resultado = null;
            List<cUsuarioSinPermisosIntranet> lista = WebService.RecuperarTodosSinPermisosIntranetPorIdUsuario(pIdUsuario);
            if (lista != null)
            {
                resultado = new List<string>();
                foreach (cUsuarioSinPermisosIntranet item in lista)
                {
                    resultado.Add(item.usp_nombreSeccion.ToUpper());
                }
            }
            //HttpContext.Current.Session["master_ListaSinPermisoSecciones"] = resultado;
            return resultado;
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
                List<cSucursalDependienteTipoEnviosCliente> lista = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente_cliente();
                if (lista != null)
                {
                    resultado = new List<cTipoEnvioClienteFront>();
                    for (int i = 0; i < lista.Count; i++)
                    {
                        cTipoEnvioClienteFront obj = new cTipoEnvioClienteFront();
                        obj.sucursal = lista[i].sde_sucursalDependiente;
                        obj.tipoEnvio = lista[i].env_codigo;

                        List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> listaTiposEnvios = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios_Excepciones(lista[i].tsd_id, objCliente.cli_codrep);
                        if (listaTiposEnvios == null || listaTiposEnvios.Count == 0)
                        {
                            listaTiposEnvios = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios().Where(x => x.tdt_idSucursalDependienteTipoEnvioCliente == lista[i].tsd_id).ToList();
                        }
                        if (listaTiposEnvios != null)
                        {
                            obj.lista = new List<cTiposEnvios>();
                            foreach (cSucursalDependienteTipoEnviosCliente_TiposEnvios itemSucursalDependienteTipoEnviosCliente_TiposEnvios in listaTiposEnvios)
                            {
                                cTiposEnvios objTipoEnvio = new cTiposEnvios();
                                objTipoEnvio.env_codigo = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_codigo;
                                objTipoEnvio.env_nombre = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_nombre;
                                objTipoEnvio.env_id = itemSucursalDependienteTipoEnviosCliente_TiposEnvios.env_id;
                                obj.lista.Add(objTipoEnvio);
                            }
                        }
                        //
                        resultado.Add(obj);
                    }
                }
                // Inicio S7
                if (((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_codrep == "S7")
                {
                    cTipoEnvioClienteFront obj = new cTipoEnvioClienteFront();
                    obj.sucursal = "SF";
                    obj.tipoEnvio = null;
                    obj.lista = new List<cTiposEnvios>();
                    cTiposEnvios objTipoEnvioR = new cTiposEnvios();
                    objTipoEnvioR.env_codigo = "R";
                    objTipoEnvioR.env_nombre = "Reparto";
                    obj.lista.Add(objTipoEnvioR);
                    cTiposEnvios objTipoEnvioE = new cTiposEnvios();
                    objTipoEnvioE.env_codigo = "E";
                    objTipoEnvioE.env_nombre = "Encomienda";
                    obj.lista.Add(objTipoEnvioE);
                    cTiposEnvios objTipoEnvioM = new cTiposEnvios();
                    objTipoEnvioM.env_codigo = "M";
                    objTipoEnvioM.env_nombre = "Mostrador";
                    obj.lista.Add(objTipoEnvioM);
                    cTiposEnvios objTipoEnvioC = new cTiposEnvios();
                    objTipoEnvioC.env_codigo = "C";
                    objTipoEnvioC.env_nombre = "Cadeteria";
                    obj.lista.Add(objTipoEnvioC);
                    resultado.Add(obj);
                }
                // Fin S7
                if (!string.IsNullOrEmpty(((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa)
                    && resultado.FirstOrDefault(x => x.sucursal == ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa) == null)
                {
                    cTipoEnvioClienteFront obj = new cTipoEnvioClienteFront();
                    obj.sucursal = ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_IdSucursalAlternativa;
                    obj.tipoEnvio = null;
                    obj.lista = new List<cTiposEnvios>();
                    cTiposEnvios objTipoEnvioR = new cTiposEnvios();
                    objTipoEnvioR.env_codigo = "R";
                    objTipoEnvioR.env_nombre = "Reparto";
                    obj.lista.Add(objTipoEnvioR);
                    resultado.Add(obj);
                }

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