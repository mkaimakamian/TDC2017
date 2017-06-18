using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class VolunteerDAL
    {
        public VolunteerDTO GetVolunteer(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT p.*, v.id volunteerId, v.branchId, v.userId FROM volunteer v INNER JOIN person p ON p.id = v.personId WHERE v.id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }
        
        public List<VolunteerDTO> GetVolunteers()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<VolunteerDTO> result = new List<VolunteerDTO>();

            sql = "SELECT p.*, v.id volunteerId, v.branchId, v.userId FROM volunteer v INNER JOIN person p ON p.id = v.personId";
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

        public bool SaveVolunteer(VolunteerDTO volunteerDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO volunteer (personId, branchId, userId) VALUES (";
            sql += volunteerDto.id + ", ";
            sql += volunteerDto.branchId + ", ";
            sql += volunteerDto.userId != 0 ? volunteerDto.userId.ToString() : "null";
            sql += ");SELECT @@IDENTITY";
            volunteerDto.volunteerId = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private VolunteerDTO Resolve(List<String> item)
        {
            VolunteerDTO result = new VolunteerDTO();
            result.id = int.Parse(item[0]);
            result.name = item[1];
            result.lastName = item[2];
            result.birthdate = DateTime.Parse(item[3]);
            result.email = item[4];
            result.phone = item[5];
            result.gender = Char.Parse(item[6]);
            result.dni = int.Parse(item[7]);
            result.addressId = int.Parse(item[8]);
            result.volunteerId = int.Parse(item[9]);
            result.branchId = int.Parse(item[10]);
            result.userId = int.Parse(item[11].Length == 0 ? "0" : item[11]);
            return result;
        }
    }
}
