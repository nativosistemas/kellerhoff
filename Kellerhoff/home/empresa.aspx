﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master/home.Master" AutoEventWireup="true" CodeBehind="empresa.aspx.cs" Inherits="Kellerhoff.home.empresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript"  src="../includes/home/js/jquery/modernizr.js"></script>
    	<script type="text/javascript" src="../includes/home/js/jquery/timeline.js"></script>
	<script type="text/javascript">
    function slideSwitch() {
      var $active = $('#slideImg IMG.active');
      if ( $active.length == 0 ) $active = $('#slideImg IMG:last');
      var $next =  $active.next().length ? $active.next()
        : $('#slideImg IMG:first');
      $active.addClass('last-active');
      $next.css({opacity: 0.0})
        .addClass('active')
        .animate({opacity: 1.0}, 1000, function() {
           $active.removeClass('active last-active');
        });
    }
    $(function() {
        setInterval( "slideSwitch()", 5000 );
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <section class="section-gris-claro">
        <div class="image-container col-md-6 col-sm-12 pull-right clearfix hidden-sm hidden-xs no-padding-right">
          <div id="slideImg" class="slideImgCss background-image-holder-right">
                <img src="../img/home/empresa/empresa5.jpg" class="float-right img-responsive  active" />               
            <%--    <img src="../img/home/empresa/empresa2.jpg" class="float-right img-responsive" />--%>
                <img src="../img/home/empresa/empresa3.jpg" class="float-right img-responsive" />
                <%--<img src="../img/home/empresa/empresa4.jpg" class="float-right img-responsive" />--%>
                <img src="../img/home/empresa/empresa6.jpg" class="float-right img-responsive" />
               <%-- <img src="../img/home/empresa/empresa7.jpg" class="float-right img-responsive" />--%>
               <%-- <img src="../img/home/empresa/empresa1.jpg" class="float-right img-responsive" />--%>
                <img src="../img/home/empresa/empresa8.jpg" class="float-right img-responsive" />
                <img src="../img/home/empresa/empresa9.jpg" class="float-right img-responsive" />
          </div>
        </div>
        <div class="container"><div class="row">
          <div class="col-md-5 col-md-offset-0 col-sm-12 clearfix">
        	<h2 class="color_emp_nd">INFRAESTRUCTURA</h2>
            <div class="clear" style="height:26px"></div>
          	<div id="slideImg" class="slideImgCss visible-sm visible-xs">
                 <img src="../img/home/empresa/empresa5.jpg" class="float-right img-responsive active" />
                <%--<img src="../img/home/empresa/empresa2.jpg" class="float-right img-responsive" />--%>
                <img src="../img/home/empresa/empresa3.jpg" class="float-right img-responsive" />
               <%-- <img src="../img/home/empresa/empresa4.jpg" class="float-right img-responsive" /> --%>       
                <img src="../img/home/empresa/empresa6.jpg" class="float-right img-responsive" />
           <%--     <img src="../img/home/empresa/empresa7.jpg" class="float-right img-responsive" />
                <img src="../img/home/empresa/empresa1.jpg" class="float-right img-responsive" />--%>
                <img src="../img/home/empresa/empresa8.jpg" class="float-right img-responsive" />
                <img src="../img/home/empresa/empresa9.jpg" class="float-right img-responsive" />
            </div>
            <div class="clear25 visible-xs"></div>
			<p>En casa central y sucursales contamos con mas de <b>11.000 m2 donde almacenamos y paletizamos los mas de 19.000 productos</b> que distribuimos. Las 450 personas que trabajan en nuestra empresa y los más de 80 vehículos propios hacen posible que lleguemos en tiempo y forma a nuestros clientes.</p> 
            <p>Nuestro depósito garantiza la óptima temperatura (refrigerado o calefaccionado) para el cuidado de casi 150.000 unidades que se preparan diariamente.</p> 
            <p>Los procesos logísticos se han ido adaptando a lo largo de los años a las nuevas tecnologías existentes en el mundo, a efectos de mejorar la velocidad de preparación y la reducción de  errores. Actualmente, contamos con un sistema de transporte y preparación automática de pedidos adquirido a la empresa SSI  Schafer Peem de Austria. Este sistema nos permite preparar automáticamente más del 70% de los productos de mayor rotación, <b>operar más de 1200 pedidos por hora y reducir la tasa de error al 0,2%</b>.</p>
            <p>Además, contamos con una amplia flota de vehículos para asegurar la <b>puntualidad en la entrega a cada uno de nuestros Clientes</b>.</p>
          </div>
        </div></div>
    </section>
    <div class="clear100 visible-lg "></div>
    <div class="clear30 visible-md"></div>


	<section id="cd-timeline" class="section-blanco-timeline">
        <div class="container">
            <div class="row"><div class="col-xs-12">
                
                <div class="cd-timeline-block">
                    <div class="cd-timeline-img segmento" style="top:0px"><div class="inicial"></div></div>
                    <div class="cd-timeline-content">
                        <span class="cd-date visible-sm visible-xs">1910</span>
                        <div class="clear5 visible-sm visible-xs"></div>
                        <h2>Los comienzos</h2>
						<p>El <b>10 de octubre de 1910, Don José Félix Kellerhoff</b> funda la empresa en el local de calle Mendoza 1015 de la ciudad de Rosario, Santa Fe, Argentina. Lleno de expectativas e ilusiones, se dedicó a la comercialización de insecticidas, para agregar luego productos de perfumería y accesorios para farmacias.</p>
                        <span class="cd-date hidden-sm hidden-xs">1910</span>
                        <img src="../img/home/historia/1.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />
                    </div> <!-- cd-timeline-content -->
                </div> <!-- cd-timeline-block -->
        		
                <div class="cd-timeline-block">
                    <div class="cd-timeline-img"></div>
                    <div class="cd-timeline-content">
                        <span class="cd-date visible-sm visible-xs">1914</span>
                        <div class="clear5 visible-sm visible-xs"></div>
						<p>Posteriormente, con el estallido de la Primera Guerra Mundial en 1914, y tras superar las lógicas dificultades de aquel momento, <b>Don José decidió incorporar los primeros productos medicinales</b> – comenzando con la sencilla y familiar aspirina – en un intento por atenuar la escasez provocada por la demanda extraordinaria de medicamentos.</p>
                        <span class="cd-date hidden-sm hidden-xs">1914</span>
                        <!--<img src="../img/home/historia/2.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />-->
                    </div> <!-- cd-timeline-content -->
                </div> <!-- cd-timeline-block -->
        
                <div class="cd-timeline-block">
                    <div class="cd-timeline-img"></div>
                    <div class="cd-timeline-content">
                        <span class="cd-date visible-sm visible-xs">1920</span>
                        <div class="clear5 visible-sm visible-xs"></div>
                    	<p>En septiembre de <b>1920 muda la Empresa al local de calle Rioja 1308</b>, pero el incesante crecimiento de la actividad hace que pronto éste resulte demasiado chico. Por eso, unos años después, en <b>febrero de 1924, se traslada a calle Santa Fe 1364, donde se establece durante veinte años</b>.</p>
                        <span class="cd-date hidden-sm hidden-xs">1920</span>
                        <!--<img src="../img/home/historia/3.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />-->
                    </div> <!-- cd-timeline-content -->
                </div> <!-- cd-timeline-block -->
        
                <div class="cd-timeline-block">
                    <div class="cd-timeline-img"></div>
                    <div class="cd-timeline-content">
                        <span class="cd-date visible-sm visible-xs">1929</span>
                        <div class="clear5 visible-sm visible-xs"></div>
                        <h2>LA 2&deg; GENERACIÓN</h2>
                        <p>En <b>1929, el hijo de Don José, Don Carlos Antonio Federico José Kellerhoff</b>, se pone al frente de la Empresa, que pasa a llamarse "Carlos Kellerhoff – Drogueria". La presencia de la misma se extendió en la plaza y en la zona. Se aumentó el personal, se ampliaron los medios técnicos y materiales de trabajo y se encaró la construcción de un local propio.</p>
                        <span class="cd-date hidden-sm hidden-xs">1929</span>
                        <img src="../img/home/historia/4.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />
                    </div> <!-- cd-timeline-content -->
                </div> <!-- cd-timeline-block -->
        
                <div class="cd-timeline-block">
                    <div class="cd-timeline-img"></div>
                    <div class="cd-timeline-content">
                        <span class="cd-date visible-sm visible-xs">1944</span>
                        <div class="clear5 visible-sm visible-xs"></div>
						<p>Finalmente, el <b>22 de febrero de 1944 se concretó el ansiado traslado de la Empresa al edificio nuevo, ubicado en calle Corrientes 981</b>. Siempre con la guía de una recta conducta comercial, se extendió aún más la larga lista de clientes, tanto de Rosario como de localidades vecinas.</p>        
                        <span class="cd-date hidden-sm hidden-xs">1944</span>
                        <img src="../img/home/historia/5.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />
                    </div> <!-- cd-timeline-content -->
                </div> <!-- cd-timeline-block -->
                
                <div class="cd-timeline-block">
                    <div class="cd-timeline-img"></div>
                    <div class="cd-timeline-content">
                        <span class="cd-date visible-sm visible-xs">1950</span>
                        <div class="clear5 visible-sm visible-xs"></div>
                        <h2>LA 3&deg; GENERACIÓN</h2>
						<p>El incesante crecimiento experimentado por entonces, dio lugar en <b>1950 a la incorporación de sus tres hijos, Francisco José, Federico Carlos e Irmgard</b>, trillizos, nacidos en Alemania, quienes de esta forma siguieron el camino que trazaran su padre y su abuelo dentro de la Empresa. Esta incorporación, entre otras cosas, llevó a cambiar la denominación y estructura de la Empresa, por la de "Drogueria Kellerhoff S.A".</p>
                        <span class="cd-date hidden-sm hidden-xs">1950</span>
                        <img src="../img/home/historia/6.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />
                    </div> <!-- cd-timeline-content -->
                </div> <!-- cd-timeline-block -->
        
                <div class="cd-timeline-block">
                    <div class="cd-timeline-img"></div>
                    <div class="cd-timeline-content">
                        <span class="cd-date visible-sm visible-xs">1970</span>
                        <div class="clear5 visible-sm visible-xs"></div>
                    	<p>En <b>1970 se hace cargo de la firma Francisco José Kellerhoff</b>. Durante su exitosa gestión, se inauguran en 1995 sus oficinas actuales, una planta modelo sita en calle Pueyrredon 1149.</p>
                        <span class="cd-date hidden-sm hidden-xs">1970</span>
<%--                        <img src="../img/home/historia/7.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />--%>
                    </div> <!-- cd-timeline-content -->
                </div> <!-- cd-timeline-block -->

                <div class="cd-timeline-block" style="padding-bottom:50px !important">
                    <div class="cd-timeline-img"></div>
                    <div class="cd-timeline-content">
                        <span class="cd-date visible-sm visible-xs">2010</span>
                        <div class="clear5 visible-sm visible-xs"></div>
                        <h2>LA 4&deg; GENERACIÓN</h2>
                        <p>En 2010, con el cumplimiento de sus <b>100 años</b> se hicieron cargo de la empresa <b>Carlos, Sonia y Elisabet Boggio, hijos de Irmgard Kellerhoff</b>, quienes decidieron seguir invirtiendo en tecnología renovando el anterior sistema automático de preparación de la empresa SCHÄFER.</p>
                        <span class="cd-date hidden-sm hidden-xs">2010</span>
        <%--                <img src="../img/home/historia/8.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />--%>
                        <img src="../img/home/historia/7.jpg" class="cd-img img-responsive" alt="Droguer&iacute;a Kellerhoff" />
                    </div> <!-- cd-timeline-content -->
                </div> <!-- cd-timeline-block -->

                <div class="cd-timeline-block hidden-sm hidden-xs" style="padding-bottom:0px !important">
                    <div class="cd-timeline-img segmento" style="top:16px !important"><div class="inicial"></div></div>
                    <div class="cd-timeline-content" style="padding-bottom:0px !important">
                        <span class="cd-date" style="top:16px">HOY</span>
                    </div> 
                </div> 
                <div class="cd-timeline-block visible-sm visible-xs" style="padding:0px !important">
                    <div class="cd-timeline-img segmento" style="top:10px !important"><div class="inicial"></div></div>
                    <div class="cd-timeline-content" style="padding-bottom:0px !important">
                        <span class="cd-date" style="padding-top:10px;">HOY</span>
                    </div> 
                </div> 

			</div></div>
        </div>
    </section>

	<section class="section-blanco-timeline2">
        <div class="container">
            <div class="row">
            	<div class="col-md-8 col-md-push-2 text-center hidden-sm hidden-xs">
                    <p class="p_17">Innovando en tecnología, con los valores que nos identificaron en nuestros más de <b>100 años de trayectoria</b>.</p>
                    <p class="p_17">Nos consolidamos como una empresa que desarrolla y crece junto a ustedes con el único objetivo de brindarles el mejor servicio.</p>
				</div>
            	<div class="col-sm-1 col-xs-1 visible-xs visible-sm"></div>
                <div class="col-sm-11 col-xs-11 text-left visible-xs visible-sm" style="padding-top:0px; padding-left:26px">
                    <p class="p_17">Innovando en tecnología, con los valores que nos identificaron en nuestros más de <b>100 años de trayectoria</b>.</p>
                    <p class="p_17">Nos consolidamos como una empresa que desarrolla y crece junto a ustedes con el único objetivo de brindarles el mejor servicio.</p>
				</div>
            </div>            
		</div>
	</section>        

</asp:Content>

