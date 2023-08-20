using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Kellerhoff.Codigo.capaDatos;
using Kellerhoff.Codigo.clases.Generales;
using DKbase.web;
using DKbase.web.capaDatos;

namespace Kellerhoff.Codigo.clases
{
    public class Seguridad
    {
        public static Usuario Login(string pLogin, string pPassword, string pIp, string pHostName, string pUserAgent)
        {
           return DKbase.Util.Login(pLogin, pPassword, pIp, pHostName, pUserAgent);
        }
        public static void CerrarSession(int pIdUsuarioLog)
        {
            DKbase.Util.CerrarSession(pIdUsuarioLog);
        }
        //public static int InsertarActualizarRol(int rol_codRol, string rol_Nombre)
        //{
        //    string accion = rol_codRol == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
        //    DataSet dsResultado = capaSeguridad_base.GestiónRol(rol_codRol, rol_Nombre, null, accion);
        //    int resultado = -1;
        //    if (rol_codRol == 0)
        //    {
        //        if (dsResultado != null)
        //        {
        //            if (dsResultado.Tables["Rol"].Rows[0]["rol_codRol"] != DBNull.Value)
        //            {
        //                resultado = Convert.ToInt32(dsResultado.Tables["Rol"].Rows[0]["rol_codRol"]);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        resultado = rol_codRol;
        //    }
        //    return resultado;
        //}
        public static List<cRol> RecuperarTodasRoles(string pFiltro)
        {
            return DKbase.Util.RecuperarTodasRoles( pFiltro);
        }
        //public static cRol RecuperarRolPorId(int pIdRol)
        //{
        //    cRol resultado = new cRol();
        //    DataSet dsResultado = capaSeguridad_base.GestiónRol(pIdRol, null, null, Constantes.cSQL_SELECT);
        //    if (dsResultado != null)
        //    {
        //        foreach (DataRow item in dsResultado.Tables["Rol"].Rows)
        //        {
        //            if (item["rol_codRol"] != DBNull.Value)
        //            {
        //                resultado.rol_codRol = Convert.ToInt32(item["rol_codRol"]);
        //            }
        //            if (item["rol_Nombre"] != DBNull.Value)
        //            {
        //                resultado.rol_Nombre = item["rol_Nombre"].ToString();
        //            }
        //        }
        //    }
        //    return resultado;
        //}
        public static List<cRol> RecuperarTodasRoles_Combo()
        {
            List<cRol> lista = new List<cRol>();
            DataSet dsResultado = capaSeguridad_base.GestiónRol(null, null, null, Constantes.cSQL_COMBO);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Rol"].Rows)
                {
                    cRol obj = new cRol();
                    if (item["rol_codRol"] != DBNull.Value)
                    {
                        obj.rol_codRol = Convert.ToInt32(item["rol_codRol"]);
                    }
                    if (item["rol_Nombre"] != DBNull.Value)
                    {
                        obj.rol_Nombre = item["rol_Nombre"].ToString();
                    }
                    lista.Add(obj);
                }
            }
            return lista;
        }
        public static void EliminarRol(int rol_codRol)
        {
            DataSet dsResultado = capaSeguridad_base.GestiónRol(rol_codRol, null, null, Constantes.cSQL_DELETE);
        }
        //public static void EliminarRegla(int rgl_codRegla)
        //{
        //    DataSet dsResultado = capaSeguridad_base.GestiónRegla(rgl_codRegla, null, null, null, null, null, null, null, Constantes.cSQL_DELETE);
        //}
        ////public static void EliminarUsuario(int usu_codigo)
        ////{
        ////    DataSet dsResultado = capaSeguridad_base.GestiónUsuario(usu_codigo, null, null, null, null, null, null, null, null, null, null, null, null, Constantes.cSQL_DELETE);
        ////}
        //public static int InsertarActualizarRegla(int rgl_codRegla, string rgl_Descripcion, string rgl_PalabraClave, bool rgl_IsAgregarSoporta, bool rgl_IsEditarSoporta, bool rgl_IsEliminarSoporta, int? rgl_codReglaPadre)
        //{
        //    string accion = rgl_codRegla == 0 ? Constantes.cSQL_INSERT : Constantes.cSQL_UPDATE;
        //    DataSet dsResultado = capaSeguridad_base.GestiónRegla(rgl_codRegla, rgl_Descripcion, rgl_PalabraClave, rgl_IsAgregarSoporta, rgl_IsEditarSoporta, rgl_IsEliminarSoporta, rgl_codReglaPadre, null, accion);
        //    int resultado = -1;
        //    if (rgl_codRegla == 0)
        //    {
        //        if (dsResultado != null)
        //        {
        //            if (dsResultado.Tables["Regla"].Rows[0]["rgl_codRegla"] != DBNull.Value)
        //            {
        //                resultado = Convert.ToInt32(dsResultado.Tables["Regla"].Rows[0]["rgl_codRegla"]);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        resultado = rgl_codRegla;
        //    }
        //    return resultado;
        //}
        //public static List<cRegla> RecuperarTodasReglas(string pFiltro)
        //{
        //    List<cRegla> lista = new List<cRegla>();
        //    DataSet dsResultado = capaSeguridad_base.GestiónRegla(null, null, null, null, null, null, null, pFiltro, Constantes.cSQL_SELECT);
        //    if (dsResultado != null)
        //    {
        //        foreach (DataRow item in dsResultado.Tables["Regla"].Rows)
        //        {
        //            cRegla obj = new cRegla();
        //            if (item["rgl_codRegla"] != DBNull.Value)
        //            {
        //                obj.rgl_codRegla = Convert.ToInt32(item["rgl_codRegla"]);
        //            }
        //            if (item["rgl_Descripcion"] != DBNull.Value)
        //            {
        //                obj.rgl_Descripcion = item["rgl_Descripcion"].ToString();
        //            }
        //            if (item["rgl_PalabraClave"] != DBNull.Value)
        //            {
        //                obj.rgl_PalabraClave = item["rgl_PalabraClave"].ToString();
        //            }
        //            if (item["rgl_IsAgregarSoporta"] != DBNull.Value)
        //            {
        //                obj.rgl_IsAgregarSoporta = Convert.ToBoolean(item["rgl_IsAgregarSoporta"]);
        //            }
        //            if (item["rgl_IsEditarSoporta"] != DBNull.Value)
        //            {
        //                obj.rgl_IsEditarSoporta = Convert.ToBoolean(item["rgl_IsEditarSoporta"]);
        //            }
        //            if (item["rgl_IsEliminarSoporta"] != DBNull.Value)
        //            {
        //                obj.rgl_IsEliminarSoporta = Convert.ToBoolean(item["rgl_IsEliminarSoporta"]);
        //            }
        //            if (item["rgl_codReglaPadre"] != DBNull.Value)
        //            {
        //                obj.rgl_codReglaPadre = Convert.ToInt32(item["rgl_codReglaPadre"]);
        //            }
        //            lista.Add(obj);
        //        }
        //    }
        //    return lista;
        //}
        //public static cRegla RecuperarReglaPorId(int pIdRegla)
        //{
        //    cRegla obj = null;
        //    DataSet dsResultado = capaSeguridad_base.GestiónRegla(pIdRegla, null, null, null, null, null, null, null, Constantes.cSQL_SELECT);
        //    if (dsResultado != null)
        //    {
        //        foreach (DataRow item in dsResultado.Tables["Regla"].Rows)
        //        {
        //            obj = new cRegla();
        //            if (item["rgl_codRegla"] != DBNull.Value)
        //            {
        //                obj.rgl_codRegla = Convert.ToInt32(item["rgl_codRegla"]);
        //            }
        //            if (item["rgl_Descripcion"] != DBNull.Value)
        //            {
        //                obj.rgl_Descripcion = item["rgl_Descripcion"].ToString();
        //            }
        //            if (item["rgl_PalabraClave"] != DBNull.Value)
        //            {
        //                obj.rgl_PalabraClave = item["rgl_PalabraClave"].ToString();
        //            }
        //            if (item["rgl_IsAgregarSoporta"] != DBNull.Value)
        //            {
        //                obj.rgl_IsAgregarSoporta = Convert.ToBoolean(item["rgl_IsAgregarSoporta"]);
        //            }
        //            if (item["rgl_IsEditarSoporta"] != DBNull.Value)
        //            {
        //                obj.rgl_IsEditarSoporta = Convert.ToBoolean(item["rgl_IsEditarSoporta"]);
        //            }
        //            if (item["rgl_IsEliminarSoporta"] != DBNull.Value)
        //            {
        //                obj.rgl_IsEliminarSoporta = Convert.ToBoolean(item["rgl_IsEliminarSoporta"]);
        //            }
        //            if (item["rgl_codReglaPadre"] != DBNull.Value)
        //            {
        //                obj.rgl_codReglaPadre = Convert.ToInt32(item["rgl_codReglaPadre"]);
        //            }
        //            break;
        //        }
        //    }
        //    return obj;
        //}
        //public static List<cCombo> RecuperarTodasReglas_Combo()
        //{
        //    List<cCombo> lista = new List<cCombo>();
        //    DataSet dsResultado = capaSeguridad_base.GestiónRegla(null, null, null, null, null, null, null, null, Constantes.cSQL_COMBO);
        //    if (dsResultado != null)
        //    {
        //        foreach (DataRow item in dsResultado.Tables["Regla"].Rows)
        //        {
        //            cCombo obj = new cCombo();
        //            if (item["rgl_codRegla"] != DBNull.Value)
        //            {
        //                obj.id = Convert.ToInt32(item["rgl_codRegla"]);
        //            }
        //            if (item["rgl_Descripcion"] != DBNull.Value)
        //            {
        //                obj.nombre = item["rgl_Descripcion"].ToString();
        //            }
        //            lista.Add(obj);
        //        }
        //    }
        //    return lista;
        //}
        public static ListaAcccionesRol RecuperarTodasAccionesPorIdRol(int pIdRol)
        {
            return DKbase.Util.RecuperarTodasAccionesPorIdRol(pIdRol);
        }

