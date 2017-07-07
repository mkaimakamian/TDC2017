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
        public StockBM stock;
        private int quantity;

        public ReleaseOrderDetailBM() { }

        public ReleaseOrderDetailBM(ReleaseOrderDetailDTO releaseOrderDtail, StockBM stockBm)
        {
            this.id = releaseOrderDtail.id;
            this.releaseorderId = releaseOrderDtail.releaseOrderId;
            this.stock = stockBm;
            this.quantity = releaseOrderDtail.quantity;
        }

        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }

        public string Name
        {
            get { return this.stock.Name; }
        }
    }
}
