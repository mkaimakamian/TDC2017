using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class PersonDAL
    {
        public PersonDTO GetPerson(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM person WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        /// <summary>
        /// Devuelve la lista de todos las personas.
        /// </summary>
        /// <returns></returns>
        //public List<UserDTO> GetUsers()
        //{
        //    DBSql dbsql = new DBSql();
        //    String sql;
        //    List<List<String>> reader;
        //    List<UserDTO> result = new List<UserDTO>();

        //    sql = "SELECT * FROM users";
        //    reader = dbsql.executeReader(sql);

        //    if (reader.Count > 0)
        //    {
        //        for (int i = 0; i < reader.Count; ++i)
        //        {
        //            result.Add(Resolve(reader[i]));
        //        }
        //    }

        //    return result;
        //}

        public bool SavePerson(PersonDTO personDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO person (name, lastName, birthdate, email, phone, gender, dni, addressId) VALUES (";
            sql += "'" + personDto.name + "', ";
            sql += "'" + personDto.lastName + "', ";
            sql += "'" + personDto.birthdate + "', ";
            sql += "'" + personDto.email + "', ";
            sql += "'" + personDto.phone+ "', ";
            sql += "'" + personDto.gender + "', ";
            sql += personDto.dni + ",";
            sql += personDto.addressId;
            sql += ");SELECT @@IDENTITY";
            personDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }
        

        //public bool UpdateUser(UserDTO userDto)
        //{
        //    DBSql dbsql = new DBSql();
        //    String sql;

        //    //ACTUALIZAR TODO MENOS EL PASSWORD QUE DEBERÍA HACERSE POR OTRO LADO POR EL TEMA DE LA ENCRIPTACION
        //    //sql = "UPDATE users SET languageId = '" + userDto.languageId + "',  hvd = '" + userDto.hdv + "' WHERE id = " + userDto.id;
        //    sql = "UPDATE users SET ";
        //    sql += "name = '" + userDto.name + "',  ";
        //    sql += "password = '" + userDto.password + "',  ";
        //    sql += "active = '" + userDto.active + "',  ";
        //    sql += "languageId = " + userDto.languageId + ",  ";
        //    sql += "permissionId = '" + userDto.permissionId + "',  ";
        //    sql += "hvd = '" + userDto.hdv + "' ";
        //    sql += "WHERE id = " + userDto.id;
        //    dbsql.ExecuteNonQuery(sql);
        //    return true;
        //}

        //public bool DeleteUser(int userId)
        //{
        //    DBSql dbsql = new DBSql();
        //    String sql;
        //    sql = "DELETE FROM users WHERE id = " + userId;
        //    dbsql.ExecuteNonQuery(sql);
        //    return true;
        //}


        private PersonDTO Resolve(List<String> item)
        {
            PersonDTO result = new PersonDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.lastName = item[2];
            result.birthdate = DateTime.Parse(item[3]);
            result.email = item[4];
            result.phone = item[5];
            result.gender = Boolean.Parse(item[6]);
            result.dni = int.Parse(item[7]);
            result.addressId = int.Parse(item[8]);
            return result;
        }
    }
}
