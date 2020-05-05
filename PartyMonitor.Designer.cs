namespace FFLogsEasyCheck
{
    partial class PartyMonitor
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RegionDropdown = new System.Windows.Forms.ComboBox();
            this.RegionLabel = new System.Windows.Forms.Label();
            this.ServerDropdown = new System.Windows.Forms.ComboBox();
            this.ServersLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.autoOpenLogsBox = new System.Windows.Forms.CheckBox();
            this.AutoOpenLogs = new System.Windows.Forms.Label();
            this.AutoNotify = new System.Windows.Forms.Label();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.showNotificationBox = new System.Windows.Forms.CheckBox();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RegionDropdown
            // 
            this.RegionDropdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RegionDropdown.FormattingEnabled = true;
            this.RegionDropdown.Location = new System.Drawing.Point(120, 3);
            this.RegionDropdown.Name = "RegionDropdown";
            this.RegionDropdown.Size = new System.Drawing.Size(1020, 20);
            this.RegionDropdown.TabIndex = 5;
            this.RegionDropdown.SelectedIndexChanged += new System.EventHandler(this.RegionDropdown_SelectedIndexChanged);
            // 
            // RegionLabel
            // 
            this.RegionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RegionLabel.Location = new System.Drawing.Point(3, 0);
            this.RegionLabel.Name = "RegionLabel";
            this.RegionLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RegionLabel.Size = new System.Drawing.Size(111, 25);
            this.RegionLabel.TabIndex = 3;
            this.RegionLabel.Text = "Home Region *";
            this.RegionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ServerDropdown
            // 
            this.ServerDropdown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerDropdown.DropDownHeight = 300;
            this.ServerDropdown.Enabled = false;
            this.ServerDropdown.FormattingEnabled = true;
            this.ServerDropdown.IntegralHeight = false;
            this.ServerDropdown.Location = new System.Drawing.Point(120, 28);
            this.ServerDropdown.MaxDropDownItems = 100;
            this.ServerDropdown.Name = "ServerDropdown";
            this.ServerDropdown.Size = new System.Drawing.Size(1020, 20);
            this.ServerDropdown.TabIndex = 6;
            this.ServerDropdown.SelectedIndexChanged += new System.EventHandler(this.ServerDropdown_SelectedIndexChanged);
            // 
            // ServersLabel
            // 
            this.ServersLabel.AutoSize = true;
            this.ServersLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServersLabel.Location = new System.Drawing.Point(3, 25);
            this.ServersLabel.Name = "ServersLabel";
            this.ServersLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ServersLabel.Size = new System.Drawing.Size(111, 25);
            this.ServersLabel.TabIndex = 4;
            this.ServersLabel.Text = "Home Server *";
            this.ServersLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.autoOpenLogsBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.AutoOpenLogs, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.AutoNotify, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.RegionLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ServerDropdown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.RegionDropdown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.logTextBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.ServersLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.showNotificationBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonClearLog, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1022, 582);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // autoOpenLogsBox
            // 
            this.autoOpenLogsBox.AutoSize = true;
            this.autoOpenLogsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoOpenLogsBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.autoOpenLogsBox.Location = new System.Drawing.Point(120, 78);
            this.autoOpenLogsBox.Name = "autoOpenLogsBox";
            this.autoOpenLogsBox.Size = new System.Drawing.Size(1020, 19);
            this.autoOpenLogsBox.TabIndex = 11;
            this.autoOpenLogsBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.autoOpenLogsBox.UseVisualStyleBackColor = true;
            this.autoOpenLogsBox.CheckedChanged += new System.EventHandler(this.autoOpenLogsBox_CheckedChanged);
            // 
            // AutoOpenLogs
            // 
            this.AutoOpenLogs.AutoSize = true;
            this.AutoOpenLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AutoOpenLogs.Location = new System.Drawing.Point(3, 75);
            this.AutoOpenLogs.Name = "AutoOpenLogs";
            this.AutoOpenLogs.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AutoOpenLogs.Size = new System.Drawing.Size(111, 25);
            this.AutoOpenLogs.TabIndex = 10;
            this.AutoOpenLogs.Text = "Auto Open Logs";
            this.AutoOpenLogs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AutoNotify
            // 
            this.AutoNotify.AutoSize = true;
            this.AutoNotify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AutoNotify.Location = new System.Drawing.Point(3, 50);
            this.AutoNotify.Name = "AutoNotify";
            this.AutoNotify.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AutoNotify.Size = new System.Drawing.Size(111, 25);
            this.AutoNotify.TabIndex = 8;
            this.AutoNotify.Text = "Show Notification";
            this.AutoNotify.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point(120, 103);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(1020, 497);
            this.logTextBox.TabIndex = 7;
            this.logTextBox.Text = "";
            this.logTextBox.WordWrap = false;
            this.logTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.logTextBox_LinkClicked);
            // 
            // showNotificationBox
            // 
            this.showNotificationBox.AutoSize = true;
            this.showNotificationBox.Checked = true;
            this.showNotificationBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showNotificationBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showNotificationBox.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.showNotificationBox.Location = new System.Drawing.Point(120, 53);
            this.showNotificationBox.Name = "showNotificationBox";
            this.showNotificationBox.Size = new System.Drawing.Size(1020, 19);
            this.showNotificationBox.TabIndex = 9;
            this.showNotificationBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.showNotificationBox.UseVisualStyleBackColor = true;
            this.showNotificationBox.CheckedChanged += new System.EventHandler(this.showNotificationBox_CheckedChanged);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonClearLog.Location = new System.Drawing.Point(3, 103);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(111, 23);
            this.buttonClearLog.TabIndex = 12;
            this.buttonClearLog.Text = "Clear";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // PartyMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PartyMonitor";
            this.Size = new System.Drawing.Size(1022, 582);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox RegionDropdown;
        private System.Windows.Forms.Label RegionLabel;
        private System.Windows.Forms.ComboBox ServerDropdown;
        private System.Windows.Forms.Label ServersLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.Label AutoNotify;
        private System.Windows.Forms.CheckBox showNotificationBox;
        private System.Windows.Forms.CheckBox autoOpenLogsBox;
        private System.Windows.Forms.Label AutoOpenLogs;
        private System.Windows.Forms.Button buttonClearLog;
    }
}
