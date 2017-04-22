using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class UserDAL
    {
        //public Boolean Exists(LogInDTO userLogin)
        //{
        //    DBSql dbsql = new DBSql();
        //    String sql;
        //    List<List<String>> reader;

        //    sql = "SELECT * ";
        //    sql += "FROM users WHERE name = '" + userLogin.user + "' AND password = '" + userLogin.password + "' and ACTIVE = 1";
        //    reader = dbsql.executeReader(sql);

        //    if (reader.Count > 0)
        //    {
        //        return true;
        //    }

        //    return false
        //}


        public UserDTO LogIn(string user, string password) 
        {            
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * ";
            sql += "FROM users WHERE name = '" + user + "' AND password = '" + password + "' and ACTIVE = 1";
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0) {
                return resolve(reader.First());
            }
        
            return null;
        }
        
        private UserDTO resolve(List<String> item) 
        {
            UserDTO result = new UserDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.password = item[2];
            result.active = bool.Parse(item[3]);
            result.languageId = int.Parse(item[4]);
            result.permissionId = item[5];
            return result;
        }
    }
}
