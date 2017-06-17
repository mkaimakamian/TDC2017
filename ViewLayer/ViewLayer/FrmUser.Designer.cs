namespace ViewLayer
{
    partial class FrmUser
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
            this.lblLanguage = new System.Windows.Forms.Label();
            this.txtPasswordCheck = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.lblPasswordConfirm = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.lblProfile = new System.Windows.Forms.Label();
            this.cmbProfile = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(12, 95);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(51, 13);
            this.lblLanguage.TabIndex = 17;
            this.lblLanguage.Text = "language";
            // 
            // txtPasswordCheck
            // 
            this.txtPasswordCheck.Location = new System.Drawing.Point(142, 71);
            this.txtPasswordCheck.Name = "txtPasswordCheck";
            this.txtPasswordCheck.Size = new System.Drawing.Size(100, 20);
            this.txtPasswordCheck.TabIndex = 16;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(12, 72);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 15;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 14;
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(12, 111);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(230, 21);
            this.cmbLanguage.TabIndex = 13;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(15, 179);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(62, 17);
            this.chkIsActive.TabIndex = 12;
            this.chkIsActive.Text = "isactive";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // lblPasswordConfirm
            // 
            this.lblPasswordConfirm.AutoSize = true;
            this.lblPasswordConfirm.Location = new System.Drawing.Point(139, 55);
            this.lblPasswordConfirm.Name = "lblPasswordConfirm";
            this.lblPasswordConfirm.Size = new System.Drawing.Size(82, 13);
            this.lblPasswordConfirm.TabIndex = 11;
            this.lblPasswordConfirm.Text = "passwordcheck";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 55);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(52, 13);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "password";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(33, 13);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "name";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 233);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 18;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Location = new System.Drawing.Point(167, 233);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(75, 23);
            this.cmdAccept.TabIndex = 19;
            this.cmdAccept.Text = "accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Location = new System.Drawing.Point(12, 135);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(35, 13);
            this.lblProfile.TabIndex = 20;
            this.lblProfile.Text = "profile";
            // 
            // cmbProfile
            // 
            this.cmbProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfile.FormattingEnabled = true;
            this.cmbProfile.Location = new System.Drawing.Point(12, 152);
            this.cmbProfile.Name = "cmbProfile";
            this.cmbProfile.Size = new System.Drawing.Size(230, 21);
            this.cmbProfile.TabIndex = 21;
            // 
            // FrmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 263);
            this.Controls.Add(this.cmbProfile);
            this.Controls.Add(this.lblProfile);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.txtPasswordCheck);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmbLanguage);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.lblPasswordConfirm);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblName);
            this.MaximizeBox = false;
            this.Name = "FrmUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmUsers";
            this.Load += new System.EventHandler(this.FrmUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.TextBox txtPasswordCheck;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label lblPasswordConfirm;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.ComboBox cmbProfile;
    }
}