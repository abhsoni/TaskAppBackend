using Microsoft.Data.SqlClient;

namespace TaskAppDAL
{
    public class TaskAppDatabaseConnection
    {
        private string _connectionString;
        public TaskAppDatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Create(string query, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(query, parameters);
        }
        public void Read(string query, Action<SqlDataReader> handleData)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    handleData(reader);
                }
            }
        }
        public int Update(string query, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(query, parameters);
        }
        public int Delete(string query, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(query, parameters);
        }
        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return -1;
                }
            }
        }
        public T ExecuteScalar<T>(string query, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the command
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();

                    // Execute the query and get the scalar result
                    object result = command.ExecuteScalar();

                    // Convert and return the result
                    if (result == null || result == DBNull.Value)
                    {
                        return default!;
                    }

                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
        }
    }
}
