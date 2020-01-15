-- To enable SQLCMD variables: go Menu 'Query', option 'SQLCMD Mode'

:setvar DATABASE_NAME    "MY_DATABSENAME"
:setvar BACKUP_FILE      "C:\Backups\###-Full.bak"

:setvar BACKUP_FILE_DIF  "C:\Backups\###-Diff.bak"

-- KILL DATABASE PROCESSES (FULL) ------------------------------------------------------------------------
USE [master]
DECLARE @execSql VARCHAR(MAX) = '' 
SELECT @execSql = @execSql + 'kill ' + CONVERT(CHAR(10), spid) + ' '  
FROM   dbo.sysprocesses   
WHERE DB_NAME(dbid) = '$(DATABASE_NAME)'
AND    dbid <> 0   
AND    spid <> @@SPID
EXEC(@execSql) 


-- RESTORE FROM DISK (FULL) ------------------------------------------------------------------------
RESTORE DATABASE [$(DATABASE_NAME)]
FROM  DISK = N'$(BACKUP_FILE)'
WITH  FILE = 1,  
MOVE N'ST_Company' TO N'C:\DataBases\$(DATABASE_NAME).mdf',            --Se crea carpeta para Log
MOVE N'ST_Company_log' TO N'C:\DataBases\$(DATABASE_NAME)_log.ldf',    --Se crea carpeta para Log
NOUNLOAD,  REPLACE,  STATS = 10, NORECOVERY
GO

-- RESTORE FROM DISK (DIFF) ------------------------------------------------------------------------
RESTORE DATABASE [$(DATABASE_NAME)]
FROM  DISK = N'$(BACKUP_FILE_DIF)'
WITH  FILE = 1,  
MOVE N'ST_Company' TO N'C:\DataBases\$(DATABASE_NAME).mdf',            --Se crea carpeta para Log
MOVE N'ST_Company_log' TO N'C:\DataBases\$(DATABASE_NAME)_log.ldf',    --Se crea carpeta para Log
NOUNLOAD,  REPLACE,  STATS = 10, RECOVERY
GO