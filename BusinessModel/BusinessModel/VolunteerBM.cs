using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class VolunteerBM : PersonBM
    {
        public int volunteerId;
        public BranchBM branch;
        public UserBM user;

        public VolunteerBM() { }

        public VolunteerBM(VolunteerDTO volunteerDto)
        {
            this.id = volunteerDto.id;
            this.Name = volunteerDto.name;
            this.LastName = volunteerDto.lastName;
            this.Birthdate = volunteerDto.birthdate;
            this.Email = volunteerDto.email;
            this.phone = volunteerDto.phone;
            this.gender = volunteerDto.gender;
            this.dni = volunteerDto.dni;
            this.volunteerId = volunteerDto.volunteerId;
        }

        public VolunteerBM(VolunteerDTO volunteerDto, AddressBM addressBm, BranchBM branchBm, UserBM userBm = null)
        {
            this.id = volunteerDto.id;
            this.Name = volunteerDto.name;
            this.LastName = volunteerDto.lastName;
            this.Birthdate = volunteerDto.birthdate;
            this.Email = volunteerDto.email;
            this.phone = volunteerDto.phone;
            this.gender = volunteerDto.gender;
            this.dni = volunteerDto.dni;
            this.address = addressBm;
            this.volunteerId = volunteerDto.volunteerId;
            this.branch = branchBm;
            this.user = userBm;
        }

        //ver esto porque no debería recibirse un PersonBM
        public VolunteerBM(PersonBM personBm, BranchBM branchBm, UserBM userBm = null)
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

            this.branch = branchBm;
            this.user = userBm;
        }
    }
}
