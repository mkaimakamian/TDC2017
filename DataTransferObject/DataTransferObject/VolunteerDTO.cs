using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class VolunteerDTO: PersonDTO
    {
        public int volunteerId;
        public int branchId;
        public int userId;
    }
}
