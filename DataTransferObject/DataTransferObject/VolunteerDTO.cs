﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class VolunteerDTO : PersonDTO
    {
        public int volunteerId;
        public int branchId;
        public int userId;

        public VolunteerDTO() { }

        public VolunteerDTO(int personId, int branchId, int userId) {
            this.id = personId;
            this.branchId = branchId;
            this.userId = userId;
        }

    }
}
