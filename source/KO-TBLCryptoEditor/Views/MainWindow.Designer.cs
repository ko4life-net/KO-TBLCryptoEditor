namespace KO.TBLCryptoEditor.Views
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
            this.components = new System.ComponentModel.Container();
            this.btnRandomKeys = new System.Windows.Forms.Button();
            this.btnUpdateClient = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.cbxManualUpdate = new System.Windows.Forms.CheckBox();
            this.gbxKeys = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCreateBackup = new System.Windows.Forms.CheckBox();
            this.gbxOptions = new System.Windows.Forms.GroupBox();
            this.cbxSkipClientVersion = new System.Windows.Forms.CheckBox();
            this.cbxSkipKOValidation = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnGeneralReport = new System.Windows.Forms.Button();
            this.btnUpdateData = new System.Windows.Forms.Button();
            this.panelDragArea = new System.Windows.Forms.Panel();
            this.tbxKey1 = new KO.TBLCryptoEditor.Controls.CryptoKeyInput();
            this.tbxKey2 = new KO.TBLCryptoEditor.Controls.CryptoKeyInput();
            this.tbxKey3 = new KO.TBLCryptoEditor.Controls.CryptoKeyInput();
            this.statusStrip.SuspendLayout();
            this.gbxKeys.SuspendLayout();
            this.gbxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRandomKeys
            // 
            this.btnRandomKeys.Enabled = false;
            this.btnRandomKeys.Location = new System.Drawing.Point(241, 143);
            this.btnRandomKeys.Name = "btnRandomKeys";
            this.btnRandomKeys.Size = new System.Drawing.Size(153, 32);
            this.btnRandomKeys.TabIndex = 0;
            this.btnRandomKeys.Text = "Randomize Keys";
            this.toolTip.SetToolTip(this.btnRandomKeys, "Randomize your current keys to something else.");
            this.btnRandomKeys.UseVisualStyleBackColor = true;
            this.btnRandomKeys.Click += new System.EventHandler(this.btnGenerateKeys_Click);
            // 
            // btnUpdateClient
            // 
            this.btnUpdateClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateClient.Enabled = false;
            this.btnUpdateClient.Location = new System.Drawing.Point(402, 181);
            this.btnUpdateClient.Name = "btnUpdateClient";
            this.btnUpdateClient.Size = new System.Drawing.Size(153, 32);
            this.btnUpdateClient.TabIndex = 2;
            this.btnUpdateClient.Text = "Update Client Encryption";
            this.toolTip.SetToolTip(this.btnUpdateClient, "Apply/patch the new keys into your target executable.");
            this.btnUpdateClient.UseVisualStyleBackColor = true;
            this.btnUpdateClient.Click += new System.EventHandler(this.btnPatchClient_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 223);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(566, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(130, 17);
            this.lblStatus.Text = "Waiting for user action.";
            // 
            // cbxManualUpdate
            // 
            this.cbxManualUpdate.AutoSize = true;
            this.cbxManualUpdate.Enabled = false;
            this.cbxManualUpdate.Location = new System.Drawing.Point(12, 102);
            this.cbxManualUpdate.Name = "cbxManualUpdate";
            this.cbxManualUpdate.Size = new System.Drawing.Size(101, 17);
            this.cbxManualUpdate.TabIndex = 3;
            this.cbxManualUpdate.Text = "Manual Change";
            this.toolTip.SetToolTip(this.cbxManualUpdate, "Manually update the tbl keys. If you can\'t update Key2, this is because there wer" +
        "e inlined functions in that exe.");
            this.cbxManualUpdate.UseVisualStyleBackColor = true;
            this.cbxManualUpdate.CheckedChanged += new System.EventHandler(this.cbxManualUpdate_CheckedChanged);
            // 
            // gbxKeys
            // 
            this.gbxKeys.Controls.Add(this.label3);
            this.gbxKeys.Controls.Add(this.label2);
            this.gbxKeys.Controls.Add(this.label1);
            this.gbxKeys.Controls.Add(this.tbxKey1);
            this.gbxKeys.Controls.Add(this.tbxKey2);
            this.gbxKeys.Controls.Add(this.cbxManualUpdate);
            this.gbxKeys.Controls.Add(this.tbxKey3);
            this.gbxKeys.Location = new System.Drawing.Point(241, 6);
            this.gbxKeys.Name = "gbxKeys";
            this.gbxKeys.Size = new System.Drawing.Size(153, 128);
            this.gbxKeys.TabIndex = 4;
            this.gbxKeys.TabStop = false;
            this.gbxKeys.Text = "Keys";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Key3:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Key2:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Key1:";
            // 
            // cbxCreateBackup
            // 
            this.cbxCreateBackup.AutoSize = true;
            this.cbxCreateBackup.Checked = true;
            this.cbxCreateBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxCreateBackup.Location = new System.Drawing.Point(6, 24);
            this.cbxCreateBackup.Name = "cbxCreateBackup";
            this.cbxCreateBackup.Size = new System.Drawing.Size(97, 17);
            this.cbxCreateBackup.TabIndex = 0;
            this.cbxCreateBackup.Text = "Create Backup";
            this.toolTip.SetToolTip(this.cbxCreateBackup, "Creates a backup before any modifications will be applied.");
            this.cbxCreateBackup.UseVisualStyleBackColor = true;
            // 
            // gbxOptions
            // 
            this.gbxOptions.Controls.Add(this.cbxSkipClientVersion);
            this.gbxOptions.Controls.Add(this.cbxSkipKOValidation);
            this.gbxOptions.Controls.Add(this.cbxCreateBackup);
            this.gbxOptions.Location = new System.Drawing.Point(403, 6);
            this.gbxOptions.Name = "gbxOptions";
            this.gbxOptions.Size = new System.Drawing.Size(152, 128);
            this.gbxOptions.TabIndex = 5;
            this.gbxOptions.TabStop = false;
            this.gbxOptions.Text = "Options";
            // 
            // cbxSkipClientVersion
            // 
            this.cbxSkipClientVersion.AutoSize = true;
            this.cbxSkipClientVersion.Location = new System.Drawing.Point(6, 71);
            this.cbxSkipClientVersion.Name = "cbxSkipClientVersion";
            this.cbxSkipClientVersion.Size = new System.Drawing.Size(114, 17);
            this.cbxSkipClientVersion.TabIndex = 2;
            this.cbxSkipClientVersion.Text = "Skip Client Version";
            this.toolTip.SetToolTip(this.cbxSkipClientVersion, "Check this to prevent the parser from trying to retrieve the internal client vers" +
        "ion. Only use if it failed loading the target exe.");
            this.cbxSkipClientVersion.UseVisualStyleBackColor = true;
            // 
            // cbxSkipKOValidation
            // 
            this.cbxSkipKOValidation.AutoSize = true;
            this.cbxSkipKOValidation.Location = new System.Drawing.Point(6, 47);
            this.cbxSkipKOValidation.Name = "cbxSkipKOValidation";
            this.cbxSkipKOValidation.Size = new System.Drawing.Size(114, 17);
            this.cbxSkipKOValidation.TabIndex = 1;
            this.cbxSkipKOValidation.Text = "Skip KO Validation";
            this.toolTip.SetToolTip(this.cbxSkipKOValidation, "Check this to prevent the parser from trying to retrieve the internal window name" +
        " \"Knight OnLine Client\". Only use if it failed loading the target exe.");
            this.cbxSkipKOValidation.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // btnGeneralReport
            // 
            this.btnGeneralReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneralReport.Enabled = false;
            this.btnGeneralReport.Location = new System.Drawing.Point(241, 181);
            this.btnGeneralReport.Name = "btnGeneralReport";
            this.btnGeneralReport.Size = new System.Drawing.Size(153, 32);
            this.btnGeneralReport.TabIndex = 1;
            this.btnGeneralReport.Text = "Show General Report";
            this.toolTip.SetToolTip(this.btnGeneralReport, "View general information and all keys that were found from the target executable." +
        "");
            this.btnGeneralReport.UseVisualStyleBackColor = true;
            this.btnGeneralReport.Click += new System.EventHandler(this.btnViewOffsets_Click);
            // 
            // btnUpdateData
            // 
            this.btnUpdateData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateData.Enabled = false;
            this.btnUpdateData.Location = new System.Drawing.Point(402, 143);
            this.btnUpdateData.Name = "btnUpdateData";
            this.btnUpdateData.Size = new System.Drawing.Size(153, 32);
            this.btnUpdateData.TabIndex = 3;
            this.btnUpdateData.Text = "Update Data Encryption";
            this.toolTip.SetToolTip(this.btnUpdateData, "Batch update your current TBLs to match with your executable new encryption.");
            this.btnUpdateData.UseVisualStyleBackColor = true;
            this.btnUpdateData.Click += new System.EventHandler(this.btnUpdateData_Click);
            // 
            // panelDragArea
            // 
            this.panelDragArea.AllowDrop = true;
            this.panelDragArea.BackgroundImage = global::KO.TBLCryptoEditor.Properties.Resources.ko4life;
            this.panelDragArea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDragArea.Location = new System.Drawing.Point(9, 12);
            this.panelDragArea.Name = "panelDragArea";
            this.panelDragArea.Size = new System.Drawing.Size(226, 201);
            this.panelDragArea.TabIndex = 5;
            // 
            // tbxKey1
            // 
            this.tbxKey1.BackColor = System.Drawing.SystemColors.Control;
            this.tbxKey1.Location = new System.Drawing.Point(43, 21);
            this.tbxKey1.Name = "tbxKey1";
            this.tbxKey1.ReadOnly = true;
            this.tbxKey1.Size = new System.Drawing.Size(48, 20);
            this.tbxKey1.TabIndex = 0;
            this.tbxKey1.Text = "0xFFFF";
            // 
            // tbxKey2
            // 
            this.tbxKey2.BackColor = System.Drawing.SystemColors.Control;
            this.tbxKey2.Location = new System.Drawing.Point(43, 47);
            this.tbxKey2.Name = "tbxKey2";
            this.tbxKey2.ReadOnly = true;
            this.tbxKey2.Size = new System.Drawing.Size(48, 20);
            this.tbxKey2.TabIndex = 1;
            this.tbxKey2.Text = "0xFFFF";
            // 
            // tbxKey3
            // 
            this.tbxKey3.BackColor = System.Drawing.SystemColors.Control;
            this.tbxKey3.Location = new System.Drawing.Point(43, 73);
            this.tbxKey3.Name = "tbxKey3";
            this.tbxKey3.ReadOnly = true;
            this.tbxKey3.Size = new System.Drawing.Size(48, 20);
            this.tbxKey3.TabIndex = 2;
            this.tbxKey3.Text = "0xFFFF";
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 245);
            this.Controls.Add(this.gbxOptions);
            this.Controls.Add(this.gbxKeys);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panelDragArea);
            this.Controls.Add(this.btnGeneralReport);
            this.Controls.Add(this.btnUpdateData);
            this.Controls.Add(this.btnUpdateClient);
            this.Controls.Add(this.btnRandomKeys);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::KO.TBLCryptoEditor.Properties.Resources.app;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Knight OnLine Table Encryption Editor";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.gbxKeys.ResumeLayout(false);
            this.gbxKeys.PerformLayout();
            this.gbxOptions.ResumeLayout(false);
            this.gbxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRandomKeys;
        private System.Windows.Forms.Button btnUpdateClient;
        private Controls.CryptoKeyInput tbxKey1;
        private Controls.CryptoKeyInput tbxKey2;
        private Controls.CryptoKeyInput tbxKey3;
        private System.Windows.Forms.Panel panelDragArea;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.CheckBox cbxManualUpdate;
        private System.Windows.Forms.GroupBox gbxKeys;
        private System.Windows.Forms.CheckBox cbxCreateBackup;
        private System.Windows.Forms.GroupBox gbxOptions;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnGeneralReport;
        private System.Windows.Forms.CheckBox cbxSkipKOValidation;
        private System.Windows.Forms.CheckBox cbxSkipClientVersion;
        private System.Windows.Forms.Button btnUpdateData;
    }
}

