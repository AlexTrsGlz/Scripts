-- =============================================
-- Script Template
-- =============================================

BEGIN TRY
  BEGIN TRANSACTION
		
	IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ST_CuentasBancarias' AND COLUMN_NAME = 'IsDefault')
	BEGIN
	END

  COMMIT TRANSACTION
END TRY

BEGIN CATCH
 
	DECLARE @ErrorMessage NVARCHAR(MAX);  
	DECLARE @ErrorSeverity INT;  
	DECLARE @ErrorState INT;  

	SELECT   
		@ErrorMessage = ERROR_MESSAGE(),  
		@ErrorSeverity = ERROR_SEVERITY(),  
		@ErrorState = ERROR_STATE();  

	RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState); 
	ROLLBACK TRANSACTION

END CATCH
