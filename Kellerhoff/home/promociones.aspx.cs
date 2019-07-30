using Kellerhoff.Codigo.capaDatos;
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
    public partial class promociones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Session["homeBodyCss"] = "bd_sec";
        }
        [WebMethod(EnableSession = true)]
        public static string RecuperarTodasOfertas()
        {
            List<cOferta> resultado = WebService.RecuperarTodasOfertaPublicar();
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