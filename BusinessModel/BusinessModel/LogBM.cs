using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel
{
    public class LogBM
    {
        public int id;
        public string logLevel;
        private string action;
        public string description;
        public string entity;
        public DateTime created;

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string LogLevel
        {
            get { return this.logLevel; }
            set { this.logLevel = value; }
        }

        public string Action
        {
            get { return this.action; }
            set { this.action = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public string Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }

        public DateTime Created
        {
            get { return this.created; }
            set { this.created = value; }
        }
    }
}
