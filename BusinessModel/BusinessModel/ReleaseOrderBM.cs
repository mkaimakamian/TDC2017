using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class ReleaseOrderBM
    {
        public int id;
        public BeneficiaryBM beneficiary;
        public string comment;
        public DateTime released;
        public DateTime received;
        public string status;
        public List<ReleaseOrderDetailBM> detail;
    }
}
