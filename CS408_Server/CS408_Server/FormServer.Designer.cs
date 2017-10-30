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
            this.lstUsers = new System.Windows.Forms.ListBox();
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
            this.txtInformation.Location = new System.Drawing.Point(12, 28);
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.ReadOnly = true;
            this.txtInformation.Size = new System.Drawing.Size(475, 201);
            this.txtInformation.TabIndex = 0;
            this.txtInformation.Text = "";
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblInformation.Location = new System.Drawing.Point(227, 9);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(73, 16);
            this.lblInformation.TabIndex = 1;
            this.lblInformation.Text = "Information";
            // 
            // lstUsers
            // 
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.Location = new System.Drawing.Point(505, 28);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(191, 199);
            this.lstUsers.TabIndex = 2;
            // 
            // lblUserList
            // 
            this.lblUserList.AutoSize = true;
            this.lblUserList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserList.Location = new System.Drawing.Point(581, 9);
            this.lblUserList.Name = "lblUserList";
            this.lblUserList.Size = new System.Drawing.Size(60, 16);
            this.lblUserList.TabIndex = 3;
            this.lblUserList.Text = "User List";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(53, 236);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(117, 20);
            this.txtPort.TabIndex = 4;
            this.txtPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPort_KeyDown);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPort.Location = new System.Drawing.Point(12, 239);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 16);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port:";
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(176, 236);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(75, 21);
            this.btnListen.TabIndex = 6;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(623, 239);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 26);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(705, 267);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblUserList);
            this.Controls.Add(this.lstUsers);
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.txtInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormServer";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtInformation;
        private System.Windows.Forms.Label lblInformation;
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Label lblUserList;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.Button btnExit;
    }
}

