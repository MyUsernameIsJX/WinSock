namespace WinSock
{
    partial class Form2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMonitor = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnConnectToServer = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbSendMsg = new System.Windows.Forms.TextBox();
            this.btnCSend = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbPort);
            this.panel1.Controls.Add(this.tbIP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(43, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 154);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // tbIP
            // 
            this.tbIP.BackColor = System.Drawing.SystemColors.Info;
            this.tbIP.Location = new System.Drawing.Point(36, 18);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(147, 25);
            this.tbIP.TabIndex = 2;
            // 
            // tbPort
            // 
            this.tbPort.BackColor = System.Drawing.SystemColors.Info;
            this.tbPort.Location = new System.Drawing.Point(58, 64);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(125, 25);
            this.tbPort.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "聊天内容：";
            // 
            // tbMonitor
            // 
            this.tbMonitor.Location = new System.Drawing.Point(260, 70);
            this.tbMonitor.Multiline = true;
            this.tbMonitor.Name = "tbMonitor";
            this.tbMonitor.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMonitor.Size = new System.Drawing.Size(568, 343);
            this.tbMonitor.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnConnectToServer);
            this.panel2.Location = new System.Drawing.Point(43, 258);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 154);
            this.panel2.TabIndex = 3;
            // 
            // btnConnectToServer
            // 
            this.btnConnectToServer.Location = new System.Drawing.Point(33, 43);
            this.btnConnectToServer.Name = "btnConnectToServer";
            this.btnConnectToServer.Size = new System.Drawing.Size(140, 32);
            this.btnConnectToServer.TabIndex = 0;
            this.btnConnectToServer.Text = "连接服务器";
            this.btnConnectToServer.UseVisualStyleBackColor = true;
            this.btnConnectToServer.Click += new System.EventHandler(this.btnConnectToServer_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(33, 107);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(140, 36);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "关闭客户端";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tbSendMsg
            // 
            this.tbSendMsg.Location = new System.Drawing.Point(76, 451);
            this.tbSendMsg.Name = "tbSendMsg";
            this.tbSendMsg.Size = new System.Drawing.Size(643, 25);
            this.tbSendMsg.TabIndex = 4;
            // 
            // btnCSend
            // 
            this.btnCSend.Location = new System.Drawing.Point(737, 451);
            this.btnCSend.Name = "btnCSend";
            this.btnCSend.Size = new System.Drawing.Size(91, 32);
            this.btnCSend.TabIndex = 5;
            this.btnCSend.Text = "发送";
            this.btnCSend.UseVisualStyleBackColor = true;
            this.btnCSend.Click += new System.EventHandler(this.btnCSend_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Name:";
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.SystemColors.Info;
            this.tbName.Location = new System.Drawing.Point(58, 109);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(124, 25);
            this.tbName.TabIndex = 5;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 528);
            this.Controls.Add(this.btnCSend);
            this.Controls.Add(this.tbSendMsg);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tbMonitor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "Client";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMonitor;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnConnectToServer;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbSendMsg;
        private System.Windows.Forms.Button btnCSend;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbName;
    }
}