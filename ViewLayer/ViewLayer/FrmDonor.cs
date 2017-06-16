using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessModel;

namespace ViewLayer
{
    public partial class FrmDonor : Form
    {
        private DonorBM entity;
        private bool isUpdate = false;

        public DonorBM Entity
        {
            get { return this.entity; }
            set { this.entity = value; }
        }

        public Boolean IsUpdate
        {
            get { return this.isUpdate; }
            set { this.isUpdate = value; }
        }

        public FrmDonor()
        {
            InitializeComponent();
        }

        private void FrmDonor_Load(object sender, EventArgs e)
        {

        }
    }
}
