namespace IbanValidator.Demo
{
    partial class MainWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.ibanTextbox = new System.Windows.Forms.TextBox();
            this.validIbanLabel = new System.Windows.Forms.Label();
            this.ibanInformationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IBAN:";
            // 
            // ibanTextbox
            // 
            this.ibanTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ibanTextbox.Font = new System.Drawing.Font("Consolas", 9F);
            this.ibanTextbox.Location = new System.Drawing.Point(15, 41);
            this.ibanTextbox.Name = "ibanTextbox";
            this.ibanTextbox.Size = new System.Drawing.Size(299, 22);
            this.ibanTextbox.TabIndex = 1;
            this.ibanTextbox.TextChanged += new System.EventHandler(this.IbanTextboxTextChanged);
            // 
            // validIbanLabel
            // 
            this.validIbanLabel.AutoSize = true;
            this.validIbanLabel.Location = new System.Drawing.Point(12, 66);
            this.validIbanLabel.Name = "validIbanLabel";
            this.validIbanLabel.Size = new System.Drawing.Size(88, 15);
            this.validIbanLabel.TabIndex = 2;
            this.validIbanLabel.Text = "No IBAN given.";
            // 
            // ibanInformationLabel
            // 
            this.ibanInformationLabel.AutoSize = true;
            this.ibanInformationLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ibanInformationLabel.Location = new System.Drawing.Point(12, 101);
            this.ibanInformationLabel.Name = "ibanInformationLabel";
            this.ibanInformationLabel.Size = new System.Drawing.Size(182, 14);
            this.ibanInformationLabel.TabIndex = 3;
            this.ibanInformationLabel.Text = "No information available.";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(326, 300);
            this.Controls.Add(this.ibanInformationLabel);
            this.Controls.Add(this.validIbanLabel);
            this.Controls.Add(this.ibanTextbox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainWindow";
            this.Text = "IbanValidator Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ibanTextbox;
        private System.Windows.Forms.Label validIbanLabel;
        private System.Windows.Forms.Label ibanInformationLabel;
    }
}

