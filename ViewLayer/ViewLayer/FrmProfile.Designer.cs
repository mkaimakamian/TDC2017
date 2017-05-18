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
            this.button1 = new System.Windows.Forms.Button();
            this.treeProfile = new System.Windows.Forms.TreeView();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // treeDescription
            // 
            this.treeDescription.Location = new System.Drawing.Point(225, 77);
            this.treeDescription.Name = "treeDescription";
            this.treeDescription.Size = new System.Drawing.Size(313, 215);
            this.treeDescription.TabIndex = 1;
            // 
            // chkListProfile
            // 
            this.chkListProfile.FormattingEnabled = true;
            this.chkListProfile.Location = new System.Drawing.Point(40, 77);
            this.chkListProfile.Name = "chkListProfile";
            this.chkListProfile.Size = new System.Drawing.Size(179, 214);
            this.chkListProfile.TabIndex = 2;
            this.chkListProfile.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkListProfile_ItemCheck);
            this.chkListProfile.SelectedIndexChanged += new System.EventHandler(this.chkListProfile_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(597, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // treeProfile
            // 
            this.treeProfile.CheckBoxes = true;
            this.treeProfile.Location = new System.Drawing.Point(225, 298);
            this.treeProfile.Name = "treeProfile";
            this.treeProfile.Size = new System.Drawing.Size(313, 96);
            this.treeProfile.TabIndex = 4;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(40, 298);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(100, 20);
            this.txtDescription.TabIndex = 5;
            // 
            // FrmProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 498);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.treeProfile);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeProfile;
        private System.Windows.Forms.TextBox txtDescription;
    }
}