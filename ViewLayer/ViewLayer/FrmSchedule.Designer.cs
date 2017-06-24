namespace ViewLayer
{
    partial class FrmSchedule
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
            this.lblDonor = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lblDonation = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblVisit = new System.Windows.Forms.Label();
            this.dtVisit = new System.Windows.Forms.DateTimePicker();
            this.lbllnkDonor = new System.Windows.Forms.LinkLabel();
            this.lbllnkDonation = new System.Windows.Forms.LinkLabel();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbllnkDonation);
            this.groupBox1.Controls.Add(this.lbllnkDonor);
            this.groupBox1.Controls.Add(this.dtVisit);
            this.groupBox1.Controls.Add(this.lblVisit);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.lblDonation);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.lblDonor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 304);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblDonor
            // 
            this.lblDonor.AutoSize = true;
            this.lblDonor.Location = new System.Drawing.Point(7, 20);
            this.lblDonor.Name = "lblDonor";
            this.lblDonor.Size = new System.Drawing.Size(34, 13);
            this.lblDonor.TabIndex = 0;
            this.lblDonor.Text = "donor";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 49);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(6, 164);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(200, 21);
            this.comboBox2.TabIndex = 2;
            // 
            // lblDonation
            // 
            this.lblDonation.AutoSize = true;
            this.lblDonation.Location = new System.Drawing.Point(7, 135);
            this.lblDonation.Name = "lblDonation";
            this.lblDonation.Size = new System.Drawing.Size(48, 13);
            this.lblDonation.TabIndex = 3;
            this.lblDonation.Text = "donation";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 76);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 56);
            this.textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 191);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 56);
            this.textBox2.TabIndex = 5;
            // 
            // lblVisit
            // 
            this.lblVisit.AutoSize = true;
            this.lblVisit.Location = new System.Drawing.Point(7, 250);
            this.lblVisit.Name = "lblVisit";
            this.lblVisit.Size = new System.Drawing.Size(25, 13);
            this.lblVisit.TabIndex = 4;
            this.lblVisit.Text = "visit";
            // 
            // dtVisit
            // 
            this.dtVisit.Location = new System.Drawing.Point(6, 266);
            this.dtVisit.Name = "dtVisit";
            this.dtVisit.Size = new System.Drawing.Size(200, 20);
            this.dtVisit.TabIndex = 6;
            // 
            // lbllnkDonor
            // 
            this.lbllnkDonor.AutoSize = true;
            this.lbllnkDonor.Location = new System.Drawing.Point(7, 33);
            this.lbllnkDonor.Name = "lbllnkDonor";
            this.lbllnkDonor.Size = new System.Drawing.Size(90, 13);
            this.lbllnkDonor.TabIndex = 7;
            this.lbllnkDonor.TabStop = true;
            this.lbllnkDonor.Text = "create new donor";
            // 
            // lbllnkDonation
            // 
            this.lbllnkDonation.AutoSize = true;
            this.lbllnkDonation.Location = new System.Drawing.Point(7, 148);
            this.lbllnkDonation.Name = "lbllnkDonation";
            this.lbllnkDonation.Size = new System.Drawing.Size(104, 13);
            this.lbllnkDonation.TabIndex = 8;
            this.lbllnkDonation.TabStop = true;
            this.lbllnkDonation.Text = "create new donation";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(12, 338);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 44;
            this.cmdClose.Text = "close";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Location = new System.Drawing.Point(157, 338);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(75, 23);
            this.cmdAccept.TabIndex = 43;
            this.cmdAccept.Text = "accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            // 
            // FrmSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 373);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FrmSchedule";
            this.Text = "FrmSchedule";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblDonation;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblDonor;
        private System.Windows.Forms.LinkLabel lbllnkDonation;
        private System.Windows.Forms.LinkLabel lbllnkDonor;
        private System.Windows.Forms.DateTimePicker dtVisit;
        private System.Windows.Forms.Label lblVisit;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdAccept;
    }
}