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

        public UserDTO GetUser(int userId)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM users WHERE id = " + userId;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public UserDTO GetUser(string userName)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM users WHERE name = '" + userName +"'";
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
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

        public bool SaveUser(UserDTO userDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO users (name, password, active, languageId, permissionId, hvd) VALUES (";
            sql += "'" + userDto.name + "', " ;
            sql += "'" + userDto.password + "', ";
            sql += "'" +userDto.active + "', ";
            sql += userDto.languageId + ", ";
            sql += "'" + userDto.permissionId + "', ";
            sql += "'" + userDto.hdv + "'";
            sql += ");SELECT @@IDENTITY";
            userDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }
        
        public bool UpdateUser(UserDTO userDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            //ACTUALIZAR TODO MENOS EL PASSWORD QUE DEBERÍA HACERSE POR OTRO LADO POR EL TEMA DE LA ENCRIPTACION
            //sql = "UPDATE users SET languageId = '" + userDto.languageId + "',  hvd = '" + userDto.hdv + "' WHERE id = " + userDto.id;
            sql = "UPDATE users SET ";
            sql += "name = '" + userDto.name + "',  ";
            sql += "password = '" + userDto.password + "',  ";
            sql += "active = '" + userDto.active + "',  ";
            sql+= "languageId = " + userDto.languageId + ",  ";
            sql += "permissionId = '" + userDto.permissionId + "',  ";
            sql+= "hvd = '" + userDto.hdv + "' ";
            sql += "WHERE id = " + userDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool DeleteUser(int userId)
        {
            DBSql dbsql = new DBSql();
            String sql;
            sql = "DELETE FROM users WHERE id = " + userId;
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
