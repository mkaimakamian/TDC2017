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
    public partial class FrmLanguage : Form
    {
        public FrmLanguage()
        {
            InitializeComponent();
            SessionHelper.RegisterForTranslation(lblLanguage, Codes.MNU_OP001_LBL_LANGUAGE);
            SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
            SessionHelper.RegisterForTranslation(cmdCancel, Codes.BTN_CANCEL);
        }

        private void FrmLanguage_Load(object sender, EventArgs e)
        {
            //Carga los idiomas
            LanguageBLL languageBll = new LanguageBLL();
            cmbLanguage.DataSource =  languageBll.GetLanguages();
            cmbLanguage.DisplayMember = "name";

            //Posicionarse de acuerdo con el idioma del usuario.
            //UserBM userBm = SessionHelper.GetLoggedUser();
            //cmbLanguage.SelectedValue = SessionHelper.Get
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            LanguageBM language = (LanguageBM) cmbLanguage.SelectedValue;
            UserBLL userBll = new UserBLL();
            if (userBll.ChangeCurrentLanguage(language.Id))
            {
               this.Close();
            }
        }

        private void lblLanguage_Click(object sender, EventArgs e)
        {

        }
    }
}
