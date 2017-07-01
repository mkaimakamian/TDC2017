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
    }
}
