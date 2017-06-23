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

        public DonorBM(DonorDTO donorDto, AddressBM address, OrganizationBM organizationBm)
        {
            this.id = donorDto.id;
            this.Name = donorDto.name;
            this.LastName = donorDto.lastName;
            this.Birthdate = donorDto.birthdate;
            this.Email = donorDto.email;
            this.phone = donorDto.phone;
            this.gender = donorDto.gender;
            this.dni = donorDto.dni;
            this.address = address;
            this.donorId = donorDto.donorId;
            this.organization = organizationBm;
            this.canBeContacted = donorDto.canBeContacted;
        }

        public DonorBM(bool canBeContacted, PersonBM personBm, OrganizationBM organizationBm)
        {
            this.id = personBm.id;
            this.Name = personBm.Name;
            this.LastName = personBm.LastName;
            this.Birthdate = personBm.Birthdate;
            this.Email = personBm.Email;
            this.phone = personBm.phone;
            this.gender = personBm.gender;
            this.dni = personBm.dni;
            this.address = personBm.address;

            this.organization = organizationBm;
            this.canBeContacted = canBeContacted;
        }

        public DonorBM(DonorDTO donorDto)
        {
            this.id = donorDto.id;
            this.Name = donorDto.name;
            this.LastName = donorDto.lastName;
            this.Birthdate = donorDto.birthdate;
            this.Email = donorDto.email;
            this.phone = donorDto.phone;
            this.gender = donorDto.gender;
            this.dni = donorDto.dni;
            this.donorId = donorDto.donorId;
            this.canBeContacted = donorDto.canBeContacted;
        }

        public bool CanBeContacted
        {
            get { return this.canBeContacted; }
            set { this.canBeContacted = value; }
        }
    }
}
