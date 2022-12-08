﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kellerhoff.Codigo.clases;
using System.Web.Services;

namespace Kellerhoff.admin.pages
{
    public partial class GestionRegla : cBaseAdmin
    {
        public const string consPalabraClave = "gestionregla";
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad(consPalabraClave);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "var listaArbolParaComboLoad = \'" + CargarArbolCombo() + "\';", true);
        }
        [WebMethod(EnableSession = true)]
        public static string CargarArbolCombo()
        {
            List<Kellerhoff.Codigo.capaDatos.ListaCheck> listaResultado = Kellerhoff.Codigo.clases.Seguridad.RecuperarTodasReglasPorNivel();
            return Codigo.clases.Generales.Serializador.SerializarAJson(listaResultado);
        }
        [WebMethod(EnableSession = true)]
        public static List<bool> IsNombreOPalabraNoSeRepite(int pIdRegla, string pNombre, string pPalabra)
        {
            List<bool> resultado = new List<bool>();
            List<DKbase.web.cRegla> listaRegla = Kellerhoff.Codigo.clases.Seguridad.RecuperarTodasReglas(string.Empty);
            resultado.Add(listaRegla.Where(x => x.rgl_Descripcion == pNombre && x.rgl_codRegla != pIdRegla).Count() > 0 ? false : true);
            resultado.Add(listaRegla.Where(x => x.rgl_PalabraClave == pPalabra.ToLower() && x.rgl_codRegla != pIdRegla).Count() > 0 ? false : true);
            return resultado;
        }
        [WebMethod(EnableSession = true)]
        public static bool InsertarRegla(string pDescripcion, string pPalabra, bool pAgregar, bool pEditar, bool pEliminar, int pIdReglaPadre)
        {
            return InsertarActualizarRegla(0, pDescripcion, pPalabra, pAgregar, pEditar, pEliminar, pIdReglaPadre);
        }
        [WebMethod(EnableSession = true)]
        public static bool ActualizarRegla(int pIdRegla, string pDescripcion, string pPalabra, bool pAgregar, bool pEditar, bool pEliminar, int pIdReglaPadre)
        {
            return InsertarActualizarRegla(pIdRegla, pDescripcion, pPalabra, pAgregar, pEditar, pEliminar, pIdReglaPadre);
        }

        public static bool InsertarActualizarRegla(int pIdRegla, string pDescripcion, string pPalabra, bool pAgregar, bool pEditar, bool pEliminar, int pIdReglaPadre)
        {
            bool respuesta = false;
            if ((pIdRegla == 0 && Kellerhoff.Codigo.clases.cBaseAdmin.isAgregar(consPalabraClave)) || (pIdRegla != 0 && Kellerhoff.Codigo.clases.cBaseAdmin.isEditar(consPalabraClave)))
            {
                int? idPadreRegla = null;
                if (pIdReglaPadre != 0)
                {
                    idPadreRegla = pIdReglaPadre;
                }
                int resultado = Kellerhoff.Codigo.clases.Seguridad.InsertarActualizarRegla(pIdRegla, pDescripcion.Trim(), pPalabra.Trim().ToLower(), pAgregar, pEditar, pEliminar, idPadreRegla);
                if (resultado > 0)
                {
                    cBaseAdmin.CargarAccionesEnVariableSession();
                    respuesta = true;
                }
            }
            return respuesta;
        }
        [WebMethod(EnableSession = true)]
        public static Kellerhoff.Codigo.capaDatos.ListaCheck RecuperarReglaRaiz()
        {
            Kellerhoff.Codigo.capaDatos.ListaCheck resultado = null;
            List<DKbase.web.cRegla> listaRegla = Kellerhoff.Codigo.clases.Seguridad.RecuperarTodasReglas(string.Empty).Where(x => x.rgl_codReglaPadre == null).ToList();
            if (listaRegla.Count > 0)
            {
                return ConvertToListaCheck(listaRegla[0]);
            }
            return resultado;
        }
        [WebMethod(EnableSession = true)]
        public static Kellerhoff.Codigo.capaDatos.ListaCheck RecuperarReglaPorId(int pIdRegla)
        {
            Kellerhoff.Codigo.capaDatos.ListaCheck resultado = null;
            DKbase.web.cRegla regla = Kellerhoff.Codigo.clases.Seguridad.RecuperarReglaPorId(pIdRegla);
            if (regla != null)
            {
                return ConvertToListaCheck(regla);
            }
            return resultado;
        }
        [WebMethod(EnableSession = true)]
        public static bool EliminarRegla(int pIdRegla)
        {
            bool resultado = false;
            try
            {
                if (Kellerhoff.Codigo.clases.cBaseAdmin.isEliminar(consPalabraClave))
                {
                    Kellerhoff.Codigo.clases.Seguridad.EliminarRegla(pIdRegla);
                    resultado = true;
                }
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }
        private static Kellerhoff.Codigo.capaDatos.ListaCheck ConvertToListaCheck(DKbase.web.cRegla pRegla)
        {
            Kellerhoff.Codigo.capaDatos.ListaCheck resultado = new Kellerhoff.Codigo.capaDatos.ListaCheck();
            resultado.id = pRegla.rgl_codRegla;
            resultado.descripcion = pRegla.rgl_Descripcion;
            resultado.palabra = pRegla.rgl_PalabraClave;
            resultado.idPadreRegla = pRegla.rgl_codReglaPadre;
            if ((bool)pRegla.rgl_IsAgregarSoporta)
            {
                resultado.checkAgregar = 1;
            }
            else
            {
                resultado.checkAgregar = 0;
            }
            if ((bool)pRegla.rgl_IsEditarSoporta)
            {
                resultado.checkEditar = 1;
            }
            else
            {
                resultado.checkEditar = 0;
            }
            if ((bool)pRegla.rgl_IsEliminarSoporta)
            {
                resultado.checkEliminar = 1;
            }
            else
            {
                resultado.checkEliminar = 0;
            }
            List<DKbase.web.cRegla> listaReglaParametro = Kellerhoff.Codigo.clases.Seguridad.RecuperarTodasReglas(string.Empty);
            resultado.listaIdHijas = Kellerhoff.Codigo.clases.Seguridad.RecuperarTodosIdReglasHijas(pRegla.rgl_codRegla, listaReglaParametro);
            return resultado;
        }
    }
}