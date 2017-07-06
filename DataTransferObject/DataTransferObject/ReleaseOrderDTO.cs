using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ReleaseOrderDTO
    {
        public int id;
        public int beneficiaryId;
        public string comment;
        public DateTime released;
        public DateTime received;
        public string status;

        public ReleaseOrderDTO() { }

        public ReleaseOrderDTO(int id, int beneficiaryId, string comment, DateTime released, DateTime received, string status) { 
            this.id = id;
            this.beneficiaryId = beneficiaryId;
            this.comment = comment;
            this.released = released;
            this.received = received;
            this.status = status;
        }
    }
}
