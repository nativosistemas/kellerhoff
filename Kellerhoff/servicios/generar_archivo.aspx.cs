using Kellerhoff.Codigo.capaDatos;
using Kellerhoff.Codigo.clases;
using Kellerhoff.Codigo.clases.Generales;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kellerhoff.servicios
{
    public partial class generar_archivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.ContentType = "text/plain";
            string factura = Request.QueryString["factura"];
            string rutaTemporal = Constantes.cRaizArchivos + @"\archivos\facturas\";

            DirectoryInfo DIR = new DirectoryInfo(rutaTemporal);
            if (!DIR.Exists)
            {
                DIR.Create();
            }
            string nombreTXT = GrabarFacturaTXT(rutaTemporal, factura);
            if (nombreTXT != string.Empty)
            {
                String path = Constantes.cRaizArchivos + @"/archivos/facturas/" + nombreTXT;

                System.IO.FileInfo toDownload = new System.IO.FileInfo(path);

                if (toDownload.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name);
                    Response.AddHeader("Content-Length", toDownload.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(Constantes.cRaizArchivos + @"/archivos/facturas/" + nombreTXT);
                    Response.End();
                }
            }
        }
        public string GrabarFacturaTXT(string pRuta, string pFactura)
        {
            string resultado = string.Empty;
            if (HttpContext.Current.Session["clientesDefault_Cliente"] != null && pFactura != null)
            {
                ServiceReferenceDLL.cFactura objFactura = WebService.ObtenerFactura(pFactura, ((cClientes)HttpContext.Current.Session["clientesDefault_Cliente"]).cli_login);
                if (objFactura != null)
                {
                    string nombreArchivoTXT = string.Empty;
                    string fechaArchivoTXT = string.Empty;
                    if (objFactura.Fecha != null)
                    {
                        fechaArchivoTXT = ((DateTime)objFactura.Fecha).Day.ToString("00") + ((DateTime)objFactura.Fecha).Month.ToString("00") + ((DateTime)objFactura.Fecha).Year.ToString("0000");
                    }
                    else
                    {
                        fechaArchivoTXT = "00" + "00" + "0000";
                    }
                    nombreArchivoTXT = "Kell" + fechaArchivoTXT + "-" + pFactura + ".txt";
                    resultado = nombreArchivoTXT;
                    System.IO.StreamWriter FAC_txt = new System.IO.StreamWriter(pRuta + nombreArchivoTXT, false, System.Text.Encoding.UTF8);
                    //[1] eeeeeeeedd (e - Entero / d - Decimal)
                    //1 número C(13) 
                    //2 fecha N(8) ddmmyyyy 
                    //3 monto total N(10) [1] 
                    //4 monto exento N(10) [1] 
                    //5 monto gravado N(10) [1] 
                    //6 monto IVA inscripto N(10) [1] 
                    //7 monto IVA no inscripto N(10) [1] 
                    //8 monto percepción DGR N(10) [1] 
                    //9 descuento especial N(10) [1] 
                    //10 descuento netos N(10) [1] 
                    //11 descuento perfumería N(10) [1] 
                    //12 descuento web N(10) [1] 
                    //13 Monto Percepcion Municipal N(10) [1] 
                    string strCabeceraFAC = string.Empty;
                    // número C(13) 
                    strCabeceraFAC += objFactura.Numero.PadRight(13, ' ');
                    // Fecha
                    if (objFactura.Fecha != null)
                    {
                        strCabeceraFAC += ((DateTime)objFactura.Fecha).Day.ToString("00") + ((DateTime)objFactura.Fecha).Month.ToString("00") + ((DateTime)objFactura.Fecha).Year.ToString("0000");
                    }
                    else
                    {
                        strCabeceraFAC += "00" + "00" + "0000";
                    }
                    // fin fecha
                    //monto total N(10) [1]        //eeeeeeeedd
                    string montoTotal = string.Empty;
                    //  var fdf = Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoTotal).ToString());
                    montoTotal += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoTotal).ToString()).PadLeft(8, '0');
                    montoTotal += Numerica.ParteDecimal(objFactura.MontoTotal).ToString().PadLeft(2, '0');
                    strCabeceraFAC += montoTotal;
                    // fin monto total N(10) [1] 

                    //4 monto exento N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoExento).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.MontoExento).ToString().PadLeft(2, '0');
                    //5 monto gravado N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoGravado).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.MontoGravado).ToString().PadLeft(2, '0');
                    //6 monto IVA inscripto N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoIvaInscripto).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.MontoIvaInscripto).ToString().PadLeft(2, '0');
                    //7 monto IVA no inscripto N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoIvaNoInscripto).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.MontoIvaNoInscripto).ToString().PadLeft(2, '0');
                    //8 monto percepción DGR N(10) [1] 
                    //strCabeceraFAC += Math.Truncate(objFactura.MontoPercepcionDGR).ToString().PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.MontoPercepcionDGR).ToString().PadRight(2, '0');
                    //string parteEntera = Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoPercepcionDGR).ToString()).PadLeft(8, '0');
                    // string parteDecimal = Numerica.ParteDecimal(objFactura.MontoPercepcionDGR).ToString().PadLeft(2, '0');
                    // string juntos = parteEntera + parteDecimal;
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoPercepcionDGR).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.MontoPercepcionDGR).ToString().PadLeft(2, '0');
                    //9 descuento especial N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.DescuentoEspecial).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.DescuentoEspecial).ToString().PadLeft(2, '0');
                    //10 descuento netos N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.DescuentoNetos).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.DescuentoNetos).ToString().PadLeft(2, '0');
                    //11 descuento perfumería N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.DescuentoPerfumeria).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.DescuentoPerfumeria).ToString().PadLeft(2, '0');
                    //12 descuento web N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.DescuentoWeb).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.DescuentoWeb).ToString().PadLeft(2, '0');
                    //13 Monto Percepcion Municipal N(10) [1] 
                    strCabeceraFAC += Numerica.AgregarSeparaciónDeMiles(Math.Truncate(objFactura.MontoPercepcionMunicipal).ToString()).PadLeft(8, '0') + Numerica.ParteDecimal(objFactura.MontoPercepcionMunicipal).ToString().PadLeft(2, '0');


                    FAC_txt.WriteLine(strCabeceraFAC);

                    foreach (ServiceReferenceDLL.cFacturaDetalle item in objFactura.lista)
                    {
                        if (item.Troquel != null)
                        {
                            if (item.Troquel != string.Empty)
                            {
                                //If NOT ISNULL(Importe)
                                if (item.Importe != null)
                                {
                                    if (item.Importe.Trim() != string.Empty)
                                    {
                                        string detalleFAC = string.Empty;
                                        //Nro. Campo Tipo Comentario
                                        //1 código de barras producto C(13)
                                        //2 descripción producto C(60)
                                        //3 cantidad N(5)
                                        //4 característica C(1)
                                        //Espacio en blanco - Sin característica
                                        //F - Farmabono
                                        //D - Tarjeta D
                                        //C - Colfacor
                                        //B - Bonos CIL
                                        //P - Bonos PAP
                                        //$ - Ofertas
                                        //T - Transfer
                                        //5 neto N(1) 0 - Normail / 1 - Neto + IVA
                                        //6 precio público N(10) [1]
                                        //7 precio unitario N(10) [1]
                                        //8 importe N(10) [1]   //eeeeeeeedd
                                        cProductos producto = WebService.RecuperarProductoPorNombre(item.Descripcion);
                                        bool isNoTieneCodigoBarra = true;//código de barras producto C(13)
                                        if (producto != null)
                                        {
                                            if (producto.pro_codigobarra != null)
                                            {
                                                isNoTieneCodigoBarra = false;
                                                detalleFAC += producto.pro_codigobarra.PadRight(13, ' ');
                                            }
                                        }
                                        if (isNoTieneCodigoBarra)
                                        {
                                            detalleFAC += " ".PadRight(13, ' ');
                                        }
                                        detalleFAC += item.Descripcion.PadRight(60, ' ');
                                        detalleFAC += item.Cantidad.PadLeft(5, '0');
                                        if (item.Caracteristica == null)
                                        {
                                            detalleFAC += " ";
                                        }
                                        else
                                        {
                                            if (item.Caracteristica == string.Empty)
                                            {
                                                detalleFAC += " ";
                                            }
                                            else
                                            {
                                                detalleFAC += item.Caracteristica.PadLeft(1, ' ');
                                            }
                                        }
                                        if (producto != null)
                                        {
                                            detalleFAC += producto.pro_neto ? "1" : "0"; // Neto --- neto N(1) 0 - Normail / 1 - Neto + IVA                            
                                        }
                                        else
                                        {
                                            detalleFAC += " ";
                                        }
                                        detalleFAC += Numerica.AgregarSeparaciónDeMilesConDecimal(item.PrecioPublico);
                                        detalleFAC += Numerica.AgregarSeparaciónDeMilesConDecimal(item.PrecioUnitario);
                                        detalleFAC += Numerica.AgregarSeparaciónDeMilesConDecimal(item.Importe);

                                        //resultado += detalleFAC + "\n";
                                        FAC_txt.WriteLine(detalleFAC);
                                        //listaResultado.Add(resultado);

                                    }//   if (item.Importe.Trim() != string.Empty) { 
                                }//     if (item.Importe != null) { 

                            }// fin if (item.Troquel != string.Empty)

                        }// fin if (item.Troquel != null)
                    }
                    FAC_txt.Close();
                }
            }

            return resultado;
        }
    }
}