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
        public int statusId;
        public int donorId;
        public string comment;
        public int volunteerId;
        public int stocked; //indica cuantos de los ítems están stockeados

        public DonationDTO() { }

        public DonationDTO(int items, DateTime arrival, int statusId, int donorId, string comment, int volunteerId, int id = 0)
        {
            this.id = id;
            this.items = items;
            this.arrival = arrival;
            this.statusId = statusId;
            this.donorId = donorId;
            this.comment = comment;
            this.volunteerId = volunteerId;
        }

       
    }
}
