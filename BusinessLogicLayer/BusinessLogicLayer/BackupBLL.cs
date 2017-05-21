using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using BusinessModel;
using Helper;

namespace BusinessLogicLayer
{
    public class BackupBLL
    {

        public ResultBM PerformBackup(string fullBackupPath)
        {
            try
            {
                BackupDAL backupDal = new BackupDAL();
                backupDal.PerformBackup(fullBackupPath);
                return new ResultBM(ResultBM.Type.OK, "Backup en " + fullBackupPath + " exitoso.");
            }
            catch (Exception exception)
            {
                return new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception);
            }
        }

        public ResultBM PerformRestore(string fullBackupPath)
        {
            try
            {
                BackupDAL backupDal = new BackupDAL();
                backupDal.PerformRestore(fullBackupPath);
                
                SessionHelper.EndSession();
                return new ResultBM(ResultBM.Type.OK, "Restore de " + fullBackupPath + " exitoso.");

            } catch (Exception exception) {
                return new ResultBM(ResultBM.Type.EXCEPTION, exception.Message, exception);
            }            

        }
    }
}
