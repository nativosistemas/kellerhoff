using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kellerhoff.Codigo.clases;

namespace Kellerhoff.admin.pages
{
    public partial class GestionRol : cBaseAdmin
    {
        public const string consPalabraClave = "gestionrol";
        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad(consPalabraClave);
            if (!IsPostBack)
            {
                Session["GestionRol_Filtro"] = null;
                Session["GestionRol_IdRol"] = null;
            }
        }
        protected void cmd_nuevo_Click(object sender, EventArgs e)
        {
            LlamarMetodosAcciones(Constantes.cSQL_INSERT, null, consPalabraClave);
        }
        public override void Modificar(int pIdRol)
        {
            DKbase.web.cRol rol = DKbase.Util.RecuperarRolPorId(pIdRol);
            Session["GestionRol_IdRol"] = rol.rol_codRol;
            txt_nombre.Text = rol.rol_Nombre;
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
        public override void Insertar()
        {
            Session["GestionRol_IdRol"] = 0;
            txt_nombre.Text = string.Empty;
            pnl_grilla.Visible = false;
            pnl_formulario.Visible = true;
        }
        public override void Eliminar(int pIdRol)
        {
            List<DKbase.web.cUsuario> listaUsuarioTemp = Kellerhoff.Codigo.clases.Seguridad.RecuperarTodosUsuarios(string.Empty);
            listaUsuarioTemp = listaUsuarioTemp.Where(x => x.usu_codRol == pIdRol).ToList();
            if (listaUsuarioTemp.Count == 0)
            {
                Kellerhoff.Codigo.clases.Seguridad.EliminarRol(pIdRol);
                gv_datos.DataBind();
            }
        }
        protected void cmd_buscar_Click(object sender, EventArgs e)
        {
            Session["GestionRol_Filtro"] = txt_buscar.Text;
            gv_datos.DataBind();
        }

        protected void cmd_cancelar_Click(object sender, EventArgs e)
        {
            pnl_grilla.Visible = true;
            pnl_formulario.Visible = false;
        }

        protected void cmd_guardar_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid)
            {
                if (Session["GestionRol_IdRol"] != null)
                {
                    int codigoRol = Convert.ToInt32(Session["GestionRol_IdRol"]);
                    if ((codigoRol == 0 && cBaseAdmin.isAgregar(consPalabraClave)) || (codigoRol != 0 && cBaseAdmin.isEditar(consPalabraClave)))
                    {
                        DKbase.Util.InsertarActualizarRol(codigoRol, txt_nombre.Text);
                    }
                }
                gv_datos.DataBind();
                pnl_grilla.Visible = true;
                pnl_formulario.Visible = false;
            }
        }
        protected void gv_datos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                LlamarMetodosAcciones(Constantes.cSQL_UPDATE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
            }
            else if (e.CommandName == "Eliminar")
            {
                LlamarMetodosAcciones(Constantes.cSQL_DELETE, Convert.ToInt32(e.CommandArgument), consPalabraClave);
            }
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool resultado = true;
            if (Session["GestionRol_IdRol"] != null)
            {
                List<DKbase.web.cRol> listaRol = DKbase.Util.RecuperarTodasRoles(string.Empty);
                foreach (DKbase.web.cRol item in listaRol)
                {
                    if (item.rol_Nombre == args.Value)
                    {
                        if (Convert.ToInt32(Session["GestionRol_IdRol"]) == 0 || Convert.ToInt32(Session["GestionRol_IdRol"]) != item.rol_codRol)
                        {
                            resultado = false;
                            break;
                        }
                    }
                }
            }
            args.IsValid = resultado;
        }
    }
}