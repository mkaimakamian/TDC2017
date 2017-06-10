using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using BusinessModel;

namespace BusinessModel
{
    public class DonationBM
    {
        public int id;
        public int items;
        public DateTime arrival;
        public DonationStatusBM donationStatus;
        public int donorId;
        public string comment;
        public int volunteerId;

        public DonationBM(DonationDTO donationDto, DonationStatusBM donationStatusBm)
        {
            this.id = donationDto.id;
            this.items = donationDto.items;
            this.arrival = donationDto.arrival;
            this.donationStatus = donationStatusBm;
            this.donorId = donationDto.donorId;
            this.comment = donationDto.comment;
            this.volunteerId = donationDto.volunteerId;
        }

        public DonationBM(int items,int donorId, DonationStatusBM donationStatusBm, string comment = null, int volunteerId = 0)
        {
            this.items = items;
            this.donationStatus = donationStatusBm;
            this.donorId = donorId;
            this.comment = comment;
            this.volunteerId = volunteerId;
        }
    }
}
