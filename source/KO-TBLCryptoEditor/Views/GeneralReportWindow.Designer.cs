namespace KO.TBLCryptoEditor.Views
{
    partial class GeneralReportWindow
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
            this.view = new System.Windows.Forms.WebBrowser();
            this.btnCopyClipboard = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.LayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.layoutButtons = new System.Windows.Forms.TableLayoutPanel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.LayoutMain.SuspendLayout();
            this.layoutButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // view
            // 
            this.view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view.Location = new System.Drawing.Point(101, 3);
            this.view.MinimumSize = new System.Drawing.Size(20, 20);
            this.view.Name = "view";
            this.view.Size = new System.Drawing.Size(667, 380);
            this.view.TabIndex = 0;
            // 
            // btnCopyClipboard
            // 
            this.btnCopyClipboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(94)))), ((int)(((byte)(84)))));
            this.btnCopyClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCopyClipboard.FlatAppearance.BorderSize = 0;
            this.btnCopyClipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyClipboard.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyClipboard.ForeColor = System.Drawing.Color.White;
            this.btnCopyClipboard.Location = new System.Drawing.Point(1, 123);
            this.btnCopyClipboard.Margin = new System.Windows.Forms.Padding(1);
            this.btnCopyClipboard.Name = "btnCopyClipboard";
            this.btnCopyClipboard.Size = new System.Drawing.Size(90, 44);
            this.btnCopyClipboard.TabIndex = 0;
            this.btnCopyClipboard.Text = "Copy to Clipboard";
            this.btnCopyClipboard.UseVisualStyleBackColor = false;
            this.btnCopyClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(94)))), ((int)(((byte)(84)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(1, 215);
            this.btnExit.Margin = new System.Windows.Forms.Padding(1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 42);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(94)))), ((int)(((byte)(84)))));
            this.btnSaveToFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveToFile.FlatAppearance.BorderSize = 0;
            this.btnSaveToFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveToFile.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveToFile.ForeColor = System.Drawing.Color.White;
            this.btnSaveToFile.Location = new System.Drawing.Point(1, 169);
            this.btnSaveToFile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(90, 44);
            this.btnSaveToFile.TabIndex = 0;
            this.btnSaveToFile.Text = "Save to File";
            this.btnSaveToFile.UseVisualStyleBackColor = false;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // LayoutMain
            // 
            this.LayoutMain.ColumnCount = 2;
            this.LayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.LayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutMain.Controls.Add(this.view, 1, 0);
            this.LayoutMain.Controls.Add(this.layoutButtons, 0, 0);
            this.LayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayoutMain.Location = new System.Drawing.Point(0, 0);
            this.LayoutMain.Name = "LayoutMain";
            this.LayoutMain.RowCount = 1;
            this.LayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LayoutMain.Size = new System.Drawing.Size(771, 386);
            this.LayoutMain.TabIndex = 1;
            // 
            // layoutButtons
            // 
            this.layoutButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(211)))), ((int)(((byte)(102)))));
            this.layoutButtons.ColumnCount = 1;
            this.layoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.layoutButtons.Controls.Add(this.btnExit, 0, 3);
            this.layoutButtons.Controls.Add(this.btnSaveToFile, 0, 2);
            this.layoutButtons.Controls.Add(this.btnCopyClipboard, 0, 1);
            this.layoutButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutButtons.Location = new System.Drawing.Point(3, 3);
            this.layoutButtons.Name = "layoutButtons";
            this.layoutButtons.RowCount = 5;
            this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.layoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutButtons.Size = new System.Drawing.Size(92, 380);
            this.layoutButtons.TabIndex = 1;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 386);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(771, 22);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusStrip1";
            // 
            // GeneralReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 408);
            this.Controls.Add(this.LayoutMain);
            this.Controls.Add(this.statusBar);
            this.Icon = global::KO.TBLCryptoEditor.Properties.Resources.app;
            this.MinimumSize = new System.Drawing.Size(787, 447);
            this.Name = "GeneralReportWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Report";
            this.Load += new System.EventHandler(this.ViewOffsetsWindow_Load);
            this.LayoutMain.ResumeLayout(false);
            this.layoutButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCopyClipboard;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.WebBrowser view;
        private System.Windows.Forms.TableLayoutPanel LayoutMain;
        private System.Windows.Forms.TableLayoutPanel layoutButtons;
        private System.Windows.Forms.StatusStrip statusBar;
    }
}