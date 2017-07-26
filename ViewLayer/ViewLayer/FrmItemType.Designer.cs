namespace ViewLayer
{
    partial class FrmItemType
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
            this.grpCategory = new System.Windows.Forms.GroupBox();
            this.checkPerishable = new System.Windows.Forms.CheckBox();
            this.rbMedicine = new System.Windows.Forms.RadioButton();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.rbEdible = new System.Windows.Forms.RadioButton();
            this.rbConstruction = new System.Windows.Forms.RadioButton();
            this.rbIndumentary = new System.Windows.Forms.RadioButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Controls.Add(this.lblComment);
            this.groupBox1.Controls.Add(this.grpCategory);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 304);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(6, 173);
            this.txtComment.MaxLength = 200;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(200, 125);
            this.txtComment.TabIndex = 9;
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(7, 157);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(50, 13);
            this.lblComment.TabIndex = 8;
            this.lblComment.Text = "comment";
            // 
            // grpCategory
            // 
            this.grpCategory.Controls.Add(this.checkPerishable);
            this.grpCategory.Controls.Add(this.rbMedicine);
            this.grpCategory.Controls.Add(this.rbOther);
            this.grpCategory.Controls.Add(this.rbEdible);
            this.grpCategory.Controls.Add(this.rbConstruction);
            this.grpCategory.Controls.Add(this.rbIndumentary);
            this.grpCategory.Location = new System.Drawing.Point(6, 62);
            this.grpCategory.Name = "grpCategory";
            this.grpCategory.Size = new System.Drawing.Size(200, 92);
            this.grpCategory.TabIndex = 7;
            this.grpCategory.TabStop = false;
            this.grpCategory.Text = "itemCategory";
            // 
            // checkPerishable
            // 
            this.checkPerishable.AutoSize = true;
            this.checkPerishable.Location = new System.Drawing.Point(109, 65);
            this.checkPerishable.Name = "checkPerishable";
            this.checkPerishable.Size = new System.Drawing.Size(74, 17);
            this.checkPerishable.TabIndex = 7;
            this.checkPerishable.Text = "perishable";
            this.checkPerishable.UseVisualStyleBackColor = true;
            // 
            // rbMedicine
            // 
            this.rbMedicine.AutoSize = true;
            this.rbMedicine.Location = new System.Drawing.Point(6, 65);
            this.rbMedicine.Name = "rbMedicine";
            this.rbMedicine.Size = new System.Drawing.Size(85, 17);
            this.rbMedicine.TabIndex = 4;
            this.rbMedicine.TabStop = true;
            this.rbMedicine.Text = "radioButton3";
            this.rbMedicine.UseVisualStyleBackColor = true;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Checked = true;
            this.rbOther.Location = new System.Drawing.Point(109, 42);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(85, 17);
            this.rbOther.TabIndex = 6;
            this.rbOther.TabStop = true;
            this.rbOther.Text = "radioButton5";
            this.rbOther.UseVisualStyleBackColor = true;
            // 
            // rbEdible
            // 
            this.rbEdible.AutoSize = true;
            this.rbEdible.Location = new System.Drawing.Point(6, 19);
            this.rbEdible.Name = "rbEdible";
            this.rbEdible.Size = new System.Drawing.Size(85, 17);
            this.rbEdible.TabIndex = 2;
            this.rbEdible.TabStop = true;
            this.rbEdible.Text = "radioButton1";
            this.rbEdible.UseVisualStyleBackColor = true;
            // 
            // rbConstruction
            // 
            this.rbConstruction.AutoSize = true;
            this.rbConstruction.Location = new System.Drawing.Point(109, 19);
            this.rbConstruction.Name = "rbConstruction";
            this.rbConstruction.Size = new System.Drawing.Size(85, 17);
            this.rbConstruction.TabIndex = 5;
            this.rbConstruction.TabStop = true;
            this.rbConstruction.Text = "radioButton4";
            this.rbConstruction.UseVisualStyleBackColor = true;
            this.rbConstruction.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // rbIndumentary
            // 
            this.rbIndumentary.AutoSize = true;
            this.rbIndumentary.Location = new System.Drawing.Point(6, 42);
            this.rbIndumentary.Name = "rbIndumentary";
            this.rbIndumentary.Size = new System.Drawing.Size(85, 17);
            this.rbIndumentary.TabIndex = 3;
            this.rbIndumentary.TabStop = true;
            this.rbIndumentary.Text = "radioButton2";
            this.rbIndumentary.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(6, 36);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(7, 20);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(33, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "name";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(13, 338);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 42;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Location = new System.Drawing.Point(158, 338);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(75, 23);
            this.cmdAccept.TabIndex = 41;
            this.cmdAccept.Text = "accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // FrmItemType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 373);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmItemType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmItemType";
            this.Load += new System.EventHandler(this.FrmItemType_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpCategory.ResumeLayout(false);
            this.grpCategory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbConstruction;
        private System.Windows.Forms.RadioButton rbMedicine;
        private System.Windows.Forms.RadioButton rbIndumentary;
        private System.Windows.Forms.RadioButton rbEdible;
        private System.Windows.Forms.GroupBox grpCategory;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.CheckBox checkPerishable;
        private System.Windows.Forms.TextBox txtComment;
    }
}