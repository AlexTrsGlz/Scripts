using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Tools
{
	public class DataSetHelper
	{
		public static DataSet ExecuteDateSet(String commandText, object[,] parameters)
		{
			DataSet dataSet = new DataSet();

			using (SqlConnection connection = new SqlConnection(ApplicationSettings.ConnectionString))
			{
				using (SqlCommand command = new SqlCommand(commandText, connection))
				{
                    command.CommandTimeout = connection.ConnectionTimeout;

					if (parameters != null)
					{
						for (int i = 0; i < parameters.GetLength(0); i++)
						{
							command.Parameters.Add(
								new SqlParameter(
									parameters[i, 0] as string,
									parameters[i, 1] ?? System.DBNull.Value
								)
							);
						}
					}

					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						adapter.Fill(dataSet);
					}
				}
			}

			return dataSet;
		}

		public static DataTable ExecuteDataTable(String commandText, object[,] parameters)
		{
			DataSet dataSet = ExecuteDateSet(commandText, parameters);

			if (dataSet.Tables.Count > 0)
			{
				return dataSet.Tables[0];
			}

			return null;
		}

		public static void SetTableNames(DataSet dataSet, params string[] names)
		{
			for (int i = 0; i < dataSet.Tables.Count && i < names.Length; i++)
			{
				dataSet.Tables[i].TableName = names[i];
			}
		}

		public static void ReplaceColumnNames(DataSet dataSet, string[,] columnNames)
		{
			foreach (DataTable table in dataSet.Tables)
			{
				ReplaceColumnNames(table, columnNames);
			}
		}

		public static void ReplaceColumnNames(DataTable table, string[,] columnNames)
		{
			foreach (DataColumn column in table.Columns)
			{
				column.ColumnName = search_name(column.ColumnName, columnNames);
			}
		}

		private static string search_name(string name, string[,] columnNames)
		{
			for (int i = 0; i < columnNames.GetLength(0); i++)
			{
				if (columnNames[i, 0] == name)
				{
					return columnNames[i, 1];
				}
			}

			return name;
		}

		public static bool IsEmpty(DataSet dataSet)
		{
			foreach (DataTable dataTable in dataSet.Tables)
			{
				if (dataTable.Rows.Count > 0 && dataTable.Columns.Count > 0)
				{
					return false;
				}
			}

			return true;
		}

		public static void SetColumns(DataTable table, string[,] columnNames)
		{
			List<string> todelete = new List<string>();
			List<string> neworder = new List<string>();

			foreach (DataColumn column in table.Columns)
			{
				bool delete = true;

				for (int i = 0; i < columnNames.GetLength(0); i++)
				{
					if (columnNames[i, 0] == column.ColumnName)
					{
						delete = false;
						break;
					}
				}

				if (delete)
				{
					todelete.Add(column.ColumnName);
				}
			}

			for (int i = 0; i < columnNames.GetLength(0); i++)
			{
				neworder.Add(columnNames[i, 0]);
			}

			RemoveColumns(table, todelete.ToArray());
			OrderColumns(table, neworder.ToArray());
			ReplaceColumnNames(table, columnNames);
		}

		public static void OrderColumns(DataTable table, params string[] columnNames)
		{
			int ordinal = 0;

			foreach (string columnName in columnNames)
			{
				DataColumn column = table.Columns[columnName];

				if (column != null)
				{
					column.SetOrdinal(ordinal);
					ordinal++;
				}
			}
		}

		public static void RemoveColumns(DataTable table, params string[] columnNames)
		{
			foreach (string columnName in columnNames)
			{
				for (int i = table.Columns.Count - 1; i >= 0; i--)
				{
					if (table.Columns[i].ColumnName == columnName)
					{
						table.Columns.RemoveAt(i);
					}
				}
			}
		}

        /// <summary>
        /// This method is similar than SqlHelper.ExecuteDataset, custom command timeout
        /// </summary>
        /// <param name="p_Connection"></param>
        /// <param name="p_CommandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    command.CommandTimeout = connection.ConnectionTimeout;

                    if (parameters != null)
                    {
                        foreach (SqlParameter itemParameter in parameters)
                        {
                            if (itemParameter != null)
                            {
                                command.Parameters.Add(itemParameter);
                            }
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                    }
                }
            }

            return dataSet;
        }

        /// <summary>
        /// This method is similar than SqlHelper.ExecuteScalar, custom command timeout
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] parameters)
        {
            object objReturned = new object();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    command.CommandTimeout = connection.ConnectionTimeout;

                    foreach (SqlParameter itemParameter in parameters)
                    {
                        if (itemParameter != null)
                        {
                            command.Parameters.Add(itemParameter);
                        }
                    }

                    connection.Open();
                    objReturned = command.ExecuteScalar();
                    connection.Close();
                }
            }

            return objReturned;
        }

        /// <summary>
        /// This method is similar than SqlHelper.ExecuteNonQuery, custom command timeout
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            int iReturn = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;
                    command.CommandTimeout = connection.ConnectionTimeout;

                    foreach (SqlParameter itemParameter in parameters)
                    {
                        if (itemParameter != null)
                        {
                            command.Parameters.Add(itemParameter);
                        }
                    }

                    connection.Open();
                    iReturn = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return iReturn;
        }

	}
}
