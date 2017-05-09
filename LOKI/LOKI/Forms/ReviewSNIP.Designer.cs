namespace LOKI.Forms
{
    partial class ReviewSNIP
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
            this.ReviewSNIPPanel = new System.Windows.Forms.Panel();
            this.SNIPDatagridview = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nomenclature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SNIPTotalLabel = new System.Windows.Forms.Label();
            this.SNIPTotalTextbox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ReviewSNIPPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SNIPDatagridview)).BeginInit();
            this.SuspendLayout();
            // 
            // ReviewSNIPPanel
            // 
            this.ReviewSNIPPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReviewSNIPPanel.Controls.Add(this.SNIPDatagridview);
            this.ReviewSNIPPanel.Controls.Add(this.SNIPTotalLabel);
            this.ReviewSNIPPanel.Controls.Add(this.SNIPTotalTextbox);
            this.ReviewSNIPPanel.Controls.Add(this.CloseButton);
            this.ReviewSNIPPanel.Location = new System.Drawing.Point(12, 12);
            this.ReviewSNIPPanel.Name = "ReviewSNIPPanel";
            this.ReviewSNIPPanel.Size = new System.Drawing.Size(403, 266);
            this.ReviewSNIPPanel.TabIndex = 50;
            // 
            // SNIPDatagridview
            // 
            this.SNIPDatagridview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SNIPDatagridview.BackgroundColor = System.Drawing.SystemColors.Window;
            this.SNIPDatagridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Nomenclature});
            this.SNIPDatagridview.Location = new System.Drawing.Point(14, 38);
            this.SNIPDatagridview.Name = "SNIPDatagridview";
            this.SNIPDatagridview.RowHeadersVisible = false;
            this.SNIPDatagridview.Size = new System.Drawing.Size(377, 177);
            this.SNIPDatagridview.TabIndex = 4;
            this.SNIPDatagridview.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.SNIPDatagridview_CellValueChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 20F;
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 20F;
            this.dataGridViewTextBoxColumn2.HeaderText = "PN";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.FillWeight = 20F;
            this.dataGridViewTextBoxColumn3.HeaderText = "SN";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // Nomenclature
            // 
            this.Nomenclature.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nomenclature.FillWeight = 40F;
            this.Nomenclature.HeaderText = "Nomenclature";
            this.Nomenclature.Name = "Nomenclature";
            // 
            // SNIPTotalLabel
            // 
            this.SNIPTotalLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SNIPTotalLabel.AutoSize = true;
            this.SNIPTotalLabel.Location = new System.Drawing.Point(147, 15);
            this.SNIPTotalLabel.Name = "SNIPTotalLabel";
            this.SNIPTotalLabel.Size = new System.Drawing.Size(31, 13);
            this.SNIPTotalLabel.TabIndex = 3;
            this.SNIPTotalLabel.Text = "Total";
            // 
            // SNIPTotalTextbox
            // 
            this.SNIPTotalTextbox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.SNIPTotalTextbox.Location = new System.Drawing.Point(184, 12);
            this.SNIPTotalTextbox.Name = "SNIPTotalTextbox";
            this.SNIPTotalTextbox.ReadOnly = true;
            this.SNIPTotalTextbox.Size = new System.Drawing.Size(59, 20);
            this.SNIPTotalTextbox.TabIndex = 2;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CloseButton.Location = new System.Drawing.Point(158, 221);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ReviewSNIP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 290);
            this.ControlBox = false;
            this.Controls.Add(this.ReviewSNIPPanel);
            this.Name = "ReviewSNIP";
            this.ShowInTaskbar = false;
            this.Text = "SNIP Table Viewer";
            this.ReviewSNIPPanel.ResumeLayout(false);
            this.ReviewSNIPPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SNIPDatagridview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ReviewSNIPPanel;
        private System.Windows.Forms.DataGridView SNIPDatagridview;
        private System.Windows.Forms.Label SNIPTotalLabel;
        private System.Windows.Forms.TextBox SNIPTotalTextbox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nomenclature;
    }
}