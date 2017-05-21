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
            SessionHelper.RegisterForTranslation(cmdCancel, Codes.BTN_CLOSE);
        }

        private void FrmLanguage_Load(object sender, EventArgs e)
        {
            try
            {
                //Carga los idiomas
                LanguageBLL languageBll = new LanguageBLL();
                ResultBM language = languageBll.GetLanguages();

                if (language.IsValid())
                {
                    cmbLanguage.DataSource = language.GetValue<List<LanguageBM>>();
                    cmbLanguage.DisplayMember = "Name";

                    //Desprolijo - mejorar (BAJA PRIORIDAD)
                    bool found = false;

                    for (int i = 0; i < language.GetValue<List<LanguageBM>>().Count && !found; ++i)
                    {
                        found = language.GetValue<List<LanguageBM>>()[i].Id == SessionHelper.GetLoggedUser().LanguageId;
                        if (found)
                        {
                            cmbLanguage.SelectedIndex = i;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(language.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            LanguageBM language = (LanguageBM) cmbLanguage.SelectedValue;
            UserBLL userBll = new UserBLL();

            ResultBM result = userBll.ChangeCurrentLanguage(language.Id);
            try
            {
                if (result.IsValid())
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } catch (Exception excpetion) {
                MessageBox.Show("Se ha producido el siguiente error: " + excpetion.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }

        private void lblLanguage_Click(object sender, EventArgs e)
        {

        }
    }
}
