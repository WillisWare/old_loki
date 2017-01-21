using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Media;

namespace LOKI
{
    public partial class MainForm : Form
    {
        public Inventory CurrentInventory;      //  ┐
        public R14Entry CurrentEntry;           //  │
                                                //  │
        public Bitmap PrintingCard;             //  │
                                                //  │
        public Size MainSize;                   //  │
        public Size SnipSize;                   //  ├─── All of these variables are global so there
        public Size DefaultNotesSize;           //  │       doesn't need to be a ton of parameter
        public Size PrintingNotesSize;          //  │            passing between functions.
                                                //  │
        public bool AutoScan;                   //  │
        public bool TimeWarpMode;               //  │
        public List<Control> EditControls;      //  │
        public List<Control> AdminControls;     //  │
        public List<Control> LockedControls;    //  ┘

        /// <summary>
        /// The entry point for the MainForm class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Initializations();
        }

        /// <summary>
        /// Loads or creates the inventory file and initializes global variables.
        /// </summary>
        private void Initializations()
        {
            /*  First, check to see if an inventory file already
             *  exists and load it if so; otherwise, start a new
             *  inventory.  */

            if (File.Exists(Application.StartupPath + @"\Current\Inventory.xml"))
            {
                Inventory TempInventory = ImportInventory(true);

                if (TempInventory != null)
                {
                    CurrentInventory = TempInventory;

                    MessageBox.Show("Inventory loaded successfully!");

                    OptimizeInventory();
                }
                else
                {
                    MessageBox.Show("There was an error loading the current inventory...\n\nCreating a new inventory...");

                    StartNewInventory();
                }
            }
            else
            {
                MessageBox.Show("No current inventory detected...\n\nCreating new inventory...");

                Directory.CreateDirectory(Application.StartupPath + @"\Current");

                StartNewInventory();
            }

            /*  The rest of this is setup for panel sizes and locations
             *  and filling up the control lists.   */

            MainSize = new Size(DefaultLandmark.Location.X, DefaultLandmark.Location.Y);
            SnipSize = new Size(SNIPLandmark.Location.X, SNIPLandmark.Location.Y);
            DefaultNotesSize = new Size(R14NotesTextbox.Size.Width, R14NotesTextbox.Size.Height);
            PrintingNotesSize = new Size(PrintNotesSize.Location.X - PrintNotesLandmark.Location.X, PrintNotesSize.Location.Y - PrintNotesLandmark.Location.Y);

            this.Size = MainSize;

            MainPanel.Visible = true;
            MainPanel.Location = new Point(12, 12);
            AdminPanel.Visible = false;
            AdminPanel.Location = new Point(12, 12);
            AddSNIPPanel.Visible = false;
            AddSNIPPanel.Location = new Point(12, 12);
            ReviewSNIPPanel.Visible = false;
            ReviewSNIPPanel.Location = new Point(12, 12);
            IncompletePanel.Visible = false;
            IncompletePanel.Location = new Point(12, 12);

            EditControls = new List<Control>()
            {
                CompletedCheckbox,
                CompletedLabel,
                NewQueryButton,
                IncompleteEntriesButton,
                AddNewIDButton,
                DeleteIDButton,
                AutoScanButton,
                AdministratorButton,
                SNIPTableButton,
                ExitButton,
                UpdateButton,
                PrintCardButton,
                PrintingNotesLabel,
                TotalFoundLabel,
                TotalFoundTextbox,
                IDsLabel,
                IDsListbox,
                NomenclatureLabel,
                NomenclatureTextbox,
                SerialNumberLabel,
                SerialNumberTextbox,
                PartNumberLabel,
                PartNumberTextbox,
                LocationLabel,
                LocationTextbox,
                PAMSNotesTextbox,
                NotesLabel,
                ItemsDataGridView
            };

            AdminControls = new List<Control>()
            {
                IOGroupBox,
                SpecialGroupBox,
                AdminCancelButton,
                WaitLabel
            };

            LockedControls = new List<Control>()
            {
                AddNewIDButton,
                DeleteIDButton,
                AutoScanButton,
                UpdateButton,
                AdministratorButton,
                QueryingFromLabel,
                InventoryNameLabel
            };

            AutoScan = false;
            TimeWarpMode = false;

            if (!Directory.Exists(Application.StartupPath + @"\Archive"))
                Directory.CreateDirectory(Application.StartupPath + @"\Archive");

            if (CurrentInventory.name == "" || CurrentInventory.name == null)
            {
                CurrentInventory.Rename();

                ExportFile(typeof(Inventory), CurrentInventory, "Inventory", true);

                MessageBox.Show("Inventory appears to be unnamed!\nName has been changed to: " + CurrentInventory.name);
            }

            SetPrintingDefaults();
        }

        #region Control Events