        //public static void InsertarActualizarRelacionRolRegla(int pIdRol, string pXML)
        //{
        //    DataSet dsResultado = capaSeguridad_base.GestiónRoleRegla(pIdRol, null, pXML, Constantes.cSQL_UPDATE);
        //}

       // public static List<capaDatos.ReglaPorRol> RecuperarRelacionRolReglasPorRol(int pIdRol)
        //{
        //    List<capaDatos.ReglaPorRol> listaResultado = new List<capaDatos.ReglaPorRol>();
        //    DataSet dsResultado = capaSeguridad_base.GestiónRoleRegla(pIdRol, null, null, Constantes.cSQL_SELECT);
        //    if (dsResultado != null)
        //    {
        //        foreach (DataRow item in dsResultado.Tables["RelacionRoleRegla"].Rows)
        //        {
        //            capaDatos.ReglaPorRol obj = new capaDatos.ReglaPorRol();
        //            obj.idRegla = Convert.ToInt32(item["rrr_codRegla"]);
        //            obj.idRelacionReglaRol = Convert.ToInt32(item["rrr_codRelacionRolRegla"]);
        //            obj.isActivo = Convert.ToBoolean(item["rrr_IsActivo"]);
        //            if (item["rrr_IsAgregar"] is DBNull)
        //            {
        //                obj.isAgregar = null;
        //            }
        //            else
        //            {
        //                obj.isAgregar = Convert.ToBoolean(item["rrr_IsAgregar"]);
        //            }
        //            if (item["rrr_IsEditar"] is DBNull)
        //            {
        //                obj.isEditar = null;
        //            }
        //            else
        //            {
        //                obj.isEditar = Convert.ToBoolean(item["rrr_IsEditar"]);
        //            }
        //            if (item["rrr_IsEliminar"] is DBNull)
        //            {
        //                obj.isEliminar = null;
        //            }
        //            else
        //            {
        //                obj.isEliminar = Convert.ToBoolean(item["rrr_IsEliminar"]);
        //            }
        //            listaResultado.Add(obj);
        //        }
        //    }
        //    return listaResultado;
        //}
        //
        public static List<int> RecuperarTodosIdReglasHijas(int pIdRegla, List<cRegla> pListaRegla)
        {
            List<int> listaHijas = new List<int>();
            if (pListaRegla != null)
            {
                List<cRegla> listaRegla = pListaRegla.Where(x => x.rgl_codReglaPadre == pIdRegla).ToList();
                foreach (cRegla item in listaRegla)
                {
                    listaHijas.Add(item.rgl_codRegla);
                }
            }
            return listaHijas;
        }
        public static List<capaDatos.ListaCheck> RecuperarTodasReglasPorNivel()
        {
            List<cRegla> listaReglaParametro = DKbase.Util.RecuperarTodasReglas(string.Empty);
            List<capaDatos.ListaCheck> listaResultado = new List<capaDatos.ListaCheck>();
            DataTable tabla = capaDatos.capaSeguridad.RecuperarTodasReglasPorNivel();
            for (int i = 0; i < tabla.Rows.Count; i++)
            {
                ListaCheck obj = new ListaCheck();

                obj.id = Convert.ToInt32(tabla.Rows[i]["rgl_codRegla"]);
                obj.descripcion = tabla.Rows[i]["rgl_Descripcion"].ToString();
                obj.palabra = tabla.Rows[i]["rgl_PalabraClave"].ToString();
                obj.Nivel = Convert.ToInt32(tabla.Rows[i]["Level"]);
                if (tabla.Rows[i]["rgl_codReglaPadre"] is DBNull)
                {
                    obj.idPadreRegla = null;
                }
                else
                {
                    obj.idPadreRegla = Convert.ToInt32(tabla.Rows[i]["rgl_codReglaPadre"]);
                }
                obj.listaIdHijas = RecuperarTodosIdReglasHijas(Convert.ToInt32(tabla.Rows[i]["rgl_codRegla"]), listaReglaParametro);
                obj.isGraficada = false;
                //
                if (Convert.ToBoolean(tabla.Rows[i]["rgl_IsAgregarSoporta"]))
                {
                    obj.checkAgregar = 0;
                }
                else
                {
                    obj.checkAgregar = -1;
                }
                if (Convert.ToBoolean(tabla.Rows[i]["rgl_IsEditarSoporta"]))
                {
                    obj.checkEditar = 0;
                }
                else
                {
                    obj.checkEditar = -1;
                }
                if (Convert.ToBoolean(tabla.Rows[i]["rgl_IsEliminarSoporta"]))
                {
                    obj.checkEliminar = 0;
                }
                else
                {
                    obj.checkEliminar = -1;
                }

                listaResultado.Add(obj);

            }

            return listaResultado;
        }
    
