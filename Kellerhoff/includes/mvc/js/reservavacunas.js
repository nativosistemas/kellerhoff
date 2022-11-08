var listaReservasVacunas = null;
var listaReservasVacunas_mis = null;
var listaReservasVacunas_total = null;

$(document).ready(function () {
    //prepararListaOfertas($('#hiddenListaOfertas').val());
    if (listaReservasVacunas == null) {
        listaReservasVacunas = eval('(' + $('#hiddenListaReservasVacunas').val() + ')');
        if (typeof listaReservasVacunas == 'undefined') {
            listaReservasVacunas = null;
        }
    }
    if (listaReservasVacunas_mis == null) {
        listaReservasVacunas_mis = eval('(' + $('#hiddenListaReservasVacunas_mis').val() + ')');
        if (typeof listaReservasVacunas_mis == 'undefined') {
            listaReservasVacunas_mis = null;
        }
    }
    if (listaReservasVacunas_total == null) {
        listaReservasVacunas_total = eval('(' + $('#hiddenListaReservasVacunas_total').val() + ')');
        if (typeof listaReservasVacunas_total == 'undefined') {
            listaReservasVacunas_total = null;
        }
    }

    ReservasVacunas();
    ReservasVacunas_mis();
    ReservasVacunas_total();
});
function onclickReservarVacunas() {
    var l_reserva = [];
    for (var i = 0; i < listaReservasVacunas.length; i++) {

        //listaReservasVacunas[i].rdv_nombre
        var oTextNumber = document.getElementById('textReserva' + i).value;
        if (isNotNullEmpty(oTextNumber)) {
            var data = {};
            data.ID = 0;
            data.Login = cli_login();
            data.NombreProducto = listaReservasVacunas[i].rdv_nombre;
            data.UnidadesVendidas = oTextNumber;
            l_reserva.push(data);
        }
    }
    if (l_reserva.length > 0) {
        enviarReservaVacunas(l_reserva);
    }
}
function ReservasVacunas() {
    if (listaReservasVacunas != null) {
        if (listaReservasVacunas.length > 0) {
            var strHtml = '';
            strHtml += '<table class="footable table sin_b table-stripped" data-empty="No hay informacion disponible" width="100%" align="center" cellspacing="1" cellpadding="5" border="0">';
            //
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Descripci&oacute;n</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Condici&oacute;n</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Plazo Pago</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Unid. Pedidas</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            //
            strHtml += '<tbody>';
            for (var i = 0; i < listaReservasVacunas.length; i++) {
                var strHtmlColorFondo = 'wht';
                if (i % 2 != 0) {
                    strHtmlColorFondo = 'grs';
                }
                strHtml += '<tr class="' + strHtmlColorFondo + '">';
                strHtml += '<td class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">';
                strHtml += listaReservasVacunas[i].rdv_nombre;
                strHtml += '</td>';
                strHtml += '<td class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">';
                strHtml += listaReservasVacunas[i].rdv_condicion;
                strHtml += '</td>';
                strHtml += '<td class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">';
                strHtml += listaReservasVacunas[i].rdv_plazo;
                strHtml += '</td>';
                strHtml += '<td col-lg-3 col-md-3 col-sm-3 col-xs-3 text-right">';
                strHtml += '<input id="' + 'textReserva' + i + '" class="form-control2" onblur="onblurValidarMultiplo(event, this)" type="number" placeholder="Cantidad" value=""  />';
                strHtml += '</td>';
                strHtml += '</tr>';
            }
            strHtml += '<tbody>';
            strHtml += '</table>';
            if (listaReservasVacunas.length > 0) { 
                strHtml += '<button class="btn_emp" onclick="onclickReservarVacunas(); return false;">Enviar</button>';
            }
        }
        $('#divGridReservaVacunas').html(strHtml);
    }
}
function ReservasVacunas_mis() {
    if (listaReservasVacunas_mis != null) {
        if (listaReservasVacunas_mis.length > 0) {
            var strHtml = '';
            strHtml += '<table class="footable table sin_b table-stripped" data-empty="No hay informacion disponible" width="100%" align="center" cellspacing="1" cellpadding="5" border="0">';
            //
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Fecha</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Descripci&oacute;n</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Unid. Pedidas</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            //
            strHtml += '<tbody>';
            for (var i = 0; i < listaReservasVacunas_mis.length; i++) {
                var strHtmlColorFondo = 'wht';
                if (i % 2 != 0) {
                    strHtmlColorFondo = 'grs';
                }
                strHtml += '<tr class="' + strHtmlColorFondo + '">';
                strHtml += '<td class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">';
                strHtml += listaReservasVacunas_mis[i].fechaToString;
                strHtml += '</td>';
                strHtml += '<td class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">';
                strHtml += listaReservasVacunas_mis[i].rdv_nombre;
                strHtml += '</td>';
                strHtml += '<td col-lg-3 col-md-3 col-sm-3 col-xs-3 text-right">';
                strHtml += '<input  class="form-control2" type="number" placeholder="Cantidad" value="' + listaReservasVacunas_mis[i].unidadPedidas + '" disabled />';
                strHtml += '</td>';
                strHtml += '</tr>';
            }
            strHtml += '<tbody>';
            strHtml += '</table>';
        }
        $('#divGridReservaVacunas').html(strHtml);
    }
}
function ReservasVacunas_total() {
    if (listaReservasVacunas_total != null) {
        if (listaReservasVacunas_total.length > 0) {
            var strHtml = '';
            strHtml += '<table class="footable table sin_b table-stripped" data-empty="No hay informacion disponible" width="100%" align="center" cellspacing="1" cellpadding="5" border="0">';
            //
            strHtml += '<thead>';
            strHtml += '<tr>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Descripci&oacute;n</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '<th class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-center no-padding">';
            strHtml += '<table width="100%" cellpadding="0" cellspacing="0">';
            strHtml += '<tr><td class="col-lg-12 text-center">&nbsp;<div class="clear5"></div></td></tr>';
            strHtml += '<tr class="tr_thead"><td class="col-lg-12 text-center">Unidades Totales</td></tr>';
            strHtml += '</table>';
            strHtml += '</th>';
            strHtml += '</tr>';
            strHtml += '</thead>';
            //
            strHtml += '<tbody>';
            for (var i = 0; i < listaReservasVacunas_total.length; i++) {
                var strHtmlColorFondo = 'wht';
                if (i % 2 != 0) {
                    strHtmlColorFondo = 'grs';
                }
                strHtml += '<tr class="' + strHtmlColorFondo + '">';
                strHtml += '<td class="col-lg-3 col-md-3 col-sm-3 col-xs-3 text-left">';
                strHtml += listaReservasVacunas_total[i].rdv_nombre;
                strHtml += '</td>';
                strHtml += '<td col-lg-3 col-md-3 col-sm-3 col-xs-3 text-right">';
                strHtml += '<input  class="form-control2" type="number" placeholder="Cantidad" value="' + listaReservasVacunas_total[i].unidadTotales + '" disabled  />';
                strHtml += '</td>';
                strHtml += '</tr>';
            }
            strHtml += '<tbody>';
            strHtml += '</table>';
        }
        $('#divGridReservaVacunas').html(strHtml);
    }
}
/*
  onkeypress="return onkeypressEnterReserva(event, this);"
function onkeypressEnterReserva(e, elemento) {
    let result = true;
    let numero;
    let multiplo;
    let tecla;
    tecla = (document.all) ? e.keyCode : e.which;
    teclaEnNumero = 0;
    numero = parseInt(toString(elemento.value) + toString(teclaEnNumero));
    multiplo = 2;
   
    if (tecla == 13) {//es numerico
        
    }
   


    //if (numero % multiplo == 0) {
    //    result = true;
    //}
    return result;
}
*/

function onblurValidarMultiplo(e, elemento) {
    if (elemento.value != '') {
        let numero;
        let multiplo;
        multiplo =  listaReservasVacunas[parseInt(elemento.id.replace('textReserva', ''))].rdv_multiplo;
        numero = parseInt(elemento.value);
        //multiplo = 2;
        if (numero % multiplo == 0) {

        }
        else {
            elemento.value = '';
            elemento.style.backgroundColor = "red";
            setTimeout(function () {
                elemento.style.backgroundColor = '';
            }, 700);
        }
    }
}