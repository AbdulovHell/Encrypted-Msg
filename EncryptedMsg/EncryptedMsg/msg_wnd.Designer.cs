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
            this.InputMsgEdit = new System.Windows.Forms.TextBox();
            this.OutMsgEdit = new System.Windows.Forms.TextBox();
            this.SendMsgBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InputMsgEdit
            // 
            this.InputMsgEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.InputMsgEdit.Location = new System.Drawing.Point(0, 0);
            this.InputMsgEdit.Multiline = true;
            this.InputMsgEdit.Name = "InputMsgEdit";
            this.InputMsgEdit.Size = new System.Drawing.Size(544, 224);
            this.InputMsgEdit.TabIndex = 0;
            // 
            // OutMsgEdit
            // 
            this.OutMsgEdit.Location = new System.Drawing.Point(0, 230);
            this.OutMsgEdit.Multiline = true;
            this.OutMsgEdit.Name = "OutMsgEdit";
            this.OutMsgEdit.Size = new System.Drawing.Size(452, 47);
            this.OutMsgEdit.TabIndex = 1;
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
            // msg_wnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 277);
            this.Controls.Add(this.SendMsgBtn);
            this.Controls.Add(this.OutMsgEdit);
            this.Controls.Add(this.InputMsgEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "msg_wnd";
            this.Text = "username";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputMsgEdit;
        private System.Windows.Forms.TextBox OutMsgEdit;
        private System.Windows.Forms.Button SendMsgBtn;
    }
}