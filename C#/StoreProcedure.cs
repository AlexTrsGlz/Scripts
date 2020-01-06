public int GetStoredProcedure(string ConnectionString,string NombreProcedimiento ,int? idParam)
{
	////Obtener arreglo con valores
	SqlParameter[] arrParams = new SqlParameter[1];
	arrParams[0] = new SqlParameter("@idParam", SqlDbType.Int);

	////Asignación de valores
	arrParams[0].Value = (idParam > 0) ? idParam : null;

	int idEventLog = -1;
	//Ejecucion de procedimiento almacenado
	using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CMSConnectionString"].ConnectionString))
	{
		using (SqlCommand command = new SqlCommand(NombreProcedimiento, connection))
		{
			command.CommandType = CommandType.StoredProcedure;
			command.CommandTimeout = connection.ConnectionTimeout;

			command.Parameters.AddRange(arrParams);
			connection.Open();
			objReturned = command.ExecuteScalar();
			connection.Close();
		}
	}

	int.TryParse(objReturned.ToString(), out idEventLog);
	return idEventLog;
}