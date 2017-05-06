using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class LogDTO
    {

        public enum Level 
        {
            DEBUG=1,
            WARNING=2,
            CRITICAL=3,
            INFO=4
        };

    public int id;
    public Level logLevel;
    public string action;
    public string description;
    public string entity;
    public DateTime created;

    public LogDTO()
    {
    }
    public LogDTO (Level loglevel, string action, string description, string entity) {
        this.logLevel = loglevel;
        this.action = action;
        this.description = description;
        this.entity = entity;
        }
    }
}
