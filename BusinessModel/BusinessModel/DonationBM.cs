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
        public int donorId; //debería ser un objeto
        private string comment;
        public VolunteerBM volunteer;
        public int stocked;

        public DonationBM() { }

        public DonationBM(DonationDTO donationDto, DonationStatusBM donationStatusBm = null, VolunteerBM volunteerBm = null)
        {
            this.id = donationDto.id;
            this.items = donationDto.items;
            this.arrival = donationDto.arrival;
            this.donationStatus = donationStatusBm;
            this.donorId = donationDto.donorId;
            this.comment = donationDto.comment;
            this.volunteer = volunteerBm;
            this.stocked = donationDto.stocked;
        }

        //ver si no conviene eliminar el primero y aceptar este con valores nullo default
        public DonationBM(int items, int donorId, DonationStatusBM donationStatusBm, string comment = null, VolunteerBM volunteerBm = null)
        {
            this.items = items;
            this.donationStatus = donationStatusBm;
            this.donorId = donorId;
            this.comment = comment;
            this.volunteer = volunteerBm;
        }


        //ESTE CONSTRUCTOR DEBE SER REEMPLAZADO POR EL PRIMERO.
        //SE CREA ESTE PARA PODER AVANZAR CON EL PROYECTO
        //public DonationBM(DonationDTO donationDto)
        //{
        //    this.id = donationDto.id;
        //    this.items = donationDto.items;
        //    this.arrival = donationDto.arrival;
        //    //this.donationStatus = donationStatusBm;
        //    this.donorId = donationDto.donorId;
        //    this.comment = donationDto.comment;
        //    //this.volunteer = volunteerBm;
        //}


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

        public int Lot
        {
            get { return this.id; }
            set { this.id = value; }
        }   
    }
}
