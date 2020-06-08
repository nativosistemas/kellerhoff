USE [db_Kellerhoff]
GO
/****** Object:  StoredProcedure [Devoluciones].[spRecuperarItemsDevolucionesPrecargaVencidosPorCliente]    Script Date: 06/08/2020 14:42:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [Devoluciones].[spRecuperarItemsDevolucionesPrecargaVencidosPorCliente]
	@numerocliente int
AS
BEGIN
	SELECT [dev_numeroitem]
		,[dev_numerocliente]
		,[dev_numerofactura]
		,[dev_nombreproductodevolucion]
		,[dev_fecha]
		,[dev_motivo]
		,[dev_numeroitemfactura]
		,[dev_nombreproductofactura]
		,[dev_cantidad]
		,[dev_numerolote]
		,[dev_fechavencimientolote]
		,[dev_idsucursal]
	FROM [tmp_Solicitudes_Devoluciones_Items]
	WHERE [dev_numerocliente] = @numerocliente
	AND [dev_numerofactura] IS NULL
	ORDER BY [dev_fecha]
END