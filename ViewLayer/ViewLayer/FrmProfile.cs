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

namespace ViewLayer
{
    public partial class FrmProfile : Form
    {
        public FrmProfile()
        {
            InitializeComponent();
        }

        private void FrmProfile_Load(object sender, EventArgs e)
        {
            try
            {
                ProfileBLL profileBll = new ProfileBLL();
                ResultBM result = profileBll.GetProfiles();
                if (result.IsValid())
                {
                    
                    chkListProfile.DataSource = result.GetValue<List<PermissionMDL>>();
                    chkListProfile.DisplayMember = "description";
                }
                else {
                    MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void chkListProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProfileBM selection = ((ProfileBM)((CheckedListBox)sender).SelectedItem);
                ProfileBLL profileBll = new ProfileBLL();
                ResultBM result = profileBll.GetProfile(selection.code);
                if (result.IsValid())
                {
                    treeDescription.Nodes.Clear();
                    PopulateTree(result.GetValue() as ProfileBM);                    
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

        private void PopulateTree(ProfileBM element, TreeNode fatherNode=null)
        {
            if (fatherNode == null)
            {
                //Si no posee padre, significa que es root
                fatherNode = new TreeNode(element.Description);
                treeDescription.Nodes.Add(fatherNode);
            }

            if (element.IsFather())
            {
                foreach (ProfileBM permission in element.GetChildren())
                {
                    //Creo un nodo y lo vinculo con su padre
                    TreeNode node = new TreeNode(permission.Description);
                    fatherNode.Nodes.Add(node);
                    PopulateTree(permission);
                }
            }            
        }
    }
}
