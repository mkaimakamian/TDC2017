using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class BackupBLL
    {

        public ResultBM PerformBackup(string fullBackupPath)
        {
            BackupDAL backupDal = new BackupDAL();
            bool success = backupDal.PerformBackup(fullBackupPath);

            if (success)
            {
                return new ResultBM(ResultBM.Type.OK, "Backup en " + fullBackupPath + " exitoso.");
            }
            else
            {
                return new ResultBM(ResultBM.Type.FAIL, "No se ha podido generar el backup en " + fullBackupPath + ".");
            }
        }

        public ResultBM PerformRestore(string fullBackupPath)
        {
            BackupDAL backupDal = new BackupDAL();
            bool success = backupDal.PerformRestore(fullBackupPath);

            if (success)
            {
                return new ResultBM(ResultBM.Type.OK, "Restore de " + fullBackupPath + " exitoso.");
            }
            else
            {
                return new ResultBM(ResultBM.Type.FAIL, "No se ha podido restaurar la base de " + fullBackupPath + ".");
            }
        }
    }
}
