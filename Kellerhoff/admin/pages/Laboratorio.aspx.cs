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
    public partial class Laboratorio : cBaseAdmin
    {
        public const string consPalabraClave = "gestionapp";
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad(consPalabraClave);
            if (!IsPostBack)
            {

            }
        }
        [WebMethod(EnableSession = true)]
        public static string GetAll()
        {
            List<DKbase.Entities.Laboratorio> resultado = WebService.GetLaboratorios();
            return resultado == null ? string.Empty : Kellerhoff.Codigo.clases.Generales.Serializador.SerializarAJson(resultado);
        }
        [WebMethod(EnableSession = true)]
        public static void Delete(int id)
        {
            WebService.DeleteLaboratorios(id);
        }
    }
}