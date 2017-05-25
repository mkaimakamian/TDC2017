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

        private ProfileBM entity;
        private bool isUpdate = false;
        
        public ProfileBM Entity
        {
            get { return this.entity; }
            set { this.entity = value;}
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value;}
        }


        //ProfileBM root_profile;

        public FrmProfile()
        {
            InitializeComponent();
        }

        private void FrmProfile_Load(object sender, EventArgs e)
        {
            try
            {
                // Se recuperan los permisos (root) y se llena la lista
                ProfileBLL profileBll = new ProfileBLL();
                ResultBM result = profileBll.GetSystemPermissions();
                
                if (result.IsValid())
                {
                    chkListProfile.DataSource = result.GetValue<List<PermissionMDL>>();
                    chkListProfile.DisplayMember = "description";
                }
                else {
                    MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                //En case de ser nuevo, se requiere contar con un permiso "nodo" que facilita su muestra por pantalla y al momento de guardarlo.
                //Si se trata de una actualización, se debe recuperar el listado con los permisos y sus exclusiones.
                if (!this.isUpdate)
                {
                    //Si se quiere dar de alta un nuevo permiso, se genera un root para mantener ordenado los permisos que se agreguen
                    this.Entity = new PermissionsMDL(null, GenCode(5), "descripcion", false);
                }
                else
                {
                    txtDescription.Text = this.entity.Description;
                    ProfileBM profile =  GetPermissionHierarchy(this.Entity);
                    PopulateTree(treeProfile, profile);
                    treeProfile.ExpandAll();
                    //chkListProfile.SetItemCheckState(0, CheckState.Checked);
                }
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
                //Se le asigna el valor el entity para tenerlo disponible al momento de guardado
                ProfileBM selection = (ProfileBM)((CheckedListBox)sender).SelectedItem;
                ProfileBM result = GetPermissionHierarchy(selection);
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
                this.Entity.Description = txtDescription.Text;

                if (e.NewValue == CheckState.Checked)
                {
                    //Agrega a la lista de elegidos, los permisos seleccionados del listado de permisos disponibles
                    ProfileBM selection = (ProfileBM)((CheckedListBox)sender).SelectedItem;
                    ProfileBM result = GetPermissionHierarchy(selection);
                    this.Entity.AddPermission(result);                                        
                }
                else
                {
                    //Elimina del listado de permisos a agregar, aquel que coincida con el código
                    ProfileBM selection = (ProfileBM)((CheckedListBox)sender).SelectedItem;
                    this.Entity.DeletePermission(selection.code);
                }

                PopulateTree(treeProfile, this.Entity);
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
                //se utiliza el tag para almacenar el permiso
                fatherNode.Tag = element;
                component.Nodes.Add(fatherNode);
                fatherNode.Checked = true;
            }

            if (element.IsFather())
            {
                foreach (ProfileBM permission in element.GetChildren())
                {
                    //Creo un nodo y lo vinculo con su padre
                    TreeNode childNode = new TreeNode(permission.Description);
                    fatherNode.Nodes.Add(childNode);
                    //se utiliza el tag para almacenar el permiso
                    childNode.Tag = permission;
                    childNode.Checked = !permission.excluded;
                    PopulateTree(component, permission, childNode);
                }
            }            
        }

        /// <summary>
        /// Devuelve la jerarquía de permisos para el elemento seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private ProfileBM GetPermissionHierarchy(ProfileBM selection)
        {
            ProfileBLL profileBll = new ProfileBLL();
            ResultBM result = profileBll.GetProfile(selection.code);

            if (!result.IsValid())
                MessageBox.Show(result.description, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            return result.GetValue() as ProfileBM;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                ProfileBLL profileBll = new ProfileBLL();

                if (isUpdate)
                {
                    profileBll.UpdateProfile(this.Entity);                    
                }
                else
                {
                    profileBll.CreateProfile(this.Entity);
                }

                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Crea una cadena alfanumérica para crear los ids de los permisos.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GenCode(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void treeProfile_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //Si se chequea, e sporque se quiere
            ProfileBM profile = e.Node.Tag as ProfileBM;
            profile.excluded = !e.Node.Checked;
        }
    }
}
