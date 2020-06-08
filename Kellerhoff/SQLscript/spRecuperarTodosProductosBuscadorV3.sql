USE [db_Kellerhoff]
GO
/****** Object:  StoredProcedure [Productos].[spRecuperarTodosProductosBuscadorV3]    Script Date: 06/08/2020 13:55:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  procedure [Productos].[spRecuperarTodosProductosBuscadorV3]
@Where nvarchar (4000),
@WherePrimeraOrdenacion nvarchar (4000),
@Sucursal nvarchar(2),
@codCliente int,
@cli_codprov nvarchar(75)
 AS

 Declare @cli_tipo nvarchar(1)
 SELECT @cli_tipo = [cli_tipo]--'P'
  FROM [tbl_Clientes]
  where [cli_codigo] = @codCliente 

DECLARE @SQLStringPrimeraOrdenacion nvarchar (4000)
DECLARE @SQLString nvarchar (4000)

--SET @SQLStringPrimeraOrdenacion = N'Select  pro_codigo,pro_nombre,pro_precio,pro_preciofarmacia,pro_ofeunidades,pro_ofeporcentaje ,pro_neto,pro_codtpopro,pro_descuentoweb,pro_laboratorio,pro_monodroga,pro_codtpovta,pro_codigobarra,pro_codigoalfabeta, 0 as pro_Ranking,pro_isTrazable,pro_isCadenaFrio,pro_entransfer,pro_canmaxima,pro_vtasolotransfer,pro_troquel,pro_acuerdo,pro_NoTransfersEnClientesPerf,pro_familia,pro_PackDeVenta,pro_PrecioBase, pro_PorcARestarDelDtoDeCliente
SET @SQLStringPrimeraOrdenacion = N'Select  pro_codigo,pro_nombre,pro_precio,pro_preciofarmacia,pro_ofeunidades,pro_ofeporcentaje ,pro_neto,pro_codtpopro,pro_descuentoweb,pro_laboratorio,pro_monodroga,pro_codtpovta,pro_codigobarra,pro_codigoalfabeta, 0 as pro_Ranking,pro_isTrazable,pro_isCadenaFrio,pro_entransfer,pro_canmaxima,pro_vtasolotransfer,pro_troquel,pro_acuerdo,pro_NoTransfersEnClientesPerf,pro_familia,pro_PackDeVenta,pro_PrecioBase, pro_PorcARestarDelDtoDeCliente,pro_AceptaVencidos,pro_ProductoRequiereLote
from Productos.vistaProductosBusqueda
where ' + @WherePrimeraOrdenacion + ' AND ' +  @Where + ' '

--SET @SQLString = N'Select  pro_codigo,pro_nombre,pro_precio,pro_preciofarmacia,pro_ofeunidades,pro_ofeporcentaje ,pro_neto,pro_codtpopro,pro_descuentoweb,pro_laboratorio,pro_monodroga,pro_codtpovta,pro_codigobarra,pro_codigoalfabeta, 0 as pro_Ranking,pro_isTrazable,pro_isCadenaFrio,pro_entransfer,pro_canmaxima,pro_vtasolotransfer,pro_troquel,pro_acuerdo,pro_NoTransfersEnClientesPerf,pro_familia,pro_PackDeVenta,pro_PrecioBase, pro_PorcARestarDelDtoDeCliente
SET @SQLString = N'Select  pro_codigo,pro_nombre,pro_precio,pro_preciofarmacia,pro_ofeunidades,pro_ofeporcentaje ,pro_neto,pro_codtpopro,pro_descuentoweb,pro_laboratorio,pro_monodroga,pro_codtpovta,pro_codigobarra,pro_codigoalfabeta, 0 as pro_Ranking,pro_isTrazable,pro_isCadenaFrio,pro_entransfer,pro_canmaxima,pro_vtasolotransfer,pro_troquel,pro_acuerdo,pro_NoTransfersEnClientesPerf,pro_familia,pro_PackDeVenta,pro_PrecioBase, pro_PorcARestarDelDtoDeCliente,pro_AceptaVencidos,pro_ProductoRequiereLote
from Productos.vistaProductosBusqueda 
where ' + @Where 

 IF OBJECT_ID ( 'tempdb..#tempTablaProductos' ) IS NOT NULL 
BEGIN
   DROP TABLE #tempTablaProductos
END
--create table #tempTablaProductos (pro_codigo  nvarchar(50) ,pro_nombre nvarchar(75) ,pro_precio decimal(9,2),pro_preciofarmacia decimal(9,2),pro_ofeunidades int,pro_ofeporcentaje decimal(6,2),pro_neto bit,pro_codtpopro nvarchar(1),pro_descuentoweb decimal(6,2),pro_laboratorio nvarchar(75),pro_monodroga nvarchar(75),pro_codtpovta  nvarchar(2),pro_codigobarra nvarchar(13),pro_codigoalfabeta nvarchar(10), pro_Ranking int, pro_isTrazable bit,pro_isCadenaFrio bit,pro_entransfer bit null,pro_canmaxima int null, pro_vtasolotransfer bit null,pro_troquel nvarchar(50),pro_acuerdo int,pro_NoTransfersEnClientesPerf bit null,pro_familia nvarchar(75) null,pro_PackDeVenta int null,pro_PrecioBase decimal(9, 2) NULL, pro_PorcARestarDelDtoDeCliente decimal(6, 2) NULL)
create table #tempTablaProductos (pro_codigo  nvarchar(50) ,pro_nombre nvarchar(75) ,pro_precio decimal(9,2),pro_preciofarmacia decimal(9,2),pro_ofeunidades int,pro_ofeporcentaje decimal(6,2),pro_neto bit,pro_codtpopro nvarchar(1),pro_descuentoweb decimal(6,2),pro_laboratorio nvarchar(75),pro_monodroga nvarchar(75),pro_codtpovta  nvarchar(2),pro_codigobarra nvarchar(13),pro_codigoalfabeta nvarchar(10), pro_Ranking int, pro_isTrazable bit,pro_isCadenaFrio bit,pro_entransfer bit null,pro_canmaxima int null, pro_vtasolotransfer bit null,pro_troquel nvarchar(50),pro_acuerdo int,pro_NoTransfersEnClientesPerf bit null,pro_familia nvarchar(75) null,pro_PackDeVenta int null,pro_PrecioBase decimal(9, 2) NULL, pro_PorcARestarDelDtoDeCliente decimal(6, 2) NULL, pro_AceptaVencidos bit,pro_ProductoRequiereLote bit)

insert into #tempTablaProductos  EXEC sp_executesql @SQLString

 IF OBJECT_ID ( 'tempdb..#tempTablaProductosPrimeraOrdenacion' ) IS NOT NULL 
BEGIN
   DROP TABLE #tempTablaProductosPrimeraOrdenacion
END
--create table #tempTablaProductosPrimeraOrdenacion (pro_codigo  nvarchar(50) ,pro_nombre nvarchar(75) ,pro_precio decimal(9,2),pro_preciofarmacia decimal(9,2),pro_ofeunidades int,pro_ofeporcentaje decimal(6,2),pro_neto bit,pro_codtpopro nvarchar(1),pro_descuentoweb decimal(6,2),pro_laboratorio nvarchar(75),pro_monodroga nvarchar(75),pro_codtpovta  nvarchar(2),pro_codigobarra nvarchar(13),pro_codigoalfabeta nvarchar(10), pro_Ranking int, pro_isTrazable bit,pro_isCadenaFrio bit,pro_entransfer bit null,pro_canmaxima int null, pro_vtasolotransfer bit null,pro_troquel nvarchar(50),pro_acuerdo int,pro_NoTransfersEnClientesPerf bit null,pro_familia nvarchar(75) null,pro_PackDeVenta int null,pro_PrecioBase decimal(9, 2) NULL, pro_PorcARestarDelDtoDeCliente decimal(6, 2) NULL)
create table #tempTablaProductosPrimeraOrdenacion (pro_codigo  nvarchar(50) ,pro_nombre nvarchar(75) ,pro_precio decimal(9,2),pro_preciofarmacia decimal(9,2),pro_ofeunidades int,pro_ofeporcentaje decimal(6,2),pro_neto bit,pro_codtpopro nvarchar(1),pro_descuentoweb decimal(6,2),pro_laboratorio nvarchar(75),pro_monodroga nvarchar(75),pro_codtpovta  nvarchar(2),pro_codigobarra nvarchar(13),pro_codigoalfabeta nvarchar(10), pro_Ranking int, pro_isTrazable bit,pro_isCadenaFrio bit,pro_entransfer bit null,pro_canmaxima int null, pro_vtasolotransfer bit null,pro_troquel nvarchar(50),pro_acuerdo int,pro_NoTransfersEnClientesPerf bit null,pro_familia nvarchar(75) null,pro_PackDeVenta int null,pro_PrecioBase decimal(9, 2) NULL, pro_PorcARestarDelDtoDeCliente decimal(6, 2) NULL, pro_AceptaVencidos bit,pro_ProductoRequiereLote bit)

insert into #tempTablaProductosPrimeraOrdenacion  EXEC sp_executesql @SQLStringPrimeraOrdenacion

----
IF OBJECT_ID ( 'tempdb..#tbl_Transfers_Temporal' ) IS NOT NULL 
BEGIN
   DROP TABLE #tbl_Transfers_Temporal
END
SELECT *
INTO #tbl_Transfers_Temporal
FROM [guest].[fn_Transfers] (@codCliente)
----

SELECT p.*, tde_codpro as isTransfer,
CASE ISNULL(val_codprov,'0')
         WHEN '0' THEN 0
         ELSE 1
         END as 'RequiereVale'
FROM #tempTablaProductos as p
LEFT JOIN (select distinct tde_codpro from tbl_Transfers_Detalle INNER JOIN  #tbl_Transfers_Temporal ON tde_codtfr = tfr_codigo WHERE (tfr_facturaciondirecta  is null OR tfr_facturaciondirecta  = 0) AND (tfr_provincia is null OR  tfr_provincia = @cli_codprov)) as tb1 
on tb1.tde_codpro = pro_nombre and not (ISNULL( pro_NoTransfersEnClientesPerf,0) = 1 and @cli_tipo = 'P') --(ISNULL( pro_NoTransfersEnClientesPerf,0) = 0 and @cli_tipo = 'P')
LEFT JOIN (SELECT val_codprov,val_codpro FROM tbl_Productos_Vales WHERE val_codprov =  @cli_codprov) as tblProductosVales ON pro_codigo = val_codpro
WHERE pro_codigo NOT IN (SELECT pro_codigo FROM #tempTablaProductosPrimeraOrdenacion)
ORDER BY pro_nombre

SELECT stk_codpro,stk_codsuc,stk_stock
FROM tbl_Productos_Stocks
WHERE stk_codpro in (select pro_codigo from #tempTablaProductos)
AND stk_codsuc in (select sde_sucursalDependiente from Clientes.tbl_sucursalDependiente where sde_sucursal = @Sucursal)

SELECT p.*, tde_codpro as isTransfer,
CASE ISNULL(val_codprov,'0')
         WHEN '0' THEN 0
         ELSE 1
         END as 'RequiereVale'
from #tempTablaProductosPrimeraOrdenacion  as p
LEFT JOIN (select distinct tde_codpro from tbl_Transfers_Detalle INNER JOIN  #tbl_Transfers_Temporal ON tde_codtfr = tfr_codigo WHERE (tfr_facturaciondirecta  is null OR tfr_facturaciondirecta  = 0) AND (tfr_provincia is null OR  tfr_provincia = @cli_codprov)) as tb1 
on tb1.tde_codpro = pro_nombre and not (ISNULL( pro_NoTransfersEnClientesPerf,0) = 1 and @cli_tipo = 'P') --(ISNULL( pro_NoTransfersEnClientesPerf,0) = 0 and @cli_tipo = 'P')
LEFT JOIN (SELECT val_codprov,val_codpro FROM tbl_Productos_Vales WHERE val_codprov =  @cli_codprov) as tblProductosVales ON pro_codigo = val_codpro
ORDER BY pro_nombre

SELECT tde.*,tfr.*,tcl_IdTransfer
from tbl_Transfers_Detalle as tde
INNER JOIN  (SELECT * FROM #tbl_Transfers_Temporal WHERE (tfr_facturaciondirecta = 1 AND (tfr_provincia is null OR  tfr_provincia = @cli_codprov))) as tfr on  [tde_codtfr] = tfr.tfr_codigo
LEFT JOIN tbl_TransfersClientes on [tde_codtfr] = tcl_IdTransfer and tcl_NumeroCliente = @codCliente
WHERE tde_codpro in (select pro_nombre from #tempTablaProductos)

--select distinct tcl_IdTransfer
--FROM  tbl_TransfersClientes where tcl_NumeroCliente = @codCliente
