using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Kellerhoff.Codigo.clases;

namespace Kellerhoff.Codigo.capaDatos
{
    public class cNombreArchivos : DKbase.web.capaDatos.cNombreArchivos
    {
    }
    public class capaRecurso
    {
        public static DataSet GestiónArchivo(int? arc_codRecurso, int? arc_codRelacion, string arc_galeria, string arc_tipo, string arc_mime, string arc_nombre, string arc_titulo, string arc_descripcion, string arc_hash, int? arc_codUsuarioUltMov, int? arc_estado, int? arc_accion, string filtro, string accion)
        {
            return DKbase.web.capaDatos.capaRecurso_base.GestiónArchivo(arc_codRecurso, arc_codRelacion, arc_galeria, arc_tipo, arc_mime, arc_nombre, arc_titulo, arc_descripcion, arc_hash, arc_codUsuarioUltMov, arc_estado, arc_accion, filtro, accion);
        }
    
        public static string obtenerMIME(string pExtencion)
        {
            string resultado = string.Empty;

            switch (pExtencion.Trim().ToLower())
            {
                case "jpe":
                    return Constantes.cMIME_jpe;
                //break;
                case "jpeg":
                    return Constantes.cMIME_jpeg;
                //break;
                case "jpg":
                    return Constantes.cMIME_jpg;
                //break;                
                case "doc":
                    return Constantes.cMIME_doc;
                //break;
                case "docx":
                    return Constantes.cMIME_docx;
                //break;
                case "xls":
                    return Constantes.cMIME_xls;
                //break;
                case "xlsx":
                    return Constantes.cMIME_xlsx;
                case "gif":
                    return Constantes.cMIME_gif;
                //break;
                case "png":
                    return Constantes.cMIME_png;
                //break;
                case "ai":
                    return Constantes.cMIME_ai;
                //break;
                case "pdf":
                    return Constantes.cMIME_pdf;
                //break;
                case "psd":
                    return Constantes.cMIME_psd;
                //break;
                case "vsd":
                    return Constantes.cMIME_vsd;
                //break;
                default:
                    break;
            }
            return resultado;
        }
      

    }
}