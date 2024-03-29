﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master/home.Master" AutoEventWireup="true" CodeBehind="registracion.aspx.cs" Inherits="Kellerhoff.home.registracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../includes/home/js/registracion.js?n=1"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <section class="section-gris-claro">
        <div class="container">
            <div class="row">
          	<div class="col-md-6 col-md-offset-0 col-sm-12 clearfix">
                <h2 class="color_emp_nd">REGISTRACI&Oacute;N</h2>
                <div class="clear25"></div>
				<p><span class="color_emp_st">En caso de que Ud. no sea cliente, deberá tildar la opci&oacute;n "No"</span> y completar los campos obligatorios para la solicitud de apertura de cuenta.</p>
            	<div class="clear25"></div>
			</div>
                 
            <div class="clear"></div>	
                <div id="successOk" class="alert"></div>
          	<div class="col-lg-6 col-md-6 col-xs-12 col_registro_left">
                <div class="clear5"></div>
                <label class="cliente">¿Es cliente?</label>
                <div class="radio radio-info radio-inline">
                    <input type="radio" name="radioEsCliente" value="Si" checked="checked"   />
                    <label for="radioEsClienteSi">SI</label>
                </div>
                <div class="radio radio-inline">
                    <input type="radio" name="radioEsCliente" value="No"   />
                    <label for="radioEsClienteNo">NO</label>
                </div>                
                <div class="clear15"></div>
                <input name="txtTitularFarmacia" id="txtTitularFarmacia" class="form-control" type="text" placeholder="Nombre Titular de la Farmacia (*)" value="" required="required"   />
                <div class="clear20"></div>
                <input name="txtUsuarioWeb" id="txtUsuarioWeb" class="form-control" type="text" placeholder="Usuario para ingresar a la web" value=""    />
                <div class="clear20"></div>
                <input name="txtFechaNacimiento" id="txtFechaNacimiento" class="form-control" type="text" placeholder="Fecha nacimiento" value="" data-mask="99-99-9999"  />
                <div class="clear20"></div>
                <input name="txtContacto" id="txtContacto" class="form-control" type="text" placeholder="Nombre de Contacto" value=""  />
                <div class="clear20"></div>
                <input name="txtNombreFarmacia" id="txtNombreFarmacia" class="form-control" type="text" placeholder="Nombre de la farmacia (*)" value="" required="required"  />
                <div class="clear10"></div>
                <div class="radio-checkbox">
                    <input id="txtEs24" class="checkbox" type="checkbox" value=""  />
                    <label for="txtEs24" >es 24hs</label>
                </div>
                <div class="clear10"></div>
                <input name="txtEmail" id="txtEmail" class="form-control" type="email" placeholder="E-mail (*)" value="" required="required"   />
			</div>
            <div class="clear20 visible-xs visible-sm"></div>
          	<div class="col-lg-6 col-md-6 col-xs-12 col_registro_right">
                <input name="txtTel" id="txtTel" class="form-control" type="tel" placeholder="Tel&eacute;fono (*)" value=""  required="required"  />
                <div class="clear20"></div>
                <input name="txtDireccion" id="txtDireccion" class="form-control" type="text" placeholder="Direcci&oacute;n" value=""  />
                <div class="clear20"></div>
                <input name="txtCodigoPostal" id="txtCodigoPostal" class="form-control" type="text" placeholder="C&oacute;digo postal" value=""  />
                <div class="clear20"></div>
                <input name="txtLocalidad" id="txtLocalidad" class="form-control" type="text" placeholder="Localidad (*)" value="" required="required"  />
                <div class="clear20"></div>
                <input name="txtProvincia" id="txtProvincia" class="form-control" type="text" placeholder="Provincia" value=""  />
                <div class="clear20"></div>
                <input name="txtPaginaWeb" id="txtPaginaWeb" class="form-control" type="text" placeholder="P&aacute;gina web" value=""  />
                <div class="clear20"></div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 no-padding">
	                <div class="g-recaptcha float-left" data-sitekey="6LdTLCMUAAAAABbaNMb2Sv-jxXRM6D35GrcF7WeX"></div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 no-padding">
                    <div class="clear20 visible-xs"></div>
                    <div class="oblig float-right hidden-xs">(*) Campos obligatorios</div>
                    <div class="oblig float-left visible-xs" style="margin-top:10px">(*) Campos obligatorios</div>
                    <div class="clear20 hidden-xs"></div>
                    <button class="btn_emp float-right" type="submit" onclick="return GrabarRegistracion(); ">ENVIAR</button>
                </div>
	        </div>
        </div>
            <div id="success" class="alert"></div>


        </div>
    </section>
      <%  AgregarHtmlOculto(); %>
  <script src='https://www.google.com/recaptcha/api.js'></script>
    <script>
	$('#inlineRadio2').click(function() {
        $("#solicitud").css("display","block");
 	})	
	$('#inlineRadio1').click(function() {
        $("#solicitud").css("display","none");
 	})	
	</script>
</asp:Content>

