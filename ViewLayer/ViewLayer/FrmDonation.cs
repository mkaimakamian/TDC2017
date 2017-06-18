using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using BusinessModel;
using BusinessLogicLayer;

namespace ViewLayer
{
    public partial class FrmDonation : Form
    {
        private DonationBM entity;
        private bool isUpdate = false;

        public DonationBM Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }

        public FrmDonation()
        {
            InitializeComponent();
        }

        private void DonationFrm_Load(object sender, EventArgs e)
        {
            try {
                
                //Traducciones
                SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
                SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

                SessionHelper.RegisterForTranslation(lblLot, Codes.LBL_LOT);
                SessionHelper.RegisterForTranslation(lblArrival, Codes.LBL_ARRIVAL);
                SessionHelper.RegisterForTranslation(lblResponsible, Codes.LBL_RESPONSIBLE);
                SessionHelper.RegisterForTranslation(lblDonor, Codes.LBL_DONOR);
                SessionHelper.RegisterForTranslation(lblItems, Codes.LBL_ITEMS);
                SessionHelper.RegisterForTranslation(lblComment, Codes.LBL_OBSERVATION);

                DonorBLL donorBll = new DonorBLL();
                ResultBM donorResult = donorBll.GetDonors();
                VolunteerBLL volunteerBll = new VolunteerBLL();
                ResultBM volunteerResult = volunteerBll.GetVolunteers();

                if (donorResult.IsValid())
                {
                    cmbDonor.DataSource = donorResult.GetValue<List<DonorBM>>();
                    cmbDonor.DisplayMember = "Name";
                }
                else
                    MessageBox.Show(donorResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (volunteerResult.IsValid())
                {
                    cmbVolunteer.DataSource = volunteerResult.GetValue<List<VolunteerBM>>();
                    cmbVolunteer.DisplayMember = "Name";
                }
                else
                    MessageBox.Show(volunteerResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (this.IsUpdate)
                {
                    DonationBLL donationBll = new DonationBLL();
                    ResultBM donationResult = donationBll.GetDonation(this.Entity.id);

                    if (donationResult.IsValid())
                    {
                        //Estoy asumiento que se recuperó algo, más alla de que la operación fue exitosa
                        this.Entity = donationResult.GetValue<DonationBM>();

                        lblLotId.Text = this.Entity.Lot.ToString();
                        dateArrival.Value = this.Entity.Arrival;
                        numericItems.Value = this.Entity.Items;
                        txtComment.Text = this.Entity.Comment;

                        //Posicionar donador
                        bool found = false;

                        for (int i = 0; i < cmbDonor.Items.Count && !found; ++i)
                        {
                            found = ((DonorBM)cmbDonor.Items[i]).donorId == this.Entity.donorId;
                            if (found)
                            {
                                cmbDonor.SelectedIndex = i;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(donationResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                }
                else
                {
                    this.Entity = new DonationBM();
                }


            }
            catch (Exception exception) {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                DonationBLL donationBll = new DonationBLL();
                ResultBM donationResult;

                this.Entity.Arrival = dateArrival.Value;
                this.Entity.Items = int.Parse(numericItems.Value.ToString());
                this.Entity.Comment = txtComment.Text;
                this.Entity.donorId = ((DonorBM)cmbDonor.SelectedItem).donorId;

                //Hack para evitar recuperar los estados
                DonationStatusBM status = new DonationStatusBM();
                status.id = (int) DonationStatusBM.Status.RECEIVED;
                this.Entity.donationStatus = status;


                if (isUpdate) donationResult = donationBll.UpdateDonation(this.Entity);
                else donationResult = donationBll.SaveDonation(this.Entity);

                if (donationResult.IsValid())
                {
                    if (!this.IsUpdate) MessageBox.Show("Lote creado #" + this.Entity.Lot, "Lote creado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Close();
                }
                else MessageBox.Show("Se ha producido el siguiente error: " + donationResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
