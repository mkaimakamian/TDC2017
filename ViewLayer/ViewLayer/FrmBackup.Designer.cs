namespace ViewLayer
{
    partial class FrmBackup
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
            this.cmdBackup = new System.Windows.Forms.Button();
            this.cmdrestore = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.lblBackup = new System.Windows.Forms.Label();
            this.lblRestore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdBackup
            // 
            this.cmdBackup.Location = new System.Drawing.Point(12, 31);
            this.cmdBackup.Name = "cmdBackup";
            this.cmdBackup.Size = new System.Drawing.Size(75, 23);
            this.cmdBackup.TabIndex = 0;
            this.cmdBackup.Text = "backup";
            this.cmdBackup.UseVisualStyleBackColor = true;
            this.cmdBackup.Click += new System.EventHandler(this.cmdBackup_Click);
            // 
            // cmdrestore
            // 
            this.cmdrestore.Location = new System.Drawing.Point(12, 80);
            this.cmdrestore.Name = "cmdrestore";
            this.cmdrestore.Size = new System.Drawing.Size(75, 23);
            this.cmdrestore.TabIndex = 1;
            this.cmdrestore.Text = "restore";
            this.cmdrestore.UseVisualStyleBackColor = true;
            this.cmdrestore.Click += new System.EventHandler(this.cmdrestore_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 126);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            // 
            // lblBackup
            // 
            this.lblBackup.AutoSize = true;
            this.lblBackup.Location = new System.Drawing.Point(104, 36);
            this.lblBackup.Name = "lblBackup";
            this.lblBackup.Size = new System.Drawing.Size(19, 13);
            this.lblBackup.TabIndex = 3;
            this.lblBackup.Text = "***";
            // 
            // lblRestore
            // 
            this.lblRestore.AutoSize = true;
            this.lblRestore.Location = new System.Drawing.Point(104, 85);
            this.lblRestore.Name = "lblRestore";
            this.lblRestore.Size = new System.Drawing.Size(19, 13);
            this.lblRestore.TabIndex = 4;
            this.lblRestore.Text = "***";
            // 
            // FrmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 161);
            this.Controls.Add(this.lblRestore);
            this.Controls.Add(this.lblBackup);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdrestore);
            this.Controls.Add(this.cmdBackup);
            this.MaximizeBox = false;
            this.Name = "FrmBackup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmBackup";
            this.Load += new System.EventHandler(this.FrmBackup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdBackup;
        private System.Windows.Forms.Button cmdrestore;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Label lblBackup;
        private System.Windows.Forms.Label lblRestore;
    }
}