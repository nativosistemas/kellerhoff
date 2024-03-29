﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kellerhoff.Codigo.clases;
using System.Web.Services;
using Kellerhoff.Codigo.capaDatos;
using DKbase.web.capaDatos;

namespace Kellerhoff.admin.pages
{
    public partial class GestionMensajeNewV4Editar : cBaseAdmin
    {
        public const string consPalabraClave = "gestionmensaje";
        protected void Page_Load(object sender, EventArgs e)
        {

            Seguridad(consPalabraClave);
            if (!IsPostBack)
            {
                Session["GestionMensajeNewV4_mensaje"] = null;
                if (Request.QueryString.AllKeys.Contains("id"))
                {
                    int id = Convert.ToInt32(Request.QueryString.Get("id"));
                    HttpContext.Current.Session["GestionMensajeNewV4_tme_codigo"] = id;
                    if (id == 0)
                    {
                        Insertar_editar();
                    }
                    else
                    {
                        Modificar_editar(id);
                    }

                }
            }



        }
        protected void cmd_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/admin/pages/GestionMensajeNewV4.aspx");
            //pnl_grilla.Visible = true;
            //pnl_formulario.Visible = false;
        }
        protected void cmd_guardar_Click(object sender, EventArgs e)
        {
            if (Session["GestionMensajeNewV4_tme_codigo"] != null && Session["BaseAdmin_Usuario"] != null)
            {
                int codigoMensaje = Convert.ToInt32(Session["GestionMensajeNewV4_tme_codigo"]);
                if ((codigoMensaje == 0 && Kellerhoff.Codigo.clases.cBaseAdmin.isAgregar(consPalabraClave)) || (codigoMensaje != 0 && Kellerhoff.Codigo.clases.cBaseAdmin.isEditar(consPalabraClave)))
                {

                    int codigoUsuarioEnSession = ((DKbase.web.Usuario)Session["BaseAdmin_Usuario"]).id;
                    DateTime? fechaDesde = null;
                    DateTime? fechaHasta = null;
                    bool importante = checkboxImportante.Checked;

                    if (checkboxImportante.Checked)
                    {
                        fechaDesde = CalendarFechaDesde.SelectedDate;
                        fechaHasta = CalendarFechaHasta.SelectedDate;
                    }
                    //List<string> listaSucursal = new List<string>();
                    string sucursales = string.Empty;
                    string tipo = string.Empty;
                    if (RadioButtonSucursal.Checked)
                    {
                        tipo = DKbase.generales.Constantes.cTipoMensaje_Sucursal;
                    }
                    else if (RadioButtonCliente.Checked)
                    {
                        tipo = DKbase.generales.Constantes.cTipoMensaje_Cliente;
                    }
                    else if (RadioButtonReparto.Checked)
                    {
                        tipo = DKbase.generales.Constantes.cTipoMensaje_Reparto;
                    }
                    if (RadioButtonSucursal.Checked && CheckBoxListSucursales.Items != null)
                    {
                        for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
                        {
                            if (CheckBoxListSucursales.Items[i].Selected)
                            {
                                sucursales += "<" + CheckBoxListSucursales.Items[i].Value + ">";
                            }
                        }
                    }
                    string repartos = string.Empty;
                    if (RadioButtonReparto.Checked && CheckBoxListReparto.Items != null)
                    {
                        for (int i = 0; i < CheckBoxListReparto.Items.Count; i++)
                        {
                            if (CheckBoxListReparto.Items[i].Selected)
                            {
                                repartos += "<" + CheckBoxListReparto.Items[i].Value + ">";
                            }
                        }
                    }
                    string mensajeHTML = HiddenField_mensaje.Value;//mensajeHTML
                    int resultado = WebService.ActualizarInsertarMensajeNew(codigoMensaje, txt_asunto.Text, mensajeHTML, fechaDesde, fechaHasta, importante, sucursales, repartos, tipo);
                    Response.Redirect("~/admin/pages/GestionMensajeNewV4.aspx");
                }
            }
        }
        public void Insertar_editar()
        {
            Session["GestionMensajeNewV4_isNuevo"] = true;
            Session["GestionMensajeNewV4_tme_todos"] = 0;
            Session["GestionMensajeNewV4_tme_codigo"] = 0;
            Session["GestionMensajeNewV4_tme_todosSucursales"] = 0;
            PanelFecha.Visible = false;
            checkboxImportante.Checked = false;
            //CalendarFechaDesde.SelectedDate = DateTime.Now;
            //CalendarFechaHasta.SelectedDate = DateTime.Now;
            //pnl_grilla.Visible = false;
            //pnl_formulario.Visible = true;
            //cmbClientes.SelectedIndex = -1;
            //cmbEstado.SelectedIndex = -1;
            txt_asunto.Text = string.Empty;
            txt_mensaje.Text = string.Empty;//txt_mensaje.Content = string.Empty;
            HiddenField_mensaje.Value = string.Empty;
            //SetearComboSucursalCliente();
            CheckRadioButton();
        }
        public void Modificar_editar(int pId)
        {
            Session["GestionMensajeNewV4_tme_codigo"] = pId;
            cMensaje mensaje = WebService.RecuperarMensajeNewV4PorId(pId);
            Session["GestionMensajeNewV4_mensaje"] = mensaje;
            if (mensaje != null)
            {
                //int clienteCombo = 0;
                Session["GestionMensajeNewV4_tme_todosSucursales"] = mensaje.tme_todosSucursales;
                Session["GestionMensajeNewV4_tme_todos"] = mensaje.tme_todos;
                Session["GestionMensajeNewV4_isNuevo"] = false;

                checkboxImportante.Checked = mensaje.tme_importante;
                if (mensaje.tme_importante)
                {
                    PanelFecha.Visible = checkboxImportante.Checked;
                    CalendarFechaDesde.SelectedDate = (DateTime)mensaje.tme_fechaDesde;
                    CalendarFechaHasta.SelectedDate = (DateTime)mensaje.tme_fechaHasta;
                }
                else
                {
                    PanelFecha.Visible = checkboxImportante.Checked;
                    //clienteCombo = mensaje.tme_codClienteDestinatario;
                }
                txt_asunto.Text = mensaje.tme_asunto;
                txt_mensaje.Text = mensaje.tme_mensaje;// txt_mensaje.Content = mensaje.tme_mensaje;
                HiddenField_mensaje.Value = mensaje.tme_mensaje;
                HiddenField_loadMsg.Value = "1";
                if (!string.IsNullOrEmpty(mensaje.tmn_todosSucursales) && (string.IsNullOrEmpty(mensaje.tmn_tipo) && mensaje.tmn_tipo == DKbase.generales.Constantes.cTipoMensaje_Sucursal))
                {
                    RadioButtonSucursal.Checked = true;
                }
                else if (mensaje.tmn_tipo == DKbase.generales.Constantes.cTipoMensaje_Reparto)
                {
                    RadioButtonReparto.Checked = true;
                }
                CheckRadioButton();

            }
        }
        protected void CheckBoxListSucursales_DataBound(object sender, EventArgs e)
        {
            if (Session["GestionMensajeNewV4_mensaje"] != null)
            {
                cMensaje mensaje = (cMensaje)Session["GestionMensajeNewV4_mensaje"];
                //Session["GestionMensajeNewV4_tme_todosSucursales"] = mensaje;
                if (!string.IsNullOrEmpty(mensaje.tmn_todosSucursales) && (string.IsNullOrEmpty(mensaje.tmn_tipo) && mensaje.tmn_tipo == DKbase.generales.Constantes.cTipoMensaje_Sucursal))
                {
                    //RadioButtonSucursal.Checked = true;
                    //CheckRadioButton();
                    if (CheckBoxListSucursales.Items != null)
                    {
                        for (int i = 0; i < CheckBoxListSucursales.Items.Count; i++)
                        {
                            if (mensaje.tmn_todosSucursales.Contains(CheckBoxListSucursales.Items[i].Value))
                            {
                                CheckBoxListSucursales.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }
        protected void CheckBoxListReparto_DataBound(object sender, EventArgs e)
        {
            if (Session["GestionMensajeNewV4_mensaje"] != null)
            {
                cMensaje mensaje = (cMensaje)Session["GestionMensajeNewV4_mensaje"];
                if (!string.IsNullOrEmpty(mensaje.tmn_todosRepartos) && mensaje.tmn_tipo == DKbase.generales.Constantes.cTipoMensaje_Reparto)
                {
                    if (CheckBoxListReparto.Items != null)
                    {
                        for (int i = 0; i < CheckBoxListReparto.Items.Count; i++)
                        {
                            if (mensaje.tmn_todosRepartos.Contains(CheckBoxListReparto.Items[i].Value))
                            {
                                CheckBoxListReparto.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }
        [WebMethod(EnableSession = true)]
        public static string vistaPreviaMensajeNew(string pAsunto, string pMensaje)
        {
            HttpContext.Current.Session["GestionMensajeV3Editar_vistaPrevia_Asunto"] = pAsunto;
            HttpContext.Current.Session["GestionMensajeV3Editar_vistaPrevia_Mensaje"] = pMensaje;
            return string.Empty;
        }
        public void rptr_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            System.Data.Common.DbDataRecord rec = (System.Data.Common.DbDataRecord)
                                                   e.Item.DataItem;
            if (rec != null) //Asegúrese de que tiene los datos.
            {
                Label l1 = (Label)e.Item.FindControl("lblAuthorID");
                l1.Text = rec["au_id"].ToString();
            }
        }
        protected void checkboxImportante_CheckedChanged(object sender, EventArgs e)
        {
            PanelFecha.Visible = checkboxImportante.Checked;
            CalendarFechaDesde.SelectedDate = DateTime.Now;
            CalendarFechaHasta.SelectedDate = DateTime.Now;
            //if (checkboxImportante.Checked)
            //{            
            //}
            //else 
            //{        
            //}
        }
        protected void RadioButtonSucursal_CheckedChanged(object sender, EventArgs e)
        {
            CheckRadioButton();
        }

        protected void RadioButtonCliente_CheckedChanged(object sender, EventArgs e)
        {
            CheckRadioButton();
        }
        public void CheckRadioButton()
        {
            if (RadioButtonSucursal.Checked)
            {
                CheckBoxListSucursales.Enabled = true;
                CheckBoxListReparto.Enabled = false;
            }
            else if (RadioButtonCliente.Checked)
            {
                CheckBoxListSucursales.Enabled = false;
                CheckBoxListReparto.Enabled = false;
            }
            else if (RadioButtonReparto.Checked)
            {
                CheckBoxListSucursales.Enabled = false;
                CheckBoxListReparto.Enabled = true;
            }
        }
    }
}