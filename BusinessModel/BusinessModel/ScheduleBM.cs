using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class ScheduleBM
    {
        public int id;
        public DateTime visit;
        public DonationBM donation;
        public string comment;

            public ScheduleBM() {}
            public ScheduleBM(ScheduleDTO scheduleDto, DonationBM donationBm = null) {
                this.id = scheduleDto.id;
                this.visit = scheduleDto.visit;
                this.donation = donationBm;
                this.comment = scheduleDto.comment;
            }
    }

}
