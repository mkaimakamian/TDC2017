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
                loginBll.LogIn(TxtUser.Text, TxtPassword.Text);

                FrmMain frmPrincipal = new FrmMain();
                frmPrincipal.loginReference = this;
                frmPrincipal.Show();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
