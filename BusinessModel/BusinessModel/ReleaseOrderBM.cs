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

        public ReleaseOrderBM() { }
        
        public ReleaseOrderBM(ReleaseOrderDTO releaseorderDto, BeneficiaryBM beneficiaryBm = null, List<ReleaseOrderDetailBM> releaseOrderDetailBms = null)
        {
            this.id = releaseorderDto.id;
            this.beneficiary = beneficiaryBm;
            this.comment = releaseorderDto.comment;
            this.released = releaseorderDto.released;
            this.received = releaseorderDto.released;
            this.status = releaseorderDto.status;
            this.detail = releaseOrderDetailBms;
        }
    }
}