        public static List<cUsuario> RecuperarTodosUsuarios(string pFiltro)
        {
            List<cUsuario> lista = new List<cUsuario>();
            DataSet dsResultado = capaSeguridad_base.GestiónUsuario(null, null, null, null, null, null, null, null, null, null, null, null, pFiltro, Constantes.cSQL_SELECT);
            if (dsResultado != null)
            {
                foreach (DataRow item in dsResultado.Tables["Usuario"].Rows)
                {
                    lista.Add(DKbase.web.capaDatos.capaSeguridad_base.ConvertToUsuario(item));
                }
            }
            return lista;
        }
        //public static void CambiarEstadoUsuarioPorId(int pIdUsuario, int pIdEstado, int pIdUsuarioEnSession)
        //{
        //    capaSeguridad_base.GestiónUsuario(pIdUsuario, null, null, null, null, null, null, null, null, pIdUsuarioEnSession, Constantes.cACCION_CAMBIOESTADO, pIdEstado, null, Constantes.cSQL_ESTADO);
        //}
        //public static void CambiarContraseñaUsuario(int pIdUsuario, string pConstraseña, int? pIdUsuarioEnSession)
        //{
        //    //List<capaDatos.cUsuario> lista = new List<capaDatos.cUsuario>();
        //    DataSet dsResultado = capaSeguridad_base.GestiónUsuario(pIdUsuario, null, null, null, null, null, null, pConstraseña, null, pIdUsuarioEnSession, Constantes.cACCION_CAMBIOCONTRASEÑA, null, null, Constantes.cSQL_CAMBIOCONTRASEÑA);
        //}
        //public static void CambiarContraseñaUsuario(int pIdUsuario, string pConstraseña, int? pIdUsuarioEnSession)
        //{
        //    List<capaDatos.cUsuario> lista = new List<capaDatos.cUsuario>();
        //    DataSet dsResultado = capaSeguridad.GestiónUsuario(pIdUsuario, null, null, null, null, null, null, null, pIdUsuarioEnSession, null, null, null, Constantes.cSQL_CAMBIOCONTRASEÑA);
        //}  
    }
}