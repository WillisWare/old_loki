namespace LOKI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.NewQueryButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.LogoImage = new System.Windows.Forms.PictureBox();
            this.NSNLabel = new System.Windows.Forms.Label();
            this.NSNComboBox = new System.Windows.Forms.ComboBox();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.PriceTextbox = new System.Windows.Forms.TextBox();
            this.PSMLabel = new System.Windows.Forms.Label();
            this.PSMTextbox = new System.Windows.Forms.TextBox();
            this.ASCLabel = new System.Windows.Forms.Label();
            this.ASCTextbox = new System.Windows.Forms.TextBox();
            this.DOCLabel = new System.Windows.Forms.Label();
            this.DOCTextbox = new System.Windows.Forms.TextBox();
            this.AuthLabel = new System.Windows.Forms.Label();
            this.AuthTextbox = new System.Windows.Forms.TextBox();
            this.InUseLabel = new System.Windows.Forms.Label();
            this.InUseTextbox = new System.Windows.Forms.TextBox();
            this.IncompleteEntriesButton = new System.Windows.Forms.Button();
            this.AddNewIDButton = new System.Windows.Forms.Button();
            this.DeleteIDButton = new System.Windows.Forms.Button();
            this.IDsLabel = new System.Windows.Forms.Label();
            this.IDsListbox = new System.Windows.Forms.ListBox();
            this.TotalFoundLabel = new System.Windows.Forms.Label();
            this.TotalFoundTextbox = new System.Windows.Forms.TextBox();
            this.AutoScanButton = new System.Windows.Forms.Button();
            this.NomenclatureLabel = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.PartNumberLabel = new System.Windows.Forms.Label();
            this.SerialNumberLabel = new System.Windows.Forms.Label();
            this.NomenclatureTextbox = new System.Windows.Forms.TextBox();
            this.LocationTextbox = new System.Windows.Forms.TextBox();
            this.PartNumberTextbox = new System.Windows.Forms.TextBox();
            this.SerialNumberTextbox = new System.Windows.Forms.TextBox();
            this.NotesLabel = new System.Windows.Forms.Label();
            this.PAMSNotesTextbox = new System.Windows.Forms.TextBox();
            this.R14NotesTextbox = new System.Windows.Forms.TextBox();
            this.AdministratorButton = new System.Windows.Forms.Button();
            this.SNIPTableButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.QueryPreviousButton = new System.Windows.Forms.Button();
            this.InventoryNameLabel = new System.Windows.Forms.Label();
            this.QueryingFromLabel = new System.Windows.Forms.Label();
            this.PrintingNotesLabel = new System.Windows.Forms.Label();
            this.PrintCardButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.CompletedCheckbox = new System.Windows.Forms.CheckBox();
            this.CompletedLabel = new System.Windows.Forms.Label();
            this.DefaultNotesLandmark = new System.Windows.Forms.CheckBox();
            this.PrintNotesLandmark = new System.Windows.Forms.CheckBox();
            this.PrintNotesSize = new System.Windows.Forms.CheckBox();
            this.ItemsDataGridView = new System.Windows.Forms.DataGridView();
            this.IDNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Notes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SNIPLandmark = new System.Windows.Forms.CheckBox();
            this.PrintDocument = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.LogoImage)).BeginInit();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // NewQueryButton
            // 
            this.NewQueryButton.Location = new System.Drawing.Point(21, 66);
            this.NewQueryButton.Name = "NewQueryButton";
            this.NewQueryButton.Size = new System.Drawing.Size(85, 23);
            this.NewQueryButton.TabIndex = 0;
            this.NewQueryButton.Text = "New Query";
            this.NewQueryButton.UseVisualStyleBackColor = true;
            this.NewQueryButton.Click += new System.EventHandler(this.NewQueryButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.TitleLabel.Location = new System.Drawing.Point(86, 12);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(585, 29);
            this.TitleLabel.TabIndex = 3;
            this.TitleLabel.Text = "Logistical Organizer for Keeping Inventory (LOKI)";
            // 
            // LogoImage
            // 
            this.LogoImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LogoImage.BackgroundImage")));
            this.LogoImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LogoImage.Location = new System.Drawing.Point(583, 44);
            this.LogoImage.Name = "LogoImage";
            this.LogoImage.Size = new System.Drawing.Size(143, 163);
            this.LogoImage.TabIndex = 4;
            this.LogoImage.TabStop = false;
            // 
            // NSNLabel
            // 
            this.NSNLabel.AutoSize = true;
            this.NSNLabel.Location = new System.Drawing.Point(121, 71);
            this.NSNLabel.Name = "NSNLabel";
            this.NSNLabel.Size = new System.Drawing.Size(30, 13);
            this.NSNLabel.TabIndex = 5;
            this.NSNLabel.Text = "NSN";
            // 
            // NSNComboBox
            // 
            this.NSNComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NSNComboBox.FormattingEnabled = true;
            this.NSNComboBox.Location = new System.Drawing.Point(157, 68);
            this.NSNComboBox.Name = "NSNComboBox";
            this.NSNComboBox.Size = new System.Drawing.Size(199, 21);
            this.NSNComboBox.TabIndex = 6;
            this.NSNComboBox.SelectedIndexChanged += new System.EventHandler(this.NSNComboBox_SelectedIndexChanged);
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(394, 71);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(31, 13);
            this.PriceLabel.TabIndex = 7;
            this.PriceLabel.Text = "Price";
            // 
            // PriceTextbox
            // 
            this.PriceTextbox.Location = new System.Drawing.Point(431, 68);
            this.PriceTextbox.Name = "PriceTextbox";
            this.PriceTextbox.ReadOnly = true;
            this.PriceTextbox.Size = new System.Drawing.Size(133, 20);
            this.PriceTextbox.TabIndex = 8;
            // 
            // PSMLabel
            // 
            this.PSMLabel.AutoSize = true;
            this.PSMLabel.Location = new System.Drawing.Point(395, 98);
            this.PSMLabel.Name = "PSMLabel";
            this.PSMLabel.Size = new System.Drawing.Size(30, 13);
            this.PSMLabel.TabIndex = 9;
            this.PSMLabel.Text = "PSM";
            // 
            // PSMTextbox
            // 
            this.PSMTextbox.Location = new System.Drawing.Point(431, 95);
            this.PSMTextbox.Name = "PSMTextbox";
            this.PSMTextbox.ReadOnly = true;
            this.PSMTextbox.Size = new System.Drawing.Size(28, 20);
            this.PSMTextbox.TabIndex = 10;
            // 
            // ASCLabel
            // 
            this.ASCLabel.AutoSize = true;
            this.ASCLabel.Location = new System.Drawing.Point(465, 98);
            this.ASCLabel.Name = "ASCLabel";
            this.ASCLabel.Size = new System.Drawing.Size(28, 13);
            this.ASCLabel.TabIndex = 11;
            this.ASCLabel.Text = "ASC";
            // 
            // ASCTextbox
            // 
            this.ASCTextbox.Location = new System.Drawing.Point(499, 95);
            this.ASCTextbox.Name = "ASCTextbox";
            this.ASCTextbox.ReadOnly = true;
            this.ASCTextbox.Size = new System.Drawing.Size(65, 20);
            this.ASCTextbox.TabIndex = 12;
            // 
            // DOCLabel
            // 
            this.DOCLabel.AutoSize = true;
            this.DOCLabel.Location = new System.Drawing.Point(121, 98);
            this.DOCLabel.Name = "DOCLabel";
            this.DOCLabel.Size = new System.Drawing.Size(30, 13);
            this.DOCLabel.TabIndex = 13;
            this.DOCLabel.Text = "DOC";
            // 
            // DOCTextbox
            // 
            this.DOCTextbox.Location = new System.Drawing.Point(157, 95);
            this.DOCTextbox.Name = "DOCTextbox";
            this.DOCTextbox.ReadOnly = true;
            this.DOCTextbox.Size = new System.Drawing.Size(50, 20);
            this.DOCTextbox.TabIndex = 14;
            // 
            // AuthLabel
            // 
            this.AuthLabel.AutoSize = true;
            this.AuthLabel.Location = new System.Drawing.Point(213, 98);
            this.AuthLabel.Name = "AuthLabel";
            this.AuthLabel.Size = new System.Drawing.Size(29, 13);
            this.AuthLabel.TabIndex = 15;
            this.AuthLabel.Text = "Auth";
            // 
            // AuthTextbox
            // 
            this.AuthTextbox.Location = new System.Drawing.Point(248, 95);
            this.AuthTextbox.Name = "AuthTextbox";
            this.AuthTextbox.ReadOnly = true;
            this.AuthTextbox.Size = new System.Drawing.Size(29, 20);
            this.AuthTextbox.TabIndex = 16;
            // 
            // InUseLabel
            // 
            this.InUseLabel.AutoSize = true;
            this.InUseLabel.Location = new System.Drawing.Point(283, 98);
            this.InUseLabel.Name = "InUseLabel";
            this.InUseLabel.Size = new System.Drawing.Size(38, 13);
            this.InUseLabel.TabIndex = 17;
            this.InUseLabel.Text = "In Use";
            // 
            // InUseTextbox
            // 
            this.InUseTextbox.Location = new System.Drawing.Point(327, 95);
            this.InUseTextbox.Name = "InUseTextbox";
            this.InUseTextbox.ReadOnly = true;
            this.InUseTextbox.Size = new System.Drawing.Size(29, 20);
            this.InUseTextbox.TabIndex = 18;
            // 
            // IncompleteEntriesButton
            // 
            this.IncompleteEntriesButton.Location = new System.Drawing.Point(21, 162);
            this.IncompleteEntriesButton.Name = "IncompleteEntriesButton";
            this.IncompleteEntriesButton.Size = new System.Drawing.Size(130, 23);
            this.IncompleteEntriesButton.TabIndex = 19;
            this.IncompleteEntriesButton.Text = "Incomplete Entries";
            this.IncompleteEntriesButton.UseVisualStyleBackColor = true;
            this.IncompleteEntriesButton.Click += new System.EventHandler(this.IncompleteEntriesButton_Click);
            // 
            // AddNewIDButton
            // 
            this.AddNewIDButton.Location = new System.Drawing.Point(21, 261);
            this.AddNewIDButton.Name = "AddNewIDButton";
            this.AddNewIDButton.Size = new System.Drawing.Size(85, 23);
            this.AddNewIDButton.TabIndex = 20;
            this.AddNewIDButton.Text = "Add New ID";
            this.AddNewIDButton.UseVisualStyleBackColor = true;
            this.AddNewIDButton.Click += new System.EventHandler(this.AddNewIDButton_Click);
            // 
            // DeleteIDButton
            // 
            this.DeleteIDButton.Location = new System.Drawing.Point(21, 290);
            this.DeleteIDButton.Name = "DeleteIDButton";
            this.DeleteIDButton.Size = new System.Drawing.Size(85, 23);
            this.DeleteIDButton.TabIndex = 21;
            this.DeleteIDButton.Text = "Delete ID";
            this.DeleteIDButton.UseVisualStyleBackColor = true;
            this.DeleteIDButton.Click += new System.EventHandler(this.DeleteIDButton_Click);
            // 
            // IDsLabel
            // 
            this.IDsLabel.AutoSize = true;
            this.IDsLabel.Location = new System.Drawing.Point(128, 266);
            this.IDsLabel.Name = "IDsLabel";
            this.IDsLabel.Size = new System.Drawing.Size(23, 13);
            this.IDsLabel.TabIndex = 22;
            this.IDsLabel.Text = "IDs";
            // 
            // IDsListbox
            // 
            this.IDsListbox.FormattingEnabled = true;
            this.IDsListbox.Location = new System.Drawing.Point(157, 261);
            this.IDsListbox.Name = "IDsListbox";
            this.IDsListbox.Size = new System.Drawing.Size(85, 121);
            this.IDsListbox.TabIndex = 23;
            this.IDsListbox.SelectedIndexChanged += new System.EventHandler(this.IDsListbox_SelectedIndexChanged);
            // 
            // TotalFoundLabel
            // 
            this.TotalFoundLabel.AutoSize = true;
            this.TotalFoundLabel.Location = new System.Drawing.Point(128, 238);
            this.TotalFoundLabel.Name = "TotalFoundLabel";
            this.TotalFoundLabel.Size = new System.Drawing.Size(64, 13);
            this.TotalFoundLabel.TabIndex = 24;
            this.TotalFoundLabel.Text = "Total Found";
            // 
            // TotalFoundTextbox
            // 
            this.TotalFoundTextbox.Location = new System.Drawing.Point(198, 235);
            this.TotalFoundTextbox.Name = "TotalFoundTextbox";
            this.TotalFoundTextbox.ReadOnly = true;
            this.TotalFoundTextbox.Size = new System.Drawing.Size(44, 20);
            this.TotalFoundTextbox.TabIndex = 25;
            // 
            // AutoScanButton
            // 
            this.AutoScanButton.Location = new System.Drawing.Point(21, 359);
            this.AutoScanButton.Name = "AutoScanButton";
            this.AutoScanButton.Size = new System.Drawing.Size(85, 23);
            this.AutoScanButton.TabIndex = 26;
            this.AutoScanButton.Text = "Auto-Scan";
            this.AutoScanButton.UseVisualStyleBackColor = true;
            this.AutoScanButton.Click += new System.EventHandler(this.AutoScanButton_Click);
            // 
            // NomenclatureLabel
            // 
            this.NomenclatureLabel.AutoSize = true;
            this.NomenclatureLabel.Location = new System.Drawing.Point(248, 264);
            this.NomenclatureLabel.Name = "NomenclatureLabel";
            this.NomenclatureLabel.Size = new System.Drawing.Size(73, 13);
            this.NomenclatureLabel.TabIndex = 27;
            this.NomenclatureLabel.Text = "Nomenclature";
            // 
            // LocationLabel
            // 
            this.LocationLabel.AutoSize = true;
            this.LocationLabel.Location = new System.Drawing.Point(273, 342);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(48, 13);
            this.LocationLabel.TabIndex = 28;
            this.LocationLabel.Text = "Location";
            // 
            // PartNumberLabel
            // 
            this.PartNumberLabel.AutoSize = true;
            this.PartNumberLabel.Location = new System.Drawing.Point(255, 316);
            this.PartNumberLabel.Name = "PartNumberLabel";
            this.PartNumberLabel.Size = new System.Drawing.Size(66, 13);
            this.PartNumberLabel.TabIndex = 29;
            this.PartNumberLabel.Text = "Part Number";
            // 
            // SerialNumberLabel
            // 
            this.SerialNumberLabel.AutoSize = true;
            this.SerialNumberLabel.Location = new System.Drawing.Point(248, 290);
            this.SerialNumberLabel.Name = "SerialNumberLabel";
            this.SerialNumberLabel.Size = new System.Drawing.Size(73, 13);
            this.SerialNumberLabel.TabIndex = 30;
            this.SerialNumberLabel.Text = "Serial Number";
            // 
            // NomenclatureTextbox
            // 
            this.NomenclatureTextbox.Location = new System.Drawing.Point(327, 261);
            this.NomenclatureTextbox.Name = "NomenclatureTextbox";
            this.NomenclatureTextbox.ReadOnly = true;
            this.NomenclatureTextbox.Size = new System.Drawing.Size(100, 20);
            this.NomenclatureTextbox.TabIndex = 31;
            // 
            // LocationTextbox
            // 
            this.LocationTextbox.Location = new System.Drawing.Point(327, 339);
            this.LocationTextbox.Name = "LocationTextbox";
            this.LocationTextbox.ReadOnly = true;
            this.LocationTextbox.Size = new System.Drawing.Size(100, 20);
            this.LocationTextbox.TabIndex = 32;
            // 
            // PartNumberTextbox
            // 
            this.PartNumberTextbox.Location = new System.Drawing.Point(327, 313);
            this.PartNumberTextbox.Name = "PartNumberTextbox";
            this.PartNumberTextbox.ReadOnly = true;
            this.PartNumberTextbox.Size = new System.Drawing.Size(100, 20);
            this.PartNumberTextbox.TabIndex = 33;
            // 
            // SerialNumberTextbox
            // 
            this.SerialNumberTextbox.Location = new System.Drawing.Point(327, 287);
            this.SerialNumberTextbox.Name = "SerialNumberTextbox";
            this.SerialNumberTextbox.ReadOnly = true;
            this.SerialNumberTextbox.Size = new System.Drawing.Size(100, 20);
            this.SerialNumberTextbox.TabIndex = 34;
            // 
            // NotesLabel
            // 
            this.NotesLabel.AutoSize = true;
            this.NotesLabel.Location = new System.Drawing.Point(458, 238);
            this.NotesLabel.Name = "NotesLabel";
            this.NotesLabel.Size = new System.Drawing.Size(35, 13);
            this.NotesLabel.TabIndex = 37;
            this.NotesLabel.Text = "Notes";
            // 
            // PAMSNotesTextbox
            // 
            this.PAMSNotesTextbox.Location = new System.Drawing.Point(499, 235);
            this.PAMSNotesTextbox.Multiline = true;
            this.PAMSNotesTextbox.Name = "PAMSNotesTextbox";
            this.PAMSNotesTextbox.ReadOnly = true;
            this.PAMSNotesTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.PAMSNotesTextbox.Size = new System.Drawing.Size(227, 72);
            this.PAMSNotesTextbox.TabIndex = 38;
            // 
            // R14NotesTextbox
            // 
            this.R14NotesTextbox.Location = new System.Drawing.Point(499, 310);
            this.R14NotesTextbox.Multiline = true;
            this.R14NotesTextbox.Name = "R14NotesTextbox";
            this.R14NotesTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.R14NotesTextbox.Size = new System.Drawing.Size(227, 72);
            this.R14NotesTextbox.TabIndex = 39;
            // 
            // AdministratorButton
            // 
            this.AdministratorButton.Location = new System.Drawing.Point(21, 432);
            this.AdministratorButton.Name = "AdministratorButton";
            this.AdministratorButton.Size = new System.Drawing.Size(85, 23);
            this.AdministratorButton.TabIndex = 40;
            this.AdministratorButton.Text = "Administrator";
            this.AdministratorButton.UseVisualStyleBackColor = true;
            this.AdministratorButton.Click += new System.EventHandler(this.AdministratorButton_Click);
            // 
            // SNIPTableButton
            // 
            this.SNIPTableButton.Location = new System.Drawing.Point(157, 432);
            this.SNIPTableButton.Name = "SNIPTableButton";
            this.SNIPTableButton.Size = new System.Drawing.Size(85, 23);
            this.SNIPTableButton.TabIndex = 41;
            this.SNIPTableButton.Text = "SNIP Table";
            this.SNIPTableButton.UseVisualStyleBackColor = true;
            this.SNIPTableButton.Click += new System.EventHandler(this.SNIPTableButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(499, 389);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 43;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.QueryPreviousButton);
            this.MainPanel.Controls.Add(this.InventoryNameLabel);
            this.MainPanel.Controls.Add(this.QueryingFromLabel);
            this.MainPanel.Controls.Add(this.PrintingNotesLabel);
            this.MainPanel.Controls.Add(this.PrintCardButton);
            this.MainPanel.Controls.Add(this.ExitButton);
            this.MainPanel.Controls.Add(this.CompletedCheckbox);
            this.MainPanel.Controls.Add(this.CompletedLabel);
            this.MainPanel.Controls.Add(this.TitleLabel);
            this.MainPanel.Controls.Add(this.UpdateButton);
            this.MainPanel.Controls.Add(this.LogoImage);
            this.MainPanel.Controls.Add(this.NewQueryButton);
            this.MainPanel.Controls.Add(this.SNIPTableButton);
            this.MainPanel.Controls.Add(this.AdministratorButton);
            this.MainPanel.Controls.Add(this.R14NotesTextbox);
            this.MainPanel.Controls.Add(this.NSNLabel);
            this.MainPanel.Controls.Add(this.PAMSNotesTextbox);
            this.MainPanel.Controls.Add(this.NSNComboBox);
            this.MainPanel.Controls.Add(this.NotesLabel);
            this.MainPanel.Controls.Add(this.PriceLabel);
            this.MainPanel.Controls.Add(this.PriceTextbox);
            this.MainPanel.Controls.Add(this.PSMLabel);
            this.MainPanel.Controls.Add(this.SerialNumberTextbox);
            this.MainPanel.Controls.Add(this.PSMTextbox);
            this.MainPanel.Controls.Add(this.PartNumberTextbox);
            this.MainPanel.Controls.Add(this.ASCLabel);
            this.MainPanel.Controls.Add(this.LocationTextbox);
            this.MainPanel.Controls.Add(this.ASCTextbox);
            this.MainPanel.Controls.Add(this.NomenclatureTextbox);
            this.MainPanel.Controls.Add(this.DOCLabel);
            this.MainPanel.Controls.Add(this.SerialNumberLabel);
            this.MainPanel.Controls.Add(this.DOCTextbox);
            this.MainPanel.Controls.Add(this.PartNumberLabel);
            this.MainPanel.Controls.Add(this.AuthLabel);
            this.MainPanel.Controls.Add(this.LocationLabel);
            this.MainPanel.Controls.Add(this.AuthTextbox);
            this.MainPanel.Controls.Add(this.NomenclatureLabel);
            this.MainPanel.Controls.Add(this.InUseLabel);
            this.MainPanel.Controls.Add(this.AutoScanButton);
            this.MainPanel.Controls.Add(this.InUseTextbox);
            this.MainPanel.Controls.Add(this.TotalFoundTextbox);
            this.MainPanel.Controls.Add(this.IncompleteEntriesButton);
            this.MainPanel.Controls.Add(this.TotalFoundLabel);
            this.MainPanel.Controls.Add(this.AddNewIDButton);
            this.MainPanel.Controls.Add(this.IDsListbox);
            this.MainPanel.Controls.Add(this.DeleteIDButton);
            this.MainPanel.Controls.Add(this.IDsLabel);
            this.MainPanel.Controls.Add(this.DefaultNotesLandmark);
            this.MainPanel.Controls.Add(this.PrintNotesLandmark);
            this.MainPanel.Controls.Add(this.PrintNotesSize);
            this.MainPanel.Controls.Add(this.ItemsDataGridView);
            this.MainPanel.Location = new System.Drawing.Point(12, 12);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(751, 471);
            this.MainPanel.TabIndex = 44;
            // 
            // QueryPreviousButton
            // 
            this.QueryPreviousButton.Location = new System.Drawing.Point(198, 162);
            this.QueryPreviousButton.Name = "QueryPreviousButton";
            this.QueryPreviousButton.Size = new System.Drawing.Size(96, 23);
            this.QueryPreviousButton.TabIndex = 55;
            this.QueryPreviousButton.Text = "Query Previous";
            this.QueryPreviousButton.UseVisualStyleBackColor = true;
            this.QueryPreviousButton.Click += new System.EventHandler(this.QueryPreviousButton_Click);
            // 
            // InventoryNameLabel
            // 
            this.InventoryNameLabel.AutoSize = true;
            this.InventoryNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventoryNameLabel.Location = new System.Drawing.Point(416, 165);
            this.InventoryNameLabel.Name = "InventoryNameLabel";
            this.InventoryNameLabel.Size = new System.Drawing.Size(0, 16);
            this.InventoryNameLabel.TabIndex = 54;
            this.InventoryNameLabel.Visible = false;
            // 
            // QueryingFromLabel
            // 
            this.QueryingFromLabel.AutoSize = true;
            this.QueryingFromLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QueryingFromLabel.Location = new System.Drawing.Point(300, 165);
            this.QueryingFromLabel.Name = "QueryingFromLabel";
            this.QueryingFromLabel.Size = new System.Drawing.Size(113, 16);
            this.QueryingFromLabel.TabIndex = 53;
            this.QueryingFromLabel.Text = "Querying From:";
            this.QueryingFromLabel.Visible = false;
            // 
            // PrintingNotesLabel
            // 
            this.PrintingNotesLabel.AutoSize = true;
            this.PrintingNotesLabel.Location = new System.Drawing.Point(116, 142);
            this.PrintingNotesLabel.Name = "PrintingNotesLabel";
            this.PrintingNotesLabel.Size = new System.Drawing.Size(35, 13);
            this.PrintingNotesLabel.TabIndex = 50;
            this.PrintingNotesLabel.Text = "Notes";
            this.PrintingNotesLabel.Visible = false;
            // 
            // PrintCardButton
            // 
            this.PrintCardButton.Location = new System.Drawing.Point(651, 389);
            this.PrintCardButton.Name = "PrintCardButton";
            this.PrintCardButton.Size = new System.Drawing.Size(75, 23);
            this.PrintCardButton.TabIndex = 47;
            this.PrintCardButton.Text = "Print Card";
            this.PrintCardButton.UseVisualStyleBackColor = true;
            this.PrintCardButton.Click += new System.EventHandler(this.PrintCardButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(651, 434);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 46;
            this.ExitButton.Text = "Exit LOKI";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // CompletedCheckbox
            // 
            this.CompletedCheckbox.AutoSize = true;
            this.CompletedCheckbox.Enabled = false;
            this.CompletedCheckbox.Location = new System.Drawing.Point(157, 121);
            this.CompletedCheckbox.Name = "CompletedCheckbox";
            this.CompletedCheckbox.Size = new System.Drawing.Size(15, 14);
            this.CompletedCheckbox.TabIndex = 45;
            this.CompletedCheckbox.UseVisualStyleBackColor = true;
            // 
            // CompletedLabel
            // 
            this.CompletedLabel.AutoSize = true;
            this.CompletedLabel.Location = new System.Drawing.Point(88, 122);
            this.CompletedLabel.Name = "CompletedLabel";
            this.CompletedLabel.Size = new System.Drawing.Size(63, 13);
            this.CompletedLabel.TabIndex = 44;
            this.CompletedLabel.Text = "Completed?";
            // 
            // DefaultNotesLandmark
            // 
            this.DefaultNotesLandmark.AutoSize = true;
            this.DefaultNotesLandmark.Location = new System.Drawing.Point(499, 310);
            this.DefaultNotesLandmark.Name = "DefaultNotesLandmark";
            this.DefaultNotesLandmark.Size = new System.Drawing.Size(15, 14);
            this.DefaultNotesLandmark.TabIndex = 48;
            this.DefaultNotesLandmark.UseVisualStyleBackColor = true;
            this.DefaultNotesLandmark.Visible = false;
            // 
            // PrintNotesLandmark
            // 
            this.PrintNotesLandmark.AutoSize = true;
            this.PrintNotesLandmark.Location = new System.Drawing.Point(157, 142);
            this.PrintNotesLandmark.Name = "PrintNotesLandmark";
            this.PrintNotesLandmark.Size = new System.Drawing.Size(15, 14);
            this.PrintNotesLandmark.TabIndex = 49;
            this.PrintNotesLandmark.UseVisualStyleBackColor = true;
            this.PrintNotesLandmark.Visible = false;
            // 
            // PrintNotesSize
            // 
            this.PrintNotesSize.AutoSize = true;
            this.PrintNotesSize.Location = new System.Drawing.Point(562, 212);
            this.PrintNotesSize.Name = "PrintNotesSize";
            this.PrintNotesSize.Size = new System.Drawing.Size(15, 14);
            this.PrintNotesSize.TabIndex = 51;
            this.PrintNotesSize.UseVisualStyleBackColor = true;
            this.PrintNotesSize.Visible = false;
            // 
            // ItemsDataGridView
            // 
            this.ItemsDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDNumber,
            this.PartNumber,
            this.SerialNumber,
            this.ItemLocation,
            this.Notes});
            this.ItemsDataGridView.Location = new System.Drawing.Point(21, 224);
            this.ItemsDataGridView.Name = "ItemsDataGridView";
            this.ItemsDataGridView.RowHeadersVisible = false;
            this.ItemsDataGridView.Size = new System.Drawing.Size(705, 231);
            this.ItemsDataGridView.TabIndex = 52;
            this.ItemsDataGridView.Visible = false;
            // 
            // IDNumber
            // 
            this.IDNumber.HeaderText = "ID";
            this.IDNumber.Name = "IDNumber";
            this.IDNumber.Width = 85;
            // 
            // PartNumber
            // 
            this.PartNumber.HeaderText = "PN";
            this.PartNumber.Name = "PartNumber";
            // 
            // SerialNumber
            // 
            this.SerialNumber.HeaderText = "SN";
            this.SerialNumber.Name = "SerialNumber";
            // 
            // ItemLocation
            // 
            this.ItemLocation.HeaderText = "Location";
            this.ItemLocation.Name = "ItemLocation";
            // 
            // Notes
            // 
            this.Notes.HeaderText = "Notes";
            this.Notes.Name = "Notes";
            this.Notes.Width = 300;
            // 
            // SNIPLandmark
            // 
            this.SNIPLandmark.AutoSize = true;
            this.SNIPLandmark.Location = new System.Drawing.Point(344, 320);
            this.SNIPLandmark.Name = "SNIPLandmark";
            this.SNIPLandmark.Size = new System.Drawing.Size(15, 14);
            this.SNIPLandmark.TabIndex = 51;
            this.SNIPLandmark.UseVisualStyleBackColor = true;
            this.SNIPLandmark.Visible = false;
            // 
            // PrintDocument
            // 
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_PrintPage);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(775, 493);
            this.ControlBox = false;
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.SNIPLandmark);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "LOKI";
            ((System.ComponentModel.ISupportInitialize)(this.LogoImage)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NewQueryButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.PictureBox LogoImage;
        private System.Windows.Forms.Label NSNLabel;
        public System.Windows.Forms.ComboBox NSNComboBox;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.TextBox PriceTextbox;
        private System.Windows.Forms.Label PSMLabel;
        private System.Windows.Forms.TextBox PSMTextbox;
        private System.Windows.Forms.Label ASCLabel;
        private System.Windows.Forms.TextBox ASCTextbox;
        private System.Windows.Forms.Label DOCLabel;
        private System.Windows.Forms.TextBox DOCTextbox;
        private System.Windows.Forms.Label AuthLabel;
        private System.Windows.Forms.TextBox AuthTextbox;
        private System.Windows.Forms.Label InUseLabel;
        private System.Windows.Forms.TextBox InUseTextbox;
        private System.Windows.Forms.Button IncompleteEntriesButton;
        private System.Windows.Forms.Button AddNewIDButton;
        private System.Windows.Forms.Button DeleteIDButton;
        private System.Windows.Forms.Label IDsLabel;
        private System.Windows.Forms.ListBox IDsListbox;
        private System.Windows.Forms.Label TotalFoundLabel;
        private System.Windows.Forms.TextBox TotalFoundTextbox;
        private System.Windows.Forms.Button AutoScanButton;
        private System.Windows.Forms.Label NomenclatureLabel;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.Label PartNumberLabel;
        private System.Windows.Forms.Label SerialNumberLabel;
        private System.Windows.Forms.TextBox NomenclatureTextbox;
        private System.Windows.Forms.TextBox LocationTextbox;
        private System.Windows.Forms.TextBox PartNumberTextbox;
        private System.Windows.Forms.TextBox SerialNumberTextbox;
        private System.Windows.Forms.Label NotesLabel;
        private System.Windows.Forms.TextBox PAMSNotesTextbox;
        private System.Windows.Forms.TextBox R14NotesTextbox;
        private System.Windows.Forms.Button AdministratorButton;
        private System.Windows.Forms.Button SNIPTableButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.ToolTip FormTooltip;
        private System.Windows.Forms.CheckBox CompletedCheckbox;
        private System.Windows.Forms.Label CompletedLabel;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.CheckBox SNIPLandmark;
        private System.Windows.Forms.Button PrintCardButton;
        private System.Drawing.Printing.PrintDocument PrintDocument;
        private System.Windows.Forms.CheckBox DefaultNotesLandmark;
        private System.Windows.Forms.CheckBox PrintNotesLandmark;
        private System.Windows.Forms.Label PrintingNotesLabel;
        private System.Windows.Forms.CheckBox PrintNotesSize;
        private System.Windows.Forms.DataGridView ItemsDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Notes;
        private System.Windows.Forms.Button QueryPreviousButton;
        private System.Windows.Forms.Label InventoryNameLabel;
        private System.Windows.Forms.Label QueryingFromLabel;
        public System.Windows.Forms.Panel MainPanel;
    }
}

