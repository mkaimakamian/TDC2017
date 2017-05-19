using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewLayer
{
    public partial class AcceptCancel : UserControl
    {
        public event EventHandler ClickCancel;
        public event EventHandler ClickAccept;

        public AcceptCancel()
        {
            InitializeComponent();
        }

        private void AcceptCancel_Resize(object sender, EventArgs e)
        {
            cmdAccept.Left = this.Width - cmdAccept.Width;
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            ClickAccept(sender, e);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            ClickCancel(sender, e);
        }
    }
}
