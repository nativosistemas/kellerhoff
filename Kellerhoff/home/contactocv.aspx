﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master/homeContacto.master" AutoEventWireup="true" CodeBehind="contactocv.aspx.cs" Inherits="Kellerhoff.home.contactocv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../includes/home/js/contacto.js?n=1"></script>
    <script type="text/javascript">

        jQuery(document).ready(function () {

            var result_cv = null;
            if (result_cv == null) {
                result_cv = $('#hiddenResultadoCV').val();
                if (typeof result_cv == 'undefined') {
                    result_cv = null;
                }
            }
            if (result_cv != null) {
                if (result_cv == 'Ok') {
                      $('#form1')[0].reset();
                    $('#form1').trigger("reset");
                    $('#success').removeClass('alert-danger');
                    $('#success').addClass('alert-success');
                    $('#success').html(strMesajeOk_contactoCV);
                    $('#hiddenResultadoCV').val('');
                }
                else {
                    $('#success').removeClass('alert-success');
                    $('#success').addClass('alert-danger');
                    $('#success').html(result_cv);
                }
            }

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="section-gris-claro" id="cv">
        <div class="image-container col-md-6 col-sm-12 pull-right clearfix hidden-sm hidden-xs">
            <div class="background-image-holder-right">
                <img src="../img/home/img_rrhh_new.jpg" alt="" class=" float-right img-responsive" />
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-md-5 col-md-offset-0 col-sm-12 clearfix">
                    <div id="success" class="alert"></div>
                    <h2 class="color_emp_nd">RRHH</h2>
                    <div class="clear25"></div>
                    <p>Para enviar su curriculum vitae, complete el formulario.</p>
                    <input name="nombre_cv" id="nombre_cv" class="form-control" type="text" placeholder="Nombre y Apellido (*)" value="" required="required" />
                    <div class="clear20"></div>
                    <input name="dni_cv" id="dni_cv" class="form-control" type="text" placeholder="DNI (*)" value="" required="required" />
                    <div class="clear20"></div>
                    <input name="email_cv" id="email_cv" class="form-control" type="email" placeholder="E-mail (*)" value="" required="required" />
                      <div class="clear20"></div>
                    <select name="puesto_cv" id="puesto_cv"  class="form-control" required="required" >
                        <option value="" hidden="hidden" style="color:#d7c4c6">Puesto al que se postula (*)</option>
                        <option value="VENTAS" style="color:white">VENTAS</option>
                        <option value="ADMINISTRACIÓN">ADMINISTRACIÓN</option>
                        <option value="CONTABLES">CONTABLES</option>
                        <option value="SISTEMAS">SISTEMAS</option>
                        <option value="DEPOSITO">DEPOSITO</option>
                        <option value="TOMA DE PEDIDOS">TOMA DE PEDIDOS</option>
                    </select>
                    <div class="clear20"></div>
                    <select name="sucursal_cv" id="sucursal_cv"  class="form-control" required="required" >
                        <option value="" hidden="hidden" style="color:#d7c4c6">Sucursal a la que se postula (*)</option>
                        <option value="ROSARIO" style="color:white">ROSARIO</option>
                        <option value="CHAÑAR LADEADO">CHAÑAR LADEADO</option>
                        <option value="CONCEPCIÓN DEL URUGUAY">CONCEPCIÓN DEL URUGUAY</option>
                        <option value="CONCORDIA">CONCORDIA</option>
                        <option value="CÓRDOBA">CÓRDOBA</option>
                        <option value="SAN NICOLAS">SAN NICOLAS</option>
                        <option value="SANTA FE">SANTA FE</option>
                        <option value="VILLA MARÍA">VILLA MARÍA</option>
                        <option value="RÍO CUARTO">RÍO CUARTO</option>
                    </select>
                    <div class="clear20"></div>
                    <textarea name="cuerpo_cv" id="cuerpo_cv" class="form-control" placeholder="Comentario (*)" required="required"></textarea>
                    <div class="clear20"></div>
                    <input name="file_cv" id="file_cv" class="form-control fileCv" type="file" placeholder="Archivo (*)" value="" required="required" />
                    <div class="clear20"></div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 no-padding">
                        <div class="g-recaptcha float-left" data-sitekey="6LdTLCMUAAAAABbaNMb2Sv-jxXRM6D35GrcF7WeX"></div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 no-padding">
                        <div class="clear20 visible-xs"></div>
                        <div class="oblig float-right hidden-xs">(*) Campos obligatorios</div>
                        <div class="oblig float-left visible-xs" style="margin-top: 10px">(*) Campos obligatorios</div>
                        <div class="clear20 hidden-xs"></div>
                        <button class="btn_emp float-right" type="submit">ENVIAR</button>
                    </div>
                </div>
            </div>

        </div>
    </section>
    <% AgregarHtmlOculto(); %>
</asp:Content>

