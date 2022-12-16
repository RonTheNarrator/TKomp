using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TKomp
{
    public static class BuisnessLogic
    {
        private const string QueryString = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS Where DATA_TYPE = 'int'";
        private const string ConnectionStringDbSelector = ";Database=DevData;";

        public static List<string> UpdateData(object sender, DbCommandArgs e)
        {
            List<string> list = new List<string>();
            using var connection = new SqlConnection(e.ConnectionString + ConnectionStringDbSelector);
            using (var cmd = new SqlCommand() { Connection = connection, CommandText = QueryString })
            {
                cmd.Parameters.AddWithValue("@Identifier", 100);

                connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                connection.Close();
            }
            return list;
        }

        public static bool TestDbConnection(object sender, DbCommandArgs e)
        {
            var canConnect = false;
            using var connection = new SqlConnection(e.ConnectionString);
            try
            {
                connection.Open();
                canConnect = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return canConnect;
        }
    }

    public class DbCommandArgs : EventArgs
    {
        public readonly string ConnectionString;

        public DbCommandArgs(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
