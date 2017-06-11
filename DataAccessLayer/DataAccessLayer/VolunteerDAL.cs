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

            sql = "SELECT * FROM volunteer WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
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

            result.volunteerId = int.Parse(item[0]);
            result.id = int.Parse(item[1]);
            result.branchId = int.Parse(item[2]);
            result.userId = int.Parse(item[3].Length == 0 ? "0" : item[3]);
            return result;
        }
    }
}
