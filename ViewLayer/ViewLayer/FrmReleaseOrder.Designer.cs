namespace ViewLayer
{
    partial class FrmReleaseOrder
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
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.nbrQuantity = new System.Windows.Forms.NumericUpDown();
            this.lstStock = new System.Windows.Forms.ListBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblBeneficiary = new System.Windows.Forms.Label();
            this.cmbBeneficiary = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgRelease = new System.Windows.Forms.DataGridView();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrQuantity)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRelease)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Controls.Add(this.lblComment);
            this.groupBox1.Controls.Add(this.nbrQuantity);
            this.groupBox1.Controls.Add(this.lstStock);
            this.groupBox1.Controls.Add(this.lblStock);
            this.groupBox1.Controls.Add(this.lblQuantity);
            this.groupBox1.Controls.Add(this.lblBeneficiary);
            this.groupBox1.Controls.Add(this.cmbBeneficiary);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 304);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(6, 251);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(200, 47);
            this.txtComment.TabIndex = 26;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(6, 235);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(50, 13);
            this.lblComment.TabIndex = 25;
            this.lblComment.Text = "comment";
            // 
            // nbrQuantity
            // 
            this.nbrQuantity.Location = new System.Drawing.Point(6, 212);
            this.nbrQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbrQuantity.Name = "nbrQuantity";
            this.nbrQuantity.Size = new System.Drawing.Size(200, 20);
            this.nbrQuantity.TabIndex = 24;
            this.nbrQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lstStock
            // 
            this.lstStock.FormattingEnabled = true;
            this.lstStock.Location = new System.Drawing.Point(6, 72);
            this.lstStock.Name = "lstStock";
            this.lstStock.Size = new System.Drawing.Size(200, 121);
            this.lstStock.TabIndex = 23;
            this.lstStock.SelectedIndexChanged += new System.EventHandler(this.lstStock_SelectedIndexChanged);
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(6, 56);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(33, 13);
            this.lblStock.TabIndex = 14;
            this.lblStock.Text = "stock";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(6, 196);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(44, 13);
            this.lblQuantity.TabIndex = 16;
            this.lblQuantity.Text = "quantity";
            // 
            // lblBeneficiary
            // 
            this.lblBeneficiary.AutoSize = true;
            this.lblBeneficiary.Location = new System.Drawing.Point(6, 16);
            this.lblBeneficiary.Name = "lblBeneficiary";
            this.lblBeneficiary.Size = new System.Drawing.Size(58, 13);
            this.lblBeneficiary.TabIndex = 0;
            this.lblBeneficiary.Text = "beneficiary";
            // 
            // cmbBeneficiary
            // 
            this.cmbBeneficiary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBeneficiary.FormattingEnabled = true;
            this.cmbBeneficiary.Location = new System.Drawing.Point(6, 32);
            this.cmbBeneficiary.Name = "cmbBeneficiary";
            this.cmbBeneficiary.Size = new System.Drawing.Size(200, 21);
            this.cmbBeneficiary.TabIndex = 22;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgRelease);
            this.groupBox2.Location = new System.Drawing.Point(320, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 304);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // dgRelease
            // 
            this.dgRelease.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRelease.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgRelease.Location = new System.Drawing.Point(6, 16);
            this.dgRelease.Name = "dgRelease";
            this.dgRelease.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRelease.Size = new System.Drawing.Size(357, 282);
            this.dgRelease.TabIndex = 0;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(239, 136);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
            this.cmdAdd.TabIndex = 44;
            this.cmdAdd.Text = ">>";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(239, 177);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(75, 23);
            this.cmdRemove.TabIndex = 45;
            this.cmdRemove.Text = "<<";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 338);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 47;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Location = new System.Drawing.Point(613, 338);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(75, 23);
            this.cmdAccept.TabIndex = 46;
            this.cmdAccept.Text = "accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // FrmReleaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 373);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FrmReleaseOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmReleaseOrder";
            this.Load += new System.EventHandler(this.FrmReleaseOrder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrQuantity)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRelease)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBeneficiary;
        private System.Windows.Forms.ComboBox cmbBeneficiary;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nbrQuantity;
        private System.Windows.Forms.ListBox lstStock;
        private System.Windows.Forms.DataGridView dgRelease;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblComment;
    }
}