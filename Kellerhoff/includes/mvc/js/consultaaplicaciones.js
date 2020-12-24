var TipoCbte = "",
    NroCbte = "",
    Aplicaciones = "",
    Movimiento = "",
    Total = 0,
    campoActual = "";

var colTipoCbte = [
    "Factura A",
    "Factura B",
    "Nota de Crédito A",
    "Nota de Crédito B",
    "Nota de Débito A",
    "Nota de Débito B",
    "Recibo",
    "Resumen"
];

$(document).ready(function () {
    $("#cboTipoCbte").focus();
    campoActual = "cboTipoCbte";

    $('body').keypress(function (e) {
        if (e.which == 13 || e.which == 9) {
            if ($("#modalModulo").is(":visible")) {
                modalModuloHide();
                $("#" + campoActual).val("");
                $("#" + campoActual).focus();
            }
        }
    });

    $("#modalModulo").click(function () {
        modalModuloHide();
        $("#" + campoActual).val("");
        $("#" + campoActual).focus();
    });

    $(".fa-times").click(function () {
        modalModuloHide();
        $("#" + campoActual).val("");
        $("#" + campoActual).focus();
    });

    $("#cboTipoCbte").change(function (e) {
        ControlarSesion();
        var Tipo = $(this).val().trim();
        var obj = $("#TipoCbteValues").find("option[value='" + Tipo + "']");
        if (obj != null && obj.length > 0) {
            var TipoId = obj[0].dataset.id;
            // console.log(TipoCbte);
            switch (TipoId) {
                case "1":
                    TipoCbte = "FAC";
                    NroCbte = "A";
                    break;
                case "2":
                    TipoCbte = "FAC";
                    NroCbte = "B";
                    break;
                case "3":
                    TipoCbte = "NCR";
                    NroCbte = "A";
                    break;
                case "4":
                    TipoCbte = "NCR";
                    NroCbte = "B";
                    break;
                case "5":
                    TipoCbte = "NDE";
                    NroCbte = "A";
                    break;
                case "6":
                    TipoCbte = "NDE";
                    NroCbte = "B";
                    break;
                case "7":
                    TipoCbte = "REC";
                    NroCbte = "X";
                    break;
                case "8":
                    TipoCbte = "RES";
                    NroCbte = "X";
                    break;
                default:
                    console.log(obj[0].dataset.id)
                    mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>Seleccione un Tipo de Comprobante válido</h5>");
                    break;
                    return false;
            }
            setTimeout(function () {
                campoActual = "txtNroComprobante";
                $("#txtNroComprobante").focus();
            }, 100);
        } else {
            mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>Seleccione un Tipo de Comprobante válido</h5>");
            // $(".fa.fa-times").hide();
        }
    });

    $("#txtNroComprobante").keypress(function (e) {
        if (e.which == 13 || e.which == 9) {
            NroCbte = NroCbte.slice(0, 1);
            ControlarSesion();
            campoActual = $(this).attr("id");
            NroCbte += ('000000000000' + $('#txtNroComprobante').val()).slice(-12);
            $(this).val(NroCbte.slice(1));
            setTimeout(function () {
                campoActual = "btnConsultarAplicaciones";
                $("#btnConsultarAplicaciones").focus();
            }, 100);
            return false;
        }
    });

    $("#btnConsultarAplicaciones").click(function () {
        var Tipo = $("#cboTipoCbte").val().trim();
        var obj = $("#TipoCbteValues").find("option[value='" + Tipo + "']");
        if (obj != null && obj.length > 0) {
            var TipoId = obj[0].dataset.id;
            // console.log(TipoCbte);
            switch (TipoId) {
                case "1":
                    TipoCbte = "NCR";
                    NroCbte = "A";
                    break;
                case "2":
                    TipoCbte = "NCR";
                    NroCbte = "B";
                    break;
                case "3":
                    TipoCbte = "REC";
                    NroCbte = "X";
                    break;
                case "4":
                    TipoCbte = "RES";
                    NroCbte = "X";
                    break;
                default:
                    console.log(obj[0].dataset.id)
                    mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>Seleccione un Tipo de Comprobante válido</h5>");
                    break;
                    return false;
            }
        }
        NroCbte = NroCbte.slice(0, 1);
        NroCbte += ('000000000000' + $('#txtNroComprobante').val()).slice(-12);
        if (NroCbte != "" && NroCbte.length == 13 && TipoCbte != "" && TipoCbte.length == 3) {
            window.location = "../ctacte/ConsultaDeAplicacionDeComprobantesResultado?t=" + TipoCbte + "&id=" + NroCbte
        }
    })

});

