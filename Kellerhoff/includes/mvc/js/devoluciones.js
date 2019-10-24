//Declaración de variables locales
var NroMotivo = 0;
var TipoCbte = "";
var Cbte = "";
var MaxCant = 0;
var objFactura = null;
var width = window.innerWidth;
var NroItem = 0;
var NombreProductoFact = "";
var listaPRD = "";
var campoActual = "";
var objPRDFac = "";
var objPRDDev = "";
var LoteDev = "";
var objItemFac = "";
var colItemDev = [];

function ItemDev(Desc, DescDev,NroFac,Motivo,Cant,Lote,FechaVencimiento) {
    this.DescripcionFacturado = Desc;
    this.DescripcionDevuelto = DescDev;
    this.NumeroFactura = NroFac;
    this.Motivo = Motivo;
    this.Cantidad = Cant;
    this.Fecha = new Date();
    this.NumeroDeLote = Lote;
    this.FechaVencimiento = FechaVencimiento;
}

$(document).ready(function () {
    $("#cmbMotivo").focus();

    $('body').keypress(function (e) {
        if (e.which == 13) {
            if ($("#modalModulo").is(":visible")) {
                modalModuloHide();
                $("#" + campoActual).val("");
                $("#" + campoActual).focus();
            }
        }
    });

    $("#cmbMotivo").focus(function () {
        $("#cmbMotivo").val("");
        $('#DEVTipoComprobante').addClass("hidden");
        $('#DEVTipoComprobante input').val("");
        $('#DEVNroComprobante').addClass("hidden");
        $('#DEVNroComprobante input').val("");
        $("#DEVFactura").addClass("hidden");
        $('#DEVFactura input').val("");
        $("#DEVDevolver").addClass("hidden");
        $('#DEVDevolver input').val("");
        $("#DEVCant").addClass("hidden");
        $('#DEVCant input').val("");
        $("#DEVLote").addClass("hidden");
        $('#DEVLote input').val("");
        $("#DEVLoteVenc").addClass("hidden");
        $('#DEVLoteVenc input').val("");
        $("#DEVAgregar").addClass("hidden");
    });


    $("#cmbTipoComprobante").focus(function () {
        $("#cmbTipoComprobante").val("");
        //$('#DEVTipoComprobante').addClass("hidden");
        //$('#DEVNroComprobante').addClass("hidden");
        //$("#DEVFactura").addClass("hidden");
        //$("#DEVDevolver").addClass("hidden");
        //$("#DEVCant").addClass("hidden");
        //$("#DEVAgregar").addClass("hidden");
        //$("#txtCantDevolver").val("");
        //$("#txtNombreProductoDev").val("");
    });

    $("#cmbNombreProducto").focus(function () {
        $("#cmbNombreProducto").val("");
        //$('#DEVTipoComprobante').addClass("hidden");
        //$('#DEVNroComprobante').addClass("hidden");
        //$("#DEVFactura").addClass("hidden");
        //$("#DEVDevolver").addClass("hidden");
        //$("#DEVCant").addClass("hidden");
        //$("#DEVAgregar").addClass("hidden");
        //$("#txtCantDevolver").val("");
        //$("#txtNombreProductoDev").val("");
    });

    $("#cmbMotivo").change(function (e) {
        var Motivo = $(this).val().trim();
        var obj = $("#MotivoValues").find("option[value='" + Motivo + "']");
        campoActual = $(this).attr("id");

        if (obj != null && obj.length > 0) {
            NroMotivo = obj[0].dataset.id;
            if (Motivo != "") {
                $('#DEVTipoComprobante').removeClass("hidden");
                $('#DEVTipoComprobante input').val("");
                $('#DEVNroComprobante').removeClass("hidden");
                $('#DEVNroComprobante input').val("");
                $("#DEVFactura").addClass("hidden");
                $("#DEVFactura input").val("");
                $("#DEVDevolver").addClass("hidden");
                $("#DEVDevolver input").val("");
                $("#DEVCant").addClass("hidden");
                $("#DEVCant input").val("");
                $("#DEVAgregar").addClass("hidden");
                $("#DEVLote").addClass("hidden");
                $("#DEVLoteVenc").addClass("hidden");
                $("#DEVLote input").val("");
                $("#DEVLoteVenc input").val("");
                $("#txtCantDevolver").val("");
                $("#txtNombreProductoDev").val("");
                objPRDFac = "";
                objPRDDev = "";
                objItemFac = "";
                setTimeout(function () {
                    campoActual = "cmbTipoComprobante";
                    $("#cmbTipoComprobante").focus()
                }, 100);

            } else {
                $('#DEVTipoComprobante').addClass("hidden");
                $('#DEVNroComprobante').addClass("hidden");
                $("#DEVFactura").addClass("hidden");
                $("#DEVDevolver").addClass("hidden");
                $("#DEVCant").addClass("hidden");
                $("#DEVAgregar").addClass("hidden");
                $("#txtCantDevolver").val("");
                $("#txtNombreProductoDev").val("");
                objPRDFac = "";
                objPRDDev = "";
                objItemFac = "";
            }
        } else {
            mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>Seleccione un motivo válido</h5>");
            $(".fa.fa-times").hide();
            $("#modalModulo").unbind("click");
        }
    });

    $("#cmbTipoComprobante").change(function () {
        TipoCbte = $(this).val().trim();
        var obj = $("#TipoCbteValues").find("option[value='" + TipoCbte + "']");
        campoActual = $(this).attr("id");

        if (obj != null && obj.length > 0) {
            setTimeout(function () {
                campoActual = "txtNroComprobante";
                $("#txtNroComprobante").focus()
            }, 100);
            objPRDFac = "";
            objPRDDev = "";
            objItemFac = "";
        } else {
            mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>Seleccione un tipo de comprobante válido</h5>");
            $(".fa.fa-times").hide();
            $("#modalModulo").unbind("click");
        }
    });

    $("#txtNroComprobante").keypress(function (e) {
        if (e.which == 13) {
            campoActual = $(this).attr("id");
            objPRDFac = "";
            objPRDDev = "";
            objItemFac = "";
            
            IsBanderaUsarDll('OnCallBackIsBanderaUsarDll_ComprobanteNro');
            return false;
        }
    });

    $("#cmbNombreProducto").change(function () {
        var seleccion = $(this).val().trim();
        var obj = $("#NombreProductoValues").find("option[value='" + seleccion + "']");

        campoActual = $(this).attr("id");
        if (obj != null && obj.length > 0) {
            var NroItem = obj[0].dataset.id;
            objItemFac = objFactura.lista[NroItem];
            NombreProductoFact = objFactura.lista[NroItem].Descripcion;
            Cant = objFactura.lista[NroItem].Cantidad;
            
            if (NroMotivo == 1 || NroMotivo == 8) {
                setTimeout(function () {
                    campoActual = "txtNombreProductoDev";
                    $("#txtNombreProductoDev").focus()
                }, 100);
            } else {
                var arrayListaColumna = new Array();
                arrayListaColumna.push("pro_nombre");
                arrayListaColumna.push("pro_codigobarra");
                RecuperarProductosParaDevoluciones(NombreProductoFact, arrayListaColumna, false, false);
            }
        } else {
            mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El producto seleccionado no pertenece a la factura ingresada. Por favor verifique.</h5>");
            $(".fa.fa-times").hide();
            $("#modalModulo").unbind("click");
        }
    });


    $("#txtNombreProductoDev").keypress(function (e) {
        if (e.which == 13) {
            var aBuscar = $(this).val().trim();
            campoActual = $(this).attr("id");

            if (aBuscar !== '') {
                if (aBuscar.length > 0) {
                    var arrayListaColumna = new Array();

                    arrayListaColumna.push("pro_nombre");
                    arrayListaColumna.push("pro_codigobarra");
                    RecuperarProductosParaDevoluciones(aBuscar, arrayListaColumna, false, false);
                } else {
                    mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>Por favor, ingrese un producto para realizar la búsqueda.</h5>");
                    $(".fa.fa-times").hide();
                    $("#modalModulo").unbind("click");
                }
            } else {
                mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>Por favor, ingrese un producto para realizar la búsqueda.</h5>");
                $(".fa.fa-times").hide();
                $("#modalModulo").unbind("click");
            }
        }
    });

    $("#txtCantDevolver").keypress(function (e) {
        if (e.which == 13) {
            campoActual = $(this).attr("id");
            var CantADev = $(this).val().trim();

            //control 0
            if (CantADev <= 0) {
                mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>La cantidad a devolver debe ser mayor a 0.</h5>");
                $(".fa.fa-times").hide();
                $("#modalModulo").unbind("click");
                return false;
            }

            //Control decimal redondo
            if (CantADev.indexOf(',') != -1) {
                if (0 == CantADev.substring(CantADev.indexOf(',') + 1, CantADev.length)) {
                    CantADev = CantADev.substring(0, CantADev.indexOf(','));
                }
            } else {
                if (0 == CantADev.substring(CantADev.indexOf('.') + 1, CantADev.length)) {
                    CantADev = CantADev.substring(0, CantADev.indexOf('.'));
                }
            }
            $(this).val(CantADev);

            //control decimal
            if ($.isNumeric(CantADev)) {
                if (CantADev.indexOf(',') != -1 || CantADev.indexOf('.') != -1) {
                    mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>La cantidad debe ser un número entero.</h5>");
                    $(".fa.fa-times").hide();
                    $("#modalModulo").unbind("click");
                    return false;
                }

                if (objItemFac != "" && (NroMotivo == 3 || NroMotivo == 4 || NroMotivo == 6 || NroMotivo == 7 || NroMotivo == 2)) {
                    if (parseInt(objItemFac.Cantidad) < parseInt(CantADev)) {
                        console.log("Entro");
                        mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>La cantidad a devolver no puede ser mayor a la facturada.</h5>");
                        $(".fa.fa-times").hide();
                        $("#modalModulo").unbind("click");
                        return false;
                    }
                }

                $("#DEVLote").removeClass("hidden");
                campoActual = "txtNumeroLote";
                $("#txtNumeroLote").focus();

            } else {
                mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>La cantidad debe ser un número entero.</h5>");
                $(".fa.fa-times").hide();
                $("#modalModulo").unbind("click");
            }
        } 
    });

    $("#txtNumeroLote").change(function () {
        var pNumeroLote = $(this).val().trim(),
            pNombreProducto = objPRDDev.pro_nombre;

        if (pNumeroLote.length >= 3) {
            $.ajax({
                type: "POST",
                url: "/devoluciones/ObtenerNumerosLoteDeProductoDeFacturaProveedorLogLotesConCadena",
                data: { pNombreProducto, pNumeroLote },
                success: function (response) {
                    console.log(response);
                    if (response.length > 0) {
                        var html = "<li class='headerlotesList' data-idlote=" + i + ">Nro Lote<span class='fven'>Vencimiento</span></li>";
                        colLotes = eval('(' + response + ')');

                        console.log(colLotes);
                        for (var i = 0; i < colLotes.length; i++) {
                            var DatosFecha = colLotes[i].FechaVencimientoToString.split("/");
                            var FechaVenc = DatosFecha[1] + "/" + DatosFecha[2];
                            console.log(FechaVenc);
                            html += "<li class='lotesList' data-idlote=" + i + ">" + colLotes[i].NumeroLote + "<span class='fven'>" + FechaVenc + "</span></li>";
                        }
                        mensaje("Seleccione un Lote", html);
                        $(".fa.fa-times").hide();
                        $("#modalModulo").unbind("click");
                        $(".lotesList").click(function () {
                            var idItem = $(this).attr("data-idlote");
                            $("#txtNumeroLote").val(colLotes[idItem].NumeroLote);
                            LoteDev = colLotes[idItem];
                            var DFecha = LoteDev.FechaVencimientoToString.split("/");
                            $("#txtNumeroLoteVenc").val(DFecha[1] + "/" + DFecha[2]);
                            $("#txtNumeroLoteVenc").attr("disabled","disabled");
                            console.log(LoteDev);
                            modalModuloHide();
                            $("#DEVLoteVenc").removeClass("hidden");
                            $("#DEVAgregar").removeClass("hidden");
                            $("#btnAgregarDev").removeAttr("disabled","disabled");
                            $("#btnAgregarDev").focus();
                        });
                    } else {
                        mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>No se encuentra Número de Lote ingresado, por favor realice una nueva búsqueda.</h5>");
                        $(".fa.fa-times").hide();
                        $("#modalModulo").unbind("click");
                    }
                }
            });
        } else {
            mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>Por favor, ingrese al menos 3 caracteres para realizar la búsqueda del lote.</h5>");
            $(".fa.fa-times").hide();
            $("#modalModulo").unbind("click");
        }
    });

});

