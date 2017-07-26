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
            System.IO.StreamWriter file = new System.IO.StreamWriter(args[1] + "\\error.txt");
            try
            {
                file.WriteLine("Paramaetros: " + args[0] + " y " + args[1]);
                file.WriteLine("Creando archivo de configuracion.");
                CrearArchivo(args[0]);
                file.WriteLine("Copiando base ...");
                string destiny = CopiarBackup(args[1]);
                file.WriteLine("Base copiada a " + destiny);
                RestaurarBase(destiny);
            }
            catch (Exception ex)
            {
                file.WriteLine(ex.Message);
            }
            finally
            {
                file.Close();
            }

        }

        private static void CrearArchivo(string instancia)
        {
            string lines = instancia + "\r\nCAMPOII";
            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter(Directory.GetCurrentDirectory() + "\\dbconfig.txt");
            file.WriteLine(lines);
            file.Close();
        }

        private static string CopiarBackup(string ruta)
        {
            string bkpName = Directory.GetCurrentDirectory() + "\\rhoddion.bkp";
            string destiny = ruta + "\\rhoddion.bkp";
            File.Copy(bkpName, destiny);
            return destiny;
        }


        private static void RestaurarBase(string bkpName)
        {
            //string bkpName = Directory.GetCurrentDirectory() + "\\rhoddion.bkp";
            string serverInstance = Directory.GetCurrentDirectory() + "\\dbconfig.txt";
            StreamReader st = new StreamReader(serverInstance);
            string srvInstance = st.ReadLine();

            string database = "CAMPOII";
            string connStr = "Data Source=.\\" + srvInstance + "; Initial Catalog=master; Integrated Security=True";

            string sql = "";
            sql = "CREATE DATABASE " + database + "  ; USE MASTER ALTER DATABASE " + database + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE " + database + " FROM DISK = '" + bkpName + "' WITH REPLACE";


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
