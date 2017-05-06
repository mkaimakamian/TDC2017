using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class LogBLL
    {

        public void AddLogInfo(String action, string description, Object entity)
        {
            CreateLog(LogDTO.Level.INFO, action, description, entity.GetType().ToString());
        }

        public void AddLogWarn(String action, string description, Object entity)
        {
            CreateLog(LogDTO.Level.WARNING, action, description, entity.GetType().ToString());
        }

        public void AddLogCritical(String action, string description, Object entity)
        {
            CreateLog(LogDTO.Level.CRITICAL, action, description, entity.GetType().ToString());
        }

        public void AddLogDebug(String action, string description, Object entity)
        {
            CreateLog(LogDTO.Level.DEBUG, action, description, entity.GetType().ToString());
        }

        private void CreateLog(LogDTO.Level loglevel, string action, string description, string entity)
        {
            LogDAL logDal = new LogDAL();
            LogDTO logDto = new LogDTO(loglevel, action, description, entity);
            logDal.SaveLog(logDto);
        }
    }
}
