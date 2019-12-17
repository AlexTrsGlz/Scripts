DROP DEFAULT CONSTRAINT
	declare @Command  varchar(200)
	select @Command = 'ALTER TABLE GT_PatentesXEntidad DROP CONSTRAINT ' + d.name
	from sys.tables t
	join sys.default_constraints d on d.parent_object_id = t.object_id
	join sys.columns c on c.object_id = t.object_id
	and c.column_id = d.parent_column_id
	where t.name = 'GT_PatentesXEntidad' and c.name = 'IdCompany'
	execute (@Command)
