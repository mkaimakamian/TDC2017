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
    public partial class FrmGenericMain : Form
    {
        private Type viewer;
        private Type entity;

        public FrmGenericMain()
        {
            InitializeComponent();
        }

        public FrmGenericMain(Type entity, Type viewer)
        {
            InitializeComponent();
            this.entity = entity;
            this.viewer = viewer;
        }

        /// <summary>
        /// Instancia un formulario / grilla y muestra los datos provistos por el tipo de entidad de negocio pasado por parámetro.
        /// </summary>
        /// <param name="entity"></param>
        public FrmGenericMain(Type entity)
        {
            InitializeComponent();
            this.entity = entity;
        }

        private void FrmGenericMain_Load(object sender, EventArgs e)
        {
            object businessLogic = Activator.CreateInstance(this.entity);
            try
            {
                ResultBM result = ((BLEntity) businessLogic).GetCollection();
                if (result.IsValid())
                {
                    dgView.DataSource = result.GetValue<List<UserBM>>();

                    SessionHelper.RegisterForTranslation(cmdNew, Codes.BTN_NEW);
                    SessionHelper.RegisterForTranslation(cmdEdit, Codes.BTN_EDIT);
                    SessionHelper.RegisterForTranslation(cmdDelete, Codes.BTN_DELETE);
                    SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);
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

        private void cmdNew_Click(object sender, EventArgs e)
        {
            ((Form)Activator.CreateInstance(this.viewer)).ShowDialog();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
