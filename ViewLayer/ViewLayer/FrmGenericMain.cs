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

        private bool canCreate;
        private bool canEdit;
        private bool canDelete;

        public FrmGenericMain()
        {
            InitializeComponent();
        }

        public FrmGenericMain(Type entity, Type viewer, bool canCreate=false, bool canEdit=false, bool canDelete=false)
        {
            InitializeComponent();
            // Entidad sobre la que se opera
            this.entity = entity;

            // Formulario a llamar en caso de creación o edición
            this.viewer = viewer;

            this.canCreate = canCreate;
            this.canEdit = canEdit;
            this.canDelete = canDelete;
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
            SessionHelper.RegisterForTranslation(cmdNew, Codes.BTN_NEW);
            SessionHelper.RegisterForTranslation(cmdEdit, Codes.BTN_EDIT);
            SessionHelper.RegisterForTranslation(cmdDelete, Codes.BTN_DELETE);            
            SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

            cmdNew.Enabled = canCreate;
            cmdEdit.Enabled = canEdit;
            cmdDelete.Enabled = canDelete;

            LoadDatagrid();
            AdjustSizes();

            // Ajusta un poco el tamaño del formulario en base a la cantidad de columnas que posee la grilla
            // para que se pueda ver todo el contenido (o la mayor cantidad posible)
            this.Width = dgView.ColumnCount * 100 + 200;
        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            ((Form)Activator.CreateInstance(this.viewer)).ShowDialog();
            LoadDatagrid();
        }

        private void LoadDatagrid()
        {            
            object businessLogic = Activator.CreateInstance(this.entity);
            try
            {
                ResultBM result = ((BLEntity)businessLogic).GetCollection();
                if (result.IsValid())
                {
                    dgView.DataSource = result.GetValue();
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


        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            Form viewForm = (Form)Activator.CreateInstance(this.viewer);
            System.Reflection.PropertyInfo Entity = viewForm.GetType().GetProperty("Entity");
            Entity.SetValue(viewForm, dgView.SelectedRows[0].DataBoundItem);
            viewForm.ShowDialog();
            LoadDatagrid();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult answer = MessageBox.Show(
                    "¿Está seguro de querer eliminar el registro seleccionado?", "Eliminar", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    );

                if (answer == DialogResult.Yes)
                {
                    object businessLogic = Activator.CreateInstance(this.entity);
                    ResultBM result = ((BLEntity)businessLogic).Delete(dgView.SelectedRows[0].DataBoundItem);
                    if (result.IsValid())
                    {
                        LoadDatagrid();
                    }
                    else
                    {
                        MessageBox.Show(result.description, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FrmGenericMain_Resize(object sender, EventArgs e)
        {
            this.AdjustSizes();
        }

        private void AdjustSizes()
        {
            dgView.Height = this.Height - 45;
            dgView.Width = this.Width - 115;

            cmdClose.Left = this.Width - cmdClose.Width - 30;
            cmdClose.Top = this.Height - cmdClose.Height - 45;

            cmdDelete.Left = this.Width - cmdDelete.Width - 30;
            cmdDelete.Top = cmdClose.Top - cmdDelete.Height - 5;

            cmdEdit.Left = this.Width - cmdEdit.Width - 30;
            cmdEdit.Top = cmdDelete.Top - cmdEdit.Height - 5;

            cmdNew.Left = this.Width - cmdNew.Width - 30;
            cmdNew.Top = cmdEdit.Top - cmdNew.Height - 5;
        }

    }
}
