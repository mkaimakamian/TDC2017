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
        private string comment;
        public DateTime released;
        public DateTime received;
        private string status;
        public List<ReleaseOrderDetailBM> detail;

        public enum Status
        {
            PENDING,
            REJECTED,
            APPROVED
        };

        public ReleaseOrderBM() {
            this.status = Status.PENDING.ToString();
        }
        
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

        public string OrderNumber
        {
            get { return "# " + this.id; }
        }

        public string OrderStatus
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public int Items
        {
            get {
                int totalItems = 0;

                foreach (ReleaseOrderDetailBM detail in this.detail) totalItems += detail.Quantity;
                return totalItems; 
            }
        }

        public string Beneficiary
        {
            get { return this.beneficiary.FullName; }
        }

        public string Comment
        {
            get { return this.comment; }
            set { this.comment = value; }
        }
    }
}
