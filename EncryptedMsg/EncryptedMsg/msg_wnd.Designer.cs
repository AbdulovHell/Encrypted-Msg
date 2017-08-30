namespace EncryptedMsg
{
    partial class Msg_wnd
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
            this.ChatLogEdit = new System.Windows.Forms.TextBox();
            this.SendMsgEdit = new System.Windows.Forms.TextBox();
            this.SendMsgBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChatLogEdit
            // 
            this.ChatLogEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChatLogEdit.Location = new System.Drawing.Point(0, 0);
            this.ChatLogEdit.Multiline = true;
            this.ChatLogEdit.Name = "ChatLogEdit";
            this.ChatLogEdit.ReadOnly = true;
            this.ChatLogEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatLogEdit.Size = new System.Drawing.Size(544, 224);
            this.ChatLogEdit.TabIndex = 0;
            // 
            // SendMsgEdit
            // 
            this.SendMsgEdit.Location = new System.Drawing.Point(0, 230);
            this.SendMsgEdit.Multiline = true;
            this.SendMsgEdit.Name = "SendMsgEdit";
            this.SendMsgEdit.Size = new System.Drawing.Size(452, 47);
            this.SendMsgEdit.TabIndex = 1;
            this.SendMsgEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SendMsgEdit_KeyPress);
            // 
            // SendMsgBtn
            // 
            this.SendMsgBtn.Location = new System.Drawing.Point(458, 230);
            this.SendMsgBtn.Name = "SendMsgBtn";
            this.SendMsgBtn.Size = new System.Drawing.Size(74, 47);
            this.SendMsgBtn.TabIndex = 2;
            this.SendMsgBtn.Text = "Send";
            this.SendMsgBtn.UseVisualStyleBackColor = true;
            this.SendMsgBtn.Click += new System.EventHandler(this.SendMsgBtn_Click);
            // 
            // Msg_wnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 277);
            this.Controls.Add(this.SendMsgBtn);
            this.Controls.Add(this.SendMsgEdit);
            this.Controls.Add(this.ChatLogEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Msg_wnd";
            this.Text = "username";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ChatLogEdit;
        private System.Windows.Forms.TextBox SendMsgEdit;
        private System.Windows.Forms.Button SendMsgBtn;
    }
}