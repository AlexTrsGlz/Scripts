SELECT NAME AS Contraints
FROM SYS.DEFAULT_CONSTRAINTS
WHERE PARENT_OBJECT_ID = OBJECT_ID(N'DBO.TABLE_NAME')
AND COL_NAME(PARENT_OBJECT_ID, PARENT_COLUMN_ID) = 'COLUMN_NAME'