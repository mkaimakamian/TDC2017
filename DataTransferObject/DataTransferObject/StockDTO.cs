using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class StockDTO
    {
        public int  id;
        public string name;
        public int quantity;
        public int itemTypeId;
        public int donationId;
        public int depotId;
        public DateTime dueDate;

        public StockDTO() { }

        public StockDTO(string name, int quantity, int itemTypeId, int donationId, int depotId, DateTime dueDate)
        {
            this.name = name;
            this.quantity = quantity;
            this.itemTypeId = itemTypeId;
            this.donationId = donationId;
            this.depotId = depotId;
            this.dueDate = dueDate;
        }
    }
}
