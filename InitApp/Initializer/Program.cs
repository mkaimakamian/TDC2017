using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace Initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            string database = "CAMPOII";
            string connStr = "Data Source=.\\UAI_EXPRESS; Initial Catalog=master; Integrated Security=True";

            string name = Directory.GetCurrentDirectory() + "\\rhoddion_201726062126.bkp";
            string sql = "";
            sql = "CREATE DATABASE " + database + "  ; USE MASTER ALTER DATABASE " + database + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE " + database + " FROM DISK = '" + name + "' WITH REPLACE";


            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand(sql, connection);
            int result = -1;

            connection.Open();
            object row = command.ExecuteScalar();

            if (row != null)
            {
                result = int.Parse(row.ToString());
            }
            connection.Close();

        }

    }
}
