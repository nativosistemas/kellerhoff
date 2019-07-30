﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Kellerhoff.Codigo.capaDatos
{
    public class capaEF
    {
        public capaEF()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static List<tbl_HomeSlide> RecuperarTodasHomeSlide()
        {
            List<tbl_HomeSlide> resultado = null;
            try
            {
                KellerhoffEntities ctx = new KellerhoffEntities();     
                resultado = ctx.tbl_HomeSlide.OrderBy(x => x.hsl_orden).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return resultado;
        }
        public static bool? ActualizarImagenHomeSlide(int hsl_idHomeSlide, int idRecurso, int pTipo)
        {
            bool? resultado = null;
            try
            {
                tbl_HomeSlide o = null;
                KellerhoffEntities ctx = new KellerhoffEntities();
                o = ctx.tbl_HomeSlide.FirstOrDefault(x => x.hsl_idHomeSlide == hsl_idHomeSlide);
                if (pTipo == 1)
                    o.hsl_idRecursoImgPC = idRecurso;
                else if (pTipo == 2)
                    o.hsl_idRecursoImgMobil = idRecurso;
                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            { return null; }
            return resultado;
        }
        public static bool? InsertarActualizarHomeSlide(int hsl_idHomeSlide, string hsl_titulo, string hsl_descr, string hsl_descrHtml, string hsl_descrHtmlReducido, int hsl_tipo, int? hsl_idOferta, int? hsl_idRecursoDoc, int? hsl_idRecursoImgPC, int? hsl_idRecursoImgMobil)
        {
            bool? resultado = null;
            try
            {
                tbl_HomeSlide o = null;

                KellerhoffEntities ctx = new KellerhoffEntities();

                if (hsl_idHomeSlide == 0)
                {
                    o = ctx.tbl_HomeSlide.Create();
                    o.hsl_publicar = false;
                }
                else
                    o = ctx.tbl_HomeSlide.FirstOrDefault(x => x.hsl_idHomeSlide == hsl_idHomeSlide);
                o.hsl_titulo = hsl_titulo;
                o.hsl_descr = hsl_descr;
                o.hsl_descrHtml = hsl_descrHtml;
                o.hsl_descrHtmlReducido = hsl_descrHtmlReducido;
                o.hsl_tipo = hsl_tipo;
                o.hsl_fecha = DateTime.Now;
                o.hsl_idOferta = hsl_idOferta;
                o.hsl_idRecursoDoc = hsl_idRecursoDoc;
                //o.hsl_idRecursoImgPC = hsl_idRecursoImgPC;
                //o.hsl_idRecursoImgMobil = hsl_idRecursoImgMobil;
                if (hsl_idHomeSlide == 0)
                {
                    tbl_HomeSlide oOrden = ctx.tbl_HomeSlide.OrderByDescending(x => x.hsl_orden).FirstOrDefault();
                    if (oOrden != null)
                        o.hsl_orden = oOrden != null ? oOrden.hsl_orden.Value + 1 : 0;
                    else
                        o.hsl_orden = 0;
                    ctx.tbl_HomeSlide.Add(o);

                }
                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                return null;
            }
            return resultado;
        }
        public static bool? EliminarHomeSlide(int hsl_idHomeSlide)
        {
            bool? resultado = null;
            try
            {
                tbl_HomeSlide o = null;
                KellerhoffEntities ctx = new KellerhoffEntities();
                o = ctx.tbl_HomeSlide.FirstOrDefault(x => x.hsl_idHomeSlide == hsl_idHomeSlide);
                ctx.tbl_HomeSlide.Remove(o);
                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                return null;
            }
            return resultado;
        }
        public static bool? CambiarOrdenHomeSlide(int hsl_idHomeSlide, bool isSubir)
        {
            bool? resultado = null;
            try
            {
                tbl_HomeSlide o = null;
                tbl_HomeSlide oTemp = null;
                KellerhoffEntities ctx = new KellerhoffEntities();
                o = ctx.tbl_HomeSlide.FirstOrDefault(x => x.hsl_idHomeSlide == hsl_idHomeSlide);
                if (isSubir)
                    oTemp = ctx.tbl_HomeSlide.Where(x => x.hsl_orden > o.hsl_orden).OrderBy(x => x.hsl_orden).FirstOrDefault();
                else
                    oTemp = ctx.tbl_HomeSlide.Where(x => x.hsl_orden < o.hsl_orden).OrderByDescending(x => x.hsl_orden).FirstOrDefault();

                if (oTemp != null)
                {
                    int? ordenTemp = o.hsl_orden;
                    o.hsl_orden = oTemp.hsl_orden;
                    oTemp.hsl_orden = ordenTemp;
                }

                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                return null;
            }
            return resultado;
        }
        public static bool? CambiarPublicarHomeSlide(int hsl_idHomeSlide)
        {
            bool? resultado = null;
            try
            {
                tbl_HomeSlide o = null;
                KellerhoffEntities ctx = new KellerhoffEntities();
                o = ctx.tbl_HomeSlide.FirstOrDefault(x => x.hsl_idHomeSlide == hsl_idHomeSlide);
                o.hsl_publicar = !o.hsl_publicar;
                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            { return null; }
            return resultado;
        }
        public static bool? InsertarOfertaRating(int ofr_idOferta, int ofr_idCliente, bool ofr_isDesdeHome)
        {
            try
            {
                tbl_Oferta_Rating o = null;
                KellerhoffEntities ctx = new KellerhoffEntities();
                o = ctx.tbl_Oferta_Rating.Create();
                o.ofr_fecha = DateTime.Now;
                o.ofr_idCliente = ofr_idCliente;
                o.ofr_idOferta = ofr_idOferta;
                o.ofr_isDesdeHome = ofr_isDesdeHome;
                ctx.tbl_Oferta_Rating.Add(o);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public static bool? InsertarHomeSlideRating(int hsr_idHomeSlide, int? hsr_idCliente)
        //{
        //    try
        //    {
        //        tbl_HomeSlide_Rating o = null;
        //        KellerhoffEntities ctx = new KellerhoffEntities();
        //        o = ctx.tbl_HomeSlide_Rating.Create();
        //        o.hsr_fecha = DateTime.Now;
        //        o.hsr_idCliente = hsr_idCliente;
        //        o.hsr_idHomeSlide = hsr_idHomeSlide;
        //        ctx.tbl_HomeSlide_Rating.Add(o);
        //        ctx.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public static bool? SubirCountadorHomeSlideRating(int hsl_idHomeSlide)
        {
            bool? resultado = null;
            try
            {
                tbl_HomeSlide o = null;
                KellerhoffEntities ctx = new KellerhoffEntities();
                o = ctx.tbl_HomeSlide.FirstOrDefault(x => x.hsl_idHomeSlide == hsl_idHomeSlide);
                o.hsl_RatingCount = o.hsl_RatingCount + 1;
                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            { return null; }
            return resultado;
        }
        //public static List<tbl_Recall> RecuperarTodaReCall()
        //{
        //    List<tbl_Recall> resultado = null;
        //    try
        //    {
        //        KellerhoffEntities ctx = new KellerhoffEntities();
        //        resultado = ctx.tbl_Recall.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return resultado;
        //}
        public static List<tbl_Recall> RecuperarTodaReCall()
        {
            List<tbl_Recall> resultado = null;
            try
            {
                DataTable tb = capaLogRegistro.RecuperarTodaReCall_aux();
                if (tb != null)
                {
                    resultado = new List<tbl_Recall>();
                    foreach (DataRow item in tb.Rows)
                    {
                        tbl_Recall o = new tbl_Recall();
                        if (item.Table.Columns.Contains("rec_id") && item["rec_id"] != DBNull.Value)
                        {
                            o.rec_id = Convert.ToInt32(item["rec_id"]);
                        }
                        if (item.Table.Columns.Contains("rec_titulo") && item["rec_titulo"] != DBNull.Value)
                        {
                            o.rec_titulo = Convert.ToString(item["rec_titulo"]);
                        }
                        if (item.Table.Columns.Contains("rec_descripcion") && item["rec_descripcion"] != DBNull.Value)
                        {
                            o.rec_descripcion = Convert.ToString(item["rec_descripcion"]);
                        }
                        if (item.Table.Columns.Contains("rec_descripcionReducido") && item["rec_descripcionReducido"] != DBNull.Value)
                        {
                            o.rec_descripcionReducido = Convert.ToString(item["rec_descripcionReducido"]);
                        }
                        if (item.Table.Columns.Contains("rec_descripcionHTML") && item["rec_descripcionHTML"] != DBNull.Value)
                        {
                            o.rec_descripcionHTML = Convert.ToString(item["rec_descripcionHTML"]);
                        }
                        if (item.Table.Columns.Contains("rec_FechaNoticia") && item["rec_FechaNoticia"] != DBNull.Value)
                        {
                            o.rec_FechaNoticia = Convert.ToDateTime(item["rec_FechaNoticia"]);
                        }
                        if (item.Table.Columns.Contains("rec_FechaFinNoticia") && item["rec_FechaFinNoticia"] != DBNull.Value)
                        {
                            o.rec_FechaFinNoticia = Convert.ToDateTime(item["rec_FechaFinNoticia"]);
                        }
                        if (item.Table.Columns.Contains("rec_Fecha") && item["rec_Fecha"] != DBNull.Value)
                        {
                            o.rec_Fecha = Convert.ToDateTime(item["rec_Fecha"]);
                        }
                        if (item.Table.Columns.Contains("rec_visible") && item["rec_visible"] != DBNull.Value)
                        {
                            o.rec_visible = Convert.ToBoolean(item["rec_visible"]);
                        }
                        resultado.Add(o);
                    }
                }
                //KellerhoffEntities ctx = new KellerhoffEntities();
                //resultado = ctx.tbl_Recall.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
            return resultado;
        }
        public static bool? InsertarActualizarReCall(int rec_id, string rec_titulo, string rec_descripcion, string rec_descripcionReducido, string rec_descripcionHTML, DateTime? rec_FechaNoticia, DateTime? rec_FechaFinNoticia)
        {
            bool? resultado = null;
            try
            {
                tbl_Recall o = null;

                KellerhoffEntities ctx = new KellerhoffEntities();

                if (rec_id == 0)
                {
                    o = ctx.tbl_Recall.Create();
                    o.rec_visible = true;
                }
                else
                    o = ctx.tbl_Recall.FirstOrDefault(x => x.rec_id == rec_id);
                o.rec_titulo = rec_titulo;
                o.rec_descripcion = rec_descripcion;
                o.rec_descripcionReducido = rec_descripcionReducido;
                o.rec_descripcionHTML = rec_descripcionHTML;
                o.rec_FechaNoticia = rec_FechaNoticia;
                o.rec_FechaFinNoticia = rec_FechaFinNoticia;
                o.rec_Fecha = DateTime.Now;
                // o.hsl_idRecursoDoc = hsl_idRecursoDoc;
                //o.hsl_idRecursoImgPC = hsl_idRecursoImgPC;
                //o.hsl_idRecursoImgMobil = hsl_idRecursoImgMobil;
                if (rec_id == 0)
                {
                    ctx.tbl_Recall.Add(o);
                }
                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                return null;
            }
            return resultado;
        }
        public static bool? CambiarPublicarReCall(int rec_id)
        {
            bool? resultado = null;
            try
            {
                tbl_Recall o = null;
                KellerhoffEntities ctx = new KellerhoffEntities();
                o = ctx.tbl_Recall.FirstOrDefault(x => x.rec_id == rec_id);
                o.rec_visible = !o.rec_visible;
                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            { return null; }
            return resultado;
        }
        public static bool? EliminarReCall(int rec_id)
        {
            bool? resultado = null;
            try
            {
                tbl_Recall o = null;
                KellerhoffEntities ctx = new KellerhoffEntities();
                o = ctx.tbl_Recall.FirstOrDefault(x => x.rec_id == rec_id);
                ctx.tbl_Recall.Remove(o);
                ctx.SaveChanges();
                resultado = true;
            }
            catch (Exception ex)
            {
                return null;
            }
            return resultado;
        }
    }

    //public partial class tbl_HomeSlide
    //{
    //    //[NotMapped]
    //    // [JsonProperty("hsl_fechaToString")]
    //    public string hsl_fechaToString
    //    {

    //        get { return this.hsl_fecha.ToShortDateString(); }
    //        set { }
    //    }
    //}

}
namespace Kellerhoff.Codigo
{
    public partial class tbl_HomeSlide
    {
        //[NotMapped]
        // [JsonProperty("hsl_fechaToString")]
        public string hsl_fechaToString
        {

            get { return this.hsl_fecha.ToShortDateString(); }
            set { }
        }
    }
    public partial class tbl_Recall
    {
        //[NotMapped]
        // [JsonProperty("hsl_fechaToString")]
        public string rec_FechaNoticiaToString
        {

            get { return this.rec_FechaNoticia == null ? string.Empty : this.rec_FechaNoticia.Value.ToShortDateString(); }
            set { }
        }
        public string descripcionReducidoMostrar
        {

            get { return this.rec_descripcionReducido == null ? string.Empty : Kellerhoff.Codigo.clases.Generales.Texto.CortarBajada(this.rec_descripcionReducido, 120); }
            set { }
        }
        public string descripcionReducidoMostrarMediano
        {

            get { return this.rec_descripcionReducido == null ? string.Empty : Kellerhoff.Codigo.clases.Generales.Texto.CortarBajada(this.rec_descripcionReducido,380); }
            set { }
        }
        private string _arc_nombre = null;
        public string arc_nombre {
            get { return _arc_nombre; }
            set { _arc_nombre = value; }
        }
    }
}