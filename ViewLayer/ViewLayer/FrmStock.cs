using System;
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
        private int availableStock = 0;
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
                if (this.Entity != null && this.Entity.donation != null && this.Entity.donation.IsStored())
                {
                    MessageBox.Show("El ítem que está intentando editar pertenece a una donación ya almacenada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    groupBox1.Enabled = false;
                    cmdAccept.Enabled = false;
                }

                //Traducciones
                SessionHelper.RegisterForTranslation(this, Codes.MNU_GE013);
                SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
                SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

                SessionHelper.RegisterForTranslation(lblLot, Codes.LBL_LOT);
                SessionHelper.RegisterForTranslation(lblDepot, Codes.LBL_DEPOT);
                SessionHelper.RegisterForTranslation(lblDescription, Codes.LBL_DESCRIPTION);
                SessionHelper.RegisterForTranslation(lblType, Codes.LBL_ITEM_TYPE);                
                SessionHelper.RegisterForTranslation(lblItemQuantity, Codes.LBL_QUANTITY);
                SessionHelper.RegisterForTranslation(lblDuedate, Codes.LBL_DUEDATE);
                SessionHelper.RegisterForTranslation(lblLocation, Codes.LBL_LOCATION);
                
                LoadDonations();
                LoadDepots();
                LoadItemTypes();
                
                if (this.IsUpdate)
                {
                    cmbDonation.Enabled = false;
                    StockBLL stockBll = new StockBLL();
                    ResultBM stockResult = stockBll.GetStock(this.Entity.id);

                    if (!stockResult.IsValid()) MessageBox.Show(stockResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Entity = stockResult.GetValue<StockBM>();
                    
                    this.availableStock = this.Entity.GetAmountItemsToStockWithoutThis();
                    numericQuantity.Maximum = this.availableStock;
                    numericQuantity.Value = this.Entity.Quantity;
                    
                    txtName.Text = this.Entity.Name;
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
                    //Cuando es nuevo, se toma el valor del elemento seleccionado del combo de donación como valores inicializadores.
                    //En dicho caso, el máximo disponible es el máximo asignable y el número que se sugiere stockear
                    DonationBM donationBm = (DonationBM)cmbDonation.SelectedItem;
                    if (donationBm != null)
                    {
                        this.availableStock = donationBm.Items - donationBm.stocked;
                        numericQuantity.Maximum = this.availableStock;
                        numericQuantity.Value = this.availableStock;
                    }
                }
                CalculateMaxStockLeft((DonationBM)cmbDonation.SelectedItem, (int)numericQuantity.Value);

            }
            catch (Exception exception) {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "EXCEPCIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDonations()
        {
            cmbDonation.SelectedIndexChanged -= cmbDonation_SelectedIndexChanged;

            DonationBLL donationBll = new DonationBLL();
            ResultBM donationResult = donationBll.GetAvaliableDonations();

            if (!donationResult.IsValid())
            {
                MessageBox.Show(donationResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                List<DonationBM> lstDonation = donationResult.GetValue<List<DonationBM>>();
                cmbDonation.DataSource = lstDonation;
                cmbDonation.DisplayMember = "Lot";

                if (lstDonation.Count == 0)
                {
                    MessageBox.Show("No existen donaciones que puedan ser procesadas para su loteo.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmdAccept.Enabled = false;
                }
            }
            cmbDonation.SelectedIndexChanged += cmbDonation_SelectedIndexChanged;
        }

        private void LoadDepots()
        {
            DepotBLL depotBll = new DepotBLL();
            ResultBM depotResult = depotBll.GetDepots();

            if (!depotResult.IsValid()) MessageBox.Show(depotResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (!itemTypeResult.IsValid()) MessageBox.Show(itemTypeResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            dtDueDate.Visible = itemType.Perishable;
        }

        private void cmbDonation_SelectedIndexChanged(object sender, EventArgs e)
        {
            //No debería accederse en edición puesto que la edición no admite cambio de lote.
            if (!this.IsUpdate)
            {
                DonationBM donation = (DonationBM)((ComboBox)sender).SelectedItem;
                this.availableStock = donation.Items - donation.stocked;
                numericQuantity.Maximum = this.availableStock;
                numericQuantity.Value = this.availableStock;
                CalculateMaxStockLeft((DonationBM)((ComboBox)sender).SelectedItem, this.availableStock);
            }
        }

        private void numericQuantity_ValueChanged(object sender, EventArgs e)
        {
            CalculateMaxStockLeft((DonationBM) cmbDonation.SelectedItem, (int) ((NumericUpDown)sender).Value);
        }

        private void CalculateMaxStockLeft(DonationBM donation, int toStock)
        {
            
            int left = this.availableStock - toStock;
            lblLeft.Text = "(" + left + ")";
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            DialogResult pressed = MessageBox.Show(SessionHelper.GetTranslation("SAVE_CHANGES_QUESTION"), "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pressed == DialogResult.No) return;
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
                else MessageBox.Show(stockResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
               
    }
}
