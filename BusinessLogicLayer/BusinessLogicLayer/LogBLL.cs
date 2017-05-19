using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAccessLayer;
using BusinessModel;

namespace BusinessLogicLayer
{
    public class LogBLL : BLEntity
    {
        public ResultBM GetLogs()
        {
            ResultBM result;
            try {
                LogDAL logDal = new LogDAL();
                List<LogDTO> logDtos = logDal.GetLogs();
                List<LogBM> logBms = ConvertIntoBusinessModel(logDtos);                
                result = new ResultBM(ResultBM.Type.OK, "Recuperación de registros exitosa.", logBms);
            }
            catch (Exception exception)
            {
                result = new ResultBM(ResultBM.Type.EXCEPTION, exception.Message);
            }

            return result;
        }

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

        /// <summary>
        /// Convierte el DTO en BM.
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private LogBM ConvertIntoBusinessModel(LogDTO log)
        {
            LogBM result = new LogBM();
            result.Id = log.id;
            result.LogLevel = log.logLevel.ToString();
            result.Action = log.action;
            result.Description = log.description;
            result.Entity = log.entity;
            result.Created = log.created;
            return result;
        }

        /// <summary>
        /// Convierte un listado de objetos DTO en uno de BM.
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        private List<LogBM> ConvertIntoBusinessModel(List<LogDTO> logs)
        {
            List<LogBM> result = new List<LogBM>();
            foreach (LogDTO log in logs) {
                result.Add(ConvertIntoBusinessModel(log));
            }
            return result;
        }

        public ResultBM GetCollection()
        {
            return this.GetLogs();
        }


        public ResultBM Delete(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
