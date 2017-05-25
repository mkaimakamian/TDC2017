namespace ViewLayer
{
    partial class FrmProfile
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
            this.treeDescription = new System.Windows.Forms.TreeView();
            this.chkListProfile = new System.Windows.Forms.CheckedListBox();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.treeProfile = new System.Windows.Forms.TreeView();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // treeDescription
            // 
            this.treeDescription.Location = new System.Drawing.Point(206, 54);
            this.treeDescription.Name = "treeDescription";
            this.treeDescription.Size = new System.Drawing.Size(237, 215);
            this.treeDescription.TabIndex = 1;
            // 
            // chkListProfile
            // 
            this.chkListProfile.FormattingEnabled = true;
            this.chkListProfile.Location = new System.Drawing.Point(21, 54);
            this.chkListProfile.Name = "chkListProfile";
            this.chkListProfile.Size = new System.Drawing.Size(179, 214);
            this.chkListProfile.TabIndex = 2;
            this.chkListProfile.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkListProfile_ItemCheck);
            this.chkListProfile.SelectedIndexChanged += new System.EventHandler(this.chkListProfile_SelectedIndexChanged);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Location = new System.Drawing.Point(700, 357);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(75, 23);
            this.cmdAccept.TabIndex = 3;
            this.cmdAccept.Text = "accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // treeProfile
            // 
            this.treeProfile.CheckBoxes = true;
            this.treeProfile.Location = new System.Drawing.Point(468, 80);
            this.treeProfile.Name = "treeProfile";
            this.treeProfile.Size = new System.Drawing.Size(256, 189);
            this.treeProfile.TabIndex = 4;
            this.treeProfile.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeProfile_AfterCheck);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(468, 54);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(256, 20);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 357);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // FrmProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 392);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.treeProfile);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.chkListProfile);
            this.Controls.Add(this.treeDescription);
            this.Name = "FrmProfile";
            this.Text = "FrmProfile";
            this.Load += new System.EventHandler(this.FrmProfile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDescription;
        private System.Windows.Forms.CheckedListBox chkListProfile;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.TreeView treeProfile;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button cmdClose;
    }
}