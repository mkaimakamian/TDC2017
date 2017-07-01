using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ReleaseOrderDetailDTO
    {
        public int id;
        public int releaseOrderId;
        public int stockId;
        public int quantity;
    }
}
