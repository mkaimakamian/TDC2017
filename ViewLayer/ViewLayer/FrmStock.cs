﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
using BusinessModel;
using Helper;

namespace ViewLayer
{
    public partial class FrmStock : Form
    {

        private StockBM entity;
        private bool isUpdate = false;

        public StockBM Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }

        public FrmStock()
        {
            InitializeComponent();
        }

        private void FrmStock_Load(object sender, EventArgs e)
        {
            try {
                //Traducciones
                SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
                SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

                LoadDonations();
                LoadDepots();
                LoadItemTypes();

                if (this.IsUpdate)
                {
                    StockBLL stockBll = new StockBLL();
                    ResultBM stockResult = stockBll.GetStock(this.Entity.id);

                    if (!stockResult.IsValid()) MessageBox.Show(stockResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Entity = stockResult.GetValue<StockBM>();

                    txtName.Text = this.Entity.Name;
                    numericQuantity.Value = this.Entity.Quantity;
                    dtDueDate.Value = this.Entity.DueDate;
                    txtLocation.Text = this.Entity.Location;

                    //Posicionar donador
                    bool found = false;

                    for (int i = 0; i < cmbItemType.Items.Count && !found; ++i)
                    {
                        found = ((ItemTypeBM)cmbItemType.Items[i]).id == this.Entity.itemType.id;
                        if (found) cmbItemType.SelectedIndex = i;

                    }

                    found = false;

                    for (int i = 0; i < cmbDonation.Items.Count && !found; ++i)
                    {
                        found = ((DonationBM)cmbDonation.Items[i]).id == this.Entity.donation.id;
                        if (found) cmbDonation.SelectedIndex = i;

                    }

                    found = false;

                    for (int i = 0; i < cmbDepot.Items.Count && !found; ++i)
                    {
                        found = ((DepotBM)cmbDepot.Items[i]).id == this.Entity.depot.id;
                        if (found) cmbDepot.SelectedIndex = i;

                    }
                }
                else
                {
                    this.Entity = new StockBM();
                }

            }
            catch (Exception exception) {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDonations()
        {
            DonationBLL donationBll = new DonationBLL();
            ResultBM donationResult = donationBll.GetAvaliableDonations();

            if (!donationResult.IsValid())
            {
                MessageBox.Show(donationResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cmbDonation.DataSource = donationResult.GetValue<List<DonationBM>>();
                cmbDonation.DisplayMember = "Lot";
            }
        }

        private void LoadDepots()
        {
            DepotBLL depotBll = new DepotBLL();
            ResultBM depotResult = depotBll.GetDepots();

            if (!depotResult.IsValid())
            {
                MessageBox.Show(depotResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cmbDepot.DataSource = depotResult.GetValue<List<DepotBM>>();
                cmbDepot.DisplayMember = "Name";
            }
        }

        private void LoadItemTypes()
        {
            ItemTypeBLL itemTypeBll = new ItemTypeBLL();
            ResultBM itemTypeResult = itemTypeBll.GetItemTypes();

            if (!itemTypeResult.IsValid())
            {
                MessageBox.Show(itemTypeResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cmbItemType.DataSource = itemTypeResult.GetValue<List<ItemTypeBM>>();
                cmbItemType.DisplayMember = "Name";
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemTypeBM itemType = (ItemTypeBM) ((ComboBox)sender).SelectedItem;
            dtDueDate.Enabled = itemType.Perishable;
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                MessageBox.Show("Función no implementada");
                return;
            }

            try
            {
                StockBLL stockBll = new StockBLL();
                ResultBM stockResult;

                this.Entity.Name = txtName.Text;
                this.Entity.Quantity = int.Parse(numericQuantity.Value.ToString());
                this.Entity.itemType = (ItemTypeBM)cmbItemType.SelectedItem;
                this.Entity.donation = (DonationBM)cmbDonation.SelectedItem;
                this.Entity.depot = (DepotBM)cmbDepot.SelectedItem;
                this.Entity.DueDate = dtDueDate.Value;
                this.Entity.Location = txtLocation.Text;


                if (isUpdate) stockResult = stockBll.UpdateStock(this.Entity);
                else stockResult = stockBll.SaveStock(this.Entity);

                if (stockResult.IsValid()) Close();
                else MessageBox.Show("Se ha producido el siguiente error: " + stockResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
