$(document).ready(function () {
    $(".cmbCliente").select2();

    $("#cmbCliente").change(function () {
        var IdCliente = $(this).val().trim();

        $.ajax({
            type: "POST",
            url: "/ctacte/CambiarClientePromotor",
            data: { IdCliente },
            success:
                function (response) {
                    console.log(response);
                    window.location.href = "/ctacte/composicionsaldo";
                },
            failure: function (response) {
                //hideCargandoBuscador();
                OnFail(response);
            },
            error: function (response) {
                //hideCargandoBuscador();
                OnFail(response);
            }
        });
    })
});

var width_old = $(window).width();
$(window).resize(function () {
    if ($(this).width() != width_old) {
        $(".cmbCliente").select2();
        width_old = $(this).width();
    }
})


//BUSCARDOR PROMOTORES
function onkeypressEnterPromotores(e, elemento) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 13) {
        onClickBuscarPromotores();
    }
}

function onClickBuscarPromotores() {
    if (isNotBuscadorEnProceso) {
        // Limpiar Paginador
        $('#divPaginador').html('');
        //intPaginadorTipoDeRecuperar = 0; // setea
        var isBuscar = false;
        // Limpiar detalle producto si se encuentra abierto
        OnMouseOutProdructo();
        //
        varPalabraBuscador = jQuery("#txtBuscador").val().trim();
        if (varPalabraBuscador !== '') {
            if (varPalabraBuscador.length > 0) {
                var isTransferSeleccionado = false;
                //if (isCarritoDiferido) {
                //    isTransferSeleccionado = false;
                //} else {
                //    isTransferSeleccionado = $('#checkBoxTodosTransfer').is(':checked');
                //}
                isTransferSeleccionado = $('#checkBoxTodosTransfer').is(':checked');

                if ($('#checkNombre').is(':checked') || $('#checkBoxCodigoBarra').is(':checked') || $('#checkBoxMonodroga').is(':checked') || $('#checkBoxLaboratorio').is(':checked') || $('#checkBoxCodigoAlfaBeta').is(':checked') || $('#checkBoxTroquel').is(':checked') || $('#checkBoxTodosOfertas').is(':checked') || isTransferSeleccionado) {
                    //
                    //Ascender_pro_precio = true;
                    //Ascender_PrecioFinal = true;
                    //Ascender_PrecioConDescuentoOferta = true;
                    var arrayListaColumna = new Array();
                    if ($('#checkNombre').is(':checked')) {
                        arrayListaColumna.push($('#checkNombre').val());
                    }
                    if ($('#checkBoxCodigoBarra').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxCodigoBarra').val());
                    }
                    if ($('#checkBoxMonodroga').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxMonodroga').val());
                    }
                    if ($('#checkBoxLaboratorio').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxLaboratorio').val());
                    }
                    if ($('#checkBoxCodigoAlfaBeta').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxCodigoAlfaBeta').val());
                    }
                    if ($('#checkBoxTroquel').is(':checked')) {
                        arrayListaColumna.push($('#checkBoxTroquel').val());
                    }
                    showCargandoBuscador();
                    intPaginadorTipoDeRecuperar = 3; // buscador común
                    //pagActual = 1;
                    //intColumnaOrdenar = -2;
                    setearVariablesBuscador();
                    RecuperarProductosVariasColumnasPromotores(varPalabraBuscador, arrayListaColumna, $('#checkBoxTodosOfertas').is(':checked'), isTransferSeleccionado);
                }
                else {
                    mensaje_informacion('Seleccione por lo menos una opción de búsqueda');
                }
                isBuscar = true;
            }

        }
        //}
        if (!isBuscar) {
            jQuery("#divResultadoBuscador").html('');
        }
    }
}

