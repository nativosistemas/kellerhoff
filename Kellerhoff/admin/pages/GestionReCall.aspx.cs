﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Kellerhoff.Codigo;
using Kellerhoff.Codigo.clases;
using Kellerhoff.Codigo.capaDatos;

namespace Kellerhoff.admin.pages
{
    public partial class GestionReCall : cBaseAdmin
    {
        public const string consPalabraClave = "gestionrecall";
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad(consPalabraClave);
            if (!IsPostBack)
            {

            }
        }
        [WebMethod(EnableSession = true)]
        public static string RecuperarTodosReCall()
        {
            List<tbl_Recall> resultado = WebService.RecuperarTodaReCall();
            return resultado == null ? string.Empty : Kellerhoff.Codigo.clases.Generales.Serializador.SerializarAJson(resultado);
        }
        [WebMethod(EnableSession = true)]
        public static string EliminarReCall(int pValor)
        {
            WebService.EliminarReCall(pValor);
            return string.Empty;
        }
        [WebMethod(EnableSession = true)]
        public static bool CambiarEstadoReCallPorId(int pValue)
        {
            WebService.CambiarPublicarReCall(pValue);
            return true;
        }
    }
}