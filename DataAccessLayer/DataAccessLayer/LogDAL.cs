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
    }
}
