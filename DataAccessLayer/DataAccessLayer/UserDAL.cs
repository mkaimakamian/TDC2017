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
        public UserDTO LogIn(string user, string password) 
        {            
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM users WHERE name = '" + user + "' AND password = '" + password + "' and ACTIVE = 1";
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0) {
                return Resolve(reader.First());
            }
        
            return null;
        }

        /// <summary>
        /// Devuelve la lista de todos los usuarios.
        /// </summary>
        /// <returns></returns>
        public List<UserDTO> GetUsers()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<UserDTO> result = new List<UserDTO>();

            sql = "SELECT * FROM users";
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                for (int i = 0; i < reader.Count; ++i)
                {
                    result.Add(Resolve(reader[i]));
                }
            }

            return result;
        }
        
        public bool UpdateUser(UserDTO userDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            //ACTUALIZAR TODO MENOS EL PASSWORD QUE DEBERÍA HACERSE POR OTRO LADO POR EL TEMA DE LA ENCRIPTACION
            sql = "UPDATE users SET languageId = '" + userDto.languageId + "' WHERE id = " + userDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }


        private UserDTO Resolve(List<String> item) 
        {
            UserDTO result = new UserDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.password = item[2];
            result.active = bool.Parse(item[3]);
            result.languageId = int.Parse(item[4]);
            result.permissionId = item[5];
            result.hdv = item[6];
            return result;
        }
    }
}
