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

        public bool SavePerson(PersonDTO personDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO person (name, lastName, birthdate, email, phone, gender, dni, addressId) VALUES (";
            sql += "'" + personDto.name + "', ";
            sql += "'" + personDto.lastName + "', ";
            sql += "CONVERT(datetime, '" + personDto.birthdate + "', 103), ";
            sql += "'" + personDto.email + "', ";
            sql += "'" + personDto.phone+ "', ";
            sql += "'" + personDto.gender + "', ";
            sql += personDto.dni + ",";
            sql += personDto.addressId;
            sql += ");SELECT @@IDENTITY";
            personDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }
        
        private PersonDTO Resolve(List<String> item)
        {
            PersonDTO result = new PersonDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.lastName = item[2];
            result.birthdate = DateTime.Parse(item[3]);
            result.email = item[4];
            result.phone = item[5];
            result.gender = Char.Parse(item[6]);
            result.dni = int.Parse(item[7]);
            result.addressId = int.Parse(item[8]);
            return result;
        }
    }
}
