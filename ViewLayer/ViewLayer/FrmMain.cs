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
using Helper;

namespace ViewLayer
{
    public partial class FrmMain : Form
    {
        public Form loginReference;
        

        public FrmMain()
        {
            InitializeComponent();

            //HERRAMIENTAS
            herramientasToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE001);
            herramientasToolStripMenuItem.Text = SessionHelper.GetTranslation(Codes.MNU_GE001);
            //Cambiar idioma
            idiomaToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.OP001);
            idiomaToolStripMenuItem.Text = SessionHelper.GetTranslation(Codes.MNU_OP001);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            loginReference.Hide();

        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginBLL loginBll = new LoginBLL();
            loginBll.Logout();
            loginReference.Show();
        }

        private void idiomaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmLanguage().ShowDialog();
        }


    }
}
