﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Kellerhoff.Codigo.clases;
using Kellerhoff.Codigo.capaDatos;
using DKbase.web;

namespace Kellerhoff.admin.pages
{
    public partial class GestionTiposEnviosSucursal : cBaseAdmin
    {
        public const string consPalabraClave = "gestiontiposenviossucursal";
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad(consPalabraClave);
            if (!IsPostBack)
            {
                Session["GestionTiposEnviosSucursal_Filtro"] = null;
                Session["GestionTiposEnviosSucursal_Env_id"] = null;


            }
        }
        protected void cmd_nuevo_Click(object sender, EventArgs e)
        {
            Session["GestionTiposEnviosSucursal_Env_id"] = 0;
            cmbSucursalesDependientes.SelectedIndex = -1;
            cmbTipoEnvioCliente.SelectedIndex = -1;
            cmbTipoEnvio.SelectedIndex = -1;
            cmbSucursalesDependientes.Enabled = true;
            cmbTipoEnvioCliente.Enabled = true;
            listTipoEnviosAsociados.Items.Clear();
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;

        }

        protected void cmd_buscar_Click(object sender, EventArgs e)
        {
            Session["GestionTiposEnviosSucursal_Filtro"] = txt_buscar.Text;
            gv_datos.DataBind();
        }
        protected void cmd_guardar_Click(object sender, EventArgs e)
        {
            lblMensajeError.Text = string.Empty;
            if (Session["GestionTiposEnviosSucursal_Env_id"] != null)
            {
                int codTiposEnviosSucursal = Convert.ToInt32(Session["GestionTiposEnviosSucursal_Env_id"]);
                int idTipoEnvioCliente = Convert.ToInt32(cmbTipoEnvioCliente.SelectedValue);
                int idSucursalesDependientes = Convert.ToInt32(cmbSucursalesDependientes.SelectedValue);

                int cantSucursalDependienteTipoEnvioCliente = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente().Where(x => x.sde_codigo == idSucursalesDependientes && (idTipoEnvioCliente == -1 ? x.tsd_idTipoEnvioCliente == null : x.tsd_idTipoEnvioCliente == idTipoEnvioCliente)).Count();
                if ((cantSucursalDependienteTipoEnvioCliente == 0 && codTiposEnviosSucursal == 0) || (cantSucursalDependienteTipoEnvioCliente >= 1 && codTiposEnviosSucursal != 0))
                {
                    List<int> listaTipoEnvio = new List<int>();
                    foreach (ListItem item in listTipoEnviosAsociados.Items)
                    {
                        listaTipoEnvio.Add(Convert.ToInt32(item.Value));
                    }
                    int codTiposEnviosSucursal_NuevoOrEdicion = WebService.InsertarActualizarSucursalDependienteTipoEnvioCliente(codTiposEnviosSucursal, idSucursalesDependientes, (idTipoEnvioCliente == -1 ? (int?)null : idTipoEnvioCliente), listaTipoEnvio);
                    pnl_grilla.Visible = true;
                    pnl_formulario.Visible = false;
                    gv_datos.DataBind();
                }
                else
                {
                    lblMensajeError.Text = "Ya existe la sucursal, sucursal dependiente y tipo de envió cliente";
                }
            }
        }
        protected void cmd_cancelar_Click(object sender, EventArgs e)
        {
            lblMensajeError.Text = string.Empty;
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }

        protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                Session["GestionTiposEnviosSucursal_Env_id"] = Convert.ToInt32(e.CommandArgument);
                cSucursalDependienteTipoEnviosCliente obj = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente().Where(x => x.tsd_id == Convert.ToInt32(e.CommandArgument)).First();
                cmbSucursalesDependientes.SelectedIndex = cmbSucursalesDependientes.Items.IndexOf(cmbSucursalesDependientes.Items.FindByValue(obj.sde_codigo.ToString()));
                cmbTipoEnvioCliente.SelectedIndex = cmbTipoEnvioCliente.Items.IndexOf(cmbTipoEnvioCliente.Items.FindByValue(obj.env_id.ToString()));
                cmbSucursalesDependientes.Enabled = false;
                cmbTipoEnvioCliente.Enabled = false;
                cmbTipoEnvio.SelectedIndex = -1;
                List<cSucursalDependienteTipoEnviosCliente_TiposEnvios> listaTipoEnvio = WebService.RecuperarTodosSucursalDependienteTipoEnvioCliente_TiposEnvios().Where(x => x.tdt_idSucursalDependienteTipoEnvioCliente == Convert.ToInt32(e.CommandArgument)).ToList();
                listTipoEnviosAsociados.Items.Clear();
                foreach (cSucursalDependienteTipoEnviosCliente_TiposEnvios item in listaTipoEnvio)
                {
                    ListItem objListItem = new ListItem(item.env_nombre, item.env_id.ToString());
                    listTipoEnviosAsociados.Items.Add(objListItem);
                }
                pnl_grilla.Visible = false;
                pnl_formulario.Visible = true;
            }
            else if (e.CommandName == "Eliminar")
            {
                WebService.EliminarSucursalDependienteTipoEnvioCliente(Convert.ToInt32(e.CommandArgument));
                gv_datos.DataBind();
            }
            else if (e.CommandName == "Reparto")
            {
                Response.Redirect("GestionTiposEnviosSucursal_Reparto.aspx?id=" + e.CommandArgument);
            }
        }

        protected void btnAgregarTipoEnvio_Click(object sender, EventArgs e)
        {
            bool isAgregar = true;
            foreach (ListItem item in listTipoEnviosAsociados.Items)
            {
                if (item.Value == cmbTipoEnvio.SelectedItem.Value)
                {
                    isAgregar = false;
                    break;
                }
            }
            if (isAgregar)
            {
                ListItem obj = new ListItem(cmbTipoEnvio.SelectedItem.Text, cmbTipoEnvio.SelectedItem.Value);
                listTipoEnviosAsociados.Items.Add(obj);
            }
        }
        protected void btnEliminarTipoEnvio_Click(object sender, EventArgs e)
        {
            if (listTipoEnviosAsociados.SelectedItem != null)
            {
                listTipoEnviosAsociados.Items.Remove(listTipoEnviosAsociados.SelectedItem);
            }
        }
    }
}