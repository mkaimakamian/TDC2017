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
        private bool showFilter;

        public FrmGenericMain()
        {
            InitializeComponent();
        }

        public FrmGenericMain(Type entity, Type viewer=null, bool canCreate=false, bool canEdit=false, bool canDelete=false, bool showFilter=false)
        {
            InitializeComponent();
            // Entidad sobre la que se opera
            this.entity = entity;

            // Formulario a llamar en caso de creación o edición
            this.viewer = viewer;

            this.canCreate = canCreate;
            this.canEdit = canEdit;
            this.canDelete = canDelete;
            this.showFilter = showFilter;
        }

        /// <summary>
        /// Instancia un formulario / grilla y muestra los datos provistos por el tipo de entidad de negocio pasado por parámetro.
        /// </summary>
        /// <param name="entity"></param>
        //public FrmGenericMain(Type entity)
        //{
        //    InitializeComponent();
        //    this.entity = entity;
        //}

        private void FrmGenericMain_Load(object sender, EventArgs e)
        {
            SessionHelper.RegisterForTranslation(cmdNew, Codes.BTN_NEW);
            SessionHelper.RegisterForTranslation(cmdEdit, Codes.BTN_EDIT);
            SessionHelper.RegisterForTranslation(cmdDelete, Codes.BTN_DELETE);            
            SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

            cmdNew.Enabled = canCreate;
            cmdEdit.Enabled = canEdit;
            cmdDelete.Enabled = canDelete;
            flowLayout.Visible = this.showFilter;
            cmdFilter.Visible = this.showFilter;
            LoadDatagrid();
            CreateFilters(dgView);
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
            try
            {
                object businessLogic = Activator.CreateInstance(this.entity);
                ResultBM result = ((BLEntity)businessLogic).GetCollection();
                if (result.IsValid()) dgView.DataSource = result.GetValue();                    
                else MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (dgView.SelectedRows.Count == 0) return;

            //Se instancia el formulario, setteando la propiedad Entity y IsUpdate
            Form viewForm = (Form)Activator.CreateInstance(this.viewer);
            System.Reflection.PropertyInfo Entity = viewForm.GetType().GetProperty("Entity");
            Entity.SetValue(viewForm, dgView.SelectedRows[0].DataBoundItem);
            Entity = viewForm.GetType().GetProperty("IsUpdate");
            Entity.SetValue(viewForm, true);
            viewForm.ShowDialog();
            LoadDatagrid();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (dgView.SelectedRows.Count == 0) return;
            try
            {
                DialogResult answer = MessageBox.Show(
                    SessionHelper.GetTranslation("DELETE_QUESTION"), "Eliminar", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    );

                if (answer == DialogResult.Yes)
                {
                    object businessLogic = Activator.CreateInstance(this.entity);
                    ResultBM result = ((BLEntity)businessLogic).Delete(dgView.SelectedRows[0].DataBoundItem);
                    
                    if (result.IsValid()) LoadDatagrid();
                    else MessageBox.Show(result.description, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            
            int deltaH = this.showFilter? flowLayout.Height : 0;

            container.Height = this.Height;
            container.Width = this.Width - 115;

            flowLayout.Width = container.Width - 15;
            dgView.Width = container.Width - 15;
            dgView.Height = container.Height - deltaH - 50;

            cmdClose.Left = this.Width - cmdClose.Width - 30;
            cmdClose.Top = this.Height - cmdClose.Height - 45;

            cmdDelete.Left = this.Width - cmdDelete.Width - 30;
            cmdDelete.Top = cmdClose.Top - cmdDelete.Height - 5;

            cmdEdit.Left = this.Width - cmdEdit.Width - 30;
            cmdEdit.Top = cmdDelete.Top - cmdEdit.Height - 5;

            cmdNew.Left = this.Width - cmdNew.Width - 30;
            cmdNew.Top = cmdEdit.Top - cmdNew.Height - 5;

            cmdFilter.Left = this.Width - cmdFilter.Width - 30;
            cmdFilter.Top = cmdNew.Top - cmdFilter.Height - 5;
        }

        /// <summary>
        /// Crea los filtros en base a los campos que son del tipo text y del tipo fecha
        /// </summary>
        /// <param name="grid"></param>
        private void CreateFilters(DataGridView grid)
        {
            try
            {
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    if (column.GetType() == typeof(DataGridViewTextBoxColumn))
                    {
                        Control control = null;
                        if (column.ValueType == typeof(String)) control = new TextBox();
                        if (column.ValueType == typeof(DateTime)) control = new DateTimePicker();

                        if (control != null)
                        {
                            control.Tag = column.Name;
                            //Se agregan las referencia a la lista para obtener mayor control al momento del filtrado.
                            lstControls.Add(control);

                            //Se crea un grupo para poder etiquetar el componente
                            GroupBox group = new GroupBox();
                            group.Controls.Add(control);
                            group.Text = column.HeaderText;
                            group.Width = control.Width + 15;
                            group.Height = control.Height + 20;
                            control.Top = 15;
                            control.Left = 5;

                            flowLayout.Controls.Add(group);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        List<Control> lstControls = new List<Control>();

        private void cmdFilter_Click(object sender, EventArgs e)
        {
            // la estrategia consiste en tomar todos los campos de los filtros y pasárselos a la BLL

            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();

                foreach (Control control in lstControls)
                    if (control.Text.Length > 0) parameters.Add(control.Tag.ToString(), control.Text);

                object businessLogic = Activator.CreateInstance(this.entity);
                ResultBM result = ((BLEntity)businessLogic).GetCollection(parameters);

                if (result.IsValid()) dgView.DataSource = result.GetValue();
                else MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgView.Refresh();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
