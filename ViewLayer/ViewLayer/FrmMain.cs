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
            SessionHelper.RegisterForTranslation(herramientasToolStripMenuItem, Codes.MNU_GE001);
            
            //Cambiar idioma
            idiomaToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.OP001);
            SessionHelper.RegisterForTranslation(idiomaToolStripMenuItem, Codes.MNU_OP001);
            
            //Seguridad
            seguridadToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE002);
            SessionHelper.RegisterForTranslation(seguridadToolStripMenuItem, Codes.MNU_GE002);
            
            //Seguridad > Perfiles
            perfilesToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE003);
            SessionHelper.RegisterForTranslation(perfilesToolStripMenuItem, Codes.MNU_GE003);

            //Mantenimiento
            mantenimientoToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE017);
            SessionHelper.RegisterForTranslation(mantenimientoToolStripMenuItem, Codes.MNU_GE017);


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

        private void herramientasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bitácoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmGenericMain(typeof(LogBLL)).ShowDialog();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmGenericMain(typeof(UserBLL)).ShowDialog();
        }

        private void perfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmGenericMain(typeof(ProfileBLL), typeof(FrmProfile)).ShowDialog();
        }

        private void mantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
