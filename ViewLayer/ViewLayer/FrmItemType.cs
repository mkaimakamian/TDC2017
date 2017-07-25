using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using BusinessModel;
using BusinessLogicLayer;

namespace ViewLayer
{
    public partial class FrmItemType : Form
    {
        private ItemTypeBM entity;
        private bool isUpdate = false;

        public ItemTypeBM Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }

        public FrmItemType()
        {
            InitializeComponent();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmItemType_Load(object sender, EventArgs e)
        {
            try
            {
                //Traducciones
                SessionHelper.RegisterForTranslation(this, Codes.MNU_GE012);
                SessionHelper.RegisterForTranslation(cmdAccept, Codes.BTN_ACCEPT);
                SessionHelper.RegisterForTranslation(cmdClose, Codes.BTN_CLOSE);

                SessionHelper.RegisterForTranslation(lblName, Codes.LBL_NAME);
                SessionHelper.RegisterForTranslation(lblComment, Codes.LBL_OBSERVATION);
                SessionHelper.RegisterForTranslation(grpCategory, Codes.LBL_CATEGORY);

                SessionHelper.RegisterForTranslation(rbEdible, Codes.LBL_EDIBLE);
                SessionHelper.RegisterForTranslation(rbIndumentary, Codes.LBL_INDUMENTARY);
                SessionHelper.RegisterForTranslation(rbMedicine, Codes.LBL_MEDICINE);
                SessionHelper.RegisterForTranslation(rbConstruction, Codes.LBL_CONSTRUCTION);
                SessionHelper.RegisterForTranslation(rbOther, Codes.LBL_OTHER);
                SessionHelper.RegisterForTranslation(checkPerishable, Codes.LBL_PERISHABLE);
                
                //No es elegante pero si es sencilla
                rbEdible.Tag = ItemTypeBM.Category.EDIBLE;
                rbIndumentary.Tag = ItemTypeBM.Category.INDUMENTARY;
                rbMedicine.Tag = ItemTypeBM.Category.MEDICINE;
                rbConstruction.Tag = ItemTypeBM.Category.CONSTRUCTION;
                rbOther.Tag = ItemTypeBM.Category.OTHER;

                if (this.IsUpdate)
                {
                    ItemTypeBLL itemTypeBll = new ItemTypeBLL();
                    ResultBM itemTypeResult = itemTypeBll.GetItemType(this.Entity.id);

                    if (!itemTypeResult.IsValid()) MessageBox.Show(itemTypeResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    this.Entity = itemTypeResult.GetValue<ItemTypeBM>();
                    txtName.Text = this.Entity.Name;
                    txtComment.Text = this.Entity.Comment;
                    checkPerishable.Checked = this.Entity.Perishable;
                    rbEdible.Checked = this.Entity.IsEdible();
                    rbIndumentary.Checked = this.Entity.IsIndumentary();
                    rbMedicine.Checked = this.Entity.IsMedicine();
                    rbConstruction.Checked = this.Entity.IsConstruction();
                    rbOther.Checked = this.Entity.IsOther();
                    
                }
                else
                {
                    this.Entity = new ItemTypeBM();
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

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            DialogResult pressed = MessageBox.Show(SessionHelper.GetTranslation("SAVE_CHANGES_QUESTION"), "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pressed == DialogResult.No) return;

            try
            {
                ItemTypeBLL itemTypeBll = new ItemTypeBLL();
                ResultBM itemTypeResult = null;

                this.Entity.Name = txtName.Text;
                this.Entity.Comment = txtComment.Text;
                this.Entity.Perishable = checkPerishable.Checked;

                //Horrible
                foreach (RadioButton rb in grpCategory.Controls.OfType<RadioButton>())
                {
                    if (rb.Checked)
                    {
                        this.Entity.category = rb.Tag.ToString();
                        break;
                    }
                }
                
                if (isUpdate) itemTypeResult = itemTypeBll.UpdateItemType(this.Entity);
                else itemTypeResult = itemTypeBll.SaveItemType(this.Entity);

                if (itemTypeResult.IsValid()) Close();
                else MessageBox.Show(itemTypeResult.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
