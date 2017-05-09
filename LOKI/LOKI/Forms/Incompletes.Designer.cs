namespace LOKI.Forms
{
    partial class Incompletes
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
            this.IncompletePanel = new System.Windows.Forms.Panel();
            this.IncompleteDatagridview = new System.Windows.Forms.DataGridView();
            this.NSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InUse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Found = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncompleteTotalLabel = new System.Windows.Forms.Label();
            this.IncompleteTotalTextbox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.IncompletePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IncompleteDatagridview)).BeginInit();
            this.SuspendLayout();
            // 
            // IncompletePanel
            // 
            this.IncompletePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IncompletePanel.Controls.Add(this.IncompleteDatagridview);
            this.IncompletePanel.Controls.Add(this.IncompleteTotalLabel);
            this.IncompletePanel.Controls.Add(this.IncompleteTotalTextbox);
            this.IncompletePanel.Controls.Add(this.CloseButton);
            this.IncompletePanel.Location = new System.Drawing.Point(12, 12);
            this.IncompletePanel.Name = "IncompletePanel";
            this.IncompletePanel.Size = new System.Drawing.Size(305, 259);
            this.IncompletePanel.TabIndex = 49;
            // 
            // IncompleteDatagridview
            // 
            this.IncompleteDatagridview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IncompleteDatagridview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.IncompleteDatagridview.BackgroundColor = System.Drawing.SystemColors.Window;
            this.IncompleteDatagridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IncompleteDatagridview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NSN,
            this.InUse,
            this.Found});
            this.IncompleteDatagridview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.IncompleteDatagridview.Location = new System.Drawing.Point(13, 38);
            this.IncompleteDatagridview.Name = "IncompleteDatagridview";
            this.IncompleteDatagridview.RowHeadersVisible = false;
            this.IncompleteDatagridview.Size = new System.Drawing.Size(280, 170);
            this.IncompleteDatagridview.TabIndex = 4;
            // 
            // NSN
            // 
            this.NSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NSN.FillWeight = 50F;
            this.NSN.HeaderText = "NSN";
            this.NSN.Name = "NSN";
            // 
            // InUse
            // 
            this.InUse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InUse.FillWeight = 25F;
            this.InUse.HeaderText = "In Use";
            this.InUse.Name = "InUse";
            // 
            // Found
            // 
            this.Found.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Found.FillWeight = 25F;
            this.Found.HeaderText = "Found";
            this.Found.Name = "Found";
            // 
            // IncompleteTotalLabel
            // 
            this.IncompleteTotalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.IncompleteTotalLabel.AutoSize = true;
            this.IncompleteTotalLabel.Location = new System.Drawing.Point(100, 15);
            this.IncompleteTotalLabel.Name = "IncompleteTotalLabel";
            this.IncompleteTotalLabel.Size = new System.Drawing.Size(31, 13);
            this.IncompleteTotalLabel.TabIndex = 3;
            this.IncompleteTotalLabel.Text = "Total";
            // 
            // IncompleteTotalTextbox
            // 
            this.IncompleteTotalTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.IncompleteTotalTextbox.Location = new System.Drawing.Point(137, 12);
            this.IncompleteTotalTextbox.Name = "IncompleteTotalTextbox";
            this.IncompleteTotalTextbox.ReadOnly = true;
            this.IncompleteTotalTextbox.Size = new System.Drawing.Size(59, 20);
            this.IncompleteTotalTextbox.TabIndex = 2;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(114, 216);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Incompletes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 283);
            this.ControlBox = false;
            this.Controls.Add(this.IncompletePanel);
            this.Name = "Incompletes";
            this.Text = "Incompletes";
            this.IncompletePanel.ResumeLayout(false);
            this.IncompletePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IncompleteDatagridview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel IncompletePanel;
        private System.Windows.Forms.DataGridView IncompleteDatagridview;
        private System.Windows.Forms.Label IncompleteTotalLabel;
        private System.Windows.Forms.TextBox IncompleteTotalTextbox;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn NSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn InUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Found;
    }
}