function RecuperarProductosVariasColumnasPromotores(pTxtBuscador, pListaColumna, pIsBuscarConOferta, pIsBuscarConTransfer) {
    $.ajax({
        type: "POST",
        url: "/mvc/RecuperarProductosVariasColumnas",
        data: { pTxtBuscador: pTxtBuscador, pListaColumna: pListaColumna, pIsBuscarConOferta: pIsBuscarConOferta, pIsBuscarConTransfer: pIsBuscarConTransfer },
        success:
            function (response) {
                hideCargandoBuscador();
                //OnCallBackRecuperarProductos(response);
                OnCallBackRecuperarProductosConPaginadorPromotores(response);
            },
        failure: function (response) {
            hideCargandoBuscador();
            OnFail(response);
        },
        error: function (response) {
            hideCargandoBuscador();
            OnFail(response);
        }
    });
}

function OnCallBackRecuperarProductosConPaginadorPromotores(args) {
    var varArgs = eval('(' + args + ')');
    var mod = varArgs.CantidadRegistroTotal % CantidadFilaPorPagina_const;
    var cantPag = 0;
    if (mod == 0) {
        cantPag = varArgs.CantidadRegistroTotal / CantidadFilaPorPagina_const;
    }
    else {
        cantPag = Math.ceil((varArgs.CantidadRegistroTotal / CantidadFilaPorPagina_const));
    }
    cantPaginaTotal = cantPag;
    if (!isSubirPedido) {
        GenerarPaginador();
    }
    OnCallBackRecuperarProductosPromotores(args);
}

