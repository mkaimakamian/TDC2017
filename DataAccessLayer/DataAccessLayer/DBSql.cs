using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace DataAccessLayer
{
    public class DBSql
    {

        public static string DATABASE = String.Empty;
        private static string connStr = String.Empty;

        public DBSql()
        {
            if (String.IsNullOrEmpty(connStr))
            {
                string serverInstance = String.Empty;
                try
                {

                    StreamReader st = new StreamReader(Directory.GetCurrentDirectory() + "\\dbconfig.txt");
                    serverInstance = st.ReadLine();
                    DATABASE = st.ReadLine();                    
                }
                catch
                {
                    DATABASE = "CAMPOII";
                    serverInstance = "UAI_EXPRESS";
                }
                
                connStr = "Data Source=.\\" + serverInstance + "; Initial Catalog=" + DATABASE + "; Integrated Security=True";
            }
            
        }

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
