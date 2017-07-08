using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class DonationStatusBM
    {
        public int id;
        public string name;
        public string description;
                
        public enum Status
        {
            RECEIVED = 1,
            STORED = 2,
            IN_TRANSIT_DELIVERY = 3,
            IN_TRANSIT_PICKUP = 4,
            TO_BE_RETRIEVED = 5
        };

        public DonationStatusBM(DonationStatusDTO donationStatusDto)
        {
            this.id = donationStatusDto.id;
            this.name = donationStatusDto.name;
            this.description = donationStatusDto.description;
        }

        public DonationStatusBM()
        {
        }
    }
}
