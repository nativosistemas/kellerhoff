using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Kellerhoff.Codigo.clases;
//using Newtonsoft.Json;
//using System.Text.Json;


namespace Kellerhoff.Codigo.capaDatos
{
    public class capaCore_WebService
    {
        private static readonly HttpClient client = new HttpClient();
        private static string url = System.Configuration.ConfigurationManager.AppSettings["url_DKcore"];
        //url_DKcore
        //public static async Task<DKbase.dll.cDllPedido> TomarPedidoConIdCarritoAsync(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<DKbase.dll.cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
        //{
        //    try
        //    {
        //       string name = "TomarPedidoConIdCarrito";
        //       string url_api = url + name;
        //        capaCAR.InicioCarritoEnProceso(pIdCarrito, Constantes.cAccionCarrito_TOMAR);
        //        DKbase.dll.cDllPedido product = null;
        //        HttpResponseMessage response = await client.GetAsync(url_api);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string data = await response.Content.ReadAsStringAsync();
        //            JavaScriptSerializer JSserializer = new JavaScriptSerializer();
        //            product = JSserializer.Deserialize<DKbase.dll.cDllPedido>(data);
        //        }
        //        return product;
        //    }
        //    catch (Exception ex)
        //    {
        //        FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);
        //        return null;
        //    }
        //    finally
        //    {
        //        capaCAR.EndCarritoEnProceso(pIdCarrito);
        //    }
        //}
        //        
        public static async Task<DKbase.dll.cDllPedido> TomarPedidoConIdCarritoAsync(int pIdCarrito, string pLoginCliente, string pIdSucursal, string pMensajeEnFactura, string pMensajeEnRemito, string pTipoEnvio, List<DKbase.dll.cDllProductosAndCantidad> pListaProducto, bool pIsUrgente)
        {
            try
            {
                string name = "TomarPedidoConIdCarrito";
                string url_api = url + name;
                DKbase.dll.post_TomarPedidoConIdCarrito parameter = new DKbase.dll.post_TomarPedidoConIdCarrito() { pIdCarrito = pIdCarrito, pLoginCliente = pLoginCliente, pIdSucursal = pIdSucursal, pMensajeEnFactura = pMensajeEnFactura, pMensajeEnRemito = pMensajeEnRemito, pTipoEnvio = pTipoEnvio, pListaProducto = pListaProducto, pIsUrgente = pIsUrgente };
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                var myContent = JSserializer.Serialize(parameter);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var resultPostAsync = await client.PostAsync(url_api, byteContent);
                var result = resultPostAsync.Content.ReadAsStringAsync().Result;
                DKbase.dll.cDllPedido product = JSserializer.Deserialize<DKbase.dll.cDllPedido>(result);
                return product;
            }
            catch (Exception ex)
            {
                FuncionesPersonalizadas.grabarLog(MethodBase.GetCurrentMethod(), ex, DateTime.Now, pIdCarrito, pLoginCliente, pIdSucursal, pMensajeEnFactura, pMensajeEnRemito, pTipoEnvio, pListaProducto, pIsUrgente);
                return null;
            }  
        }
    }
}