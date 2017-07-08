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
            SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
            SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

            try 
            {
                BeneficiaryBLL beneficiaryBll = new BeneficiaryBLL();
                ResultBM beneficiaryResult = beneficiaryBll.GetBeneficiaries();
                cmbBeneficiary.DataSource = beneficiaryResult.GetValue<List<BeneficiaryBM>>();
                cmbBeneficiary.DisplayMember = "Name";

                StockBLL stockBll = new StockBLL();
                ResultBM stockResult = stockBll.GetAvailableStocks();
                lstStock.DataSource = stockResult.GetValue<List<StockBM>>();
                lstStock.DisplayMember = "Name";

                if (IsUpdate)
                {

                    //if (resultBeneficiary.IsValid())
                    //{
                        
                    //}
                    //else
                    //{
                    //    MessageBox.Show(resultBeneficiary.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
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
            ReleaseOrderDetailBM detailBm = new ReleaseOrderDetailBM();
            detailBm.stock = (StockBM)lstStock.SelectedItem;
            detailBm.Quantity = int.Parse(nbrQuantity.Value.ToString());
            lstAdded.Add(detailBm);
            dgRelease.DataSource = lstAdded;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
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
