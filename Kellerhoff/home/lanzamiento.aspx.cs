﻿using Kellerhoff.Codigo.capaDatos;
using Kellerhoff.Codigo.clases.Generales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kellerhoff.home
{
    public partial class lanzamiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("id"))
            {
                HttpContext.Current.Session["lanzamiento_idOferta"] = Request.QueryString.Get("id");

            }
            //WebService.ActualizarAnchoAltoImagenProductosAlAmpliar();
        }
        [WebMethod(EnableSession = true)]
        public static string RecuperarTodasOfertas()
        {
            int? id = 0;
            if (HttpContext.Current.Session["lanzamiento_idOferta"] != null)
                id = Convert.ToInt32(HttpContext.Current.Session["lanzamiento_idOferta"]);

            List<cOferta> resultado = new List<cOferta>();
            cOferta o = WebService.RecuperarTodasOfertas_generico().FirstOrDefault(x => x.ofe_idOferta == id);
            if (o != null)
                resultado.Add(o);
            /*foreach (var item in resultado)
            {
                List<cArchivo> listaArchivo = WebService.RecuperarTodosArchivos(item.ofe_idOferta, "ofertas", string.Empty);
                if (listaArchivo != null)
                {
                    if (listaArchivo.Count > 0)
                    {
                        item.nameImagen = listaArchivo[0].arc_nombre;
                    }
                }
                List<cArchivo> listaArchivo_ampliada = WebService.RecuperarTodosArchivos(item.ofe_idOferta, "ofertasampliar", string.Empty);
                if (listaArchivo_ampliada != null)
                {
                    if (listaArchivo_ampliada.Count > 0)
                    {
                        item.nameImagenAmpliada = listaArchivo_ampliada[0].arc_nombre;
                    }
                }
                
            }*/


            return resultado == null ? string.Empty : Serializador.SerializarAJson(resultado);
        }
        public void AgregarHtmlOculto()
        {
            string resultado = string.Empty;
            resultado += "<input type=\"hidden\" id=\"hiddenListaOfertas\" value=\"" + Server.HtmlEncode(RecuperarTodasOfertas()) + "\" />";
            Response.Write(resultado);
        }
    }
}