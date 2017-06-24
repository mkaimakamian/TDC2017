using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ScheduleDTO
    {
        public int id;
        public DateTime visit;
        public int donationId;
        public string comment;

        public ScheduleDTO() { }
        
        public ScheduleDTO(DateTime visit, int donationId, String comment)
        {
            this.visit = visit;
            this.donationId = donationId;
            this.comment = comment;
        }
     }
}
