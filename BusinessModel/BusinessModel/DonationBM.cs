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
        public VolunteerBM volunteer;

        public DonationBM(DonationDTO donationDto, DonationStatusBM donationStatusBm, VolunteerBM volunteerBm)
        {
            this.id = donationDto.id;
            this.items = donationDto.items;
            this.arrival = donationDto.arrival;
            this.donationStatus = donationStatusBm;
            this.donorId = donationDto.donorId;
            this.comment = donationDto.comment;
            this.volunteer = volunteerBm;
        }

        public DonationBM(int items,int donorId, DonationStatusBM donationStatusBm, string comment = null, VolunteerBM volunteerBm = null)
        {
            this.items = items;
            this.donationStatus = donationStatusBm;
            this.donorId = donorId;
            this.comment = comment;
            this.volunteer = volunteerBm;
        }
    }
}
