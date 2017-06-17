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
                ResultBM countryResult = new CountryBLL().GetCountries();
                cmbCountry.DataSource = countryResult.GetValue<List<CountryBM>>();
                cmbCountry.DisplayMember = "Name";

                if (IsUpdate)
                {
                    DonorBLL donorBll = new DonorBLL();
                    ResultBM resultDonor = donorBll.GetDonor(entity.donorId);
                    this.Entity = resultDonor.GetValue<DonorBM>();

                    CompletePersonData(entity);
                    CompleteAddressData(entity);
                    CompleteCompanyData(entity);
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
            txtName.Text = donor.Name;
            txtLastName.Text = donor.LastName;
            dateBirthday.Value = donor.Birthdate;
            txtMail.Text = donor.Email;
            txtPhone.Text = donor.phone;
            rbuttonFemale.Checked = donor.gender == 'F';
            rButtonMale.Checked = donor.gender == 'M';
            txtDocument.Text = donor.dni.ToString();
            chkBoxContact.Checked = donor.canBeContacted;
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
                if (found)
                {
                    cmbCountry.SelectedIndex = i;
                }
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
            donor.Name = txtName.Text;
            donor.LastName = txtLastName.Text;
            donor.Birthdate = dateBirthday.Value;
            donor.Email = txtMail.Text;
            donor.phone = txtPhone.Text;
            donor.gender = rbuttonFemale.Checked? 'F' : 'M';
            if (txtDocument.Text.Length == 0) txtDocument.Text = "0";
            donor.dni = int.Parse(txtDocument.Text);
            donor.canBeContacted = chkBoxContact.Checked;
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
            if (donor.organization == null) donor.organization = new OrganizationBM();
            
            donor.organization.name = txtCompany.Text;
            donor.organization.category = txtCategory.Text;
            donor.organization.email = txtMailCmpny.Text;
            donor.organization.phone = txtPhoneCmpny.Text;
            donor.organization.comment = txtCommentCmpny.Text;
        }

        private void txtDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !(e.KeyChar == (char)Keys.Back)) e.Handled = true;
        }
    }
}
