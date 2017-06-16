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

        public VolunteerBM(VolunteerDTO volunteerDto, PersonBM personBm, BranchBM branchBm, UserBM userBm = null)
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

            this.volunteerId = volunteerDto.volunteerId;
            this.branch = branchBm;
            this.user = userBm;
        }

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
