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
    public partial class FrmBackup : Form
    {
        public FrmBackup()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdBackup_Click(object sender, EventArgs e)
        {
            try
            {
                BackupBLL backupBll = new BackupBLL();
                ResultBM bkpResult;
                dlgSave.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                dlgSave.FileName = "rhoddion_" + DateTime.Now.ToString("yyyymmddHHmm");
                dlgSave.Filter = "Rhoddion Backup | *.bkp";
                dlgSave.Title = "Backup";
                DialogResult pressed = dlgSave.ShowDialog();

                if (pressed == DialogResult.OK && dlgSave.FileName != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    lblBackup.Text = dlgSave.FileName;
                    bkpResult = backupBll.PerformBackup(dlgSave.FileName);
                    Cursor.Current = Cursors.Default;

                    if (bkpResult.IsValid())
                        MessageBox.Show("La operación se ha realizado exitosamente.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Se ha producido un error al generar la copia de respaldo: " + bkpResult.description, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void cmdrestore_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Restaurar la base de datos finalizará la sesión actual una vez que la operación haya terminado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                BackupBLL backupBll = new BackupBLL();
                ResultBM bkpResult;

                dlgOpen.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                dlgOpen.Filter = "Rhoddion Backup | *.bkp";
                dlgOpen.Title = "Restore";
                DialogResult pressed = dlgOpen.ShowDialog();

                if (pressed != DialogResult.OK) return;

                DialogResult answer = MessageBox.Show("¿Está seguro de querer restaurar la base de datos? \n tenga en mente finalizará la sesión actual una vez que la operación haya terminado.", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (answer == DialogResult.No)
                    return;

                if (dlgOpen.FileName != "")
                {
                    Cursor.Current = Cursors.WaitCursor;
                    lblRestore.Text = dlgOpen.FileName;
                    bkpResult = backupBll.PerformRestore(dlgOpen.FileName);
                    Cursor.Current = Cursors.Default;

                    if (bkpResult.IsValid())
                    {
                        MessageBox.Show("La operación se ha realizado exitosamente.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Se recorren los formularios abiertos y se cierran todos, salvo el de login
                        List<Form> openForms = new List<Form>();

                        foreach (Form f in Application.OpenForms)
                            openForms.Add(f);

                        foreach (Form f in openForms)
                        {
                            if (f.Name != "FrmLoginMain" && f.Name != "FrmBackup")
                                f.Close();

                            else if (f.Name != "FrmLoginMain")
                                f.Show();
                        }
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Se ha producido un error al restaurar la copia de respaldo: " + bkpResult.description, "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmBackup_Load(object sender, EventArgs e)
        {
            SessionHelper.RegisterForTranslation(this, Codes.MNU_GE006);

            cmdBackup.Enabled = SessionHelper.HasPermission(Codes.OP011);
            SessionHelper.RegisterForTranslation(cmdBackup, Codes.BTN_BACKUP);
            cmdrestore.Enabled =  SessionHelper.HasPermission(Codes.OP012);
            SessionHelper.RegisterForTranslation(cmdrestore, Codes.BTN_RESTORE);

            SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);
        }
    }
}
