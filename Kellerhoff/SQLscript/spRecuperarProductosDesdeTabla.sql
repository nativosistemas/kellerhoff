USE [db_Kellerhoff]
GO
/****** Object:  StoredProcedure [Productos].[spRecuperarProductosDesdeTabla]    Script Date: 06/08/2020 13:51:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [Productos].[spRecuperarProductosDesdeTabla]
@Tabla_Detalle ProductosArchivosPedidosTableType READONLY,
@Sucursal nvarchar(2),
@cli_codprov nvarchar(75),
@codCliente int
AS


----
IF OBJECT_ID ( 'tempdb..#tbl_Transfers_Temporal' ) IS NOT NULL 
BEGIN
   DROP TABLE #tbl_Transfers_Temporal
END
SELECT [tfr_codigo]
      ,[tfr_accion]
      ,[tfr_nombre]
      ,[tfr_deshab]
      ,[tfr_pordesadi]
      ,[tfr_tipo]
      ,[tfr_mospap]
      ,[tfr_minrenglones]
      ,[tfr_minunidades]
      ,[tfr_maxunidades]
      ,[tfr_mulunidades]
      ,[tfr_fijunidades]
      ,[tfr_descripcion]
      ,[tfr_facturaciondirecta]
      ,[tfr_provincia]
INTO #tbl_Transfers_Temporal
FROM [tbl_Transfers]
where [tfr_codigo] in (select distinct tcl_IdTransfer
FROM  tbl_TransfersClientes where tcl_NumeroCliente = @codCliente) or [tfr_codigo] not in (select distinct tcl_IdTransfer
FROM  tbl_TransfersClientes )
----

SELECT pro_codigo,pro_nombre ,pro_precio ,pro_preciofarmacia ,pro_ofeunidades ,pro_ofeporcentaje ,pro_neto ,pro_codtpopro ,pro_descuentoweb ,pro_laboratorio,pro_monodroga,pro_codtpovta,pro_codigobarra,pro_codigoalfabeta,0 as 'pro_Ranking', tde_codpro as isTransfer,pro_isTrazable,pro_isCadenaFrio,pro_entransfer,pro_canmaxima,pro_vtasolotransfer,pro_AceptaVencidos,pro_ProductoRequiereLote,
CASE ISNULL(val_codprov,'0')
         WHEN '0' THEN 0
         ELSE 1
         END as 'RequiereVale'
         ,pro_troquel
         ,pro_acuerdo
from tbl_Productos
LEFT JOIN (select distinct tde_codpro from tbl_Transfers_Detalle INNER JOIN  #tbl_Transfers_Temporal ON tde_codtfr = tfr_codigo WHERE (tfr_facturaciondirecta  is null OR tfr_facturaciondirecta  = 0) AND (tfr_provincia is null OR  tfr_provincia = @cli_codprov)) as tb1 on tb1.tde_codpro = pro_nombre
LEFT JOIN (SELECT val_codprov,val_codpro FROM tbl_Productos_Vales WHERE val_codprov =  @cli_codprov) as tblProductosVales ON pro_codigo = val_codpro
WHERE pro_nombre in (select codProducto from @Tabla_Detalle)


SELECT stk_codpro,stk_codsuc,stk_stock
FROM tbl_Productos_Stocks
WHERE stk_codpro in (select pro_codigo from tbl_Productos WHERE pro_nombre in (select codProducto from @Tabla_Detalle))
AND stk_codsuc in (select sde_sucursalDependiente from Clientes.tbl_sucursalDependiente where sde_sucursal = @Sucursal)

SELECT [tde_codtfr],[tde_codpro],[tde_descripcion],[tde_prepublico],[tde_predescuento],[tde_minuni],[tde_maxuni],[tde_muluni],[tde_fijuni],[tde_proobligatorio],tde_unidadesbonificadas,tde_unidadesbonificadasdescripcion,[tfr_codigo],[tfr_accion],[tfr_nombre],[tfr_deshab],[tfr_pordesadi],[tfr_tipo],[tfr_mospap],[tfr_minrenglones],[tfr_minunidades],[tfr_maxunidades],[tfr_mulunidades],[tfr_fijunidades],[tfr_descripcion],[tfr_facturaciondirecta]
from tbl_Transfers_Detalle
INNER JOIN  (SELECT * FROM #tbl_Transfers_Temporal WHERE (tfr_facturaciondirecta = 1 AND (tfr_provincia is null OR  tfr_provincia = @cli_codprov))) as tb1 on  [tde_codtfr] = tb1.tfr_codigo
WHERE tde_codpro in (select codProducto from @Tabla_Detalle)

