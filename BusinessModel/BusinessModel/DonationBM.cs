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
        public string lot;
        public DonationStatusBM donationStatus;
        public int donorId;
        public string comment;
        public int volunteerId;

        public DonationBM(DonationDTO donationDto, DonationStatusBM donationStatusBm)
        {
            this.id = donationDto.id;
            this.items = donationDto.items;
            this.arrival = donationDto.arrival;
            this.lot = donationDto.lot;
            this.donationStatus = donationStatusBm;
            this.donorId = donationDto.donorId;
            this.volunteerId = donationDto.volunteerId;
        }
    }
}
