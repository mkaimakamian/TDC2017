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
    public partial class FrmUser : Form
    {
        private UserBM currentElement;
        private bool isUpdate;

        public UserBM CurrentElement
        {
            get { return this.currentElement; }
            set { this.currentElement = value; isUpdate = true; }
        }

        public FrmUser()
        {
            InitializeComponent();
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            try
            {
                //cmdAccept.Visible = SessionHelper.HasPermission(Codes.GE001);
                SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
                SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);


                //Carga los idiomas
                LanguageBLL languageBll = new LanguageBLL();
                ResultBM result = languageBll.GetLanguages();

                if (result.IsValid())
                {
                    cmbLanguage.DataSource = result.GetValue<List<LanguageBM>>();
                    cmbLanguage.DisplayMember = "Name";
                }
                else
                {
                    MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //Posicionarse de acuerdo con el idioma del usuario.
                //UserBM userBm = SessionHelper.GetLoggedUser();
                //cmbLanguage.SelectedValue = SessionHelper.Get

                if (this.CurrentElement != null) {
                    txtName.Text = this.CurrentElement.Name;
                    chkIsActive.Checked = this.CurrentElement.Active;
                    //el password no se debe mostrar
                    //idioma
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            UserBLL userBll = new UserBLL();
            LanguageBM language = (LanguageBM)cmbLanguage.SelectedValue;
            try
            {
                if (isUpdate)
                {
                    // validar que esté todo en orden
                    if (txtPassword.Text == txtPasswordCheck.Text)
                    {
                        this.CurrentElement.Name = txtName.Text;
                        this.CurrentElement.Active = chkIsActive.Checked;
                        this.CurrentElement.LanguageId = language.Id;
                        //this.CurrentElement.PermissionId = txtPassword.Text;
                        this.CurrentElement.Password = txtPassword.Text;


                        userBll.UpdateUser(this.CurrentElement);
                    }
                    else
                    {
                        MessageBox.Show("El password asignado no coincide con el de verificación.");
                    }

                }
                else
                {
                    UserBM userBm = new UserBM(txtName.Text, chkIsActive.Checked, language.Id, "GE999", txtPassword.Text);
                    userBll.CreateUser(userBm);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.Close();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
