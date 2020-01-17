--DROP DEFAULT CONSTRAINT

DECLARE @Comando VARCHAR(MAX),
		@NombreDeTabla VARCHAR(MAX) = 'GT_PatentesXEntidad',
		@NombreDeColumna VARCHAR(MAX) = 'IdContainerType'

SELECT @Comando = 'ALTER TABLE ' + @NombreDeTabla + ' DROP CONSTRAINT ' + DC.[name]
FROM SYS.tables AS T
INNER JOIN SYS.default_constraints AS DC ON DC.parent_object_id = T.object_id
INNER JOIN SYS.columns AS C ON C.object_id = T.object_id AND C.column_id = DC.parent_column_id
WHERE T.[name] = @NombreDeTabla AND C.[name] = @NombreDeColumna

EXECUTE(@Comando)