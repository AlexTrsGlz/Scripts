	-- To enable SQLCMD variables: go Menu 'Query', option 'SQLCMD Mode'

:setvar DATABASE_NAME    "ALL_IN_ONE_PROD_TRUNK_BD_20191021"
:setvar BACKUP_FILE      "C:\Backups\ALL-IN-ONE-20191021000000-(665e3b96-c276-43cc-9725-4eacbb20c848)-Full.bak"

-- KILL DATABASE PROCESSES ------------------------------------------------------------------------
USE [master]
DECLARE @execSql VARCHAR(MAX) = ''
SELECT @execSql = @execSql + 'kill ' + CONVERT(CHAR(10), spid) + ' '  
FROM   dbo.sysprocesses   
WHERE DB_NAME(dbid) = '$(DATABASE_NAME)'
AND    dbid <> 0   
AND    spid <> @@SPID
EXEC(@execSql) 

-- RESTORE FROM DISK ------------------------------------------------------------------------
RESTORE DATABASE [$(DATABASE_NAME)]
FROM  DISK = N'$(BACKUP_FILE)'
WITH  FILE = 1,  
MOVE N'ST_Company' TO N'C:\DataBases\$(DATABASE_NAME).mdf',  
MOVE N'ST_Company_log' TO N'C:\DataBases\$(DATABASE_NAME)_log.ldf',  
NOUNLOAD,  REPLACE,  STATS = 10
GO