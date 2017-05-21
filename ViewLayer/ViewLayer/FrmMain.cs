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

            //Seguridad > Usuarios
            usuariosToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE003);
            SessionHelper.RegisterForTranslation(usuariosToolStripMenuItem, Codes.MNU_GE003);


            //Seguridad > Perfiles
            perfilesToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE004);
            SessionHelper.RegisterForTranslation(perfilesToolStripMenuItem, Codes.MNU_GE004);

            //Mantenimiento
            mantenimientoToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE005);
            SessionHelper.RegisterForTranslation(mantenimientoToolStripMenuItem, Codes.MNU_GE005);

            //Mantenimiento > Bitácora
            bitácoraToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.OP010);
            SessionHelper.RegisterForTranslation(bitácoraToolStripMenuItem, Codes.MNU_OP010);

            //Mantenimiento > Respaldo
            respaldoToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE006);
            SessionHelper.RegisterForTranslation(respaldoToolStripMenuItem, Codes.MNU_GE006);

            //Mantenimiento > Integridad
            integridadToolStripMenuItem.Visible = SessionHelper.HasPermission(Codes.GE007);
            SessionHelper.RegisterForTranslation(integridadToolStripMenuItem, Codes.MNU_GE007);

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
            new FrmGenericMain(
                typeof(UserBLL), 
                typeof(FrmUser),
                SessionHelper.HasPermission(Codes.OP003),
                SessionHelper.HasPermission(Codes.OP004),
                SessionHelper.HasPermission(Codes.OP005)
                ).ShowDialog();
        }

        private void perfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmGenericMain(typeof(ProfileBLL), typeof(FrmProfile)).ShowDialog();
        }

        private void mantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void respaldoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmBackup().ShowDialog();
        }

        private void integridadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmIntegrity().ShowDialog();
        }
    }
}
