namespace LOKI.Forms
{
    partial class Admin
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
            this.AdminPanel = new System.Windows.Forms.Panel();
            this.SpecialGroupBox = new System.Windows.Forms.GroupBox();
            this.ExcelExportButton = new System.Windows.Forms.Button();
            this.BatchPrintButton = new System.Windows.Forms.Button();
            this.NewInventoryButton = new System.Windows.Forms.Button();
            this.IOGroupBox = new System.Windows.Forms.GroupBox();
            this.ExportPAMSButton = new System.Windows.Forms.Button();
            this.ExportR14Button = new System.Windows.Forms.Button();
            this.ImportPAMSButton = new System.Windows.Forms.Button();
            this.ImportR14Button = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.WaitLabel = new System.Windows.Forms.Label();
            this.PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.AdminPanel.SuspendLayout();
            this.SpecialGroupBox.SuspendLayout();
            this.IOGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AdminPanel
            // 
            this.AdminPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AdminPanel.Controls.Add(this.SpecialGroupBox);
            this.AdminPanel.Controls.Add(this.IOGroupBox);
            this.AdminPanel.Controls.Add(this.CloseButton);
            this.AdminPanel.Controls.Add(this.WaitLabel);
            this.AdminPanel.Location = new System.Drawing.Point(12, 12);
            this.AdminPanel.Name = "AdminPanel";
            this.AdminPanel.Size = new System.Drawing.Size(305, 258);
            this.AdminPanel.TabIndex = 0;
            // 
            // SpecialGroupBox
            // 
            this.SpecialGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SpecialGroupBox.Controls.Add(this.ExcelExportButton);
            this.SpecialGroupBox.Controls.Add(this.BatchPrintButton);
            this.SpecialGroupBox.Controls.Add(this.NewInventoryButton);
            this.SpecialGroupBox.Location = new System.Drawing.Point(14, 100);
            this.SpecialGroupBox.Name = "SpecialGroupBox";
            this.SpecialGroupBox.Size = new System.Drawing.Size(276, 81);
            this.SpecialGroupBox.TabIndex = 1;
            this.SpecialGroupBox.TabStop = false;
            this.SpecialGroupBox.Text = "Special";
            // 
            // ExcelExportButton
            // 
            this.ExcelExportButton.Location = new System.Drawing.Point(143, 19);
            this.ExcelExportButton.Name = "ExcelExportButton";
            this.ExcelExportButton.Size = new System.Drawing.Size(95, 23);
            this.ExcelExportButton.TabIndex = 1;
            this.ExcelExportButton.Text = "Excel Export";
            this.ExcelExportButton.UseVisualStyleBackColor = true;
            this.ExcelExportButton.Click += new System.EventHandler(this.ExcelExportButton_Click);
            // 
            // BatchPrintButton
            // 
            this.BatchPrintButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.BatchPrintButton.Location = new System.Drawing.Point(42, 19);
            this.BatchPrintButton.Name = "BatchPrintButton";
            this.BatchPrintButton.Size = new System.Drawing.Size(95, 23);
            this.BatchPrintButton.TabIndex = 0;
            this.BatchPrintButton.Text = "Batch Print";
            this.BatchPrintButton.UseVisualStyleBackColor = true;
            this.BatchPrintButton.Click += new System.EventHandler(this.BatchPrintButton_Click);
            // 
            // NewInventoryButton
            // 
            this.NewInventoryButton.Location = new System.Drawing.Point(93, 48);
            this.NewInventoryButton.Name = "NewInventoryButton";
            this.NewInventoryButton.Size = new System.Drawing.Size(95, 23);
            this.NewInventoryButton.TabIndex = 2;
            this.NewInventoryButton.Text = "New Inventory";
            this.NewInventoryButton.UseVisualStyleBackColor = true;
            this.NewInventoryButton.Click += new System.EventHandler(this.NewInventoryButton_Click);
            // 
            // IOGroupBox
            // 
            this.IOGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.IOGroupBox.Controls.Add(this.ExportPAMSButton);
            this.IOGroupBox.Controls.Add(this.ExportR14Button);
            this.IOGroupBox.Controls.Add(this.ImportPAMSButton);
            this.IOGroupBox.Controls.Add(this.ImportR14Button);
            this.IOGroupBox.Location = new System.Drawing.Point(14, 11);
            this.IOGroupBox.Name = "IOGroupBox";
            this.IOGroupBox.Size = new System.Drawing.Size(276, 83);
            this.IOGroupBox.TabIndex = 0;
            this.IOGroupBox.TabStop = false;
            this.IOGroupBox.Text = "Import/Export";
            // 
            // ExportPAMSButton
            // 
            this.ExportPAMSButton.Location = new System.Drawing.Point(42, 19);
            this.ExportPAMSButton.Name = "ExportPAMSButton";
            this.ExportPAMSButton.Size = new System.Drawing.Size(95, 23);
            this.ExportPAMSButton.TabIndex = 0;
            this.ExportPAMSButton.Text = "Export PAMS";
            this.ExportPAMSButton.UseVisualStyleBackColor = true;
            this.ExportPAMSButton.Click += new System.EventHandler(this.ExportPAMSButton_Click);
            // 
            // ExportR14Button
            // 
            this.ExportR14Button.Location = new System.Drawing.Point(143, 19);
            this.ExportR14Button.Name = "ExportR14Button";
            this.ExportR14Button.Size = new System.Drawing.Size(95, 23);
            this.ExportR14Button.TabIndex = 1;
            this.ExportR14Button.Text = "Export R14";
            this.ExportR14Button.UseVisualStyleBackColor = true;
            this.ExportR14Button.Click += new System.EventHandler(this.ExportR14Button_Click);
            // 
            // ImportPAMSButton
            // 
            this.ImportPAMSButton.Location = new System.Drawing.Point(42, 48);
            this.ImportPAMSButton.Name = "ImportPAMSButton";
            this.ImportPAMSButton.Size = new System.Drawing.Size(95, 23);
            this.ImportPAMSButton.TabIndex = 2;
            this.ImportPAMSButton.Text = "Import PAMS";
            this.ImportPAMSButton.UseVisualStyleBackColor = true;
            this.ImportPAMSButton.Click += new System.EventHandler(this.ImportPAMSButton_Click);
            // 
            // ImportR14Button
            // 
            this.ImportR14Button.Location = new System.Drawing.Point(143, 48);
            this.ImportR14Button.Name = "ImportR14Button";
            this.ImportR14Button.Size = new System.Drawing.Size(95, 23);
            this.ImportR14Button.TabIndex = 3;
            this.ImportR14Button.Text = "Import R14";
            this.ImportR14Button.UseVisualStyleBackColor = true;
            this.ImportR14Button.Click += new System.EventHandler(this.ImportR14Button_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(107, 215);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(95, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.AdminCancelButton_Click);
            // 
            // WaitLabel
            // 
            this.WaitLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.WaitLabel.AutoSize = true;
            this.WaitLabel.Location = new System.Drawing.Point(115, 94);
            this.WaitLabel.Name = "WaitLabel";
            this.WaitLabel.Size = new System.Drawing.Size(73, 13);
            this.WaitLabel.TabIndex = 1;
            this.WaitLabel.Text = "Please Wait...";
            this.WaitLabel.Visible = false;
            // 
            // Admin
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 282);
            this.ControlBox = false;
            this.Controls.Add(this.AdminPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Admin";
            this.ShowInTaskbar = false;
            this.Text = "Admin Control Panel";
            this.AdminPanel.ResumeLayout(false);
            this.AdminPanel.PerformLayout();
            this.SpecialGroupBox.ResumeLayout(false);
            this.IOGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel AdminPanel;
        private System.Windows.Forms.GroupBox SpecialGroupBox;
        private System.Windows.Forms.Button ExcelExportButton;
        private System.Windows.Forms.Button BatchPrintButton;
        private System.Windows.Forms.Button NewInventoryButton;
        private System.Windows.Forms.GroupBox IOGroupBox;
        private System.Windows.Forms.Button ExportPAMSButton;
        private System.Windows.Forms.Button ExportR14Button;
        private System.Windows.Forms.Button ImportPAMSButton;
        private System.Windows.Forms.Button ImportR14Button;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label WaitLabel;
        private System.Drawing.Printing.PrintDocument PrintDocument;
    }
}