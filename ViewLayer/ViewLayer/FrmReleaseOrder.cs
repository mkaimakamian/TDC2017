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
    public partial class FrmReleaseOrder : Form
    {
        private List<ReleaseOrderDetailBM> lstAdded = new List<ReleaseOrderDetailBM>();

        private ReleaseOrderBM entity;
        private bool isUpdate = false;

        public ReleaseOrderBM Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }

        public FrmReleaseOrder()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmReleaseOrder_Load(object sender, EventArgs e)
        {
            //Traducciones
            SessionHelper.RegisterForTranslation(this, Codes.MNU_GE016);
            SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
            SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

            try 
            {
                BeneficiaryBLL beneficiaryBll = new BeneficiaryBLL();
                ResultBM beneficiaryResult = beneficiaryBll.GetBeneficiaries();
                cmbBeneficiary.DataSource = beneficiaryResult.GetValue<List<BeneficiaryBM>>();
                cmbBeneficiary.DisplayMember = "FullName";

                StockBLL stockBll = new StockBLL();
                ResultBM stockResult = stockBll.GetAvailableStocks();
                lstStock.DataSource = stockResult.GetValue<List<StockBM>>();
                lstStock.DisplayMember = "Name";

                if (IsUpdate)
                {
                    bool found = false;

                    for (int i = 0; i < cmbBeneficiary.Items.Count && !found; ++i)
                    {
                        found = ((BeneficiaryBM)cmbBeneficiary.Items[i]).id == this.Entity.beneficiary.id;
                        if (found) cmbBeneficiary.SelectedIndex = i;

                    }
                    lstAdded = this.Entity.detail;
                    dgRelease.DataSource = lstAdded;
                    txtComment.Text = this.Entity.Comment;

                }
                else
                {
                    entity = new ReleaseOrderBM();
                }
            }
            catch (Exception exception) {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lstStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            StockBM selection = (StockBM)((ListBox)sender).SelectedItem;
            nbrQuantity.Maximum = selection.Quantity;
            nbrQuantity.Value = selection.Quantity; // esto debe ser disponible :D
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            UpdateGrid(true);
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            if (dgRelease.SelectedRows.Count == 0) return;
            UpdateGrid(false);
        }

        private void UpdateGrid(bool isAdding)
        {
            ReleaseOrderDetailBM detailToAdd = null;
            StockBM stockToAdd = (StockBM)lstStock.SelectedItem;
          
            if (isAdding)
            {
                bool found = false;
                //si existe, es necesario que se controle que no se puedan agregar más unidades que las que especifica el stock disponible
                for (int i = 0; i < lstAdded.Count && !found; ++i)
                {
                    found = stockToAdd.id == lstAdded[i].stock.id;
                    if (found)
                    {
                        int total = lstAdded[i].Quantity + int.Parse(nbrQuantity.Value.ToString());

                        if (total <= stockToAdd.Quantity) lstAdded[i].Quantity = total;
                        else lstAdded[i].Quantity = stockToAdd.Quantity;
                        
                    }
                }

                // Si el stock a agregar no existe en la lista de agregados, entonces se incorpora.
                if (!found)
                {
                    detailToAdd = new ReleaseOrderDetailBM();
                    detailToAdd.stock = stockToAdd;
                    detailToAdd.Quantity = int.Parse(nbrQuantity.Value.ToString());
                    lstAdded.Add(detailToAdd);
                }
                
            }
            else
            {
                // Proceso inverso: se elimina de la lista de agregados y se debe buscar en el listado de los " a agergar"
                ReleaseOrderDetailBM toRemove = (ReleaseOrderDetailBM) dgRelease.SelectedRows[0].DataBoundItem;

                bool found = false;
                
                for (int i = 0; i < lstAdded.Count && !found; ++i)
                {
                    found = toRemove.stock.id == lstAdded[i].stock.id;
                    if (found) lstAdded.RemoveAt(i);
                }

                // Al quitar, hay que cjhequear si existe el stock en la lista para incrementar el total disponible asignable
                found = false;
                List<StockBM>  listOfStock = (List<StockBM>) lstStock.DataSource;
                for (int i = 0; i < listOfStock.Count && !found; ++i)
                {
                    found = toRemove.stock.id == listOfStock[i].id;
                    if (found)
                    {

                        //int total = lstAdded[i].Quantity + int.Parse(nbrQuantity.Value.ToString());

                        //if (total <= stockToAdd.Quantity) lstAdded[i].Quantity = total;
                        //else lstAdded[i].Quantity = stockToAdd.Quantity;

                        listOfStock[i].Quantity += listOfStock[i].Quantity + toRemove.Quantity;
                    }
                }

                if (!found)
                {
                    if (listOfStock == null) listOfStock = new List<StockBM>();
                    listOfStock.Add(toRemove.stock);                    
                    lstStock.DataSource = null;
                    lstStock.DataSource = listOfStock;
                    lstStock.DisplayMember = "Name";
                }

            }
            dgRelease.DataSource = null;
            dgRelease.DataSource = lstAdded;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            DialogResult pressed = MessageBox.Show("¿Desea guardar los cambios?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pressed == DialogResult.No) return;

            try
            {
                ReleaseOrderBLL releaseOrderBll = new ReleaseOrderBLL();
                ResultBM releaseResult = null;

                this.Entity.beneficiary = (BeneficiaryBM)cmbBeneficiary.SelectedItem;
                this.Entity.Comment = txtComment.Text;
                this.Entity.detail = lstAdded;

                if (isUpdate) releaseResult = releaseOrderBll.UpdateReleaseOrder(this.Entity);
                else releaseResult = releaseOrderBll.SaveReleaseOrder(this.Entity);

                if (releaseResult.IsValid()) Close();
                else MessageBox.Show("Se ha producido el siguiente error: " + releaseResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
    }
}
