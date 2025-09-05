namespace MQTT_Data_Parser
{
    partial class MQTTDataParser
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MQTTDataParser));
            saveFileDialog = new SaveFileDialog();
            grbParsingText = new GroupBox();
            tbParsingText = new TextBox();
            grbParsingData = new GroupBox();
            dgvParsingData = new DataGridView();
            rtbParsingData = new RichTextBox();
            lblParsingData = new Label();
            btnHelp = new Button();
            btnClear = new Button();
            pnlMSG = new Panel();
            lblMSG = new Label();
            btnSaveFile = new Button();
            grbConnection = new GroupBox();
            tbTopic = new TextBox();
            lblTopic = new Label();
            btnConnect = new Button();
            tbPassword = new TextBox();
            tbUserName = new TextBox();
            tbPort = new TextBox();
            tbHost = new TextBox();
            lstProtocol = new ListBox();
            lblProtocol = new Label();
            lblHost = new Label();
            lblPort = new Label();
            lblPassword = new Label();
            lblUserName = new Label();
            grbMQTTmonitor = new GroupBox();
            rtbMQTTmonitor = new RichTextBox();
            lblMQTTmonitor = new Label();
            grbSystemMSG = new GroupBox();
            rtbSystemMSG = new RichTextBox();
            lblSystemMSG = new Label();
            btnClose = new Button();
            spContainer = new SplitContainer();
            spContainer_L = new SplitContainer();
            spContainer_R = new SplitContainer();
            spContainer_RT = new SplitContainer();
            grbParsingText.SuspendLayout();
            grbParsingData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvParsingData).BeginInit();
            pnlMSG.SuspendLayout();
            grbConnection.SuspendLayout();
            grbMQTTmonitor.SuspendLayout();
            grbSystemMSG.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spContainer).BeginInit();
            spContainer.Panel1.SuspendLayout();
            spContainer.Panel2.SuspendLayout();
            spContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spContainer_L).BeginInit();
            spContainer_L.Panel1.SuspendLayout();
            spContainer_L.Panel2.SuspendLayout();
            spContainer_L.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spContainer_R).BeginInit();
            spContainer_R.Panel1.SuspendLayout();
            spContainer_R.Panel2.SuspendLayout();
            spContainer_R.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spContainer_RT).BeginInit();
            spContainer_RT.Panel1.SuspendLayout();
            spContainer_RT.Panel2.SuspendLayout();
            spContainer_RT.SuspendLayout();
            SuspendLayout();
            // 
            // grbParsingText
            // 
            grbParsingText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grbParsingText.Controls.Add(tbParsingText);
            grbParsingText.Location = new Point(3, 3);
            grbParsingText.Name = "grbParsingText";
            grbParsingText.Size = new Size(131, 266);
            grbParsingText.TabIndex = 7;
            grbParsingText.TabStop = false;
            grbParsingText.Text = "Parsing Text";
            // 
            // tbParsingText
            // 
            tbParsingText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbParsingText.BackColor = SystemColors.Window;
            tbParsingText.BorderStyle = BorderStyle.None;
            tbParsingText.Font = new Font("맑은 고딕", 9F);
            tbParsingText.ForeColor = SystemColors.HotTrack;
            tbParsingText.Location = new Point(6, 22);
            tbParsingText.Multiline = true;
            tbParsingText.Name = "tbParsingText";
            tbParsingText.ScrollBars = ScrollBars.Vertical;
            tbParsingText.Size = new Size(119, 238);
            tbParsingText.TabIndex = 0;
            tbParsingText.Text = resources.GetString("tbParsingText.Text");
            tbParsingText.WordWrap = false;
            tbParsingText.KeyDown += tbParsingText_KeyDown;
            // 
            // grbParsingData
            // 
            grbParsingData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grbParsingData.Controls.Add(dgvParsingData);
            grbParsingData.Controls.Add(rtbParsingData);
            grbParsingData.Controls.Add(lblParsingData);
            grbParsingData.Location = new Point(3, 3);
            grbParsingData.Name = "grbParsingData";
            grbParsingData.Size = new Size(665, 235);
            grbParsingData.TabIndex = 10;
            grbParsingData.TabStop = false;
            grbParsingData.Text = "Parsing Data";
            // 
            // dgvParsingData
            // 
            dgvParsingData.AllowUserToAddRows = false;
            dgvParsingData.AllowUserToDeleteRows = false;
            dgvParsingData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvParsingData.BackgroundColor = SystemColors.ScrollBar;
            dgvParsingData.Location = new Point(6, 18);
            dgvParsingData.Name = "dgvParsingData";
            dgvParsingData.ReadOnly = true;
            dgvParsingData.RowHeadersVisible = false;
            dgvParsingData.RowHeadersWidth = 4;
            dgvParsingData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParsingData.Size = new Size(653, 211);
            dgvParsingData.TabIndex = 17;
            dgvParsingData.VirtualMode = true;
            // 
            // rtbParsingData
            // 
            rtbParsingData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbParsingData.BackColor = SystemColors.Window;
            rtbParsingData.BorderStyle = BorderStyle.None;
            rtbParsingData.Location = new Point(6, 18);
            rtbParsingData.Name = "rtbParsingData";
            rtbParsingData.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbParsingData.Size = new Size(653, 182);
            rtbParsingData.TabIndex = 16;
            rtbParsingData.Text = "";
            // 
            // lblParsingData
            // 
            lblParsingData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblParsingData.AutoSize = true;
            lblParsingData.Location = new Point(620, 0);
            lblParsingData.Name = "lblParsingData";
            lblParsingData.Size = new Size(39, 15);
            lblParsingData.TabIndex = 14;
            lblParsingData.Text = "label1";
            lblParsingData.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnHelp
            // 
            btnHelp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.Location = new Point(3, 244);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(23, 23);
            btnHelp.TabIndex = 18;
            btnHelp.Text = "?";
            btnHelp.UseVisualStyleBackColor = true;
            btnHelp.Click += btnHelp_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Location = new Point(502, 244);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(80, 23);
            btnClear.TabIndex = 10;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClearClick;
            // 
            // pnlMSG
            // 
            pnlMSG.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlMSG.BackColor = SystemColors.Control;
            pnlMSG.Controls.Add(lblMSG);
            pnlMSG.Location = new Point(32, 244);
            pnlMSG.Name = "pnlMSG";
            pnlMSG.Size = new Size(464, 23);
            pnlMSG.TabIndex = 11;
            // 
            // lblMSG
            // 
            lblMSG.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblMSG.AutoSize = true;
            lblMSG.Location = new Point(4, 4);
            lblMSG.Name = "lblMSG";
            lblMSG.Size = new Size(0, 15);
            lblMSG.TabIndex = 0;
            lblMSG.TextAlign = ContentAlignment.BottomLeft;
            // 
            // btnSaveFile
            // 
            btnSaveFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSaveFile.BackColor = Color.Turquoise;
            btnSaveFile.FlatStyle = FlatStyle.Flat;
            btnSaveFile.Location = new Point(588, 244);
            btnSaveFile.Name = "btnSaveFile";
            btnSaveFile.Size = new Size(80, 23);
            btnSaveFile.TabIndex = 10;
            btnSaveFile.Text = "Save to File";
            btnSaveFile.UseVisualStyleBackColor = false;
            btnSaveFile.Click += btnSaveFileClick;
            // 
            // grbConnection
            // 
            grbConnection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grbConnection.Controls.Add(tbTopic);
            grbConnection.Controls.Add(lblTopic);
            grbConnection.Controls.Add(btnConnect);
            grbConnection.Controls.Add(tbPassword);
            grbConnection.Controls.Add(tbUserName);
            grbConnection.Controls.Add(tbPort);
            grbConnection.Controls.Add(tbHost);
            grbConnection.Controls.Add(lstProtocol);
            grbConnection.Controls.Add(lblProtocol);
            grbConnection.Controls.Add(lblHost);
            grbConnection.Controls.Add(lblPort);
            grbConnection.Controls.Add(lblPassword);
            grbConnection.Controls.Add(lblUserName);
            grbConnection.Location = new Point(3, 3);
            grbConnection.Name = "grbConnection";
            grbConnection.Size = new Size(131, 309);
            grbConnection.TabIndex = 6;
            grbConnection.TabStop = false;
            grbConnection.Text = "Connection";
            // 
            // tbTopic
            // 
            tbTopic.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbTopic.BorderStyle = BorderStyle.None;
            tbTopic.Font = new Font("맑은 고딕", 9F);
            tbTopic.ForeColor = SystemColors.HotTrack;
            tbTopic.Location = new Point(6, 253);
            tbTopic.Name = "tbTopic";
            tbTopic.PlaceholderText = "application/{{ .ApplicationID }}/device/{{ .DevEUI }}/event/{{ .EventType }}";
            tbTopic.Size = new Size(119, 16);
            tbTopic.TabIndex = 12;
            tbTopic.Text = "application/6/#";
            // 
            // lblTopic
            // 
            lblTopic.AutoSize = true;
            lblTopic.ForeColor = SystemColors.GrayText;
            lblTopic.Location = new Point(6, 235);
            lblTopic.Name = "lblTopic";
            lblTopic.Size = new Size(36, 15);
            lblTopic.TabIndex = 11;
            lblTopic.Text = "Topic";
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnConnect.BackColor = Color.Turquoise;
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Location = new Point(6, 282);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(119, 23);
            btnConnect.TabIndex = 10;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Click += btnConnectClick;
            // 
            // tbPassword
            // 
            tbPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPassword.BorderStyle = BorderStyle.None;
            tbPassword.Font = new Font("맑은 고딕", 9F);
            tbPassword.ForeColor = SystemColors.HotTrack;
            tbPassword.Location = new Point(6, 209);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '·';
            tbPassword.PlaceholderText = "비밀번호를 입력하세요.";
            tbPassword.Size = new Size(119, 16);
            tbPassword.TabIndex = 9;
            // 
            // tbUserName
            // 
            tbUserName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbUserName.BorderStyle = BorderStyle.None;
            tbUserName.Font = new Font("맑은 고딕", 9F);
            tbUserName.ForeColor = SystemColors.HotTrack;
            tbUserName.Location = new Point(6, 165);
            tbUserName.Name = "tbUserName";
            tbUserName.PlaceholderText = "사용자 아이디를 입력하세요.";
            tbUserName.Size = new Size(119, 16);
            tbUserName.TabIndex = 8;
            // 
            // tbPort
            // 
            tbPort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPort.BorderStyle = BorderStyle.None;
            tbPort.Font = new Font("맑은 고딕", 9F);
            tbPort.ForeColor = SystemColors.HotTrack;
            tbPort.Location = new Point(6, 121);
            tbPort.Name = "tbPort";
            tbPort.PlaceholderText = "서버 포트를 입력하세요.";
            tbPort.Size = new Size(119, 16);
            tbPort.TabIndex = 7;
            tbPort.Text = "1883";
            // 
            // tbHost
            // 
            tbHost.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbHost.BorderStyle = BorderStyle.None;
            tbHost.Font = new Font("맑은 고딕", 9F);
            tbHost.ForeColor = SystemColors.HotTrack;
            tbHost.Location = new Point(6, 77);
            tbHost.Name = "tbHost";
            tbHost.PlaceholderText = "서버 주소를 입력하세요.";
            tbHost.Size = new Size(119, 16);
            tbHost.TabIndex = 6;
            // 
            // lstProtocol
            // 
            lstProtocol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstProtocol.BorderStyle = BorderStyle.None;
            lstProtocol.Font = new Font("맑은 고딕", 9F);
            lstProtocol.ForeColor = SystemColors.HotTrack;
            lstProtocol.FormattingEnabled = true;
            lstProtocol.Items.AddRange(new object[] { "mqtt://", "ws://" });
            lstProtocol.Location = new Point(6, 37);
            lstProtocol.Name = "lstProtocol";
            lstProtocol.Size = new Size(119, 15);
            lstProtocol.TabIndex = 5;
            // 
            // lblProtocol
            // 
            lblProtocol.AutoSize = true;
            lblProtocol.ForeColor = SystemColors.GrayText;
            lblProtocol.Location = new Point(6, 19);
            lblProtocol.Name = "lblProtocol";
            lblProtocol.Size = new Size(52, 15);
            lblProtocol.TabIndex = 0;
            lblProtocol.Text = "Protocol";
            // 
            // lblHost
            // 
            lblHost.AutoSize = true;
            lblHost.ForeColor = SystemColors.GrayText;
            lblHost.Location = new Point(6, 59);
            lblHost.Name = "lblHost";
            lblHost.Size = new Size(32, 15);
            lblHost.TabIndex = 1;
            lblHost.Text = "Host";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.ForeColor = SystemColors.GrayText;
            lblPort.Location = new Point(6, 103);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 2;
            lblPort.Text = "Port";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = SystemColors.GrayText;
            lblPassword.Location = new Point(6, 191);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Password";
            lblPassword.DoubleClick += lblPassword_DoubleClick;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.ForeColor = SystemColors.GrayText;
            lblUserName.Location = new Point(6, 147);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(66, 15);
            lblUserName.TabIndex = 3;
            lblUserName.Text = "User Name";
            // 
            // grbMQTTmonitor
            // 
            grbMQTTmonitor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grbMQTTmonitor.Controls.Add(rtbMQTTmonitor);
            grbMQTTmonitor.Controls.Add(lblMQTTmonitor);
            grbMQTTmonitor.Location = new Point(3, 3);
            grbMQTTmonitor.Name = "grbMQTTmonitor";
            grbMQTTmonitor.Size = new Size(665, 230);
            grbMQTTmonitor.TabIndex = 9;
            grbMQTTmonitor.TabStop = false;
            grbMQTTmonitor.Text = "MQTT Topic Monitor";
            // 
            // rtbMQTTmonitor
            // 
            rtbMQTTmonitor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbMQTTmonitor.BackColor = SystemColors.Control;
            rtbMQTTmonitor.BorderStyle = BorderStyle.None;
            rtbMQTTmonitor.Location = new Point(6, 18);
            rtbMQTTmonitor.Name = "rtbMQTTmonitor";
            rtbMQTTmonitor.ReadOnly = true;
            rtbMQTTmonitor.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbMQTTmonitor.Size = new Size(653, 206);
            rtbMQTTmonitor.TabIndex = 15;
            rtbMQTTmonitor.Text = "";
            // 
            // lblMQTTmonitor
            // 
            lblMQTTmonitor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMQTTmonitor.AutoSize = true;
            lblMQTTmonitor.Location = new Point(620, 0);
            lblMQTTmonitor.Name = "lblMQTTmonitor";
            lblMQTTmonitor.Size = new Size(39, 15);
            lblMQTTmonitor.TabIndex = 13;
            lblMQTTmonitor.Text = "label1";
            lblMQTTmonitor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grbSystemMSG
            // 
            grbSystemMSG.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grbSystemMSG.Controls.Add(rtbSystemMSG);
            grbSystemMSG.Controls.Add(lblSystemMSG);
            grbSystemMSG.Controls.Add(btnClose);
            grbSystemMSG.Location = new Point(3, 3);
            grbSystemMSG.Name = "grbSystemMSG";
            grbSystemMSG.Size = new Size(665, 67);
            grbSystemMSG.TabIndex = 11;
            grbSystemMSG.TabStop = false;
            grbSystemMSG.Text = "System Message";
            // 
            // rtbSystemMSG
            // 
            rtbSystemMSG.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbSystemMSG.BackColor = SystemColors.Control;
            rtbSystemMSG.BorderStyle = BorderStyle.None;
            rtbSystemMSG.Location = new Point(6, 18);
            rtbSystemMSG.Name = "rtbSystemMSG";
            rtbSystemMSG.ReadOnly = true;
            rtbSystemMSG.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbSystemMSG.Size = new Size(653, 43);
            rtbSystemMSG.TabIndex = 14;
            rtbSystemMSG.Text = "";
            rtbSystemMSG.Click += rtbSystemMSG_Click;
            // 
            // lblSystemMSG
            // 
            lblSystemMSG.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSystemMSG.AutoSize = true;
            lblSystemMSG.Location = new Point(620, 0);
            lblSystemMSG.Name = "lblSystemMSG";
            lblSystemMSG.Size = new Size(39, 15);
            lblSystemMSG.TabIndex = 12;
            lblSystemMSG.Text = "label1";
            lblSystemMSG.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(6, 22);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(110, 23);
            btnClose.TabIndex = 10;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnCloseClick;
            // 
            // spContainer
            // 
            spContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            spContainer.Location = new Point(9, 9);
            spContainer.Margin = new Padding(0);
            spContainer.Name = "spContainer";
            // 
            // spContainer.Panel1
            // 
            spContainer.Panel1.Controls.Add(spContainer_L);
            spContainer.Panel1MinSize = 126;
            // 
            // spContainer.Panel2
            // 
            spContainer.Panel2.Controls.Add(spContainer_R);
            spContainer.Panel2MinSize = 206;
            spContainer.Size = new Size(816, 593);
            spContainer.SplitterDistance = 139;
            spContainer.TabIndex = 12;
            // 
            // spContainer_L
            // 
            spContainer_L.BorderStyle = BorderStyle.FixedSingle;
            spContainer_L.Dock = DockStyle.Fill;
            spContainer_L.Location = new Point(0, 0);
            spContainer_L.Margin = new Padding(0);
            spContainer_L.Name = "spContainer_L";
            spContainer_L.Orientation = Orientation.Horizontal;
            // 
            // spContainer_L.Panel1
            // 
            spContainer_L.Panel1.Controls.Add(grbConnection);
            spContainer_L.Panel1MinSize = 317;
            // 
            // spContainer_L.Panel2
            // 
            spContainer_L.Panel2.Controls.Add(grbParsingText);
            spContainer_L.Panel2MinSize = 230;
            spContainer_L.Size = new Size(139, 593);
            spContainer_L.SplitterDistance = 317;
            spContainer_L.TabIndex = 0;
            spContainer_L.SplitterMoved += SyncSplitter;
            // 
            // spContainer_R
            // 
            spContainer_R.BorderStyle = BorderStyle.FixedSingle;
            spContainer_R.Dock = DockStyle.Fill;
            spContainer_R.Location = new Point(0, 0);
            spContainer_R.Margin = new Padding(0);
            spContainer_R.Name = "spContainer_R";
            spContainer_R.Orientation = Orientation.Horizontal;
            // 
            // spContainer_R.Panel1
            // 
            spContainer_R.Panel1.Controls.Add(spContainer_RT);
            spContainer_R.Panel1MinSize = 145;
            // 
            // spContainer_R.Panel2
            // 
            spContainer_R.Panel2.Controls.Add(btnHelp);
            spContainer_R.Panel2.Controls.Add(btnClear);
            spContainer_R.Panel2.Controls.Add(grbParsingData);
            spContainer_R.Panel2.Controls.Add(btnSaveFile);
            spContainer_R.Panel2.Controls.Add(pnlMSG);
            spContainer_R.Panel2MinSize = 230;
            spContainer_R.Size = new Size(673, 593);
            spContainer_R.SplitterDistance = 317;
            spContainer_R.TabIndex = 0;
            spContainer_R.SplitterMoved += SyncSplitter;
            // 
            // spContainer_RT
            // 
            spContainer_RT.BorderStyle = BorderStyle.FixedSingle;
            spContainer_RT.Dock = DockStyle.Fill;
            spContainer_RT.Location = new Point(0, 0);
            spContainer_RT.Margin = new Padding(0);
            spContainer_RT.Name = "spContainer_RT";
            spContainer_RT.Orientation = Orientation.Horizontal;
            // 
            // spContainer_RT.Panel1
            // 
            spContainer_RT.Panel1.Controls.Add(grbSystemMSG);
            spContainer_RT.Panel1MinSize = 70;
            // 
            // spContainer_RT.Panel2
            // 
            spContainer_RT.Panel2.Controls.Add(grbMQTTmonitor);
            spContainer_RT.Panel2MinSize = 70;
            spContainer_RT.Size = new Size(673, 317);
            spContainer_RT.SplitterDistance = 75;
            spContainer_RT.TabIndex = 0;
            // 
            // MQTTDataParser
            // 
            AcceptButton = btnConnect;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(834, 611);
            Controls.Add(spContainer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(850, 650);
            Name = "MQTTDataParser";
            Text = "MQTT Data Parser";
            grbParsingText.ResumeLayout(false);
            grbParsingText.PerformLayout();
            grbParsingData.ResumeLayout(false);
            grbParsingData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvParsingData).EndInit();
            pnlMSG.ResumeLayout(false);
            pnlMSG.PerformLayout();
            grbConnection.ResumeLayout(false);
            grbConnection.PerformLayout();
            grbMQTTmonitor.ResumeLayout(false);
            grbMQTTmonitor.PerformLayout();
            grbSystemMSG.ResumeLayout(false);
            grbSystemMSG.PerformLayout();
            spContainer.Panel1.ResumeLayout(false);
            spContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spContainer).EndInit();
            spContainer.ResumeLayout(false);
            spContainer_L.Panel1.ResumeLayout(false);
            spContainer_L.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spContainer_L).EndInit();
            spContainer_L.ResumeLayout(false);
            spContainer_R.Panel1.ResumeLayout(false);
            spContainer_R.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spContainer_R).EndInit();
            spContainer_R.ResumeLayout(false);
            spContainer_RT.Panel1.ResumeLayout(false);
            spContainer_RT.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spContainer_RT).EndInit();
            spContainer_RT.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private SaveFileDialog saveFileDialog;
        private GroupBox grbParsingText;
        private TextBox tbParsingText;
        private GroupBox grbParsingData;
        private RichTextBox rtbParsingData;
        private Label lblParsingData;
        private Panel pnlMSG;
        private Label lblMSG;
        private Button btnClear;
        private Button btnSaveFile;
        private GroupBox grbConnection;
        private TextBox tbTopic;
        private Label lblTopic;
        private Button btnConnect;
        private TextBox tbPassword;
        private TextBox tbUserName;
        private TextBox tbPort;
        private TextBox tbHost;
        private ListBox lstProtocol;
        private Label lblProtocol;
        private Label lblHost;
        private Label lblPort;
        private Label lblPassword;
        private Label lblUserName;
        private GroupBox grbMQTTmonitor;
        private RichTextBox rtbMQTTmonitor;
        private Label lblMQTTmonitor;
        private GroupBox grbSystemMSG;
        private RichTextBox rtbSystemMSG;
        private Label lblSystemMSG;
        private Button btnClose;
        private SplitContainer spContainer;
        private DataGridView dgvParsingData;
        private SplitContainer spContainer_L;
        private SplitContainer spContainer_R;
        private SplitContainer spContainer_RT;
        private Button btnHelp;
    }
}