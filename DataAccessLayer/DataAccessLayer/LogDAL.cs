using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace DataAccessLayer
{
    public class LogDAL
    {
        public List<LogDTO> GetLogs(LogDTO filter = null)
        {
            DBSql dbsql = new DBSql();
            String sql;
            List<List<String>> reader;
            List<LogDTO> result = new List<LogDTO>();
            string whereClause = "";

            if (filter != null)
            {
                whereClause = "WHERE ";
                whereClause += "loglevel >= " + (int) filter.logLevel + " AND "; //siempre se incluye
                whereClause += filter.action != null ? " action LIKE '%" + filter.action + "%' AND" : "";
                whereClause += filter.description != null ? " description LIKE '%" + filter.description + "%' AND " : "";
                whereClause += filter.entity != null ? " entity LIKE '%" + filter.entity +"%' AND ": "";
                if (filter.created.Year != 1900) whereClause += "created > CONVERT(datetime, '" + filter.created + "', 103)";

                if(whereClause.EndsWith(" AND ")) whereClause.Remove(whereClause.Length - 4);
            }

            sql = "SELECT * FROM logs "+ whereClause + " ORDER BY created desc";
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


        public void SaveLog(LogDTO log)
        {

            DBSql dbsql = new DBSql();
            String sql;

            sql = "INSERT INTO logs (loglevel, action, description, entity, created) ";
            sql += " VALUES (";
            sql +=  (int) log.logLevel + ", ";
            sql += "'" + log.action + "' , ";
            sql += "'" + log.description + "' , ";
            sql += "'" + log.entity + "' , ";
            sql += "GETDATE()";
            sql += "); SELECT @@IDENTITY";
            log.id = dbsql.ExecuteNonQuery(sql);
        }

        private LogDTO Resolve(List<String> item)
        {
            LogDTO result = new LogDTO();

            result.id = int.Parse(item[0]);
            result.logLevel = (LogDTO.Level) int.Parse(item[1]);
            result.action = item[2];
            result.description = item[3];
            result.entity = item[4];
            result.created = DateTime.Parse(item[5]);
            return result;
        }
    }
}
