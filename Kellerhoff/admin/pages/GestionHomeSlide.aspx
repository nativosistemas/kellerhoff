﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master/BaseAdminJs3.Master" AutoEventWireup="true" CodeBehind="GestionHomeSlide.aspx.cs" Inherits="Kellerhoff.admin.pages.GestionHomeSlide" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="../../includes/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../../includes/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="../../includes/js/GestionHomeSlide.js?n=1" type="text/javascript"></script>

        <script type="text/javascript">
        jQuery(document).ready(function () {
            RecuperarTodasSlide();
        });
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <h2 class="sub-header">Slide</h2>
    <button type="button" class="btn btn-warning" onclick="return EditarSlide(0);" >Agregar</button>
     <div id="divContenedorGrilla"></div>
      <%AgregarHtmlOculto(); %>
</asp:Content>
