﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.Master" AutoEventWireup="true" CodeBehind="GestionTiposEnviosSucursal_Reparto.aspx.cs" Inherits="Kellerhoff.admin.pages.GestionTiposEnviosSucursal_Reparto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../includes/js/GestionReparto.js?n=1" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            CargarDatosReparto();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="sub-header">Tipo Envío (excepción)</h2>
    <button type="button" class="btn btn-info" onclick="return VolverGestionTiposEnviosSucursal();">Volver</button>
    <div class="form-group">
        <label for="txtSucursalDependiente">Sucursal y Sucursal dependiente:</label>
        <input type="text" id="txtSucursalDependiente" name="txtSucursalDependiente" readonly="readonly" />
    </div>
    <div class="form-group">
        <label for="txtTipoEnvioCliente">Tipo envió cliente:</label>
        <input type="text" id="txtTipoEnvioCliente" name="txtTipoEnvioCliente" readonly="readonly" />
    </div>
    <div class="form-group">
        <label for="txtTipoEnvio">Tipo Envío:</label>
        <div id="divTipoEnvio"></div>
    </div>
        <div class="form-group">
        <label>Excepciones:</label>
<div id="divExcepciones"></div>
    </div>
    <div class="form-group">
        <label>Excepciones por Código de repartos:</label>

    </div>
    <div id="divMasterContenedorGeneralFondo" class="cssContenedorGeneralFondo"></div>
    <table>
        <tr>
            <td style="padding-right: 20px;">

                <div class="form-group">
                    <label for="cmdCodigoRepartos">Código de repartos:</label>
                    <select id="cmdCodigoRepartos" class="selectpicker form-control" onchange="onchangeCodigoRepartos()">
                    </select>
                </div>
                &nbsp;&nbsp;
            </td>
            <td style="padding-right: 20px;">
                <div class="form-group">
                    <label for="cmdTipoEnvio">Tipo Envío:</label>
                    <select id="cmdTipoEnvio" class="selectpicker form-control">
                    </select>
                </div>
                &nbsp;&nbsp;
            </td>
            <td>
                <div class="form-group">
                    <label for="cmdTipoEnvioMostrar">Tipo Envío a mostrar:</label>
                    <select id="cmdTipoEnvioMostrar" class="selectpicker form-control" size="4">
                    </select>
                </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="padding-right: 20px;">
                <button type="button" class="btn btn-warning" onclick="return AgregarRepartoExcepcion();">Agregar</button>
            </td>
            <td>
                <button type="button" class="btn btn-info" onclick="return EliminarRepartoExcepcion();">Eliminar</button></td>
        </tr>
    </table>

    <div id="divContenedorGrilla"></div>
    <%AgregarHtmlOculto(); %>
</asp:Content>