function OnCallBackIsBanderaUsarDll_ComprobanteNro(args) {
    if (args) {
        var nro = ('00000000' + $('#txtNroComprobante').val().trim()).slice(-8);
        var parteAdelante = '';
        $('#txtNroComprobante').val(nro);
        parteAdelante = TipoCbte.substring(4);
        Cbte = parteAdelante + nro;
        ObtenerFacturaCliente(Cbte);
    } 
}

function ObtenerFacturaCliente(pNroFactura) {
    showCargandoBuscador();
    $.ajax({
        type: "POST",
        url: "/devoluciones/ObtenerFacturaCliente",
        data: { pNroFactura },
        success: function (response) {
            hideCargandoBuscador();
            if (response.length > 0) {
                objFactura = eval('(' + response + ')');
                var ahora = new Date().getTime();
                var dia = new Date().getDay();
                var dtFecha = objFactura.FechaToString.split("/");
                var FechaFactura = new Date(dtFecha[2] + "/" + dtFecha[1] + "/" + dtFecha[0]).getTime();

                // Controlo que no se haya hecho por IAPOS
                if (objFactura.NumeroCuentaCorriente == "9002") {
                    mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>La factura fue pedida a través de IAPOS, por favor comuníquese con RECLAMOS.</h5>");
                    $(".fa.fa-times").hide();
                    $("#modalModulo").unbind("click");
                    return false;
                }

                //valores para DESARROLLO
                    ahora = new Date('2019/10/15').getTime();
                    dia = new Date('2019/10/15').getDay();
                    FechaFactura = new Date('2019/10/14').getTime();
                // FIN para DESARROLLO

                var diff = parseInt((ahora - FechaFactura) / (1000 * 60 * 60 * 24));
                var fechaOK = false;
                switch (dia) {
                    case 6:
                        if (diff <= 4) {
                            fechaOK = true;
                        }
                        break;
                    case 0:
                        if (diff <= 5) {
                            fechaOK = true;
                        }
                        break;
                    case 1:
                        if (diff <= 5) {
                            fechaOK = true;
                        }
                        break;
                    case 2:
                        if (diff <= 5) {
                            fechaOK = true;
                        }
                        break;
                    case 3:
                        if (diff <= 5) {
                            fechaOK = true;
                        }
                        break;
                    case 4:
                        if (diff <= 3) {
                            fechaOK = true;
                        }
                        break;
                    case 5:
                        if (diff <= 3) {
                            fechaOK = true;
                        }
                        break;
                }

                if (fechaOK) {
                    var html = "";
                    for (var i = 0; i < objFactura.lista.length; i++) {
                        if (objFactura.lista[i].Cantidad != "") {
                            html += "<option value=\"" + objFactura.lista[i].Descripcion + "\"  data-id=\"" + i + "\">";
                        }
                        if (NroMotivo == 1 || NroMotivo == 8) {
                            $("#NombreProductoValues").html(html);
                            $("#DEVFactura").removeClass("hidden");
                            $("#DEVDevolver").removeClass("hidden");
                            $("#txtCantDevolver").val("");
                            $("#txtNombreProductoDev").val("");
                            $("#cmbNombreProducto").val("");
                            campoActual = "cmbNombreProducto";
                            $("#cmbNombreProducto").focus();
                        } else if (NroMotivo == 5) {
                            $("#DEVDevolver").removeClass("hidden");
                            $("#txtCantDevolver").val("");
                            $("#txtNombreProductoDev").val("");
                            campoActual = "txtNombreProductoDev";
                            $("#txtNombreProductoDev").focus();
                        } else {
                            $("#NombreProductoValues").html(html);
                            $("#DEVFactura").removeClass("hidden");
                            $("#txtCantDevolver").val("");
                            $("#txtNombreProductoDev").val("");
                            $("#cmbNombreProducto").val("");
                            campoActual = "cmbNombreProducto";
                            $("#cmbNombreProducto").focus();
                        }
                    }
                } else {
                    mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>La factura no puede tener mas de 72 horas de emitida.</h5>");
                    $(".fa.fa-times").hide();
                    $("#modalModulo").unbind("click");
                }
            } else {
                mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>La factura no corresponde</h5>");
                $(".fa.fa-times").hide();
                $("#modalModulo").unbind("click");
            }
        }
    });
}

