<%@ Page Title="" Language="C#" MasterPageFile="~/master/home.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs"
    Inherits="Kellerhoff.home.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="slide_dest">
    </div>



    <section class="section-gris-claro" id="destacados">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="row">
                        <div class="col-lg-10 col-md-9 col-sm-9 col-xs-12">
                            <h2 class="text-left color_emp_nd hidden-xs">OFERTAS DESTACADAS</h2>
                            <h2 class="text-center color_emp_nd visible-xs">OFERTAS DESTACADAS</h2>
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-3 col-xs-12">
                            <div class="clear5 hidden-sm"></div>
                            <a class="btn_emp float-right hidden-xs" href="promociones.aspx">VER TODAS</a>
                            <p class="text-center visible-xs"><a href="promociones.aspx">Ver todas las ofertas</a></p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear55 hidden-xs clear55_ofertas"></div>
            <div class="clear20 visible-xs"></div>
            <div id="divContenedorOfertas"></div>
        </div>
    </section>

    <!-- RECALL -->
    <section id="divContenedorRecallIndex" class="section-blanco" style="display: none;">
    </section>
    <!-- FIN RECALL -->
    <asp:Panel ID="Panel_Revista" runat="server" Visible="false">
        <section class="section-revista">
            <div class="container">
                <div class="row">
                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                        <h2 class="text-left color_white">REVISTA ONLINE</h2>
                        <div class="clear10"></div>
                        <p class="text-left color_white">Descarg&aacute; nuestra revista Mundo Kellerhoff, mantenete informado con nuestras noticias y descubr&iacute; todas las ofertas.</p>
                        <div class="clear10"></div>
                        <% htmlPublicarRevista(); %>
                    </div>
                    <div class="col-lg-6 col-md-6 hidden-xs hidden-sm">
                        <img src="../img/home/revista.png" class="revista_home" />
                    </div>
                </div>
            </div>
        </section>
    </asp:Panel>
    <section class="section-blanco-image clearfix">
        <div class="image-container col-md-6 pull-left hidden-sm hidden-xs">
            <div class="background-image-holder-left p_right">
                <img src="../img/home/empresa.jpg" alt="Droguer&iacute;a Kellerhoff" class="img-responsive" />
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-md-6 col-md-offset-6 col-sm-12 clearfix">
                    <div class="clear0" style="height: 85px"></div>
                    <p class="p_35 color_emp_nd">UNA DROGUERIA DE GRAN TRAYECTORIA</p>
                    <div class="clear15"></div>
                    <p>El 10 de octubre de 1910, Don José Félix Kellerhoff funda la empresa en el local de calle Mendoza 1015 de la ciudad de Rosario, Santa Fe, Argentina.</p>
                    <p>Lleno de expectativas e ilusiones, se dedicó a la comercialización de insecticidas, para agregar luego productos de perfumería y accesorios para farmacias. Posteriormente, con el estallido de la Primera Guerra Mundial en 1914, y tras superar las lógicas dificultades de aquel momento, Don José decidió incorporar los primeros productos medicinales – comenzando con la sencilla y familiar aspirina – en un intento por atenuar la escasez provocada por la demanda extraordinaria de medicamentos.</p>
                    <div class="clear15"></div>
                    <a class="btn_emp" href="empresa.aspx">CONOCENOS</a>
                </div>
            </div>
        </div>
        <div class="clear" style="height: 77px"></div>
    </section>

    <section id="sucursales" class="secction-white hidden-xs">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12 no-padding">
                    <div class="iframe_maps">
                        <div id="map_canvas2"></div>
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-5 div_ref_mapa">
                                    <div class="ref_mapa_bg"></div>
                                    <div class="ref_mapa">
                                        <h2 class="text-left color_white">SUCURSALES</h2>
                                        <!-- 
								Por cada sucursal incluir un A con los siguientes atributos
								
								data-role="expand-info"
								data-info="XX"
								
								donde XX debe ser el ID del div conla informacion, segun se indica abajo
								-->
                                        <a data-role="expand-info" data-info="1" href="">ROSARIO</a>
                                        <a data-role="expand-info" data-info="10" href="">DK HOSPITALARIO</a>
                                        <a data-role="expand-info" data-info="7" href="">SAN NICOLAS</a>
                                        <a data-role="expand-info" data-info="2" href="">CHA&Ntilde;AR LADEADO</a>
                                        <a data-role="expand-info" data-info="6" href="">SANTA FE</a>
                                        <a data-role="expand-info" data-info="5" href="">C&#211;RDOBA</a>
                                        <a data-role="expand-info" data-info="8" href="">VILLA MARÍA</a>
                                        <a data-role="expand-info" data-info="9" href="">RÍO CUARTO</a>
                                        <a data-role="expand-info" data-info="3" href="">CONCEPCI&#211;N DEL URUGUAY</a>
                                        <a data-role="expand-info" data-info="4" href="">CONCORDIA</a>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-5 col-sm-6 div_info_mapa hidden">
                                    <div class="info_mapa_bg"></div>
                                    <!-- 
                            Por cada sucursal incluir un DIV con class "info_mapa hidden" 
                            y el atributo data-info="XX"
                            
                            donde XX debe ser el ID asignado en el link mas arriba
                            -->
                                    <div class="info_mapa hidden" data-info="1">
                                        <b>ROSARIO</b><br />
                                        <u>Conmutador General</u>: 0341-4203300<br />
                                        <u>Venta líneas Rotativas</u>: 0341-4203311<br />
                                        <u>Cuentas Corrientes</u>: 0341-4203329 / 0341-4203336<br />
                                        <u>Reclamos</u>: 0341-4203314<br />
                                        <u>Mostrador</u>: 0341-4203363<br />
                                        <u>Jefatura de ventas</u>: Joaquín Gamborena<br />
                                        <u>Promotores</u>: Mario Palacios - Néstor Perez - Facundo Angelini<br />
                                        <u>Farmacéutica</u>: Cristina Kellerhoff<br />
                                        <u>Domicilio</u>: Pueyrredon 1149<br />
                                        <u>C.P.A.</u>: S2000QIG<br />
                                        <u>WhatsApp</u>: 341 3163291<br />
                                        <u>Fax</u>: 0341-4203377<br />
                                        <u>E-mail</u>: drogueria@kellerhoff.com.ar<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="2">
                                        <b>CHA&Ntilde;EAR LADEADO</b><br />
                                        <u>Encargado</u>: Juan Carlos Ferraris<br />
                                        <u>Promotor</u>: Alexis Ronzani<br />
                                        <u>Ctas. Ctes.</u>: Carlos Filipuzzi<br />
                                        <u>Farmacéutico</u>: Juan Carlos Ferraris<br />
                                        <u>Domicilio</u>: Belgrano 245<br />
                                        <u>C.P.A.</u>: S2139AFE<br />
                                        <u>Teléfonos</u>: 03468-481881 (Tel/Fax) / 481117<br />
                                        <u>WhatsApp</u>: 3468 648933<br />
                                        <u>E-Mail</u>: sucursalchanarladeado@kellerhoff.com.ar<br />
                                        <u>Horarios</u>: L a V : 8:00 a 13:00 hs. / 16:00 a 20:30 hs.<br />
                                        Sáb : 8:30 a 13:00 hs.<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="3">
                                        <b>CONCEPCI&#211;N DEL URUGUAY</b><br />
                                        <u>Encargado</u>: Eduardo Gutierrez<br />
                                        <u>Promotor</u>: Oscar Moyano<br />
                                        <u>Ctas. Ctes.</u>: Nadia Saade<br />
                                        <u>Farmacéutico</u>: Roberto O. Bracco<br />
                                        <u>Domicilio</u>: Posadas 976<br />
                                        <u>C.P.A.</u>: E3260ATF<br />
                                        <u>Teléfonos</u>: 03442-423103 y Rotativas / 03442-427888 (Fax)<br />
                                        <u>WhatsApp</u>: 3442 567240<br />
                                        <u>E-Mail</u>: sucursalconcepcion@kellerhoff.com.ar<br />
                                        <u>Horarios</u>: L a V : 8:00 a 13:00 hs. / 16:00 a 21:00 hs.<br />
                                        Sáb :  8:00 a 13:00 hs. / 17:00 a 19:00 hs.<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="4">
                                        <b>CONCORDIA</b><br />
                                        <u>Encargado</u>: Adrián Caglieris<br />
                                        <u>Farmacéutico</u>: Nicolás Cian<br />
                                        <u>Domicilio</u>: Sarmiento 642<br />
                                        <u>C.P.A.</u>: E3202AGL<br />
                                        <u>Teléfonos</u>: 0345-4222545 (Tel/Fax)<br />
                                        <u>WhatsApp</u>: 3442 567240<br />
                                        <u>E-Mail</u>: sucursalconcordia@kellerhoff.com.ar<br />
                                        <u>Horarios</u>: L a V : 8:00 a 12:30 hs. / 16:30 a 20:30 hs.<br />
                                        Sáb : 8:00 a 12:30 hs.<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="5">
                                        <b>C&#211;RDOBA</b><br />
                                        <u>Encargado</u>: Marcelo Bravin<br />
                                        <u>Promotor</u>: José Portelli - Olga Echenique - Juan Pablo Martellotto<br />
                                        <u>Ctas. Ctes.</u>: Carolina Beretta<br />
                                        <u>Farmacéutico</u>: Karina Nieddu<br />
                                        <u>Supervisor de Ventas</u>: Edgar Martinelli<br />
                                        <%--DOMICILIO: RAYO CORTADO 2275 - Bº EMPALME
