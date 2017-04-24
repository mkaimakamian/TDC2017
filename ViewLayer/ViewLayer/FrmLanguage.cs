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
            lblLanguage.Text = SessionHelper.GetTranslation(Codes.MNU_OP001_LBL_LANGUAGE);
            cmdAccept.Text = SessionHelper.GetTranslation(Codes.BTN_ACCEPT);
            cmdCancel.Text = SessionHelper.GetTranslation(Codes.BTN_CANCEL);
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
            UserBM userBm = SessionHelper.GetLoggedUser();
            LanguageBM language = (LanguageBM) cmbLanguage.SelectedValue;

            //User Bm quizá debería contener el objeto Lenguage
            userBm.languageId = language.Id;
            UserBLL userBll = new UserBLL();
            userBll.UpdateUser(userBm);
        }
    }
}
