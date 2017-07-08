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
    public partial class FrmIntegrity : Form
    {
        public FrmIntegrity()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdIntegrity_Click(object sender, EventArgs e)
        {
            try {
                DigitVerificatorBLL dvBll = new DigitVerificatorBLL();
                ResultBM integrityResut = dvBll.IsVerticallyConsistent();

                if (integrityResut.IsValid())
                    MessageBox.Show("La base de datos no presenta problemas de integridad.", "Integridad verificada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("La base de datos no es consistente y se sugiere restaurarla a un estado anterior válido.", "Error de integridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exception) {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }

        private void FrmIntegrity_Load(object sender, EventArgs e)
        {
            SessionHelper.RegisterForTranslation(this, Codes.MNU_GE007);
            SessionHelper.RegisterForTranslation(lblIntegrity, Codes.MNU_GE007_LBL_INTEGRITY);
            SessionHelper.RegisterForTranslation(cmdIntegrity, Codes.BTN_CHECK_INTEGRITY);
            SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);
        }
    }
}
