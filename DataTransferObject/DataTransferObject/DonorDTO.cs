using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class DonorDTO: PersonDTO
    {
        public int donorId;
        public int organizationId;
        public bool canBeContacted;

        public DonorDTO() { }

        public DonorDTO(int personId, int donorId, int organizationId, bool canBeContacted)
        {
            this.id = personId;
            this.donorId = donorId;
            this.organizationId = organizationId;
            this.canBeContacted = canBeContacted;
        }
    }
}
