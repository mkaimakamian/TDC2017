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
    public partial class FrmDonor : Form
    {
        private DonorBM entity;
        private bool isUpdate = false;

        public DonorBM Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }

        public FrmDonor()
        {
            InitializeComponent();
        }

        private void FrmDonor_Load(object sender, EventArgs e)
        {
            try
            {
                //Traducciones
                SessionHelper.RegisterForTranslation(this, Codes.MNU_GE009);
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
                SessionHelper.RegisterForTranslation(lblCompany, Codes.LBL_COMPANY);
                SessionHelper.RegisterForTranslation(lblCategory, Codes.LBL_CATEGORY);
                SessionHelper.RegisterForTranslation(lblCommentCpny, Codes.LBL_OBSERVATION);
                SessionHelper.RegisterForTranslation(lblMailCmpny, Codes.LBL_EMAIL);
                SessionHelper.RegisterForTranslation(lblPhoneCmpny, Codes.LBL_PHONE);
                SessionHelper.RegisterForTranslation(chkBoxContact, Codes.LBL_CAN_CONTACT);

                ResultBM countryResult = new CountryBLL().GetCountries();
                cmbCountry.DataSource = countryResult.GetValue<List<CountryBM>>();
                cmbCountry.DisplayMember = "Name";

                if (IsUpdate)
                {
                    DonorBLL donorBll = new DonorBLL();
                    ResultBM resultDonor = donorBll.GetDonor(entity.donorId);

                    if (resultDonor.IsValid())
                    {
                        this.Entity = resultDonor.GetValue<DonorBM>();

                        CompletePersonData(this.Entity);
                        CompleteAddressData(this.Entity);
                        CompleteCompanyData(this.Entity);
                    }
                    else
                    {
                        MessageBox.Show(resultDonor.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    entity = new DonorBM();
                }
            }
            catch (Exception exception) {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CompletePersonData(DonorBM donor)
        {
            txtName.Text = donor.name;
            txtLastName.Text = donor.lastName;
            dateBirthday.Value = donor.Birthdate;
            txtMail.Text = donor.Email;
            txtPhone.Text = donor.phone;
            rbuttonFemale.Checked = donor.gender == 'F';
            rButtonMale.Checked = donor.gender == 'M';
            txtDocument.Text = donor.dni.ToString();
            chkBoxContact.Checked = donor.CanBeContacted;
        }

        private void CompleteAddressData(DonorBM donor)
        {
            txtStreet.Text = donor.address.street;
            txtNumber.Text = donor.address.number.ToString();
            txtApartment.Text = donor.address.apartment;
            txtComment.Text = donor.address.comment;
            
            bool found = false;

            for (int i = 0; i < cmbCountry.Items.Count && !found; ++i)
            {
                found = ((CountryBM) cmbCountry.Items[i]).iso2 == donor.address.country.iso2;
                if (found) cmbCountry.SelectedIndex = i;
            }

        }

        private void CompleteCompanyData(DonorBM donor)
        {
            if (donor.organization != null)
            {
                txtCompany.Text = donor.organization.name;
                txtCategory.Text = donor.organization.category;
                txtMailCmpny.Text = donor.organization.email;
                txtPhoneCmpny.Text = donor.organization.phone;
                txtCommentCmpny.Text = donor.organization.comment;
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            DialogResult pressed = MessageBox.Show("¿Desea guardar los cambios?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pressed == DialogResult.No) return;

            try
            {
                DonorBLL donorBll = new DonorBLL();
                ResultBM donorResult;

                //Se encargan de completar la entidad con los valores necesarios
                FillPersonData(this.Entity);
                FillAddressData(this.Entity);
                FillCompanyData(this.Entity);
                
                if (isUpdate) donorResult = donorBll.UpdateDonor(this.Entity);
                else donorResult = donorBll.SaveDonor(this.Entity);

                if (donorResult.IsValid()) Close();
                else MessageBox.Show("Se ha producido el siguiente error: " + donorResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FillPersonData(DonorBM donor)
        {
            //Completa los datos básicos del donador
            donor.name = txtName.Text;
            donor.lastName = txtLastName.Text;
            donor.Birthdate = dateBirthday.Value;
            donor.Email = txtMail.Text;
            donor.phone = txtPhone.Text;
            donor.gender = rbuttonFemale.Checked? 'F' : 'M';
            if (txtDocument.Text.Length == 0) txtDocument.Text = "0";
            donor.dni = int.Parse(txtDocument.Text);
            donor.CanBeContacted = chkBoxContact.Checked;
        }

        private void FillAddressData(DonorBM donor)
        {
            //Completa los datos catastrales del donador
            if (donor.address == null) donor.address = new AddressBM();
            donor.address.street = txtStreet.Text;
            if (txtNumber.Text.Length == 0) txtNumber.Text = "0";
            donor.address.number = int.Parse(txtNumber.Text);
            donor.address.apartment = txtApartment.Text;
            donor.address.comment = txtComment.Text;
            donor.address.country = cmbCountry.SelectedItem as CountryBM;
        }

        private void FillCompanyData(DonorBM donor)
        {
            // Si se imputó algo, entonces se crea el objeto compañía
            int input = txtCompany.Text.Length + txtCategory.Text.Length + txtMailCmpny.Text.Length + txtPhoneCmpny.Text.Length + txtCommentCmpny.Text.Length;
            if (input > 0 && donor.organization == null) donor.organization = new OrganizationBM();

            if (donor.organization != null)
            {
                donor.organization.name = txtCompany.Text;
                donor.organization.category = txtCategory.Text;
                donor.organization.email = txtMailCmpny.Text;
                donor.organization.phone = txtPhoneCmpny.Text;
                donor.organization.comment = txtCommentCmpny.Text;
            }
        }

        private void txtDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back)) e.Handled = true;
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back)) e.Handled = true;
        }
    }
}