        private void PrintDocument_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(PrintingCard, new Rectangle(0, 0, PrintingCard.Width, PrintingCard.Height));
        }

        #region Main Panel

        private void NewQueryButton_Click(object sender, EventArgs e)
        {
            List<R14Entry> FoundEntries = new List<R14Entry>();
            string Query = Interaction.InputBox("Enter the PID, NSN, or primary DOC:", "New Query", "");

            if (Query != "" && Query != null)
            {
                #region Found ID

                /*  If the query length is 7 characters long,
                 *  it's a safe assumption that a PMEL ID was
                 *  scanned.    */

                if (Query.Length == 7)
                {
                    StopAutoScan();

                    foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
                    {
                        if (Entry.items.Exists(x => x.id.Equals(Query)))
                        {
                            FoundEntries.Add(Entry);

                            break;
                        }
                    }

                    if (FoundEntries.Count == 0)
                    {
                        ClearNSNTextboxes();
                        ClearItemTextboxes();

                        bool Found = false;

                        foreach (PAMSEntry Entry in CurrentInventory.masterPAMS.entries)
                        {
                            if (Entry.id.Equals(Query))
                            {
                                Found = true;
                                break;
                            }
                        }

                        if (Found)
                            MessageBox.Show("The ID could not be found in the current R14 but it was located in the current PAMS file...\n\n" +
                                "Did you forget to add it to an NSN?", "ID Found");
                        else
                            MessageBox.Show("The ID could not be found in the current R14 or the current PAMS file...\n\n" +
                                "Did you forget to add it to the SNIP table?", "ID Not Found");
                    }
                    else
                    {
                        CurrentEntry = FoundEntries[0];

                        NSNComboBox.Items.Clear();

                        foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
                            if (Entry.doc.Equals(CurrentEntry.doc))
                                NSNComboBox.Items.Add(Entry.nsn);

                        NSNComboBox.Text = CurrentEntry.nsn;

                        IDsListbox.SelectedItem = Query;
                    }
                }

                #endregion 

                #region Found NSN

                /*  If the query is 13 or 15 characters long,
                 *  an NSN was probably scanned; additionally,
                 *  this part will dissect the query to check
                 *  for NSNs without trailing letters (convert
                 *  from 15 to 13 characters basically).    */

                else if (Query.Length == 13 || Query.Length == 15)
                {
                    foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
                    {
                        if (Entry.nsn.Equals(Query))
                        {
                            FoundEntries.Add(Entry);

                            continue;
                        }
                        else if (Query.Length == 15 && Entry.nsn.Equals(Query.Remove(13)))
                        {
                            FoundEntries.Add(Entry);

                            continue;
                        }
                        else if (Query.Length == 13 && Entry.nsn.Length == 15 && Entry.nsn.Remove(13).Equals(Query))
                        {
                            FoundEntries.Add(Entry);

                            continue;
                        }
                    }

                    if (FoundEntries.Count == 0)
                    {
                        ClearNSNTextboxes();
                        MessageBox.Show("That NSN could not be found...");

                        StopAutoScan();
                    }
                    else if (FoundEntries.Count == 1)
                    {
                        CurrentEntry = FoundEntries[0];

                        NSNComboBox.Items.Clear();

                        foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
                            if (Entry.doc.Equals(CurrentEntry.doc))
                                NSNComboBox.Items.Add(Entry.nsn);

                        NSNComboBox.Text = CurrentEntry.nsn;
                    }
                    else
                    {
                        string docChoices = "";
                        for (int i = 0; i < FoundEntries.Count; i++)
                            docChoices += (i) + " - " + FoundEntries[i].doc + "\n";

                        string error = "There were multiple DOC number entries found for this NSN!";
                        bool invalid = true;

                        while (invalid)
                        {
                            //  I really abhor using InputBoxes, but...

                            string input = Interaction.InputBox(error + "\n\nPlease enter the number that corresponds with the DOC number you're querying for:\n\n" + docChoices + "\n", "Multiple NSN Selection", "0", -1, -1);

                            if (input.Length != 0)
                            {
                                try
                                {
                                    int result = int.Parse(input);

                                    if (result >= 0 && result < FoundEntries.Count)
                                    {
                                        CurrentEntry = FoundEntries[result];

                                        NSNComboBox.Items.Clear();

                                        foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
                                            if (Entry.doc.Equals(CurrentEntry.doc))
                                                NSNComboBox.Items.Add(Entry.nsn);

                                        NSNComboBox.Text = CurrentEntry.nsn;

                                        invalid = false;
                                    }
                                    else
                                        error = "That is not a valid option...";
                                }
                                catch
                                {
                                    error = "Please enter a number...";
                                }
                            }
                            else
                                invalid = false;
                        }
                    }
                }

                #endregion

                #region Found DOC

                /*  Originally, I had this section check for
                 *  queries with lengths of 4 or 14 (14 being
                 *  the full size of the DOC number, including
                 *  the base code), but the EMO monitor was
                 *  accidentally scanning the DOC barcode
                 *  during the inventory and plugging items into
                 *  the wrong R14 entries, so now it just fires
                 *  on 4 characters for manual DOC queries.     */

                else if (Query.Length == 4)
                {
                    foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
                    {
                        //  For lack of a better option, only the primary entry is returned...

                        if (Entry.doc == Query && Entry.psm.ToLower() == "p")
                        {
                            FoundEntries.Add(Entry);

                            break;
                        }
                    }

                    if (FoundEntries.Count == 0)
                    {
                        ClearNSNTextboxes();
                        MessageBox.Show("That DOC could not be found...");

                        StopAutoScan();
                    }
                    else
                    {
                        CurrentEntry = FoundEntries[0];

                        NSNComboBox.Items.Clear();

                        foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
                        {
                            if (Entry.doc.Equals(CurrentEntry.doc))
                            {
                                NSNComboBox.Items.Add(Entry.nsn);
                            }
                        }

                        NSNComboBox.Text = CurrentEntry.nsn;
                    }
                }

                #endregion

                else
                {
                    MessageBox.Show(Query + " is not a valid query...");

                    StopAutoScan();
                }
            }
            else
                StopAutoScan();
        }

        private void IncompleteEntriesButton_Click(object sender, EventArgs e)
        {
            IncompleteDatagridview.Rows.Clear();

            foreach (R14Entry entry in CurrentInventory.masterR14.entries)
                if (entry.isComplete == false)
                    IncompleteDatagridview.Rows.Add(new string[] { entry.nsn, entry.inUse.ToString(), entry.items.Count().ToString() });

            IncompleteTotalTextbox.Text = (IncompleteDatagridview.Rows.Count - 1).ToString();

            ShowPanel(IncompletePanel, SnipSize);
        }

        private void AddNewIDButton_Click(object sender, EventArgs e)
        {
            if (CurrentEntry == null)
            {
                MessageBox.Show("You must have an entry queried to add an ID...");
                return;
            }

            string NewID = Interaction.InputBox("Enter the ID you would like to add:", "Add New ID", "");

            AddItem(NewID, false);
        }

        private void DeleteIDButton_Click(object sender, EventArgs e)
        {
            if (CurrentEntry == null)
            {
                MessageBox.Show("You must have an entry queried to add an ID...");
                return;
            }

            DialogResult ConfirmDelete = MessageBox.Show("Are you sure you want to delete " + IDsListbox.Text + "?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (ConfirmDelete == DialogResult.Yes)
            {
                CurrentEntry.items.RemoveAt(CurrentEntry.items.FindIndex(x => x.id.Equals(IDsListbox.Text)));

                if (CurrentEntry.items.Count() >= CurrentEntry.inUse)
                    CurrentEntry.isComplete = true;
                else if (CurrentEntry.items.Count() < CurrentEntry.inUse)
                    CurrentEntry.isComplete = false;

                ClearItemTextboxes();

                UpdateItemList(0);
            }
        }

        private void AutoScanButton_Click(object sender, EventArgs e)
        {
            AutoScan = true;

            while (AutoScan)
            {
                NewQueryButton_Click(AutoScan, null);

                if (!AutoScan)
                    break;

                AddNewIDButton_Click(AutoScan, null);
            }
        }

        private void AdministratorButton_Click(object sender, EventArgs e)
        {
            ShowPanel(AdminPanel, SnipSize);
        }

        private void SNIPTableButton_Click(object sender, EventArgs e)
        {
            UpdateSNIPList();

            ShowPanel(ReviewSNIPPanel, SnipSize);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (IDsListbox.Text != "" && IDsListbox.Text != null)
                CurrentEntry.items[CurrentEntry.items.FindIndex(x => x.id.Equals(IDsListbox.Text))].notesR14 = R14NotesTextbox.Text;
            else
                MessageBox.Show("You must have an item selected to update its R14 text...");
        }

        private void PrintCardButton_Click(object sender, EventArgs e)
        {
            SwitchEditMode();
            PrintCard();
            SwitchEditMode();
        }

        private void NSNComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool Found = false;
            R14Entry FoundEntry = new R14Entry();

            foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
            {
                if (Entry.nsn.Equals(NSNComboBox.Text) && Entry.doc.Equals(CurrentEntry.doc))
                {
                    FoundEntry = CurrentInventory.masterR14.entries.Find(x => x.nsn.Equals(NSNComboBox.Text) && x.doc.Equals(CurrentEntry.doc));

                    Found = true;
                    break;
                }
            }

            if (!Found)
            {
                ClearNSNTextboxes();
                MessageBox.Show("That NSN could not be found...");
            }
            else
            {
                CurrentEntry = FoundEntry;
                UpdateNSNTextboxes();
            }
        }

        private void IDsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateItemTextboxes(CurrentEntry.items.Find(x => x.id.Equals(IDsListbox.SelectedValue)));
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            /*  Originally, LOKI still had the control bar
             *  and the user could exit out like normal,
             *  but I had it auto-save inventory changes on
             *  closing; it was then brought to light that
             *  the user may not always want to take inventories
             *  and sometimes they just need to query without
             *  making any changes, so I scrapped the control
             *  bar and added the Exit button.  */

            if (!TimeWarpMode)
            {
                var Response = MessageBox.Show("Would you like to save any changes made to the current inventory?", "Save and Exit?", MessageBoxButtons.YesNoCancel);
                if (Response == DialogResult.Yes)
                {
                    ExportFile(typeof(Inventory), CurrentInventory, "Inventory", true);
                    this.Close();
                }
                else if (Response == DialogResult.No)
                {
                    Response = MessageBox.Show("You are about to exit the program without saving your changes...\n" +
                        "Are you sure you want to exit without saving?", "No Save Confirmation...", MessageBoxButtons.YesNo);
                    if (Response == DialogResult.Yes)
                        this.Close();
                    else
                        return;
                }
                else
                    return;
            }
            else
                this.Close();       //  If TimeWarpMode is on, just close without saving.
        }

        private void QueryPreviousButton_Click(object sender, EventArgs e)
        {
            /*  The Query Previous function only allows the user to
             *  query from past inventories, /not/ make changes at all.     */

            if (!TimeWarpMode)
            {
                if (MessageBox.Show("Would you like to save your changes?\nUnsaved Changes will be lost...", "Save Changes?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    ExportFile(typeof(Inventory), CurrentInventory, "Inventory", true);

                Inventory tempInventory = ImportInventory(false);
                
                if (tempInventory != null)
                {
                    CurrentInventory = tempInventory;

                    TimeWarpSwitch();
                }
            }
            else
            {
                CurrentInventory = ImportInventory(true);

                TimeWarpSwitch();
            }
        }

        #endregion

        #region Admin Panel

        private void ExportPAMSButton_Click(object sender, EventArgs e)
        {
            ExportFile(typeof(PAMS), CurrentInventory.masterPAMS, "PAMS", false);
        }

        private void ImportPAMSButton_Click(object sender, EventArgs e)
        {
            PAMS TempPAMS;

            DialogResult Result = MessageBox.Show("You are about to import a new PAMS file...\n\n" +
                "Do you want to export the current PAMS file first?", 
                "Export Confirmation", 
                MessageBoxButtons.YesNo);

            if (Result == DialogResult.Yes)
            {
                ExportFile(typeof(PAMS), CurrentInventory.masterPAMS, "PAMS", false);

                TempPAMS = new PAMS();

                TempPAMS = ImportPAMS();

                if (TempPAMS != null)
                    CurrentInventory.masterPAMS = TempPAMS;
            }
            else
            {
                TempPAMS = new PAMS();

                TempPAMS = ImportPAMS();

                if (TempPAMS != null)
                    CurrentInventory.masterPAMS = TempPAMS;
            }
        }

        private void ExportR14Button_Click(object sender, EventArgs e)
        {
            ExportFile(typeof(R14), CurrentInventory.masterR14, "R14", false);
        }

        private void ImportR14Button_Click(object sender, EventArgs e)
        {
            R14 TempR14;

            DialogResult Result = MessageBox.Show("You are about to import a new R14 file...\n\n" +
                "Do you want to export the current R14 file first?",
                "Export Confirmation",
                MessageBoxButtons.YesNo);

            if (Result == DialogResult.Yes)
            {
                ExportFile(typeof(R14), CurrentInventory.masterR14, "R14", false);

                TempR14 = new R14();

                TempR14 = ImportR14();

                if (TempR14 != null)
                    CurrentInventory.masterR14 = TempR14;
            }
            else
            {
                CurrentInventory.masterR14 = new R14();

                TempR14 = new R14();

                TempR14 = ImportR14();

                if (TempR14 != null)
                    CurrentInventory.masterR14 = TempR14;
            }
        }

        private void NewInventoryButton_Click(object sender, EventArgs e)
        {
            ExportFile(typeof(Inventory), CurrentInventory, "Inventory", false);

            StartNewInventory();
        }

        private void AdminCancelButton_Click(object sender, EventArgs e)
        {
            ShowDefault(AdminPanel);
        }

        private void TransferObsoleteButton_Click(object sender, EventArgs e)
        {
            /*  This is a whimsical function that attempts
             *  to convert the old, obsolete MS Access versions
             *  of LOKI into the new format.  Because MS Access
             *  and VBA are terrible, this function may not be
             *  entirely reliable, but it's highly unlikely anyone
             *  will be using this function anymore.    */

            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Visible = true;

            string filePath = "";

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.FileName = @"master_id";
            fileDialog.DefaultExt = "xlsx";
            fileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            fileDialog.Title = "Import PAMS";
            fileDialog.InitialDirectory = Environment.SpecialFolder.UserProfile + @"\Downloads\";

            if (fileDialog.ShowDialog() != DialogResult.Cancel)
            {
                filePath = fileDialog.FileName;

                Excel.Workbook Book = ExcelApp.Workbooks.Open(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Excel.Worksheet Sheet = Book.Worksheets[1];
                Excel.Range ExcelRange = Sheet.UsedRange;

                object[,] ValueArray = (object[,])ExcelRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);

                for (int Row = 2; Row <= ExcelRange.Rows.Count; Row++)
                {
                    try
                    {
                        CurrentEntry = CurrentInventory.masterR14.entries[CurrentInventory.masterR14.entries.FindIndex(x => x.nsn.Equals(ValueArray[Row, 4].ToString()))];

                        for (int x = 12; x <= 31; x++)
                        {
                            if (ValueArray[Row, x] != null)
                                AddItem(ValueArray[Row, x].ToString().ToUpper(), true);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error at row " + Row + "; The NSN to find was " + ValueArray[Row, 4] + " and CurrentEntry ended up being " + CurrentEntry + " | " + CurrentEntry.nsn);
                    }
                }

                Marshal.ReleaseComObject(ExcelRange);
                Marshal.ReleaseComObject(Sheet.Rows);
                Marshal.ReleaseComObject(Sheet.Columns);
                Marshal.ReleaseComObject(Sheet);
                Marshal.ReleaseComObject(Book);
                Marshal.ReleaseComObject(ExcelApp.Workbooks);
            }

            ExcelApp.Quit();
        }

        private void BatchPrintButton_Click(object sender, EventArgs e)
        {
            BatchPrint();
        }

        private void ExcelExportButton_Click(object sender, EventArgs e)
        {
            AdminWait();
            ExportToExcel();
            AdminWait();
        }

        #endregion

        #region SNIP Panel

        private void SNIPDatagridview_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            /*  This function is responsible for changing the
             *  values of the SNIP entries in the table according
             *  to the data entered in the DataGridView.    */

            if (!TimeWarpMode)
            {
                if (SNIPDatagridview.SelectedCells.Count != 0)
                {
                    DataGridViewCell changedCell = SNIPDatagridview.SelectedCells[0];

                    if (changedCell.ColumnIndex == 0)
                        CurrentInventory.masterSNIP.entries[changedCell.RowIndex].id = changedCell.Value.ToString();
                    else if (changedCell.ColumnIndex == 1)
                        CurrentInventory.masterSNIP.entries[changedCell.RowIndex].partNumber = changedCell.Value.ToString();
                    else if (changedCell.ColumnIndex == 2)
                        CurrentInventory.masterSNIP.entries[changedCell.RowIndex].serialNumber = changedCell.Value.ToString();
                    else
                        CurrentInventory.masterSNIP.entries[changedCell.RowIndex].nomenclature = changedCell.Value.ToString();

                    MessageBox.Show("SNIP entry updated!");
                }
            }
            else
            {
                MessageBox.Show("You can't edit the SNIP table entries while viewing a previous inventory!");
            }
        }

        private void SNIPBackButton_Click(object sender, EventArgs e)
        {
            ShowDefault(ReviewSNIPPanel);
        }

        private void SNIPCancelButton_Click(object sender, EventArgs e)
        {
            /*  There should probably be some sort of
             *  "Exit Without Saving?" prompt here,
             *  but the EMO monitors haven't said anything
             *  about needing it.  Soooooo...   */

            ShowDefault(AddSNIPPanel);
        }

        private void SNIPSubmitButton_Click(object sender, EventArgs e)
        {
            /*  If all of the textboxes have been filled in,
             *  the SNIP entry will get added to the SNIP table
             *  and can be queried from as if it were in the 
             *  Master R14 listing.     */

            if (NewIDTextbox.Text == "" || NewNomenclatureTextbox.Text == "" || NewSerialNumberTextbox.Text == "" || NewPartNumberTextbox.Text == "" || NewLocationTextbox.Text == "")
            {
                DialogResult StillAdd = MessageBox.Show("One or more of the entry textboxes has been left blank...\n\nDo you still want to submit your entry?", "Submission Verification", MessageBoxButtons.YesNo);

                if (StillAdd == DialogResult.No)
                    return;
            }

            PAMSEntry newEntry = new PAMSEntry();
            Item newItem = new Item();

            newEntry.id = NewIDTextbox.Text;
            newEntry.nomenclature = NewNomenclatureTextbox.Text;
            newEntry.serialNumber = NewSerialNumberTextbox.Text;
            newEntry.partNumber = NewPartNumberTextbox.Text;
            newEntry.location = NewLocationTextbox.Text;
            newEntry.notes = NewNotesTextbox.Text;

            CurrentInventory.masterSNIP.entries.Add(newEntry);

            newItem.id = newEntry.id;
            newItem.nomenclature = newEntry.nomenclature;
            newItem.partNumber = newEntry.partNumber;
            newItem.serialNumber = newEntry.serialNumber;
            newItem.location = newEntry.location;
            newItem.exchange = false;
            newItem.notesPAMS = newEntry.notes;
            newItem.notesR14 = "";

            CurrentEntry.items.Add(newItem);

            if (CurrentEntry.items.Count() == CurrentEntry.inUse)
                CurrentEntry.isComplete = true;
            else if (CurrentEntry.items.Count() != CurrentEntry.inUse)
                CurrentEntry.isComplete = false;

            ShowDefault(AddSNIPPanel);

            UpdateItemList(CurrentEntry.items.Count() - 1);
        }

        #endregion

        #region Incomplete Panel

        private void IncompleteBackButton_Click(object sender, EventArgs e)
        {
            ShowDefault(IncompletePanel);
        }

        #endregion

        #endregion

        #region Functions

        #region Display Functions

        /// <summary>
        /// Returns the display to the default panel.
        /// </summary>
        /// <param name="thisPanel">The panel that is currently displayed.</param>
        public void ShowDefault(Panel thisPanel)
        {
            this.Size = MainSize;

            thisPanel.Visible = false;
            MainPanel.Visible = true;
        }

        /// <summary>
        /// Displays a panel other than the default panel.
        /// </summary>
        /// <param name="thisPanel">The panel to be displayed.</param>
        /// <param name="thisSize">The size of the panel to be displayed.</param>
        public void ShowPanel(Panel thisPanel, Size thisSize)
        {
            this.Size = thisSize;

            MainPanel.Visible = false;
            thisPanel.Visible = true;
        }

        /// <summary>
        /// Clears all textboxes pertaining to an R14 entry's NSN information.
        /// </summary>
        public void ClearNSNTextboxes()
        {
            DOCTextbox.Text = "";
            AuthTextbox.Text = "";
            InUseTextbox.Text = "";
            PriceTextbox.Text = "";
            PSMTextbox.Text = "";
            ASCTextbox.Text = "";
            TotalFoundTextbox.Text = "";

            IDsListbox.Items.Clear();

            ClearItemTextboxes();
        }

        /// <summary>
        /// Clears all textboxes pertaining to an R14 entry's item information.
        /// </summary>
        public void ClearItemTextboxes()
        {
            NomenclatureTextbox.Text = "";
            SerialNumberTextbox.Text = "";
            PartNumberTextbox.Text = "";
            LocationTextbox.Text = "";

            PAMSNotesTextbox.Text = "";
            R14NotesTextbox.Text = "";
        }

        /// <summary>
        /// Populates all textboxes pertaining to an R14 entry's NSN information.
        /// </summary>
        public void UpdateNSNTextboxes()
        {
            ClearNSNTextboxes();

            DOCTextbox.Text = CurrentEntry.doc;
            AuthTextbox.Text = CurrentEntry.auth.ToString();
            InUseTextbox.Text = CurrentEntry.inUse.ToString();
            PriceTextbox.Text = CurrentEntry.price.ToString();
            PSMTextbox.Text = CurrentEntry.psm;
            ASCTextbox.Text = CurrentEntry.asc;

            UpdateItemList(0);
        }

        /// <summary>
        /// Populates all textboxes pertaining to an R14 entry's item information.
        /// </summary>
        /// <param name="SelectedItem"></param>
        public void UpdateItemTextboxes(Item SelectedItem)
        {
            CompletedCheckbox.Checked = CurrentEntry.isComplete;

            if (CurrentEntry.items.Count() != 0)
            {
                if (SelectedItem == null)
                    SelectedItem = CurrentEntry.items.Find(x => x.id.Equals(IDsListbox.Text));

                NomenclatureTextbox.Text = SelectedItem.nomenclature;
                PartNumberTextbox.Text = SelectedItem.partNumber;
                SerialNumberTextbox.Text = SelectedItem.serialNumber;
                LocationTextbox.Text = SelectedItem.location;
                PAMSNotesTextbox.Text = SelectedItem.notesPAMS;
                R14NotesTextbox.Text = SelectedItem.notesR14;
            }
        }

        /// <summary>
        /// Populates the item list with all of the item IDs loaded in the current R14 entry.
        /// </summary>
        /// <param name="Index">The index of the item whose information is to be displayed.</param>
        public void UpdateItemList(int Index)
        {
            IDsListbox.Items.Clear();

            CurrentEntry.items.ForEach(item =>
            {
                IDsListbox.Items.Add(item.id);
            });

            if (IDsListbox.Items.Count > 0)
                IDsListbox.SelectedIndex = Index;

            TotalFoundTextbox.Text = CurrentEntry.items.Count().ToString();
        }

        /// <summary>
        /// Populates the SNIP table with all of the SNIP entries in the current inventory.
        /// </summary>
        public void UpdateSNIPList()
        {
            SNIPDatagridview.Rows.Clear();

            foreach (PAMSEntry thisEntry in CurrentInventory.masterSNIP.entries)
                SNIPDatagridview.Rows.Add(new string[] { thisEntry.id, thisEntry.partNumber, thisEntry.serialNumber, thisEntry.nomenclature });

            SNIPTotalTextbox.Text = (SNIPDatagridview.Rows.Count - 1).ToString();
        }

        #endregion

        /// <summary>
        /// Establishes default printer settings for printing R14 cards.
        /// </summary>
        public void SetPrintingDefaults()
        {
            PrintDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
            PrintDocument.DefaultPageSettings.Landscape = true;

            PrintDocument.DefaultPageSettings.PaperSize = null;
            PrintDocument.DefaultPageSettings.PaperSource = null;

            foreach (PaperSize size in PrintDocument.PrinterSettings.PaperSizes)
            {
                if (size.PaperName.ToUpper().Equals("STATEMENT"))
                {
                    PrintDocument.DefaultPageSettings.PaperSize = size;

                    break;
                }
            }

            foreach (PaperSource source in PrintDocument.PrinterSettings.PaperSources)
            {
                if (source.SourceName.ToUpper().Equals("TRAY 1"))
                {
                    PrintDocument.DefaultPageSettings.PaperSource = source;

                    break;
                }
            }
        }

        /// <summary>
        /// Initiates a new inventory and establishes a saved inventory file.
        /// </summary>
        public void StartNewInventory()
        {
            CurrentInventory = new Inventory();

            CurrentInventory.masterPAMS = new PAMS();
            CurrentInventory.masterR14 = new R14();
            CurrentInventory.masterSNIP = new PAMS();

            MessageBox.Show("Please choose the R14 file you would like to start the new inventory with...");
            CurrentInventory.masterR14 = ImportR14();

            if (CurrentInventory.masterR14 == null)
            {
                MessageBox.Show("You must select a valid file for the R14 file...\n\nThe program will now exit.");
                this.Close();
            }

            MessageBox.Show("Please choose the PAMS file you would like to start the new inventory with...");
            CurrentInventory.masterPAMS = ImportPAMS();

            if (CurrentInventory.masterPAMS == null)
            {
                MessageBox.Show("You must select a valid file for the PAMS file...\n\nThe program will now exit.");
                this.Close();
            }

            CurrentInventory.masterSNIP = new PAMS();
            CurrentInventory.Rename();

            ExportFile(typeof(Inventory), CurrentInventory, "Inventory", true);
        }

        /// <summary>
        /// Ensures that all R14 entries in the current inventory have appropriate completion categorizations and that the current inventory is named.
        /// </summary>
        public void OptimizeInventory()
        {
            bool modified = false;

            CurrentInventory.masterR14.entries.ForEach(entry =>
            {
                if (entry.items.Count() >= entry.inUse && !entry.isComplete)
                {
                    entry.isComplete = true;
                    modified = true;
                }
            });

            if (CurrentInventory.name == "" || CurrentInventory.name == null)
            {
                CurrentInventory.Rename();
                modified = true;
            }

            if (modified)
                MessageBox.Show("Inventory has been optimized!");
        }

        /// <summary>
        /// Turns Edit Mode on or off.
        /// </summary>
        public void SwitchEditMode()
        {
            EditControls.ForEach(control =>
            {
                if (!TimeWarpMode || (TimeWarpMode && !LockedControls.Contains(control)))
                {
                    if (control.Visible)
                        control.Visible = false;
                    else
                        control.Visible = true;
                }
            });

            if (R14NotesTextbox.Location != DefaultNotesLandmark.Location)
            {
                R14NotesTextbox.Location = DefaultNotesLandmark.Location;
                R14NotesTextbox.Size = DefaultNotesSize;
            }
            else
            {
                R14NotesTextbox.Location = PrintNotesLandmark.Location;
                R14NotesTextbox.Size = PrintingNotesSize;

                if (CurrentEntry != null && CurrentEntry.items.Count > 0)
                {
                    CurrentEntry.items.ForEach(item =>
                    {
                        ItemsDataGridView.Rows.Add(new string[] { item.id, item.partNumber, item.serialNumber, item.location, item.notesR14 });
                    });
                }
            }

            ItemsDataGridView.ClearSelection();
            TitleLabel.Focus();
        }

        /// <summary>
        /// Turns Time Warp Mode on or off.
        /// </summary>
        public void TimeWarpSwitch()
        {
            LockedControls.ForEach(control =>
            {
                if (control.Visible)
                    control.Visible = false;
                else
                    control.Visible = true;
            });

            if (TimeWarpMode)
            {
                TimeWarpMode = false;
                SNIPDatagridview.ReadOnly = false;
                QueryPreviousButton.Text = "Query Previous";
            }
            else
            {
                TimeWarpMode = true;
                SNIPDatagridview.ReadOnly = true;
                QueryPreviousButton.Text = "Back to Current";
                InventoryNameLabel.Text = CurrentInventory.name;
            }

            NSNComboBox.Items.Clear();
            NSNComboBox.Text = "";
            ClearNSNTextboxes();
        }

        /// <summary>
        /// Prints out an R14 card for the currently queried R14 entry.
        /// </summary>
        public void PrintCard()
        {
            PrintingCard = new Bitmap(MainPanel.Width, MainPanel.Height);
            MainPanel.DrawToBitmap(PrintingCard, MainPanel.Bounds);

            if (PrintDocument.DefaultPageSettings.PaperSize != null && PrintDocument.DefaultPageSettings.PaperSource != null)
            {
                Bitmap Resized = new Bitmap(PrintingCard, new Size(PrintDocument.DefaultPageSettings.PaperSize.Height - 20, PrintDocument.DefaultPageSettings.PaperSize.Width - 20));
                PrintingCard = Resized;
                
                PrintDocument.Print();
            }
        }

        /// <summary>
        /// Prints out an R14 card for every R14 entry in the current inventory.
        /// </summary>
        public void BatchPrint()
        {
            /*  This function prints off every single R14
             *  entry in the current inventory...  Geesh.   */

            DialogResult confirm = MessageBox.Show("WARNING:\nYou are about to print off " + CurrentInventory.masterR14.entries.Count() + " cards...\nAre you sure you would like to continue?", "Batch Print Confirmation", MessageBoxButtons.YesNoCancel);

            if (confirm == DialogResult.No || confirm == DialogResult.Cancel)
            {
                ShowDefault(AdminPanel);

                SwitchEditMode();
                CurrentInventory.masterR14.entries.ForEach(entry =>
                {
                    NSNComboBox.Items.Clear();

                    NSNComboBox.Items.Add(entry.nsn);
                    NSNComboBox.Text = entry.nsn;

                    PrintCard();
                });
                SwitchEditMode();
            }
        }

        /// <summary>
        /// Displays the Add to SNIP menu.
        /// </summary>
        /// <param name="ID">The ID of the item to be added to the SNIP table.</param>
        public void AddToSNIP(string ID)
        {
            NewIDTextbox.Text = ID;

            ShowPanel(AddSNIPPanel, SnipSize);
        }

        /// <summary>
        /// Finds and adds the item to be added to the currently queried R14 entry.
        /// </summary>
        /// <param name="NewID">The ID of the item to be added.</param>
        /// <param name="Obsolete">Whether or not the current inventory is in an obsolete format.</param>
        public void AddItem(string NewID, bool Obsolete)
        {
            bool Found = false;
            PAMSEntry FoundEntry = new PAMSEntry();
            Item NewItem = new Item();

            if (Obsolete)
            {
                foreach (PAMSEntry Entry in CurrentInventory.masterPAMS.entries)
                {
                    if (Entry.id.Equals(NewID))
                    {
                        FoundEntry = Entry;

                        Found = true;
                        break;
                    }
                }

                if (Found)
                {
                    NewItem.id = FoundEntry.id;
                    NewItem.nomenclature = FoundEntry.nomenclature;
                    NewItem.partNumber = FoundEntry.partNumber;
                    NewItem.serialNumber = FoundEntry.serialNumber;
                    NewItem.location = FoundEntry.location;
                    NewItem.exchange = false;
                    NewItem.notesPAMS = FoundEntry.notes;
                    NewItem.notesR14 = "";

                    CurrentEntry.items.Add(NewItem);

                    if (CurrentEntry.items.Count() == CurrentEntry.inUse)
                        CurrentEntry.isComplete = true;
                    else if (CurrentEntry.items.Count() != CurrentEntry.inUse)
                        CurrentEntry.isComplete = false;

                    UpdateItemList(CurrentEntry.items.Count() - 1);
                }

                return;
            }

            if (!NewID.Equals("") && !NewID.Equals(null))
            {
                foreach (R14Entry Entry in CurrentInventory.masterR14.entries)
                {
                    if (Entry.items.Exists(x => x.id.Equals(NewID)))
                    {
                        Found = true;
                        break;
                    }
                }

                if (Found)
                {
                    DialogResult AddDuplicate = MessageBox.Show("The ID you entered has already been added to an entry in the current R14 file...\n\n" +
                        "Do you want to add a duplicate?", "Add Duplicate", MessageBoxButtons.YesNo);

                    if (AddDuplicate == DialogResult.No)
                    {
                        StopAutoScan();

                        return;
                    }
                    else
                        Found = false;
                }

                foreach (PAMSEntry Entry in CurrentInventory.masterPAMS.entries)
                {
                    if (Entry.id.Equals(NewID))
                    {
                        FoundEntry = Entry;

                        Found = true;
                        break;
                    }
                }

                if (!Found)
                {
                    foreach (PAMSEntry Entry in CurrentInventory.masterSNIP.entries)
                    {
                        if (Entry.id.Equals(NewID))
                        {
                            FoundEntry = Entry;

                            Found = true;
                            break;
                        }
                    }
                }

                if (!Found)
                {
                    DialogResult Add = MessageBox.Show("That ID could not be found in either the current Master PAMS or Master SNIP files...\n\n" +
                        "Would you like to add it to the current Master SNIP file?", "Add to Master SNIP", MessageBoxButtons.YesNo);

                    if (Add == DialogResult.Yes)
                        AddToSNIP(NewID);

                    StopAutoScan();
                }
                else
                {
                    NewItem.id = FoundEntry.id;
                    NewItem.nomenclature = FoundEntry.nomenclature;
                    NewItem.partNumber = FoundEntry.partNumber;
                    NewItem.serialNumber = FoundEntry.serialNumber;
                    NewItem.location = FoundEntry.location;
                    NewItem.exchange = false;
                    NewItem.notesPAMS = FoundEntry.notes;
                    NewItem.notesR14 = "";

                    CurrentEntry.items.Add(NewItem);

                    if (CurrentEntry.items.Count() >= CurrentEntry.inUse)
                        CurrentEntry.isComplete = true;
                    else if (CurrentEntry.items.Count() < CurrentEntry.inUse)
                        CurrentEntry.isComplete = false;

                    UpdateItemList(CurrentEntry.items.Count() - 1);
                }
            }
            else
                StopAutoScan();
        }

        /// <summary>
        /// Analyzes and interprets the contents of an unrefined R14 file.
        /// </summary>
        /// <param name="Data">The contents of the unrefined R14 file.</param>
        /// <returns>An R14 object with the contents of the unrefined R14 file.</returns>
        public static R14 DecodeUnrefinedR14(List<string> Data)
        {
            R14 NewR14 = new R14();
            NewR14.entries = new List<R14Entry>();
            R14Entry NewEntry;
            List<string> NewDataList;

            while (!Data[0].StartsWith("NBR"))
                Data.RemoveAt(0); 

            Data.RemoveAt(0);

            foreach (string Line in Data)
            {
                if (ContainsItem(Line))
                {
                    NewEntry = new R14Entry();
                    NewDataList = Line.Split().ToList<string>();

                    NewDataList.RemoveAll(x => x.Equals(""));

                    if (NewDataList[0].Equals("SUB"))
                        NewEntry.doc = NewR14.entries[NewR14.entries.Count() - 1].doc;
                    else
                        NewEntry.doc = NewDataList[0];

                    NewEntry.nsn = NewDataList[1].Replace("-", "");
                    NewEntry.psm = NewDataList[NewDataList.Count() - 13];
                    NewEntry.asc = NewDataList[NewDataList.Count() - 11];
                    NewEntry.auth = int.Parse(NewDataList[NewDataList.Count() - 10]);
                    NewEntry.inUse = int.Parse(NewDataList[NewDataList.Count() - 9]);
                    NewEntry.price = double.Parse(NewDataList[NewDataList.Count() - 4]);

                    if (NewEntry.inUse == 0)
                        NewEntry.isComplete = true;

                    NewR14.entries.Add(NewEntry);
                }
            }

            return NewR14;
        }

        /// <summary>
        /// Determines whether or not a line of an unrefined R14 file contains an item.
        /// </summary>
        /// <param name="Line">The line to be analyzed.</param>
        /// <returns>Whether or not a line of an unrefined R14 file contains an item.</returns>
        public static bool ContainsItem(string Line)
        {
            if (Line.StartsWith("00") || Line.StartsWith("01") || Line.StartsWith("02") || Line.StartsWith("03") || Line.StartsWith("04") || Line.StartsWith("05") || Line.StartsWith("06"))
                return true;
            else if (Line.StartsWith("SUB"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Imports and analyzes a selected PAMS file.
        /// </summary>
        /// <returns>A PAMS object with the contents of the PAMS file.</returns>
        public PAMS ImportPAMS()
        {
            PAMS NewPAMS = new PAMS();

            XmlSerializer Serializer;
            XmlReader Reader;

            string filePath = null;

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.FileName = @"master_id";
            fileDialog.DefaultExt = "xml";
            fileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            fileDialog.Title = "Import PAMS";
            fileDialog.InitialDirectory = Environment.SpecialFolder.UserProfile + @"\Downloads\";

            if (fileDialog.ShowDialog() != DialogResult.Cancel)
                filePath = fileDialog.FileName;

            //  If the filePath results in an unrefined Master-ID file...

            #region Unrefined PAMS

            if (filePath != "" && filePath != null && filePath.EndsWith("master_id.xml"))
            {
                try
                {
                    Serializer = new XmlSerializer(typeof(UnrefinedPAMSEntry));
                    Reader = XmlReader.Create(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

                    while (Reader.Read())
                    {
                        Reader.ReadToFollowing("tablenull");
                        if (Reader.LocalName == "tablenull")
                        {
                            PAMSEntry newEntry = new PAMSEntry();

                            Reader.ReadToFollowing("LBL_NBR");
                            Reader.ReadStartElement();
                            newEntry.id = Reader.Value;
                            Reader.ReadToFollowing("PART_NBR");
                            Reader.ReadStartElement();
                            newEntry.partNumber = Reader.Value;
                            Reader.ReadToFollowing("SERIAL_NBR");
                            Reader.ReadStartElement();
                            newEntry.serialNumber = Reader.Value;
                            Reader.ReadToFollowing("PART_NOUN");
                            Reader.ReadStartElement();
                            newEntry.nomenclature = Reader.Value;
                            Reader.ReadToFollowing("CALIBRATION_AREA");
                            Reader.ReadStartElement();
                            newEntry.location = Reader.Value;
                            Reader.ReadToFollowing("REMARKS");
                            Reader.ReadStartElement();
                            newEntry.notes = Reader.Value;

                            NewPAMS.entries.Add(newEntry);

                            Reader.ReadToNextSibling("tablenull");
                        }
                    }

                    MessageBox.Show("Import successful! " + NewPAMS.entries.Count() + " items found!");

                    Reader.Close();

                    return NewPAMS;
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred...\n\nError Description:\n\n(" + e.InnerException + ")\n"
                        + e.Message);

                    return null;
                }
            }

            #endregion

            //  If the filePath results in an archived PAMS XML file...

            #region Refined PAMS

            else if (filePath != "" && filePath != null && filePath.Contains("PAMS"))
            {
                try
                {
                    Serializer = new XmlSerializer(typeof(PAMS));
                    Reader = XmlReader.Create(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

                    NewPAMS = (PAMS)Serializer.Deserialize(Reader);

                    MessageBox.Show("Import successful! " + NewPAMS.entries.Count() + " items found!");

                    Reader.Close();

                    return NewPAMS;
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred...\n\nError Description:\n\n(" + e.InnerException + ")\n"
                        + e.Message);

                    return null;
                }
            }

            #endregion

            else if (filePath == "" || filePath == null)
                return null;
            else
            {
                MessageBox.Show("Please use a valid file...\n\n" +
                    "You must either use a master_id.xml file or an archived PAMS_(DateAndTime).xml file...");

                return null;
            }
        }

        /// <summary>
        /// Imports a selected R14 file.
        /// </summary>
        /// <returns>An R14 object with the contents of the R14 file.</returns>
        public R14 ImportR14()
        {
            R14 NewR14 = new R14();

            List<string> Data = new List<string>();
            var WordApp = new Word.Application();

            string filePath = null;

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.FileName = @"ReportServlet";
            fileDialog.DefaultExt = "txt";
            fileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            fileDialog.Title = "Import R14";
            fileDialog.InitialDirectory = Environment.SpecialFolder.UserProfile + @"\Downloads\";

            if (fileDialog.ShowDialog() != DialogResult.Cancel)
                filePath = fileDialog.FileName;

            if (filePath != "" && filePath != null)
            {
                Word.Document Document = WordApp.Documents.Open(filePath);

                foreach (Word.Paragraph objParagraph in Document.Paragraphs)
                {
                    Data.Add(objParagraph.Range.Text.Trim());
                }

                Document.Close();
                WordApp.Quit();

                NewR14 = DecodeUnrefinedR14(Data);

                MessageBox.Show("Import successful! " + NewR14.entries.Count() + " entries found!");

                return NewR14;
            }
            else if (filePath == "" || filePath == null)
                return null;
            else
            {
                MessageBox.Show("Please use a valid file...\n\n" +
                    "You must use a ReportServlet.txt file...");

                return null;
            }
        }

        /// <summary>
        /// Imports either the current inventory file or a chosen archived inventory file.
        /// </summary>
        /// <param name="OpenImport">Whether or not the function is being triggered on application startup.</param>
        /// <returns>An Inventory object with the contents of the inventory file.</returns>
        public Inventory ImportInventory(bool OpenImport)
        {
            Inventory NewInventory = new Inventory();

            XmlSerializer Serializer;
            XmlReader Reader;

            string filePath = null;

            if (!OpenImport)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.FileName = @"Inventory";
                fileDialog.DefaultExt = "xml";
                fileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                fileDialog.InitialDirectory = Environment.SpecialFolder.UserProfile + @"\Archive\";

                if (fileDialog.ShowDialog() != DialogResult.Cancel)
                    filePath = fileDialog.FileName;
            }
            else
                filePath = Application.StartupPath + @"\Current\Inventory.xml";

            if (filePath != null && filePath != "")
            {
                try
                {
                    Serializer = new XmlSerializer(typeof(Inventory));
                    Reader = XmlReader.Create(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

                    NewInventory = (Inventory)Serializer.Deserialize(Reader);

                    Reader.Close();

                    return NewInventory;
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred...\n\nError Description:\n\n(" + e.InnerException + ")\n"
                        + e.Message);

                    return null;
                }
            }
            else if (filePath == "" || filePath == null)
                return null;
            else
            {
                MessageBox.Show("Please use a valid file...\n\n" +
                    "You must use an Inventory.xml file...");

                return null;
            }
        }

        /// <summary>
        /// Exports and archives either an R14 or PAMS object to an appropriate XML file.
        /// </summary>
        /// <param name="ClassType">The type of object being exported (Inventory, PAMS, or R14).</param>
        /// <param name="Item">The object to be exported.</param>
        /// <param name="ClassString">The string representation of the type of the object to be exported ("Inventory", "PAMS", or "R14).</param>
        /// <param name="AutoSave">Whether or not the function is being triggered as an inventory auto-save.</param>
        public void ExportFile(Type ClassType, Object Item, string ClassString, bool AutoSave)
        {
            XmlSerializer Serializer = new XmlSerializer(ClassType);
            XmlWriter Writer;

            string filePath = null;

            if (!AutoSave)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.FileName = ClassString + " " + CurrentInventory.name;
                fileDialog.DefaultExt = "xml";
                fileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                fileDialog.InitialDirectory = Application.StartupPath + @"\Archive\";

                if (fileDialog.ShowDialog() != DialogResult.Cancel)
                    filePath = fileDialog.FileName;
            }
            else if (AutoSave && ClassType == typeof(Inventory))
                filePath = Application.StartupPath + @"\Current\Inventory.xml";

            if (filePath != "" && filePath != null && filePath.EndsWith("xml"))
            {
                try
                {
                    Writer = XmlWriter.Create(new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite));

                    Serializer.Serialize(Writer, Item);

                    MessageBox.Show(ClassString + " file successfully saved!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("An error occurred...\n\nError Description:\n\n(" + e.InnerException + ")\n"
                        + e.Message);
                }
            }
            else if (filePath == "" || filePath == null) { }
            else
                MessageBox.Show("Please use a valid file...\n\n" +
                    "You must save as a .xml file...");
        }

        /// <summary>
        /// Creates an Excel workbook and exports the contents of the current R14 file to Excel spreadsheets.
        /// </summary>
        public void ExportToExcel()
        {
            Excel.Application ExcelApp = new Excel.Application();

            if (ExcelApp == null)
            {
                MessageBox.Show("Excel could not be started...  Make sure your Excel installation is correct...");

                return;
            }

            Excel.Workbook workbook = ExcelApp.Workbooks.Add();

            Excel.Worksheet completeWorksheet = workbook.Worksheets.Add();
            completeWorksheet.Name = "Complete";
            int completeIndex = 2;

            Excel.Worksheet overScannedWorksheet = workbook.Worksheets.Add();
            overScannedWorksheet.Name = "Over-Scanned";
            int overScannedIndex = 2;

            Excel.Worksheet underScannedWorksheet = workbook.Worksheets.Add();
            underScannedWorksheet.Name = "Under-Scanned";
            int underScannedIndex = 2;

            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                if (sheet.Name != "Complete" && sheet.Name != "Over-Scanned" && sheet.Name != "Under-Scanned")
                {
                    sheet.Delete();

                    continue;
                }

                sheet.Cells[1, "A"].Value2 = "NSN";
                sheet.Cells[1, "B"].Value2 = "DOC";
                sheet.Cells[1, "C"].Value2 = "PSM";
                sheet.Cells[1, "D"].Value2 = "In Use";
                sheet.Cells[1, "E"].Value2 = "Total Found";
            }

            Excel.Worksheet destination;
            int currentIndex;

            CurrentInventory.masterR14.entries.ForEach(entry =>
            {
                if (!entry.isComplete)
                {
                    destination = underScannedWorksheet;
                    currentIndex = underScannedIndex;
                }
                else if (entry.items.Count() > entry.inUse)
                {
                    destination = overScannedWorksheet;
                    currentIndex = overScannedIndex;
                }
                else
                {
                    destination = completeWorksheet;
                    currentIndex = completeIndex;
                }

                destination.Cells[currentIndex, "A"].Value2 = "'" + entry.nsn;
                destination.Cells[currentIndex, "B"].Value2 = "'" + entry.doc;
                destination.Cells[currentIndex, "C"].Value2 = "'" + entry.psm;
                destination.Cells[currentIndex, "D"].Value2 = entry.inUse.ToString();
                destination.Cells[currentIndex, "E"].Value2 = entry.items.Count.ToString();

                if (!entry.isComplete)
                    underScannedIndex++;
                else if (entry.items.Count() > entry.inUse)
                    overScannedIndex++;
                else
                    completeIndex++;
            });

            foreach(Excel.Worksheet sheet in workbook.Worksheets)
            {
                for (int i = 1; i <= 5; i++)
                    sheet.Columns[i].AutoFit();
            }

            ExcelApp.Visible = true;
        }

        /// <summary>
        /// Hides or shows all Admin controls while a particularly lengthy operation is performed.
        /// </summary>
        public void AdminWait()
        {
            AdminControls.ForEach(control =>
            {
                if (!TimeWarpMode || (TimeWarpMode && !LockedControls.Contains(control)))
                {
                    if (control.Visible)
                        control.Visible = false;
                    else
                        control.Visible = true;
                }
            });

            Application.DoEvents();
        }

        /// <summary>
        /// Sounds an alert tone and halts the auto-scan function.
        /// </summary>
        public void StopAutoScan()
        {
            if (AutoScan)
            {
                SystemSounds.Asterisk.Play();

                AutoScan = false;
            }
        }

        #endregion
    }
}