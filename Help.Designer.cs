namespace MQTT_Data_Parser
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            btnClose = new Button();
            rtbHelp = new RichTextBox();
            chkCopytoClipBoard = new CheckBox();
            lbl = new Label();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Location = new Point(17, 17);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 1;
            btnClose.Text = "button1";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // rtbHelp
            // 
            rtbHelp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbHelp.BackColor = SystemColors.Control;
            rtbHelp.BorderStyle = BorderStyle.None;
            rtbHelp.Location = new Point(12, 12);
            rtbHelp.Name = "rtbHelp";
            rtbHelp.Size = new Size(260, 187);
            rtbHelp.TabIndex = 2;
            rtbHelp.Text = "";
            // 
            // chkCopytoClipBoard
            // 
            chkCopytoClipBoard.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            chkCopytoClipBoard.AutoSize = true;
            chkCopytoClipBoard.Location = new Point(257, 185);
            chkCopytoClipBoard.Name = "chkCopytoClipBoard";
            chkCopytoClipBoard.Size = new Size(15, 14);
            chkCopytoClipBoard.TabIndex = 3;
            chkCopytoClipBoard.UseVisualStyleBackColor = true;
            // 
            // lbl
            // 
            lbl.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lbl.AutoSize = true;
            lbl.ForeColor = SystemColors.ControlDark;
            lbl.Location = new Point(140, 184);
            lbl.Name = "lbl";
            lbl.Size = new Size(111, 15);
            lbl.TabIndex = 4;
            lbl.Text = "선택영역 자동 복사";
            lbl.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Help
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(284, 211);
            Controls.Add(lbl);
            Controls.Add(chkCopytoClipBoard);
            Controls.Add(rtbHelp);
            Controls.Add(btnClose);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(300, 250);
            Name = "Help";
            Text = "Help";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnClose;
        private RichTextBox rtbHelp;
        private CheckBox chkCopytoClipBoard;
        private Label lbl;
    }
}