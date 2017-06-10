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
        private UserBM entity;
        private bool isUpdate = false;

        public UserBM Entity
        {
            get { return this.entity; }
            set { this.entity = value;}
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }

        public FrmUser()
        {
            InitializeComponent();
        }

        private void FrmUser_Load(object sender, EventArgs e)
        {
            try
            {
                //Traducciones
                SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
                SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

                SessionHelper.RegisterForTranslation(lblName, Codes.LBL_NAME);
                SessionHelper.RegisterForTranslation(lblPassword, Codes.LBL_PASSWORD);
                SessionHelper.RegisterForTranslation(lblPasswordConfirm, Codes.LBL_PASSWORD_CHECK);
                SessionHelper.RegisterForTranslation(lblLanguage, Codes.LBL_LANGUAGE);
                SessionHelper.RegisterForTranslation(lblProfile, Codes.LBL_PROFILE);
                SessionHelper.RegisterForTranslation(chkIsActive, Codes.LBL_ACTIVE);
                
                //Idioma
                LanguageBLL languageBll = new LanguageBLL();
                ResultBM language = languageBll.GetLanguages();

                //Permisos
                ProfileBLL profileBll = new ProfileBLL();
                ResultBM profile = profileBll.GetProfiles();

                if (language.IsValid())
                {
                    cmbLanguage.DataSource = language.GetValue<List<LanguageBM>>();
                    cmbLanguage.DisplayMember = "Name";
                }
                else
                {
                    MessageBox.Show(language.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                if (profile.IsValid())
                {
                    cmbProfile.DataSource = profile.GetValue<List<PermissionMDL>>();
                    cmbProfile.DisplayMember = "Description";
                }
                else
                {
                    MessageBox.Show(language.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //Si se está actualizando, entonces se completan los campos
                if (isUpdate)
                {
                    txtName.Text = this.Entity.Name;
                    chkIsActive.Checked = this.Entity.Active;

                    //Desprolijo - mejorar (BAJA PRIORIDAD)
                    bool found = false;

                    for (int i = 0; i < language.GetValue<List<LanguageBM>>().Count && !found; ++i)
                    {
                        found = language.GetValue<List<LanguageBM>>()[i].Id == entity.LanguageId;
                        if (found)
                        {
                            cmbLanguage.SelectedIndex = i;
                        }
                    }

                    found = false;
                    for (int i = 0; i < profile.GetValue<List<PermissionMDL>>().Count && !found; ++i)
                    {
                        found = profile.GetValue<List<PermissionMDL>>()[i].Code == entity.PermissionId;
                        if (found)
                        {
                            cmbProfile.SelectedIndex = i;
                        }
                    }
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
            PermissionMDL profile = (PermissionMDL)cmbProfile.SelectedValue;
            ResultBM saveResult;
            UserBM userBm;

            if (txtPassword.Text != txtPasswordCheck.Text)
            {
                MessageBox.Show("El password asignado no coincide con el de verificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (isUpdate)
                {
                 
                    this.Entity.Name = txtName.Text;
                    this.Entity.Active = chkIsActive.Checked;
                    this.Entity.LanguageId = language.Id;
                    this.Entity.PermissionId = profile.Code;

                    //Hubo cambio de password
                    bool updatePassword = txtPassword.Text.Length > 0;
                    if (updatePassword)
                    {
                        this.Entity.Password = txtPassword.Text;
                    }

                    saveResult = userBll.UpdateUser(this.Entity, updatePassword);

                    if (saveResult.IsValid())
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Los cambios no fueron guardados: " + saveResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    userBm = new UserBM(txtName.Text, chkIsActive.Checked, language.Id, profile.code, txtPassword.Text);
                    saveResult = userBll.SaveUser(userBm);

                    if (saveResult.IsValid())
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Los cambios no fueron guardados: " + saveResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
