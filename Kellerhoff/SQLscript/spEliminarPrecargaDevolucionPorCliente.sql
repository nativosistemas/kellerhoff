USE [db_Kellerhoff]
GO
/****** Object:  StoredProcedure [Devoluciones].[spEliminarPrecargaDevolucionPorCliente]    Script Date: 06/08/2020 14:41:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [Devoluciones].[spEliminarPrecargaDevolucionPorCliente]
	@NumeroCliente int,
	@isOk bit OUTPUT
AS

BEGIN TRANSACTION [Tran1]
BEGIN TRY
	DELETE FROM [tmp_Solicitudes_Devoluciones_Items]
	WHERE [dev_numerocliente] = @NumeroCliente
	AND NOT [dev_numerofactura] IS NULL
	
	COMMIT TRANSACTION [Tran1]
	SET @isOk = 1
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION [Tran1]
	EXEC LogRegistro.spLogError @mensaje = N'';
	SET @isOk = 0
END CATCH