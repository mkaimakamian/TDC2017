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
        private LogBLL log = new LogBLL();

        public ResultBM PerformBackup(string fullBackupPath)
        {
            try
            {
                log.AddLogInfo("Creando copia de seguridad", "Creando copia de seguridad en ....", this);
                BackupDAL backupDal = new BackupDAL();
                backupDal.PerformBackup(fullBackupPath);

                log.AddLogInfo("Creando copia de seguridad", "Copia de seguridad exitosa y disponible en el destino.", this);
                return new ResultBM(ResultBM.Type.OK, "Backup en " + fullBackupPath + " exitoso.");
            }
            catch (Exception exception)
            {
                log.AddLogCritical("Creando copia de seguridad", "Excepción", this);
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("BACKUP_ERROR") + " " + exception.Message, exception);
            }
        }

        public ResultBM PerformRestore(string fullBackupPath)
        {
            try
            {
                BackupDAL backupDal = new BackupDAL();
                backupDal.PerformRestore(fullBackupPath);
                
                SessionHelper.EndSession();
                log.AddLogInfo("Restaurando copia de seguridad", "Copia de seguridad restaurada de ....", this);
                return new ResultBM(ResultBM.Type.OK, "Restore de " + fullBackupPath + " exitoso.");

            } catch (Exception exception) {
                log.AddLogCritical("Creando copia de seguridad", "Excepción", this);
                return new ResultBM(ResultBM.Type.EXCEPTION, SessionHelper.GetTranslation("RESTORE_ERROR") + " " + exception.Message, exception);
            }            

        }
    }
}
