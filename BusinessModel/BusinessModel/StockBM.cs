﻿using System;
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
        private string location;

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
            this.location = stockDto.loaction;
        }

        public string Lot
        {
            get { return this.donation.Lot; }
        }

        public string DonationStatus
        {
            get { return  this.donation.Status; }
        }

        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }           

        public DateTime DueDate
        {
            get { return this.dueDate; }
            set { this.dueDate = value; }
        }

        public string Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        //Devuelve la cantidad de ítems que faltan stockear, descontando del ya stockeado la cantidad informada en Quantity.
        public int GetAmountItemsToStockWithoutThis()
        {
            return this.donation.Items - (this.donation.stocked - this.Quantity);
        }
    }
}
