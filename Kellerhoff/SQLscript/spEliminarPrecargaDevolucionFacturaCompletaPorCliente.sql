USE [db_Kellerhoff]
GO
/****** Object:  StoredProcedure [Devoluciones].[spEliminarPrecargaDevolucionFacturaCompletaPorCliente]    Script Date: 06/08/2020 14:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [Devoluciones].[spEliminarPrecargaDevolucionFacturaCompletaPorCliente]
	@NumeroCliente int,
	@isOk bit OUTPUT
AS

BEGIN TRANSACTION [Tran1]
BEGIN TRY
	DELETE FROM [tmp_Solicitudes_Devoluciones_Items]
	WHERE [dev_numerocliente] = @NumeroCliente
	AND [dev_nombreproductofactura] = 'F.Completa'
	
	COMMIT TRANSACTION [Tran1]
	SET @isOk = 1
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION [Tran1]
	EXEC LogRegistro.spLogError @mensaje = N'';
	SET @isOk = 0
END CATCH