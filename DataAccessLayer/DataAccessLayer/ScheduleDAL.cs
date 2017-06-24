using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class ScheduleDAL
    {

        public ScheduleDTO GetSchedule(int id)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;

            sql = "SELECT * FROM schedule WHERE id = " + id;
            reader = dbsql.executeReader(sql);

            if (reader.Count > 0)
            {
                return Resolve(reader.First());
            }

            return null;
        }

        public bool SaveSchedule(ScheduleDTO scheduleDto)
        {
            DBSql dbsql = new DBSql();
            String sql;
            sql = "INSERT INTO schedule (visit, donationId, comment) VALUES (";
            sql += "'" + "CONVERT(datetime, '" + scheduleDto.visit + "', 103), ";
            sql += scheduleDto.donationId + ", ";
            sql += "'" + scheduleDto.comment + "'";
            sql += ");SELECT @@IDENTITY";
            scheduleDto.id = dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public List<ScheduleDTO> GetSchedules()
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<ScheduleDTO> result = new List<ScheduleDTO>();

            sql = "SELECT * FROM schedule";
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

        public bool UpdateSchedule(ScheduleDTO scheduleDto)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "UPDATE schedule SET ";
            sql += "visit = '" + "CONVERT(datetime, '" + scheduleDto.visit + "', 103), ";
            sql += "donationId = " + scheduleDto.donationId + ", ";
            sql += "comment = '" + scheduleDto.comment + "'";
            sql += "WHERE id = " + scheduleDto.id;
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        private ScheduleDTO Resolve(List<String> item)
        {
            ScheduleDTO result = new ScheduleDTO();

            result.id = int.Parse(item[0]);
            result.visit = DateTime.Parse(item[1]);
            result.donationId = int.Parse(item[2]);
            result.comment = item[3];
            return result;

        }
    }
}
