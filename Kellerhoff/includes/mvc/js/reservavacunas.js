var listaReservasVacunas = null;


$(document).ready(function () {
    //prepararListaOfertas($('#hiddenListaOfertas').val());
    if (listaReservasVacunas == null) {
        listaReservasVacunas = eval('(' + $('#hiddenListaReservasVacunas').val() + ')');
        if (typeof listaReservasVacunas == 'undefined') {
            listaReservasVacunas = null;
        }
    }
  
    ReservasVacunas();

});
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
                strHtml += listaReservasVacunas[i].rdv_nombre;
                strHtml += '</td>';
                strHtml += '<td col-lg-3 col-md-3 col-sm-3 col-xs-3 text-right">';
               // strHtml += listaReservasVacunas[i].rdv_nombre;
                strHtml += '<input  class="form-control2" type="number" placeholder="Cantidad" value=""  />';
                //strHtml += '<a class="btn_emp pdf" href="..\\..\\archivos\\catalogo\\' + listaCatalogo[i].tbc_descripcion + '" target="_blank" title="DESCARGAR"><span>DESCARGAR</span></a>';
                //strHtml += '<a class="btn_emp pdf" href="..\\..\\servicios\\descargarArchivo.aspx?t=catalogo&n=' + listaCatalogo[i].tbc_descripcion + '" target="_blank" title="DESCARGAR"><span>DESCARGAR</span></a>';

                strHtml += '</td>';
                strHtml += '</tr>';
            }
            strHtml += '<tbody>';
            strHtml += '</table>';
        }
        $('#divGridReservaVacunas').html(strHtml);
    }
}