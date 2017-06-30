namespace ViewLayer
{
    partial class FrmDonation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkPickup = new System.Windows.Forms.CheckBox();
            this.cmbVolunteer = new System.Windows.Forms.ComboBox();
            this.lblResponsible = new System.Windows.Forms.Label();
            this.dateArrival = new System.Windows.Forms.DateTimePicker();
            this.lblArrival = new System.Windows.Forms.Label();
            this.lblLotId = new System.Windows.Forms.Label();
            this.lblLot = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.cmbDonor = new System.Windows.Forms.ComboBox();
            this.lblDonor = new System.Windows.Forms.Label();
            this.lblItems = new System.Windows.Forms.Label();
            this.numericItems = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericItems)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(13, 338);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 40;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Location = new System.Drawing.Point(158, 338);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(75, 23);
            this.cmdAccept.TabIndex = 39;
            this.cmdAccept.Text = "accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPickup);
            this.groupBox1.Controls.Add(this.cmbVolunteer);
            this.groupBox1.Controls.Add(this.lblResponsible);
            this.groupBox1.Controls.Add(this.dateArrival);
            this.groupBox1.Controls.Add(this.lblArrival);
            this.groupBox1.Controls.Add(this.lblLotId);
            this.groupBox1.Controls.Add(this.lblLot);
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Controls.Add(this.lblComment);
            this.groupBox1.Controls.Add(this.cmbDonor);
            this.groupBox1.Controls.Add(this.lblDonor);
            this.groupBox1.Controls.Add(this.lblItems);
            this.groupBox1.Controls.Add(this.numericItems);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 304);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            // 
            // chkPickup
            // 
            this.chkPickup.AutoSize = true;
            this.chkPickup.Location = new System.Drawing.Point(137, 65);
            this.chkPickup.Name = "chkPickup";
            this.chkPickup.Size = new System.Drawing.Size(58, 17);
            this.chkPickup.TabIndex = 54;
            this.chkPickup.Text = "pickup";
            this.chkPickup.UseVisualStyleBackColor = true;
            this.chkPickup.CheckedChanged += new System.EventHandler(this.chkPickup_CheckedChanged);
            // 
            // cmbVolunteer
            // 
            this.cmbVolunteer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVolunteer.FormattingEnabled = true;
            this.cmbVolunteer.Location = new System.Drawing.Point(6, 101);
            this.cmbVolunteer.Name = "cmbVolunteer";
            this.cmbVolunteer.Size = new System.Drawing.Size(200, 21);
            this.cmbVolunteer.TabIndex = 53;
            // 
            // lblResponsible
            // 
            this.lblResponsible.AutoSize = true;
            this.lblResponsible.Location = new System.Drawing.Point(6, 85);
            this.lblResponsible.Name = "lblResponsible";
            this.lblResponsible.Size = new System.Drawing.Size(60, 13);
            this.lblResponsible.TabIndex = 52;
            this.lblResponsible.Text = "responsible";
            // 
            // dateArrival
            // 
            this.dateArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateArrival.Location = new System.Drawing.Point(6, 62);
            this.dateArrival.Name = "dateArrival";
            this.dateArrival.Size = new System.Drawing.Size(125, 20);
            this.dateArrival.TabIndex = 51;
            // 
            // lblArrival
            // 
            this.lblArrival.AutoSize = true;
            this.lblArrival.Location = new System.Drawing.Point(6, 46);
            this.lblArrival.Name = "lblArrival";
            this.lblArrival.Size = new System.Drawing.Size(35, 13);
            this.lblArrival.TabIndex = 50;
            this.lblArrival.Text = "arrival";
            // 
            // lblLotId
            // 
            this.lblLotId.AutoSize = true;
            this.lblLotId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotId.Location = new System.Drawing.Point(6, 29);
            this.lblLotId.Name = "lblLotId";
            this.lblLotId.Size = new System.Drawing.Size(19, 13);
            this.lblLotId.TabIndex = 49;
            this.lblLotId.Text = "...";
            // 
            // lblLot
            // 
            this.lblLot.AutoSize = true;
            this.lblLot.Location = new System.Drawing.Point(6, 16);
            this.lblLot.Name = "lblLot";
            this.lblLot.Size = new System.Drawing.Size(18, 13);
            this.lblLot.TabIndex = 48;
            this.lblLot.Text = "lot";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(6, 220);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(200, 78);
            this.txtComment.TabIndex = 47;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(6, 204);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(50, 13);
            this.lblComment.TabIndex = 46;
            this.lblComment.Text = "comment";
            // 
            // cmbDonor
            // 
            this.cmbDonor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDonor.FormattingEnabled = true;
            this.cmbDonor.Location = new System.Drawing.Point(6, 141);
            this.cmbDonor.Name = "cmbDonor";
            this.cmbDonor.Size = new System.Drawing.Size(200, 21);
            this.cmbDonor.TabIndex = 45;
            this.cmbDonor.SelectedIndexChanged += new System.EventHandler(this.cmbDonor_SelectedIndexChanged);
            // 
            // lblDonor
            // 
            this.lblDonor.AutoSize = true;
            this.lblDonor.Location = new System.Drawing.Point(6, 125);
            this.lblDonor.Name = "lblDonor";
            this.lblDonor.Size = new System.Drawing.Size(34, 13);
            this.lblDonor.TabIndex = 44;
            this.lblDonor.Text = "donor";
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Location = new System.Drawing.Point(6, 165);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(31, 13);
            this.lblItems.TabIndex = 43;
            this.lblItems.Text = "items";
            // 
            // numericItems
            // 
            this.numericItems.Location = new System.Drawing.Point(6, 181);
            this.numericItems.Name = "numericItems";
            this.numericItems.Size = new System.Drawing.Size(200, 20);
            this.numericItems.TabIndex = 42;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtContact);
            this.groupBox2.Controls.Add(this.lblContact);
            this.groupBox2.Location = new System.Drawing.Point(249, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 304);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            // 
            // txtContact
            // 
            this.txtContact.Enabled = false;
            this.txtContact.Location = new System.Drawing.Point(7, 33);
            this.txtContact.Multiline = true;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(200, 265);
            this.txtContact.TabIndex = 1;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(7, 16);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(43, 13);
            this.lblContact.TabIndex = 0;
            this.lblContact.Text = "contact";
            // 
            // FrmDonation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 373);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAccept);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDonation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DonationFrm";
            this.Load += new System.EventHandler(this.DonationFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericItems)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbDonor;
        private System.Windows.Forms.Label lblDonor;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.NumericUpDown numericItems;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblLotId;
        private System.Windows.Forms.Label lblLot;
        private System.Windows.Forms.DateTimePicker dateArrival;
        private System.Windows.Forms.Label lblArrival;
        private System.Windows.Forms.ComboBox cmbVolunteer;
        private System.Windows.Forms.Label lblResponsible;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.CheckBox chkPickup;
    }
}