function RecuperarProductosParaDevoluciones(pTxtBuscador, pListaColumna, pIsBuscarConOferta, pIsBuscarConTransfer) {
    $.ajax({
        type: "POST",
        url: "/mvc/RecuperarProductosVariasColumnas",
        data: { pTxtBuscador: pTxtBuscador, pListaColumna: pListaColumna, pIsBuscarConOferta: pIsBuscarConOferta, pIsBuscarConTransfer: pIsBuscarConTransfer },
        success:
            function (response) {
                listaPRD = eval('(' + response + ')');
                var html = "";
                if (campoActual == "txtNombreProductoDev") {
                    if (listaPRD.listaProductos.length > 0) {
                        for (var i = 0; i < listaPRD.listaProductos.length; i++) {
                            html += "<li class='msjList' data-NroProd=" + i + ">" + listaPRD.listaProductos[i].pro_nombre + "</li>";
                        }
                        mensaje("Seleccione un producto", html);
                        $(".fa.fa-times").hide();
                        $("#modalModulo").unbind("click");
                        $(".msjList").click(function () {
                            var idItem = $(this).attr("data-nroprod");
                            $("#txtNombreProductoDev").val(listaPRD.listaProductos[idItem].pro_nombre);
                            objPRDDev = listaPRD.listaProductos[idItem];
                            console.log(objPRDDev);
                            modalModuloHide();
                            if (objPRDDev.pro_isCadenaFrio) {
                                mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El producto que está intentando devolver lleva CADENA DE FRÍO, por favor comuníquese con RECLAMOS.</h5>");
                                $(".fa.fa-times").hide();
                                $("#modalModulo").unbind("click");
                                return false;
                            } else if (objPRDDev.pro_isTrazable) {
                                mensaje("<span style='color: steelblue !important;'><i class='fa fa-exclamation-triangle fa-2x'></i> Información</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El producto que está devolviendo es un producto TRAZABLE, si usted ha CONFIRMADO la recepción del mismo, deberá trazar la devolución a la droguería como 'Envio de producto en carácter de devolución'.</h5>");
                                $(".fa.fa-times").hide();
                                $("#modalModulo").unbind("click");
                                $("#DEVCant").removeClass("hidden");
                                campoActual = "txtCantDevolver";
                                $("#txtCantDevolver").focus();
                            } else if (objPRDDev.isValePsicotropicos) {
                                mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El producto que está intentando devolver requiere VALE por PSICOTRÓPICO, por favor comuníquese con RECLAMOS.</h5>");
                                $(".fa.fa-times").hide();
                                $("#modalModulo").unbind("click");
                                return false;
                            } else {
                                $("#DEVCant").removeClass("hidden");
                                campoActual = "txtCantDevolver";
                                $("#txtCantDevolver").focus();
                            }
                        });
                    } else {
                        mensaje("<span style='color: steelblue !important;'><i class='fa fa-exclamation-triangle fa-2x'></i> Información</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>No se encontraron productos en esta búsqueda.</h5>");
                        $(".fa.fa-times").hide();
                        $("#modalModulo").unbind("click");
                    }
                } else {
                    if (listaPRD.listaProductos.length == 1) {
                        objPRDFac = listaPRD.listaProductos[0];
                        objPRDDev = objPRDFac;
                        if (objPRDDev.pro_isCadenaFrio) {
                            mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El producto que está intentando devolver lleva CADENA DE FRÍO, por favor comuníquese con RECLAMOS.</h5>");
                            $(".fa.fa-times").hide();
                            $("#modalModulo").unbind("click");
                            return false;
                        } else if (objPRDDev.pro_isTrazable) {
                            mensaje("<span style='color: steelblue !important;'><i class='fa fa-exclamation-triangle fa-2x'></i> Información</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El producto que está devolviendo es un producto TRAZABLE, si usted ha CONFIRMADO la recepción del mismo, deberá trazar la devolución a la droguería como 'Envio de producto en carácter de devolución'.</h5>");
                            $(".fa.fa-times").hide();
                            $("#modalModulo").unbind("click");
                            $("#DEVCant").removeClass("hidden");
                            campoActual = "txtCantDevolver";
                            $("#txtCantDevolver").focus();
                        } else if (objPRDDev.isValePsicotropicos) {
                            mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El producto que está intentando devolver requiere VALE por PSICOTRÓPICO, por favor comuníquese con RECLAMOS.</h5>");
                            $(".fa.fa-times").hide();
                            $("#modalModulo").unbind("click");
                            return false;
                        } else {
                            $("#DEVCant").removeClass("hidden");
                            campoActual = "txtCantDevolver";
                            $("#txtNumeroLote").val("");
                            $("#txtNumeroLoteVenc").val("");
                            $("#txtCantDevolver").focus();
                        }
                    } else {
                        mensaje("<span style='color: red !important;'><i class='fa fa-times-circle fa-2x'></i> ERROR</span>", "<h5 style='text-align:center;line-height:1.5em;font-weight:300;font-size:16px;'>El producto que está intentando devolver está discontinuado, por favor comuníquese con RECLAMOS.</h5>");
                        $(".fa.fa-times").hide();
                        $("#modalModulo").unbind("click");
                        return false;
                    }
                }
            },
        failure: function (response) {
            OnFail(response);
        },
        error: function (response) {
            OnFail(response);
        }
    });
}
