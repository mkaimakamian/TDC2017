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
        public bool SaveVolunteer(VolunteerDTO volunteerDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO volunteer (personId, branchId, userId) VALUES (";
            sql += volunteerDto.volunteerId + ", ";
            sql += volunteerDto.branchId + ", ";
            sql += volunteerDto.userId;
            sql += ");SELECT @@IDENTITY";
            volunteerDto.volunteerId = dbsql.ExecuteNonQuery(sql);
            return true;
        }
    }
}
