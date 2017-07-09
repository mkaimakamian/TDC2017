using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class DonorBM: PersonBM
    {
        public int donorId;
        public OrganizationBM organization;
        private bool canBeContacted;

        public DonorBM() { }

        public DonorBM(DonorDTO donorDto, AddressBM addressBm=null, OrganizationBM organizationBm=null): base(donorDto, addressBm)
        {
            this.address = addressBm;
            this.donorId = donorDto.donorId;
            this.organization = organizationBm;
            this.canBeContacted = donorDto.canBeContacted;
        }
        
        public bool CanBeContacted
        {
            get { return this.canBeContacted; }
            set { this.canBeContacted = value; }
        }

        public string GetContectInfo()
        {
            string contactInfo = "";
            contactInfo += this.FullName+ Environment.NewLine;
            contactInfo += this.dni + Environment.NewLine;
            contactInfo += this.Email + Environment.NewLine;
            contactInfo += this.phone + Environment.NewLine;
            contactInfo += "-------------------" + Environment.NewLine;

            if (this.address != null)
            {
                contactInfo += this.address.street + " " + this.address.number + Environment.NewLine;
                contactInfo += this.address.apartment + Environment.NewLine;
                contactInfo += this.address.neighborhood + Environment.NewLine;
                contactInfo += "-------------------" + Environment.NewLine;
                contactInfo += this.address.comment;
            }
            return contactInfo;
        }
    }
}
