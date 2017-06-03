using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class DonationDTO
    {
        public int id;
        public int items;
        public DateTime arrival;
        public string lot;
        public int statusId;
        public int donorId;
        public string comment;
        public int volunteerId;
    }
}
