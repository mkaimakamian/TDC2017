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
            this.SuspendLayout();
            // 
            // treeDescription
            // 
            this.treeDescription.Location = new System.Drawing.Point(224, 50);
            this.treeDescription.Name = "treeDescription";
            this.treeDescription.Size = new System.Drawing.Size(313, 215);
            this.treeDescription.TabIndex = 1;
            // 
            // chkListProfile
            // 
            this.chkListProfile.FormattingEnabled = true;
            this.chkListProfile.Location = new System.Drawing.Point(39, 50);
            this.chkListProfile.Name = "chkListProfile";
            this.chkListProfile.Size = new System.Drawing.Size(179, 214);
            this.chkListProfile.TabIndex = 2;
            this.chkListProfile.SelectedIndexChanged += new System.EventHandler(this.chkListProfile_SelectedIndexChanged);
            // 
            // FrmProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 498);
            this.Controls.Add(this.chkListProfile);
            this.Controls.Add(this.treeDescription);
            this.Name = "FrmProfile";
            this.Text = "FrmProfile";
            this.Load += new System.EventHandler(this.FrmProfile_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeDescription;
        private System.Windows.Forms.CheckedListBox chkListProfile;
    }
}