namespace ViewLayer
{
    partial class FrmIntegrity
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
            this.lblIntegrity = new System.Windows.Forms.Label();
            this.cmdIntegrity = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 156);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 0;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblIntegrity
            // 
            this.lblIntegrity.Location = new System.Drawing.Point(12, 9);
            this.lblIntegrity.Name = "lblIntegrity";
            this.lblIntegrity.Size = new System.Drawing.Size(181, 55);
            this.lblIntegrity.TabIndex = 1;
            this.lblIntegrity.Text = "label1";
            // 
            // cmdIntegrity
            // 
            this.cmdIntegrity.Location = new System.Drawing.Point(52, 90);
            this.cmdIntegrity.Name = "cmdIntegrity";
            this.cmdIntegrity.Size = new System.Drawing.Size(101, 23);
            this.cmdIntegrity.TabIndex = 2;
            this.cmdIntegrity.Text = "checkIntegrity";
            this.cmdIntegrity.UseVisualStyleBackColor = true;
            this.cmdIntegrity.Click += new System.EventHandler(this.cmdIntegrity_Click);
            // 
            // FrmIntegrity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(210, 191);
            this.Controls.Add(this.cmdIntegrity);
            this.Controls.Add(this.lblIntegrity);
            this.Controls.Add(this.cmdClose);
            this.MaximizeBox = false;
            this.Name = "FrmIntegrity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmIntegrity";
            this.Load += new System.EventHandler(this.FrmIntegrity_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label lblIntegrity;
        private System.Windows.Forms.Button cmdIntegrity;
    }
}