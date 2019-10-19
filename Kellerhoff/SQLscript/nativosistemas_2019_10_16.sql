alter table [dbo].[tbl_Productos]
add [pro_Familia] [nvarchar](75) NULL
GO
--

ALTER VIEW [Productos].[vistaProductosBusqueda]
WITH SCHEMABINDING
AS
SELECT pro_codigo,pro_nombre,pro_precio,pro_preciofarmacia,pro_ofeunidades,pro_ofeporcentaje ,pro_neto,pro_codtpopro,pro_descuentoweb,pro_laboratorio,pro_monodroga,pro_codtpovta,pro_codigobarra,pro_codigoalfabeta,
pro_codigo + ' ' + ISNULL(pro_nombre,'')  + ' ' + ISNULL(pro_laboratorio,'') + ' ' + ISNULL(pro_monodroga,'') + ' ' + ISNULL(pro_codigobarra,'') + ' ' + ISNULL(pro_codigoalfabeta,'') as pro_columnaWhere
,ISNULL(pro_nombre,'')  + ' ' +  ISNULL(pro_codigobarra,'')  + ' ' + ISNULL(pro_laboratorio,'') as pop_columnaWhereDefault,pro_isTrazable,pro_isCadenaFrio,pro_entransfer,pro_canmaxima,pro_vtasolotransfer,pro_troquel,pro_acuerdo
,pro_NoTransfersEnClientesPerf,pro_familia
FROM dbo.tbl_Productos

GO
---