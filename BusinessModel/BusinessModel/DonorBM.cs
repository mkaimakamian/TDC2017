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
        public bool canBeContacted;

        public DonorBM(DonorDTO donorDto, PersonBM personBm, OrganizationBM organizationBm)
        {
            this.id = personBm.id;
            this.name = personBm.name;
            this.lastName = personBm.lastName;
            this.birthdate = personBm.birthdate;
            this.email = personBm.email;
            this.phone = personBm.phone;
            this.gender = personBm.gender;
            this.dni = personBm.dni;
            this.address = personBm.address;
            
            this.donorId = donorDto.id;
            this.organization = organizationBm;
            this.canBeContacted = donorDto.canBeContacted;            
        }
    }
}
