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
    public partial class FrmUser : Form
    {
        public FrmUser()
        {
            InitializeComponent();
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            try
            {
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
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void acceptCancel1_ClickAccept(object sender, EventArgs e)
        {
            UserBLL userBll = new UserBLL();
            LanguageBM language = (LanguageBM)cmbLanguage.SelectedValue;
            UserBM userBm = new UserBM(txtName.Text, chkIsActive.Checked, language.Id, "GE999", txtPassword.Text);
            userBll.CreateUser(userBm);
            this.Close();
        }
    }
}
