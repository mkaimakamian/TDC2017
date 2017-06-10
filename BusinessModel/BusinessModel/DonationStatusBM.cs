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
            RECEIVED,
            STOCKED,
            DELIVERING,
            RECOVERING,
            TO_BE_PICKUP
        };

        public DonationStatusBM(DonationStatusDTO donationStatusDto)
        {
            this.id = donationStatusDto.id;
            this.name = donationStatusDto.name;
            this.description = donationStatusDto.description;
        }
    }
}
