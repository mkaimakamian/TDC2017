using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class ReleaseOrderDetailBM
    {
        public int id;
        public int releaseorderId;
        public int stockId;
        public int quantity;

        public ReleaseOrderDetailBM() { }

        public ReleaseOrderDetailBM(ReleaseOrderDetailDTO releaseOrderDtail)
        {
            this.id = releaseOrderDtail.id;
            this.releaseorderId = releaseOrderDtail.releaseOrderId;
            this.stockId = releaseOrderDtail.stockId;
            this.quantity = releaseOrderDtail.quantity;
        }
    }
}
