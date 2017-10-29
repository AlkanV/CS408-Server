namespace CS408_Server
{
    partial class FormServer
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
            this.txtInformation = new System.Windows.Forms.RichTextBox();
            this.lblInformation = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblUserList = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnListen = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtInformation
            // 
            this.txtInformation.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtInformation.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtInformation.Location = new System.Drawing.Point(24, 54);
            this.txtInformation.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.ReadOnly = true;
            this.txtInformation.Size = new System.Drawing.Size(946, 383);
            this.txtInformation.TabIndex = 0;
            this.txtInformation.Text = "";
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblInformation.Location = new System.Drawing.Point(454, 17);
            this.lblInformation.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(140, 30);
            this.lblInformation.TabIndex = 1;
            this.lblInformation.Text = "Information";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(1012, 54);
            this.listBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(378, 379);
            this.listBox1.TabIndex = 2;
            // 
            // lblUserList
            // 
            this.lblUserList.AutoSize = true;
            this.lblUserList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserList.Location = new System.Drawing.Point(1162, 17);
            this.lblUserList.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblUserList.Name = "lblUserList";
            this.lblUserList.Size = new System.Drawing.Size(115, 30);
            this.lblUserList.TabIndex = 3;
            this.lblUserList.Text = "User List";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(106, 454);
            this.txtPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtPort.Multiline = true;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(230, 41);
            this.txtPort.TabIndex = 4;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPort.Location = new System.Drawing.Point(24, 460);
            this.lblPort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(67, 30);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port:";
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(352, 454);
            this.btnListen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(150, 46);
            this.btnListen.TabIndex = 6;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(1244, 460);
            this.btnExit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 44);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1410, 513);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblUserList);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.txtInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "FormServer";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtInformation;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblUserList;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.Button btnExit;
    }
}

