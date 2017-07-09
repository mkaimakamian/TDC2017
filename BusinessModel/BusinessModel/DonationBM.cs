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
        private int items;
        private DateTime arrival;
        public DonationStatusBM donationStatus;
        public DonorBM donor;
        private string comment;
        public VolunteerBM volunteer;
        public int stocked;

        public DonationBM() { }

        public DonationBM(DonationDTO donationDto, DonorBM donorBm, DonationStatusBM donationStatusBm = null, VolunteerBM volunteerBm = null)
        {
            this.id = donationDto.id;
            this.items = donationDto.items;
            this.arrival = donationDto.arrival;
            this.donationStatus = donationStatusBm;
            this.donor = donorBm;
            this.comment = donationDto.comment;
            this.volunteer = volunteerBm;
            this.stocked = donationDto.stocked;
        }

        //ver si no conviene eliminar el primero y aceptar este con valores nullo default
        //public DonationBM(int items, int donorId, DonationStatusBM donationStatusBm, string comment = null, VolunteerBM volunteerBm = null)
        //{
        //    this.items = items;
        //    this.donationStatus = donationStatusBm;
        //    this.donorId = donorId;
        //    this.comment = comment;
        //    this.volunteer = volunteerBm;
        //}

        public string Lot
        {
            get { return "# " + this.id; }
        }

        public string Status
        {
            get
            {
                return this.donationStatus.name;
            }
        }

        //public string Donor
        //{
        //    get { return this.donor.Name + " " +this.donor.LastName; }
        //}

        public string Responsible
        {
            get
            {
                if (this.volunteer != null) return this.volunteer.FullName;
                else return "";
            }
        }

        public int Items
        {
            get { return this.items; }
            set { this.items = value; }
        }

        public DateTime Arrival
        {
            get { return this.arrival; }
            set { this.arrival = value; }
        }

        public string Comment
        {
            get { return this.comment; }
            set { this.comment = value; }
        }

        public bool IsStored()
        {
            return donationStatus.id == (int) DonationStatusBM.Status.STORED;
        }

        public bool IsToBeRetrieved()
        {
            return donationStatus.id == (int)DonationStatusBM.Status.TO_BE_RETRIEVED;
        }
    }
}
