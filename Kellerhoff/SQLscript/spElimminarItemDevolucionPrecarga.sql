USE [db_Kellerhoff]
GO
/****** Object:  StoredProcedure [Devoluciones].[spElimminarItemDevolucionPrecarga]    Script Date: 06/08/2020 14:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [Devoluciones].[spElimminarItemDevolucionPrecarga]
	@NumeroItem int,
	@isOK  bit OUTPUT
AS
BEGIN TRANSACTION [Tran1]
BEGIN TRY
	DELETE FROM [tmp_Solicitudes_Devoluciones_Items]
	WHERE [dev_numeroitem] = @NumeroItem
	
	COMMIT TRANSACTION [Tran1]
	SET @isOk = 1
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION [Tran1]
	EXEC LogRegistro.spLogError @mensaje = N'';
	SET @isOk = 0
END CATCH