namespace ViewLayer
{
    partial class FrmLoginMain
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
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.BtnAccess = new System.Windows.Forms.Button();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.TxtUser = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(9, 48);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(61, 13);
            this.Label2.TabIndex = 9;
            this.Label2.Text = "Contraseña";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(9, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(43, 13);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Usuario";
            // 
            // BtnAccess
            // 
            this.BtnAccess.Location = new System.Drawing.Point(172, 62);
            this.BtnAccess.Name = "BtnAccess";
            this.BtnAccess.Size = new System.Drawing.Size(75, 23);
            this.BtnAccess.TabIndex = 7;
            this.BtnAccess.Text = "Acceder";
            this.BtnAccess.UseVisualStyleBackColor = true;
            this.BtnAccess.Click += new System.EventHandler(this.BtnAccess_Click);
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(12, 64);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = 'x';
            this.TxtPassword.Size = new System.Drawing.Size(154, 20);
            this.TxtPassword.TabIndex = 6;
            this.TxtPassword.Text = "Admin";
            // 
            // TxtUser
            // 
            this.TxtUser.Location = new System.Drawing.Point(12, 25);
            this.TxtUser.Name = "TxtUser";
            this.TxtUser.Size = new System.Drawing.Size(154, 20);
            this.TxtUser.TabIndex = 5;
            this.TxtUser.Text = "Admin";
            // 
            // FrmLoginMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 98);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.BtnAccess);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.TxtUser);
            this.Name = "FrmLoginMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button BtnAccess;
        internal System.Windows.Forms.TextBox TxtPassword;
        internal System.Windows.Forms.TextBox TxtUser;
    }
}