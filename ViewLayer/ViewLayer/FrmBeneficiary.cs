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
    public partial class FrmBeneficiary : Form
    {
        private BeneficiaryBM entity;
        private bool isUpdate = false;

        public BeneficiaryBM Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }

        public FrmBeneficiary()
        {
            InitializeComponent();
        }

        private void FrmBeneficiary_Load(object sender, EventArgs e)
        {
            try {
                //Traducciones
                SessionHelper.RegisterForTranslation(this, Codes.MNU_GE014);
                SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
                SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

                SessionHelper.RegisterForTranslation(lblName, Codes.LBL_NAME);
                SessionHelper.RegisterForTranslation(lblLastName, Codes.LBL_LAST_NAME);
                SessionHelper.RegisterForTranslation(lblBirthday, Codes.LBL_BIRTHDAY);
                SessionHelper.RegisterForTranslation(lblMail, Codes.LBL_EMAIL);
                SessionHelper.RegisterForTranslation(lblPhone, Codes.LBL_PHONE);
                SessionHelper.RegisterForTranslation(rbuttonFemale, Codes.LBL_FEMALE);
                SessionHelper.RegisterForTranslation(rButtonMale, Codes.LBL_MALE);
                SessionHelper.RegisterForTranslation(lblDocument, Codes.LBL_UID);

                SessionHelper.RegisterForTranslation(lblStreet, Codes.LBL_STREET);
                SessionHelper.RegisterForTranslation(lblNumber, Codes.LBL_NUMBER);
                SessionHelper.RegisterForTranslation(lblApartment, Codes.LBL_APARTMENT);
                SessionHelper.RegisterForTranslation(lblComment, Codes.LBL_OBSERVATION);
                SessionHelper.RegisterForTranslation(lblCountry, Codes.LBL_COUNTRY);

                SessionHelper.RegisterForTranslation(lblDestination, Codes.LBL_DESTINATARY);
                SessionHelper.RegisterForTranslation(lblAges, Codes.LBL_AGE_RANGE);
                SessionHelper.RegisterForTranslation(lblHealth, Codes.LBL_SALUBRITY);
                SessionHelper.RegisterForTranslation(lblAccesibility, Codes.LBL_ACCESSIBILITY);
                SessionHelper.RegisterForTranslation(lblMajor, Codes.LBL_MAJOR_PROBLEM);

                SessionHelper.RegisterForTranslation(lblDescDestinatary, Codes.LBL_DESC_DESTINATARY);
                SessionHelper.RegisterForTranslation(lblDescAges, Codes.LBL_DESC_AGE_RANGE);
                SessionHelper.RegisterForTranslation(lblDescHealth, Codes.LBL_DESC_SALUBRITY);
                SessionHelper.RegisterForTranslation(lblDescAccessibility, Codes.LBL_DESC_ACCESSIBILITY);
                SessionHelper.RegisterForTranslation(lblDescMajor, Codes.LBL_DESC_MAJOR_PROBLEM);

                ResultBM countryResult = new CountryBLL().GetCountries();
                cmbCountry.DataSource = countryResult.GetValue<List<CountryBM>>();
                cmbCountry.DisplayMember = "Name";

                if (IsUpdate)
                {
                    BeneficiaryBLL beneficiaryBll = new BeneficiaryBLL();
                    ResultBM resultBeneficiary = beneficiaryBll.GetBeneficiary(this.Entity.beneficiaryId);

                    if (resultBeneficiary.IsValid())
                    {
                        this.Entity = resultBeneficiary.GetValue<BeneficiaryBM>();

                        CompletePersonData(this.Entity);
                        CompleteAddressData(this.Entity);
                        SetGauges(this.Entity);
                    }
                    else
                    {
                        MessageBox.Show(resultBeneficiary.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    entity = new BeneficiaryBM();
                }
            }
            catch (Exception exception) {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CompletePersonData(BeneficiaryBM beneficiaryBm)
        {
            txtName.Text = beneficiaryBm.name;
            txtLastName.Text = beneficiaryBm.lastName;
            dateBirthday.Value = beneficiaryBm.Birthdate;
            txtMail.Text = beneficiaryBm.Email;
            txtPhone.Text = beneficiaryBm.phone;
            rbuttonFemale.Checked = beneficiaryBm.gender == 'F';
            rButtonMale.Checked = beneficiaryBm.gender == 'M';
            txtDocument.Text = beneficiaryBm.dni.ToString();
        }

        private void CompleteAddressData(BeneficiaryBM beneficiaryBm)
        {
            txtStreet.Text = beneficiaryBm.address.street;
            txtNumber.Text = beneficiaryBm.address.number.ToString();
            txtApartment.Text = beneficiaryBm.address.apartment;
            txtComment.Text = beneficiaryBm.address.comment;

            bool found = false;

            for (int i = 0; i < cmbCountry.Items.Count && !found; ++i)
            {
                found = ((CountryBM)cmbCountry.Items[i]).iso2 == beneficiaryBm.address.country.iso2;
                if (found) cmbCountry.SelectedIndex = i;
            }

        }

        private void SetGauges(BeneficiaryBM beneficiaryBm)
        {
            trkDestination.Value = beneficiaryBm.destination;
            trkAges.Value = beneficiaryBm.ages;
            trkHealth.Value = beneficiaryBm.health;
            trkAccessibility.Value = beneficiaryBm.accessibility;
            trkMajor.Value = beneficiaryBm.majorProblem;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back)) e.Handled = true;
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back)) e.Handled = true;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            DialogResult pressed = MessageBox.Show(SessionHelper.GetTranslation("SAVE_CHANGES_QUESTION"), "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pressed == DialogResult.No) return;

            try
            {
                BeneficiaryBLL beneficiaryBll = new BeneficiaryBLL();
                ResultBM beneficiaryResult;

                //Se encargan de completar la entidad con los valores necesarios
                FillPersonData(this.Entity);
                FillAddressData(this.Entity);
                FillGauges(this.Entity);
                
                if (isUpdate) beneficiaryResult = beneficiaryBll.UpdateBeneficiary(this.Entity);
                else beneficiaryResult = beneficiaryBll.SaveBeneficiary(this.Entity);

                if (beneficiaryResult.IsValid()) Close();
                else MessageBox.Show(beneficiaryResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }        
        }

        private void FillPersonData(BeneficiaryBM beneficiaryBm)
        {
            //Completa los datos básicos del beneficiario
            beneficiaryBm.name = txtName.Text;
            beneficiaryBm.lastName = txtLastName.Text;
            beneficiaryBm.Birthdate = dateBirthday.Value;
            beneficiaryBm.Email = txtMail.Text;
            beneficiaryBm.phone = txtPhone.Text;
            beneficiaryBm.gender = rbuttonFemale.Checked ? 'F' : 'M';
            if (txtDocument.Text.Length == 0) txtDocument.Text = "0";
            beneficiaryBm.dni = int.Parse(txtDocument.Text);
        }

        private void FillAddressData(BeneficiaryBM beneficiaryBm)
        {
            //Completa los datos catastrales del beneficiario
            if (beneficiaryBm.address == null) beneficiaryBm.address = new AddressBM();
            beneficiaryBm.address.street = txtStreet.Text;
            if (txtNumber.Text.Length == 0) txtNumber.Text = "0";
            beneficiaryBm.address.number = int.Parse(txtNumber.Text);
            beneficiaryBm.address.apartment = txtApartment.Text;
            beneficiaryBm.address.comment = txtComment.Text;
            beneficiaryBm.address.country = cmbCountry.SelectedItem as CountryBM;
        }

        private void FillGauges(BeneficiaryBM beneficiaryBm)
        {
            beneficiaryBm.destination = trkDestination.Value;
            beneficiaryBm.ages = trkAges.Value;
            beneficiaryBm.health = trkHealth.Value;
            beneficiaryBm.accessibility = trkAccessibility.Value;
            beneficiaryBm.majorProblem = trkMajor.Value;
        }
    }
}
