using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Kellerhoff.Codigo.clases;
using Kellerhoff.Codigo.capaDatos;

namespace Kellerhoff.admin.pages
{
    public partial class GestionOferta : cBaseAdmin
    {
        public const string consPalabraClave = "gestionhome";
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad(consPalabraClave);
            if (!IsPostBack)
            {

            }
        }
        [WebMethod(EnableSession = true)]
        public static string RecuperarTodasOfertas()
        {
            List<cOferta> resultado = WebService.RecuperarTodasOfertas();
            return resultado == null ? string.Empty : Kellerhoff.Codigo.clases.Generales.Serializador.SerializarAJson(resultado);
        }
        [WebMethod(EnableSession = true)]
        public static bool ElimimarOfertaPorId(int pValue)
        {
            bool? resultado = WebService.ElimimarOfertaPorId(pValue);
            return true;
        }
        [WebMethod(EnableSession = true)]
        public static bool CambiarEstadoPublicarOferta(int pValue)
        {
            bool? resultado = WebService.CambiarEstadoPublicarOferta(pValue);
            return true;
        }
    }
}