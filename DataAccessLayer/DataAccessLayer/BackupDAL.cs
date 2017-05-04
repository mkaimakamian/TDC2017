using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BackupDAL
    {
        public bool PerformBackup(string fullBackupPath)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "BACKUP DATABASE CAMPOI TO DISK = '" + fullBackupPath + "'";
            dbsql.ExecuteNonQuery(sql);
            return true;
        }

        public bool PerformRestore(string fullBackupPath)
        {
            DBSql dbsql = new DBSql();
            String sql;

            sql = "USE MASTER ALTER DATABASE CAMPOI SET SINGLE_USER WITH ROLLBACK IMMEDIATE RESTORE DATABASE CAMPOI FROM DISK = '" + fullBackupPath + "' WITH REPLACE";
            dbsql.ExecuteNonQuery(sql);
            return true;
        }
    }
}
