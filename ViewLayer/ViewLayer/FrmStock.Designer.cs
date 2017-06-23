namespace ViewLayer
{
    partial class FrmStock
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbDonation = new System.Windows.Forms.ComboBox();
            this.cmbDepot = new System.Windows.Forms.ComboBox();
            this.lblDonation = new System.Windows.Forms.Label();
            this.lblDepot = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.numericQuantity = new System.Windows.Forms.NumericUpDown();
            this.cmbItemType = new System.Windows.Forms.ComboBox();
            this.dtDueDate = new System.Windows.Forms.DateTimePicker();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblItemQuantity = new System.Windows.Forms.Label();
            this.lblDuedate = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblQuantity);
            this.groupBox1.Controls.Add(this.lblDepot);
            this.groupBox1.Controls.Add(this.lblDonation);
            this.groupBox1.Controls.Add(this.lblLocation);
            this.groupBox1.Controls.Add(this.cmbDepot);
            this.groupBox1.Controls.Add(this.lblDuedate);
            this.groupBox1.Controls.Add(this.cmbDonation);
            this.groupBox1.Controls.Add(this.lblItemQuantity);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtLocation);
            this.groupBox1.Controls.Add(this.dtDueDate);
            this.groupBox1.Controls.Add(this.numericQuantity);
            this.groupBox1.Controls.Add(this.cmbItemType);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 304);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbDonation
            // 
            this.cmbDonation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDonation.FormattingEnabled = true;
            this.cmbDonation.Location = new System.Drawing.Point(6, 32);
            this.cmbDonation.Name = "cmbDonation";
            this.cmbDonation.Size = new System.Drawing.Size(200, 21);
            this.cmbDonation.TabIndex = 0;
            // 
            // cmbDepot
            // 
            this.cmbDepot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepot.FormattingEnabled = true;
            this.cmbDepot.Location = new System.Drawing.Point(6, 72);
            this.cmbDepot.Name = "cmbDepot";
            this.cmbDepot.Size = new System.Drawing.Size(200, 21);
            this.cmbDepot.TabIndex = 1;
            // 
            // lblDonation
            // 
            this.lblDonation.AutoSize = true;
            this.lblDonation.Location = new System.Drawing.Point(6, 16);
            this.lblDonation.Name = "lblDonation";
            this.lblDonation.Size = new System.Drawing.Size(48, 13);
            this.lblDonation.TabIndex = 3;
            this.lblDonation.Text = "donation";
            // 
            // lblDepot
            // 
            this.lblDepot.AutoSize = true;
            this.lblDepot.Location = new System.Drawing.Point(6, 56);
            this.lblDepot.Name = "lblDepot";
            this.lblDepot.Size = new System.Drawing.Size(34, 13);
            this.lblDepot.TabIndex = 4;
            this.lblDepot.Text = "depot";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(60, 16);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(44, 13);
            this.lblQuantity.TabIndex = 5;
            this.lblQuantity.Text = "quantity";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(6, 112);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 0;
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(9, 273);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(200, 20);
            this.txtLocation.TabIndex = 1;
            // 
            // numericQuantity
            // 
            this.numericQuantity.Location = new System.Drawing.Point(6, 191);
            this.numericQuantity.Name = "numericQuantity";
            this.numericQuantity.Size = new System.Drawing.Size(200, 20);
            this.numericQuantity.TabIndex = 2;
            // 
            // cmbItemType
            // 
            this.cmbItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemType.FormattingEnabled = true;
            this.cmbItemType.Location = new System.Drawing.Point(6, 151);
            this.cmbItemType.Name = "cmbItemType";
            this.cmbItemType.Size = new System.Drawing.Size(200, 21);
            this.cmbItemType.TabIndex = 3;
            this.cmbItemType.SelectedIndexChanged += new System.EventHandler(this.cmbItemType_SelectedIndexChanged);
            // 
            // dtDueDate
            // 
            this.dtDueDate.Location = new System.Drawing.Point(6, 230);
            this.dtDueDate.Name = "dtDueDate";
            this.dtDueDate.Size = new System.Drawing.Size(200, 20);
            this.dtDueDate.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 96);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(58, 13);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "description";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 135);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(46, 13);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "itemtype";
            // 
            // lblItemQuantity
            // 
            this.lblItemQuantity.AutoSize = true;
            this.lblItemQuantity.Location = new System.Drawing.Point(6, 175);
            this.lblItemQuantity.Name = "lblItemQuantity";
            this.lblItemQuantity.Size = new System.Drawing.Size(44, 13);
            this.lblItemQuantity.TabIndex = 8;
            this.lblItemQuantity.Text = "quantity";
            // 
            // lblDuedate
            // 
            this.lblDuedate.AutoSize = true;
            this.lblDuedate.Location = new System.Drawing.Point(6, 214);
            this.lblDuedate.Name = "lblDuedate";
            this.lblDuedate.Size = new System.Drawing.Size(46, 13);
            this.lblDuedate.TabIndex = 9;
            this.lblDuedate.Text = "duedate";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(9, 257);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(44, 13);
            this.lblLocation.TabIndex = 10;
            this.lblLocation.Text = "location";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 338);
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
            // FrmStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 373);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FrmStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmStock";
            this.Load += new System.EventHandler(this.FrmStock_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericQuantity)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblDepot;
        private System.Windows.Forms.Label lblDonation;
        private System.Windows.Forms.ComboBox cmbDepot;
        private System.Windows.Forms.ComboBox cmbDonation;
        private System.Windows.Forms.Label lblItemQuantity;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.DateTimePicker dtDueDate;
        private System.Windows.Forms.ComboBox cmbItemType;
        private System.Windows.Forms.NumericUpDown numericQuantity;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblDuedate;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAccept;
    }
}