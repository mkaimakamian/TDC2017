using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessModel
{
    public class StockBM
    {
        public int id;
        private string name;
        private int quantity;
        public ItemTypeBM itemType;
        public DonationBM donation;
        public DepotBM depot;
        private DateTime dueDate;

        public StockBM() { }

        public StockBM(StockDTO stockDto, DonationBM donationBm=null, DepotBM depotBm=null, ItemTypeBM itemTypeBm=null)
        {
            this.id = stockDto.id;
            this.name = stockDto.name;
            this.quantity = stockDto.quantity;
            this.itemType = itemTypeBm;
            this.donation = donationBm;
            this.depot = depotBm;
            this.dueDate = stockDto.dueDate;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }

        public DateTime DueDate
        {
            get { return this.dueDate; }
            set { this.dueDate = value; }
        }
    }
}
