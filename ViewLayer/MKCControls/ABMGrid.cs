using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MKCControls
{
    public partial class ABMGrid : UserControl
    {
        public ABMGrid()
        {
            InitializeComponent();
        }

        private void ABMGrid_Load(object sender, EventArgs e)
        {
        }

        private void ABMGrid_Resize(object sender, EventArgs e)
        {
            MKCGrid.Height = this.Height;
            MKCGrid.Width = this.Width - 100;
            cmdNew.Left = MKCGrid.Width + 12;
            cmdEdit.Left = MKCGrid.Width + 12;
            cmdDelete.Left = MKCGrid.Width + 12;
            cmdClose.Left = MKCGrid.Width + 12;
        }

        public object DataSource
        {
            get { return MKCGrid.DataSource; }
            set { MKCGrid.DataSource = value; }
        }

        public DataGridViewColumnCollection Columns
        {
            get { return MKCGrid.Columns; }            
        }
      
    }
}
