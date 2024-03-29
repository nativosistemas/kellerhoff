﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master/home.Master" AutoEventWireup="true" CodeBehind="contactoCtaCte.aspx.cs" Inherits="Kellerhoff.home.contactoCtaCte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript"  src="../includes/home/js/jquery/modernizr.js"></script>
    	<script type="text/javascript" src="../includes/home/js/jquery/timeline.js"></script>
	<script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        tr {
            cursor:default !important;
        }
        .table>tbody>tr:hover {
            background-color: transparent !important;
        }
    </style>
    <div class="row">
        <div class="col-xs-12 col-sm-10 col-sm-offset-1 col-md-6 col-md-offset-3">
            <div class="clear30 visible-xs"></div>
            <h3>WhatsApp comprobantes de pago </h3>
            <div class="clear30"></div>
            <table class="table">
                <thead class="thead-dark" style="font-size:1em">
                    <tr>
                        <th style="font-size:1.35em !important">
                            Sucursal
                        </th>
                        <th style="font-size:1.35em !important">
                            Whatsapp Pagos
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            CASA CENTRAL / SAN NICOLÁS
                        </td>
                        <td>
                            + 54 9 3413163291
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CHAÑAR LADEADO
                        </td>
                        <td>
                            + 54 9 3468648933
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CONCEPCIÓN
                        </td>
                        <td>
                            + 54 9 3442567240
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CONCORDIA
                        </td>
                        <td>
                            + 54 9 3442567240
                        </td>
                    </tr>
                    <tr>
                        <td>
                            CÓRDOBA
                        </td>
                        <td>
                            + 54 9 3516523189
                        </td>
                    </tr>
                    <tr>
                        <td>
                            SANTA FE
                        </td>
                        <td>
                            + 54 9 3424070448
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>Para adjuntar la foto de sus pagos, en todos los casos le solicitamos que sea una copia clara y legible, debiendo constar los datos de:</p>
            <ul>
                <li>Fecha de operación.</li>
                <li>Número de comprobante.</li>
                <li>Número de cuenta.</li>
                <li>Monto.</li>
            </ul>
        </div>
    </div>
    <div class="clear100 visible-lg "></div>
    <div class="clear30 visible-md"></div>

</asp:Content>

