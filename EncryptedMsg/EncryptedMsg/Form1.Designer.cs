namespace EncryptedMsg
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConnectP2P = new System.Windows.Forms.Button();
            this.P2PAddrEdit = new System.Windows.Forms.TextBox();
            this.NameEdit = new System.Windows.Forms.TextBox();
            this.ProgLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConnectP2P
            // 
            this.ConnectP2P.Location = new System.Drawing.Point(12, 38);
            this.ConnectP2P.Name = "ConnectP2P";
            this.ConnectP2P.Size = new System.Drawing.Size(100, 23);
            this.ConnectP2P.TabIndex = 0;
            this.ConnectP2P.Text = "Connect p2p";
            this.ConnectP2P.UseVisualStyleBackColor = true;
            this.ConnectP2P.Click += new System.EventHandler(this.ConnectP2P_Click);
            // 
            // P2PAddrEdit
            // 
            this.P2PAddrEdit.Location = new System.Drawing.Point(12, 12);
            this.P2PAddrEdit.Name = "P2PAddrEdit";
            this.P2PAddrEdit.Size = new System.Drawing.Size(100, 20);
            this.P2PAddrEdit.TabIndex = 1;
            this.P2PAddrEdit.Text = "127.0.0.1";
            // 
            // NameEdit
            // 
            this.NameEdit.Location = new System.Drawing.Point(118, 12);
            this.NameEdit.Name = "NameEdit";
            this.NameEdit.Size = new System.Drawing.Size(150, 20);
            this.NameEdit.TabIndex = 2;
            // 
            // ProgLog
            // 
            this.ProgLog.Location = new System.Drawing.Point(12, 124);
            this.ProgLog.Multiline = true;
            this.ProgLog.Name = "ProgLog";
            this.ProgLog.Size = new System.Drawing.Size(324, 106);
            this.ProgLog.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 242);
            this.Controls.Add(this.ProgLog);
            this.Controls.Add(this.NameEdit);
            this.Controls.Add(this.P2PAddrEdit);
            this.Controls.Add(this.ConnectP2P);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectP2P;
        private System.Windows.Forms.TextBox P2PAddrEdit;
        private System.Windows.Forms.TextBox NameEdit;
        private System.Windows.Forms.TextBox ProgLog;
    }
}