CPA: X5006IMG
TELEFONO: 0351-5236100 / 0351-5236111 (PEDIDOS)--%>
                                        <u>Domicilio</u>: RAYO CORTADO 2275 - Bº EMPALME<br />
                                        <u>C.P.A.</u>: X5006IMG<br />
                                        <u>Teléfonos</u>: 0351-5236100 / 0351-5236111 (PEDIDOS)<br />
                                        <%--                                        <u>Domicilio</u>: Libertad 248<br />
                                        <u>C.P.A.</u>: X5000FGF<br />
                                        <u>Teléfonos</u>: 0351-4251118 y Rotativas<br />--%>
                                        <u>WhatsApp</u>: 351 6523189<br />
                                        <u>E-Mail</u>: sucursalcordoba@kellerhoff.com.ar<br />
                                        <u>Horarios</u>: L a V : 8:00 a 20:30 hs.<br />
                                        Sáb : 8:00 a 13:00 hs. / 17.30 a 19.00 hs.<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="6">
                                        <b>SANTA FE</b><br />
                                        <u>Encargado</u>: Walter Cesar Luque<br />
                                        <u>Promotor</u>: Maximiliano Flores<br />
                                        <u>Ctas. Ctes.</u>: Daniela Cardozo – Claudia Campagnucci<br />
                                        <u>Farmacéutico</u>: Patricia Borrelli<br />
                                        <u>Domicilio</u>: Av. Blas Parera 6633<br />
                                        <u>C.P.A.</u>: S3000FCC<br />
                                        <u>Teléfonos</u>: 0342-4587900 y Rotativas<br />
                                        <u>WhatsApp</u>: 342 4070448<br />
                                        <u>E-Mail</u>: sucursalsantafe@kellerhoff.com.ar<br />
                                        <u>Horarios</u>: L a V : 08:00 a 21:00 hs.<br />
                                        Sáb : 08:00 a 15:00 hs.<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="7">
                                        <b>SAN NICOLAS</b><br />
                                        <u>Encargado</u>: Guillermo Díaz<br />
                                        <u>Promotor</u>: German Gonzalez<br />
                                        <u>Ctas. Ctes.</u>: Nanci Giussani<br />
                                        <u>Farmacéutico</u>: Pablo Prado<br />
                                        <u>Domicilio</u>: Pte Perón 1445<br />
                                        <u>C.P.A.</u>: B2902AGE<br />
                                        <u>Teléfonos</u>: 0336-4443006<br />
                                        <u>E-Mail</u>: sucursalsannicolas@kellerhoff.com.ar<br />
                                        <u>Horarios</u>: L a V de 10:00 a 12:00 y de 18:00 a 20:00 hs.<br />
                                        Sáb :  10:00 a 12:00 hs.<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="8">
                                        <b>VILLA MARÍA</b><br />
                                        <u>Encargado</u>: Manuel Vinzón<br />
                                        <u>Farmacéutico</u>: Natalia Buhlman<br />
                                        <u>Promotor</u>: Agustín Arias<br />
                                        <u>Domicilio</u>: Leandro N Alem 330<br />
                                        <u>C.P.A.</u>: X5900JAQ<br />
                                        <u>Teléfonos</u>: 0353-4548519 / 0353-4546570<br />
                                        <u>E-Mail</u>: sucursalvillamaria@kellerhoff.com.ar<br />
                                        <u>Horarios</u>: L a V de  8:30 a 12:30 y de 16:30 a 20:30 hs.<br />
                                        Sáb :  8:30 a 12:30 hs.<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="9">
                                        <b>RÍO CUARTO</b><br />
                                        <u>Encargado</u>: Gustavo Oscar Antonucci<br />
                                        <u>Farmacéutico</u>: Guillermo Canónico<br />
                                        <u>Promotor</u>: Agustín Arias<br />
                                        <u>Domicilio</u>: Buenos Aires 264<br />
                                        <u>C.P.A.</u>: X5800DIF<br />
                                        <u>Teléfonos</u>: 0358-4633632 / 4650717 / 4654048 / 4702634<br />
                                        <u>E-Mail</u>: sucursalriocuarto@kellerhoff.com.ar<br />
                                        <u>Horarios</u>: L a V de 8:30 a 21:00  hs.<br />
                                        Sáb :  8:30 a 13:00 hs.<br />
                                    </div>
                                    <div class="info_mapa hidden" data-info="10">
                                        <b>DK HOSPITALARIO</b><br />
                                        <u>Conmutador General</u>: 0341-4239280/4239280<br />
                                        <u>Cuentas Corrientes</u>: 0341-4203329 / 0341-4203336<br />
                                        <u>Gerente de Ventas</u>: Carlos Mira<br />
                                        <u>Responsable Administrativa</u>: María Alejandra Vitali<br />
                                        <u>Promotores</u>: Maximiliano Frontera - Walter Pasquinelli - Federico Gauna - Esteban Cohen<br />
                                        <u>Farmacéutico</u>: Carlos Giadans<br />
                                        <u>Domicilio</u>: Uriburu 1923/1927<br />
                                        <u>C.P.A.</u>: S2001CFM<br />
                                        <u>Fax</u>: 0341-4239288<br />
                                        <u>E-mail</u>: dkinstitucional@kellerhoff.com.ar<br />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <%  AgregarHtmlOculto(); %>

    <%--    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCj5tbgIz5_otRZz97proggQAnX0p1ZyRU"></script>
    <script type="text/javascript" src="../includes/home/js/jquery/maps.js"></script>--%>
    <script src="../includes/home/js/oferta.js?n=9"></script>
    <script src="../includes/home/js/slider.js?n=15"></script>
    <script src="../includes/home/js/index.js?n=4"></script>
    <script type="text/javascript">
        $(window).on('resize', function () {
            // Set interval until timer is cleared, indicating end of the resize event
            waitForFinalEvent(function () {
                // Get owl carousel
                var owldata = $(".owl-carousel").data('owlCarousel');
                // Use the owl Carousel's reset method
                owldata.updateVars();
            }, 100, "OwlCarouselResizeReset");
        });
        var waitForFinalEvent = (function () {
            var timers = {};
            return function (callback, ms, uniqueId) {
                if (!uniqueId) {
                    uniqueId = "Don't call this twice without a uniqueId";
                }
                if (timers[uniqueId]) {
                    clearTimeout(timers[uniqueId]);
                }
                timers[uniqueId] = setTimeout(callback, ms);
            };
        })();

    </script>

</asp:Content>
