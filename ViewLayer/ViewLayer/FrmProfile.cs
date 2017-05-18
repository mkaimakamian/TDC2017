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
        ProfileBM root_profile;

        public FrmProfile()
        {
            InitializeComponent();
        }

        private void FrmProfile_Load(object sender, EventArgs e)
        {
            try
            {
                // Se recuperan los permisos y se llena la lista
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

                //Se crea un nodo padre para mantener ordenado el árbol de permisos que el usuario dará de alta
                root_profile = new PermissionsMDL(null, "CODE", "descripcion");
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void chkListProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Muestra la composición jerárquica del permiso seleccionado
            try
            {
                ProfileBM result = GetPermissionHierarchy(sender);
                treeDescription.Nodes.Clear();
                PopulateTree(treeDescription, result);
                treeDescription.ExpandAll();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void chkListProfile_ItemCheck(object sender, ItemCheckEventArgs e)
        {            
            try
            {
                treeProfile.Nodes.Clear();
                root_profile.Description = txtDescription.Text;

                if (e.NewValue == CheckState.Checked)
                {
                    //Agrega a la lista de elegidos, los permisos seleccionados del listado de permisos disponibles
                    ProfileBM result = GetPermissionHierarchy(sender);
                    root_profile.AddPermission(result);                                        
                }
                else
                {
                    //Elimina del listado de permisos a agregar, aquel que coincida con el código
                    ProfileBM selection = (ProfileBM)((CheckedListBox)sender).SelectedItem;
                    root_profile.DeletePermission(selection.code);
                }

                PopulateTree(treeProfile, root_profile);
                treeProfile.ExpandAll();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PopulateTree(TreeView component, ProfileBM element, TreeNode fatherNode=null)
        {
            if (fatherNode == null)
            {
                //Si no posee padre, significa que es root
                fatherNode = new TreeNode(element.Description);
                component.Nodes.Add(fatherNode);
            }

            if (element.IsFather())
            {
                foreach (ProfileBM permission in element.GetChildren())
                {
                    //Creo un nodo y lo vinculo con su padre
                    TreeNode childNode = new TreeNode(permission.Description);
                    fatherNode.Nodes.Add(childNode);
                    PopulateTree(component, permission, childNode);
                }
            }            
        }

        private ProfileBM GetPermissionHierarchy(object sender)
        {
            ProfileBM selection = (ProfileBM)((CheckedListBox)sender).SelectedItem;
            ProfileBLL profileBll = new ProfileBLL();
            ResultBM result = profileBll.GetProfile(selection.code);

            if (!result.IsValid())
            {
                MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return result.GetValue() as ProfileBM;
        }
    }
}
