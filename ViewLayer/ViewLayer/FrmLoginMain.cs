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
    public partial class FrmLoginMain : Form
    {
        public FrmLoginMain()
        {
            InitializeComponent();
        }

        private void BtnAccess_Click(object sender, EventArgs e)
        {
            try
            {
                LoginBLL loginBll = new LoginBLL();
                ResultBM result = loginBll.LogIn(TxtUser.Text, TxtPassword.Text);

                if (result.IsValid())
                {
                    FrmMain frmPrincipal = new FrmMain();
                    frmPrincipal.loginReference = this;
                    frmPrincipal.Show();
                }
                else
                {
                    MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
