using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DBSql
    {
    
        private string connStr = "Data Source=.\\UAI_EXPRESS; Initial Catalog=CAMPOII; Integrated Security=True";

        public int ExecuteNonQuery(string sql)
        {
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand(sql, connection);
            int result = -1;

            connection.Open();
            object row = command.ExecuteScalar();
            
            if (row != null) {
                result = int.Parse(row.ToString());
            }
            connection.Close();
            return result;
        }
        
        public List<List<String>> executeReader(string sql)
        {
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader;
            List<List<String>> result = new List<List<string>>();
            connection.Open();
            reader = command.ExecuteReader();

            if (reader.HasRows) {
                while (reader.Read()) {
                    result.Add(resolve(reader));
                }
            }

        connection.Close();
        return result;
        }

        private List<String> resolve(SqlDataReader reader) {
            List<String> result = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++) {                
                result.Add(reader[i].ToString());
            }

            return result;
        }
    }
}
