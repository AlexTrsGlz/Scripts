public static DataSet GetStoredProcedure(string ConnectionString, int idParam)
{
    ////Obtener arreglo con valores
    SqlParameter[] arrParams = new SqlParameter[1];
    arrParams[0] = new SqlParameter("@idParam", idParam);

    ////Asignación de valores
    arrParams[0].Value = (idParam > 0)? idParam:null;
    return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "NombreProcedimientoAlmacenado", arrParams);
}