function OnCallBackRecuperarProductosPromotores(args) {
    if (args !== null) {
        if (args !== '') {
            var strHtml = '';
            args = eval('(' + args + ')');
            listaSucursal = args.listaSucursal;
            listaProductosBuscados = args.listaProductos;


            var cssTh_cabecera = ' class="col-lg-3 col-md-3 col-sm-3 col-xs-9 text-left no-padding" ';
            var cssTd_cabeceraBody = ' col-lg-5 col-md-5 text-left ';
            var cssFootTd1 = ' col-lg-10 col-md-9 ';
            var cssFootTd2 = ' col-lg-1 col-md-1 text-center ';
            var cssFootTd3 = ' col-lg-1 col-md-1 text-center no-padding ';
            var cssFootTd3Div = ' col-xs-12 col_small flex_8 ';
            switch (listaSucursal.length) {
                case 1:
                    cssTh_cabecera = ' class="col-lg-5 col-md-5 text-left th1ColumnaCabecera"  ';
                    cssTd_cabeceraBody = ' col-lg-5 col-md-5 text-left th1ColumnaCabeceraBody ';
                    cssFootTd1 = ' col-lg-10 col-md-9 ';
                    cssFootTd2 = ' col-lg-1 col-md-1 text-center ';
                    cssFootTd3 = ' col-lg-1 col-md-1 text-center no-padding ';
                    cssFootTd3Div = ' col-xs-12 col_small flex_8 ';
                    break;
                case 2:
                    cssTh_cabecera = ' class="col-lg-4 col-md-4 col-sm-3 col-xs-9 text-left th1ColumnaCabecera"  ';
                    cssTd_cabeceraBody = ' col-lg-4 col-md-4 col-sm-3 text-left th1ColumnaCabeceraBody ';
                    cssFootTd1 = ' col-lg-9 col-md-9 ';
                    cssFootTd2 = ' col-lg-1 col-md-1 col-sm-1 text-center ';
                    cssFootTd3 = ' col-lg-3 col-md-3 col-sm-1 col-xs-1 text-center no-padding ';
                    cssFootTd3Div = ' col-xs-6 col_small flex_8 ';
                    break;
                case 3:
                    cssTh_cabecera = ' class="col-lg-3 col-md-3 col-sm-3 col-xs-9 text-left th1ColumnaCabecera"  ';
                    cssTd_cabeceraBody = ' col-lg-3 col-md-3 col-sm-3 text-left th1ColumnaCabeceraBody ';
                    cssFootTd1 = ' col-lg-8 col-md-8 ';
                    cssFootTd2 = ' col-lg-1 col-md-1 col-sm-1 text-center ';
                    cssFootTd3 = ' col-lg-3 col-md-3 col-sm-1 col-xs-1 text-center no-padding ';
                    cssFootTd3Div = ' col-xs-4 col_small flex_8 ';
                    break;
                case 4:
                    cssTh_cabecera = ' class="col-lg-2 col-md-2 col-sm-2 col-xs-9 text-left th1ColumnaCabecera"  ';
                    cssTd_cabeceraBody = ' col-lg-3 col-md-3 col-sm-3 text-left th1ColumnaCabeceraBody ';
                    cssFootTd1 = ' col-lg-8 col-md-8 ';
                    cssFootTd2 = ' col-lg-1 col-md-1 col-sm-1 text-center ';
                    cssFootTd3 = ' col-lg-3 col-md-3 col-sm-1 col-xs-1 text-center no-padding ';
                    cssFootTd3Div = ' col-xs-4 col_small flex_8 ';
                    break;
                default:
                    break;
            }
            cantMaxFila = listaProductosBuscados.length;
            cantMaxColumna = listaSucursal.length;

            var classSubirPedidoTheadTr1 = '';
            if (isSubirPedido) {
                cssTh_cabecera += ' colspan="2" ';
                strHtml += '<div class="div_subirpedido hidden-xs hidden-sm">';
                strHtml += '<div class="div_tblsubirpedido_head scroll">';
                strHtml += '<table class="footable table table-stripped" width="100%" align="center" cellspacing="1" cellpadding="5" border="0">';
                classSubirPedidoTheadTr1 = ' style="border-top: none !important" ';
            }
            else {
                strHtml += '<table class="footable table table-stripped hidden-xs" width="100%" align="center" cellspacing="1" cellpadding="5" border="0">';
            }
            strHtml += '<thead>';
            strHtml += '<tr ' + classSubirPedidoTheadTr1 + '>';
            //
            strHtml += '<th ' + cssTh_cabecera + ' >';// strHtml += '<th class="bp-off-top-left thOrdenar porCamara_cabecera" rowspan="2" onclick="onclickOrdenarProducto(-1)"><div class="bp-top-left">Detalle Producto</div></th>';

            if (isSubirPedido) {
                strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
                strHtml += '<tr><td colspan="2" class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
                strHtml += '<tr class="tr_thead">';
                strHtml += '<td class="col-lg-10 col-md-10 pl text-left no_border click"><span data-toggle="tooltip" data-placement="bottom" title="Ordenar" onclick="onclickOrdenarProducto(-1)">Producto</span><span id="spanOrder_1" class="order ' + getCSSColumnaOrdenar(-1) + '"></span></td>';
                strHtml += '<td class="col-lg-2 text-center click no-padding hidden-md" data-toggle="tooltip" data-placement="bottom" title="Ordenar" onclick="onclickOrdenarProducto(4)">Orden <span id="spanOrder4" class="order ' + getCSSColumnaOrdenar(4) + '"></span></td>';
                strHtml += '<td class="col-md-2 text-center click no-padding visible-md" data-toggle="tooltip" data-placement="bottom" onclick="onclickOrdenarProducto(4)">Or <span id="spanOrder4b" class="order ' + getCSSColumnaOrdenar(4) + '"></span></td>';
                strHtml += '</tr>';
                strHtml += '</table>';
            } else {
                strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
                strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></td></tr>';
                strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-left pl click" data-toggle="tooltip" data-placement="bottom" title="Ordenar" onclick="onclickOrdenarProducto(-1)">Producto <span id="spanOrder_1" class="order ' + getCSSColumnaOrdenar(-1) + '"></span></td></tr>';
                strHtml += '</table>'
            }

            strHtml += '</th>';

            strHtml += '<th class="col-lg-2 col-md-2 col-sm-2  text-center" colspan="2">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td colspan="2" class="col-lg-12 text-center">Precio<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead">';
            strHtml += '<td class="col-lg-6 text-center click no_border" onclick="onclickOrdenarProducto(0)"><span data-toggle="tooltip" data-placement="bottom" title="Ordenar">P&uacute;blico</span><span id="spanOrder0" class="order ' + getCSSColumnaOrdenar(0) + '"></span></td>';
            strHtml += '<td class="col-lg-6 text-center click"  data-toggle="tooltip" data-placement="bottom" title="Ordenar" onclick="onclickOrdenarProducto(1)">Cliente <span id="spanOrder1" class="order ' + getCSSColumnaOrdenar(1) + '"></span></td>';
            strHtml += '</tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            //
            strHtml += '<th class="col-lg-2 col-md-2 col-sm-2 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td colspan="3" class="col-lg-12 text-center">Oferta<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead">';
            strHtml += '<td class="col-lg-3 col-md-3 col-sm-3 text-center no_border">%</td>';
            strHtml += '<td class="col-lg-3 col-md-3 col-sm-3 text-center no_border">Min</td>';
            strHtml += '<td class="col-lg-6 col-md-6 col-sm-6 click text-center" data-toggle="tooltip" data-placement="bottom" title="Ordenar" onclick="onclickOrdenarProducto(2)">Precio<span id="spanOrder2" class="order ' + getCSSColumnaOrdenar(2) + '"></span></td>';
            strHtml += '</tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            //
            //if (!isCarritoDiferido) {
            strHtml += '<th class="col-lg-2 col-md-2 col-sm-2  text-center" colspan="2">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td colspan="2" class="col-lg-12 text-center">Transfer<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead">';
            strHtml += '<td class="col-lg-6 text-center no_border">Cond</td>';
            strHtml += '<td class="col-lg-6 text-center click" data-toggle="tooltip" data-placement="bottom" title="Ordenar"  onclick="onclickOrdenarProducto(3)">Precio<span  id="spanOrder3" class="order ' + getCSSColumnaOrdenar(3) + '"></span></td>';
            strHtml += '</tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            //}

            for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                strHtml += '<th class="col-lg-1 col-md-1 col-sm-1 col-xs-1 text-center no-padding">';
                strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
                strHtml += '<tr><td class="col-lg-12 text-center ">&nbsp;<div class="clear5"></div></td></tr>';
                strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">' + ConvertirSucursalParaColumna(listaSucursal[iEncabezadoSucursal]) + '</td></tr>';
                strHtml += '</table>';
                strHtml += '</th>';
            }



            strHtml += '</tr>';
            strHtml += '</thead>';
            /////////////////////////////

            if (isSubirPedido) {
                strHtml += '</table>';
                strHtml += '</div>';
                strHtml += '<div class="div_tblsubirpedido_cont">';
                strHtml += '<table class="footable table table-stripped mb_0" width="100%" align="center" cellspacing="1" cellpadding="5" border="0">';
            }
            /////////////////////////////

            strHtml += '<tbody>';
            if (listaProductosBuscados.length > 0) {
                for (var i = 0; i < listaProductosBuscados.length; i++) {
                    var isNotGLNisTrazable = false;
                    var isMostrarImput = true;
                    if (cli_isGLN() !== null) {
                        if (!cli_isGLN()) {
                            if (listaProductosBuscados[i].pro_isTrazable) {
                                isNotGLNisTrazable = true;
                                isMostrarImput = false;
                            }
                        }
                    }
                    if (!isMostrarImput_AceptaPsicotropicos(listaProductosBuscados[i].pro_codtpovta)) {
                        isMostrarImput = false;
                    }
                    var strTrColorFondo = ' wht';
                    if (i % 2 !== 0) {
                        strTrColorFondo = ' grs';
                    }
                    // subir archivo
                    if (isSubirPedido) {
                        if (listaProductosBuscados[i].isProductoNoEncontrado) {
                            strTrColorFondo += ' rosa ';
                        }
                        var isRemarcarFila = false;
                        for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                            Log('OnCallBackRecuperarProductos:' + '0');
                            var varValores = CargarProductoCantidadDependiendoTransfer_base(i, iEncabezadoSucursal, listaProductosBuscados[i].cantidad, true);
                            if ((varValores[1] == 0 && varValores[2]) || (varValores[0] > 0 && varValores[3])) {
                                isRemarcarFila = true;
                                break;
                            }
                        }
                        if (isRemarcarFila && cli_tomaTransfers() && listaProductosBuscados[i].isMostrarTransfersEnClientesPerf && listaProductosBuscados[i].isPermitirPedirProducto) {
                            strTrColorFondo += ' violeta ';
                        }
                    }
                    // fin
                    strHtml += '<tr class="' + strTrColorFondo + ' cssFilaBuscadorDesmarcar cssFilaBuscador_' + i + '">';
                    var strHtmlColorFondo = '';
                    var tdBodyClass = '';
                    if (isSubirPedido) {
                        tdBodyClass = cssTd_cabeceraBody;
                    }
                    strHtml += '<td class="' + tdBodyClass + ' tdNombreProducto ';
                    strHtml += strHtmlColorFondo + '" ';//'">';
                    strHtml += ' OnMouseMove="OnMouseMoveProdructo(event)" OnMouseOver="OnMouseOverProdructo(' + i + ')" OnMouseOut="OnMouseOutProdructo()"  onclick="onclickRecuperarTransfer(' + i + '); return false;" >'
                    if (isSubirPedido) {
                        strHtml += '<div class="col-lg-10 col-md-10 col-sm-10 div_prod_sp">';
                    }
                    strHtml += listaProductosBuscados[i].pro_nombre;//AgregarMark( listaProductosBuscados[i].pro_nombre);
                    if (isNotGLNisTrazable) {
                        strHtml += '<span class="p_trazable">Producto trazable. Farmacia sin GLN.</span>';
                    }
                    if (cli_tomaTransfers() && listaProductosBuscados[i].isMostrarTransfersEnClientesPerf) {
                        if (listaProductosBuscados[i].isTieneTransfer) {
                            strHtml += '<span class="trf">TRANSFER COMBO</span>';//'<span>&nbsp;&nbsp;&raquo;&nbsp;Transfer Combo</span>';
                        } else if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                            strHtml += '<span class="trf">TRF</span>';//'<span>&nbsp;&nbsp;&raquo;&nbsp;TRF</span>';
                        }
                        // Mostrar solo producto Transfer 
                        if (listaProductosBuscados[i].pro_vtasolotransfer) {
                            strHtml += '<span class="p_trazable">Se vende solo por transfer</span>';// '<span class="spanProductoTrazableCLiSinGLN" >&nbsp;&nbsp;&nbsp;Se vende solo por transfer</span>';
                        }
                    }
                    // Vale Psicotropicos
                    if (listaProductosBuscados[i].isValePsicotropicos) {
                        strHtml += '<span class="p_trazable" >Requiere Vale</span>';
                        isMostrarImput = false;
                    }
                    // FIN Vale Psicotropicos
                    // + IVA
                    if (listaProductosBuscados[i].pro_neto) {
                        strHtml += '<span class="iva">+IVA</span>';
                    }
                    // FIN + IVA
                    // Producto erroneo
                    if (listaProductosBuscados[i].isProductoNoEncontrado) {
                        strHtml += '<span class="p_erronero">REGISTRO ERRONEO</span>';
                    }

                    // Ver si mostrar input solo producto Transfer 
                    if (listaProductosBuscados[i].pro_vtasolotransfer) {
                        isMostrarImput = false;
                    }

                    if (listaProductosBuscados[i].pri_nombreArchivo !== null) {
                        strHtml += '<i class="fa fa-camera color_emp_st pull-right"></i>';//style="width:20px;height:20px; "
                    }
                    if (isSubirPedido) {
                        strHtml += '</div>';
                        strHtml += '<div class="col-lg-2 col-md-2 col-sm-2 col_small">' + listaProductosBuscados[i].nroordenamiento + '</div>';//&nbsp;
                    }


                    strHtml += '</td>';


                    //

                    //

                    //
                    var precioPublico = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_precio.toFixed(2));
                    if (listaProductosBuscados[i].pro_precio === 0) {
                        precioPublico = '';
                    }

                    strHtml += '<td class="col-lg-1 col-md-1 col-sm-1 text-center">' + precioPublico + '</td>';

                    // 15/02/2018 mail de luciana para el producto
                    if (listaProductosBuscados[i].pro_nombre.match("^PAÑAL PAMI AD")) {
                        strHtml += '<td class="col-lg-1 col-md-1 col-sm-1 text-center">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].pro_preciofarmacia.toFixed(2)) + '</td>';
                    }
                    else {
                        strHtml += '<td class="col-lg-1 col-md-1 col-sm-1 text-center">$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinal.toFixed(2)) + '</td>';
                    }
                    // fin 15/02/2018 mail de luciana para el producto

                    var varOfeunidades = ' &nbsp; ';
                    var varOfeporcentaje = ' &nbsp; ';
                    var varPrecioConDescuentoOferta = ' &nbsp; ';
                    if (cli_tomaOfertas()) {
                        if (listaProductosBuscados[i].pro_ofeunidades !== 0 || listaProductosBuscados[i].pro_ofeporcentaje !== 0) {
                            varOfeunidades = listaProductosBuscados[i].pro_ofeunidades;
                            varOfeporcentaje = listaProductosBuscados[i].pro_ofeporcentaje;
                            varPrecioConDescuentoOferta = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioConDescuentoOferta.toFixed(2));
                        }
                    }
                    //Oferta
                    strHtml += '<td class="col-lg-2 col-md-2 col-sm-2 text-center no-padding">';
                    strHtml += '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 col_small">' + varOfeporcentaje + '</div>';
                    strHtml += '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-3 col_small">' + varOfeunidades + '</div>';
                    strHtml += '<div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 col_small">' + varPrecioConDescuentoOferta + '</div>'
                    strHtml += '</td>';
                    // FIn Oferta
                    // NUEVO Transfer facturacion directa
                    //if (!isCarritoDiferido) {
                    var varTransferFacturacionDirectaCondicion = '';
                    var varTransferFacturacionDirectaPrecio = '';
                    if (cli_tomaTransfers() && listaProductosBuscados[i].isMostrarTransfersEnClientesPerf) {
                        if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                            if (listaProductosBuscados[i].tde_unidadesbonificadasdescripcion !== null) {
                                varTransferFacturacionDirectaCondicion = listaProductosBuscados[i].tde_unidadesbonificadasdescripcion;
                            }
                            if (listaProductosBuscados[i].PrecioFinalTransfer !== null) {
                                varTransferFacturacionDirectaPrecio = '$&nbsp;' + FormatoDecimalConDivisorMiles(listaProductosBuscados[i].PrecioFinalTransfer.toFixed(2));
                            }
                        }
                    }
                    //
                    strHtml += '<td class="col-lg-1 col-md-1 col-sm-1 text-center">';
                    if (varTransferFacturacionDirectaCondicion !== '') {
                        strHtml += '<div  OnMouseMove="OnMouseMoveProdructoFacturacionDirecta(event)" OnMouseOver="OnMouseOverProdructoFacturacionDirecta(' + i + ')" OnMouseOut="OnMouseOutProdructoFacturacionDirecta()"  style="cursor:pointer;" >'
                        strHtml += varTransferFacturacionDirectaCondicion;
                        strHtml += '</div>';
                    }
                    strHtml += '</td>';
                    strHtml += '<td class="col-lg-1 col-md-1 col-sm-1 text-center">' + varTransferFacturacionDirectaPrecio + '</td>';
                    //}
                    // NUEVO Transfer facturacion directa

                    // Optimizar
                    for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                        strHtml += '<td class="col-lg-1 col-md-1 col-sm-1 col-xs-1 text-center">';
                        var isDibujarStock = false;
                        if (intPaginadorTipoDeRecuperar !== 2) {// todo transfer
                            isDibujarStock = true;
                        } // fin      if (intPaginadorTipoDeRecuperar != 2) { 
                        else { // si selecciona todo transfer y es producto facturacion directa
                            //if (!isCarritoDiferido) {
                            if (listaProductosBuscados[i].isProductoFacturacionDirecta) {
                                isDibujarStock = true;
                            }
                            //}
                        }
                        if (isDibujarStock) {
                            for (var iSucursal = 0; iSucursal < listaProductosBuscados[i].listaSucursalStocks.length; iSucursal++) {
                                if (listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_codsuc === listaSucursal[iEncabezadoSucursal]) {
                                    strHtml += '<div class="' + getNameClassStock(listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_stock) + '" style="float: none !important;"></div>';
                                    //  strHtml += '<div class="cont-estado-input"><div class="estado-' + listaProductosBuscados[i].listaSucursalStocks[iSucursal].stk_stock.toLowerCase() + '"></div>';
                                    //strHtml += '</div>';
                                    break;
                                }
                            }
                        }
                        strHtml += '</td>';
                    }
                    //}
                    strHtml += '</tr>';
                }
            } else {
                strHtml += '<tr><td colspan="8" class="text-center"><p class="color_red">La búsqueda no arroja resultados</p></td></tr>';
            }
            strHtml += '</tbody>';

            if (isSubirPedido) {
                strHtml += '</table>';
                strHtml += '</div>';
                strHtml += '<div class="div_tblsubirpedido_foot">';
                strHtml += '<table class="footable table table-stripped" width="100%" align="center" cellspacing="0" cellpadding="0" border="0">';

                var strHtml_part_1 = '';
                var strHtml_part_2 = '';
                for (var iEncabezadoSucursal = 0; iEncabezadoSucursal < listaSucursal.length; iEncabezadoSucursal++) {
                    strHtml_part_1 += '<div  id="tdRenglones_' + iEncabezadoSucursal + '" class="' + cssFootTd3Div + '" ></div>';
                    strHtml_part_2 += '<div id="tdUnidades_' + iEncabezadoSucursal + '" class="' + cssFootTd3Div + '" ></div>';
                }

                strHtml += '<tfoot>';
                strHtml += '<tr>';
                strHtml += '<td colspan="5" class="' + cssFootTd1 + '"></td>';
                strHtml += '<td class="' + cssFootTd2 + '">Renglones</td>';
                strHtml += '<td class="' + cssFootTd3 + '">';
                strHtml += strHtml_part_1
                strHtml += '</td>';
                strHtml += '</tr>';

                strHtml += '<tr>';
                strHtml += '<td colspan="5" class="' + cssFootTd1 + '"></td>';
                strHtml += '<td class="' + cssFootTd2 + '">Unidades</td>';
                strHtml += '<td class="' + cssFootTd3 + '">';
                strHtml += strHtml_part_2;
                strHtml += '</td>';
                strHtml += '</tr>';

                strHtml += '</tfoot>';
                strHtml += '</table>';
                strHtml += '</div>';
                strHtml += '<div><a class="btn_emp float-right" href="#" data-toggle="modal" data-target="#modalCargarPedido" onclick="CargarPedido(); return false;">CARGAR PEDIDO</a></div>';
                strHtml += '</div>';
            } else {
                strHtml += '</table>';
            }


            document.getElementById('divResultadoBuscador').innerHTML = strHtml + getHtmlTablaResolucionCelular();
            // Elejir el primer producto
            if ($('#inputSuc0_0').length) {
                $('#inputSuc0_0').focus();
                selectedInput = document.getElementById('inputSuc0_0');
            }
            if (isSubirPedido) {
                setTimeout(function () { CargarUnidadesRenglones(); }, 300);
                setTimeout(function () { ReAjustarColumnasBuscador(); }, 40);
            }
        }
    }
}