function cargarAplicaciones() {
    ControlarSesion();
    showCargandoBuscador();
    $.ajax({
        type: "POST",
        url: "/ctacte/ObtenerMovimientoPorTipoYNumeroDeComprobante",
        data: {
            TipoComprobante: $("#txtTipoCbte").val(),
            NumeroComprobante: $("#txtNroCbte").val(),
            Login: $("#txtLoginCliente").val()
        },
        success: function (response) {

            var html = "";
            $("#tblAplicaciones").html("");
            $("#divContenedorDocumentoCabecera").html("");
            if (response == "") {
                hideCargandoBuscador();
                mensaje("<span style='color: red !important;'><i class='fa fa-info-circle fa-2x'></i> INFORMACIÓN</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>No se ha encontrado el comprobante " + $("#txtTipoCbte").val() + " " + $("#txtNroCbte").val() + ". Por favor realice una nueva búsqueda.</h5><button type='button' class='btn btn-primary pull-right' style='margin-top:1em;' id='btnABusquedaOk'>ACEPTAR</button>");
                // $(".fa.fa-times").hide();
                $("#btnABusquedaOk").click(function () {
                    location.href = "/ctacte/ConsultaDeAplicacionDeComprobantes";
                });
                return false;
            }
            Movimiento = eval('(' + response + ')');
            console.log(Movimiento);
            // return false;
            if (Movimiento != null) {
                var fechaCbte = ""
                if (Movimiento.FechaToString != "") {
                    fechaCbte = Movimiento.FechaToString
                }
                var fechaApl = ""
                if (Movimiento.FechaPagoToString != "") {
                    fechaApl = Movimiento.FechaPagoToString
                }
                var fechaVto = ""
                if (Movimiento.FechaVencimientoToString != "") {
                    fechaVto = Movimiento.FechaVencimientoToString
                }
                html = "<div class='div_cont_ctacte col-xs-12'>";
                html += "<div class='col-xs-12 col-md-4'>";
                html += "<b>Comprobante</b>: " + Movimiento.TipoComprobanteToString + " " + Movimiento.NumeroComprobante;
                html += "</div>";
                html += '<div class="clear10 visible-xs"></div>';
                html += '<div class="clear15 visible-sm"></div>';
                html += "<div class='col-xs-12 col-sm-4'>";
                html += "<b>Fecha</b>: " + fechaCbte;
                html += "</div>";
                html += '<div class="clear10 visible-xs"></div>';
                html += "<div class='col-xs-12 col-sm-4'>";
                html += "<b>Vencimiento</b>: " + fechaVto;
                html += "</div>";
                html += '<div class="clear10 visible-xs"></div>';
                html += '<div class="clear15 hidden-xs"></div>';
                html += "<div class='col-xs-12 col-sm-4'>";
                html += "<b>Monto</b>: $" + Movimiento.Importe;
                html += "</div>";
                html += '<div class="clear10 visible-xs"></div>';
                html += "<div class='col-xs-12 col-sm-4'>";
                html += "<b>Saldo sin imputar</b>: $" + Movimiento.Saldo;
                html += "</div>";
                html += "</div>";


                $("#divContenedorDocumentoCabecera").append(html);

                $.ajax({
                    type: "POST",
                    url: "/ctacte/ObtenerAplicacionesDeComprobantesPorTipoYNumero",
                    data: {
                        TipoComprobante: $("#txtTipoCbte").val(),
                        NumeroComprobante: $("#txtNroCbte").val(),
                        Login: $("#txtLoginCliente").val()
                    },
                    success: function (response) {
                        hideCargandoBuscador();

                        var html = "";
                        $("#tblAplicaciones").html("");
                        if (response == "") {
                            hideCargandoBuscador();
                            mensaje("<span style='color: red !important;'><i class='fa fa-info-circle fa-2x'></i> INFORMACIÓN</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El comprobante " + $("#txtTipoCbte").val() + " " + $("#txtNroCbte").val() + " no tiene aplicaciones hasta el momento.</h5>");
                            // $(".fa.fa-times").hide();
                            return false;

                        }
                        Aplicaciones = eval('(' + response + ')');
                        console.log(Aplicaciones);
                        Total = 0;
                        // return false;
                        if (Aplicaciones.length > 0) {
                            for (i = 0; i < Aplicaciones.length; i++) {
                                var link = Aplicaciones[i].NumeroComprobante;
                                if (Aplicaciones[i].TipoComprobanteToString != "NCI" && Aplicaciones[i].TipoComprobanteToString != "NDI")
                                {
                                    link = "<a href='../ctacte/Documento?t=" + Aplicaciones[i].TipoComprobanteToString + "&id=" + Aplicaciones[i].NumeroComprobante + "'>" + Aplicaciones[i].NumeroComprobante + "</a>";
                                }
                                var cuota = "";
                                if (Aplicaciones[i].Semana != "") {
                                    cuota = Aplicaciones[i].Semana
                                }
                                var fechaCbte = ""
                                if (Aplicaciones[i].FechaToString != "") {
                                    fechaCbte = Aplicaciones[i].FechaToString
                                }
                                var fechaApl = ""
                                if (Aplicaciones[i].FechaPagoToString != "") {
                                    fechaApl = Aplicaciones[i].FechaPagoToString
                                }
                                html = "<tr>";
                                html += "<td class='text-center'>" + Aplicaciones[i].TipoComprobanteToString + "</td>";
                                html += "<td>" + link + "</td>";
                                html += "<td class='text-center'>" + cuota + "</td>";
                                html += "<td>" + fechaCbte + "</td>";
                                html += "<td class='text-right'> $ " + FormatoDecimalConDivisorMiles(Aplicaciones[i].Saldo.toFixed(2)) + "</td>";
                                html += "<td>" + Aplicaciones[i].MedioPago + "</td>";
                                html += "<td>" + fechaApl + "</td>";
                                html += "<td class=' text-right'> $ " + FormatoDecimalConDivisorMiles(Aplicaciones[i].Importe.toFixed(2)) + "</td >";
                                html += "</tr>";
                                Total += Aplicaciones[i].Importe;
                                $("#tblAplicaciones").append(html);
                            }
                            html = "<tr style='border-top: 1px solid #a1a1a1 !important; background: #98999a; height: 30px !important'>";
                            html += "<td class='text-right' colspan='7' style='color: #fff !important;'><b>TOTAL</b>: </td>";
                            html += "<td class=' text-right' style='color: #fff !important;'><b> $ " + FormatoDecimalConDivisorMiles(Total.toFixed(2)) + "</b></td >";
                            html += "</tr>";
                            $("#tblAplicaciones").append(html);
                        } else {

                            hideCargandoBuscador();
                            mensaje("<span style='color: red !important;'><i class='fa fa-info-circle fa-2x'></i> INFORMACIÓN</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El comprobante " + $("#txtTipoCbte").val() + " " + $("#txtNroCbte").val() + " no tiene aplicaciones hasta el momento.</h5>");
                            // $(".fa.fa-times").hide();
                            return false;
                        }

                    }
                });
                $("#tblResultadoCabecera").removeClass("hidden");
                $("#tblResultado").removeClass("hidden");

            } else {
                hideCargandoBuscador();
                mensaje("<span style='color: red !important;'><i class='fa fa-info-circle fa-2x'></i> INFORMACIÓN</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>No se ha encontrado el comprobante " + $("#txtTipoCbte").val() + " " + $("#txtNroCbte").val() + ". Por favor realice una nueva búsqueda.</h5><button type='button' class='btn btn-primary pull-right' style='margin-top:1em;' id='btnABusquedaOk'>ACEPTAR</button>");
                // $(".fa.fa-times").hide();
                $("#btnABusquedaOk").click(function () {
                    location.href = "/ctacte/ConsultaDeAplicacionDeComprobantes";
                });
                return false;
            }

        }
    });
}