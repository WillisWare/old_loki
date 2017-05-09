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
using LOKI.Forms;

namespace LOKI
{
    public partial class Main : Form
    {
        public Bitmap PrintingCard;

        public Size MainSize;
        public Size SnipSize;
        public Size DefaultNotesSize;
        public Size PrintingNotesSize;

        public bool AutoScan;
        public List<Control> EditControls;
        public List<Control> AdminControls;
        public List<Control> LockedControls;

        /// <summary>
        /// The entry point for the MainForm class.
        /// </summary>
        public Main()
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
                    GlobalVariables.CurrentInventory = TempInventory;

                    MessageBox.Show("Inventory loaded successfully!");

                    OptimizeInventory();
                }
                else
                {
                    MessageBox.Show("There was an error loading the current inventory...\n\nCreating a new inventory...");

                    using (Admin admin = new Admin())
                        admin.StartNewInventory();
                }
            }
            else
            {
                MessageBox.Show("No current inventory detected...\n\nCreating new inventory...");

                Directory.CreateDirectory(Application.StartupPath + @"\Current");

                using (Admin admin = new Admin())
                    admin.StartNewInventory();
            }

            /*  The rest of this is setup for panel sizes and locations
             *  and filling up the control lists.   */

            DefaultNotesSize = new Size(R14NotesTextbox.Size.Width, R14NotesTextbox.Size.Height);
            PrintingNotesSize = new Size(PrintNotesSize.Location.X - PrintNotesLandmark.Location.X, PrintNotesSize.Location.Y - PrintNotesLandmark.Location.Y);
            
            MainPanel.Visible = true;

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
            GlobalVariables.TimeWarpMode = false;

            if (!Directory.Exists(Application.StartupPath + @"\Archive"))
                Directory.CreateDirectory(Application.StartupPath + @"\Archive");

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

                    foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
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

                        foreach (PAMSEntry Entry in GlobalVariables.CurrentInventory.masterPAMS.entries)
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
                        GlobalVariables.CurrentEntry = FoundEntries[0];

                        NSNComboBox.Items.Clear();

                        foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
                            if (Entry.doc.Equals(GlobalVariables.CurrentEntry.doc))
                                NSNComboBox.Items.Add(Entry.nsn);

                        NSNComboBox.Text = GlobalVariables.CurrentEntry.nsn;

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
                    foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
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
                        GlobalVariables.CurrentEntry = FoundEntries[0];

                        NSNComboBox.Items.Clear();

                        foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
                            if (Entry.doc.Equals(GlobalVariables.CurrentEntry.doc))
                                NSNComboBox.Items.Add(Entry.nsn);

                        NSNComboBox.Text = GlobalVariables.CurrentEntry.nsn;
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
                                        GlobalVariables.CurrentEntry = FoundEntries[result];

                                        NSNComboBox.Items.Clear();

                                        foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
                                            if (Entry.doc.Equals(GlobalVariables.CurrentEntry.doc))
                                                NSNComboBox.Items.Add(Entry.nsn);

                                        NSNComboBox.Text = GlobalVariables.CurrentEntry.nsn;

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
                    foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
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
                        GlobalVariables.CurrentEntry = FoundEntries[0];

                        NSNComboBox.Items.Clear();

                        foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
                        {
                            if (Entry.doc.Equals(GlobalVariables.CurrentEntry.doc))
                            {
                                NSNComboBox.Items.Add(Entry.nsn);
                            }
                        }

                        NSNComboBox.Text = GlobalVariables.CurrentEntry.nsn;
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
            GlobalVariables.IncompletesForm = new Incompletes();

            GlobalVariables.IncompletesForm.Show();
        }

        private void AddNewIDButton_Click(object sender, EventArgs e)
        {
            if (GlobalVariables.CurrentEntry == null)
            {
                MessageBox.Show("You must have an entry queried to add an ID...");
                return;
            }

            string NewID = Interaction.InputBox("Enter the ID you would like to add:", "Add New ID", "");

            AddItem(NewID);
        }

        private void DeleteIDButton_Click(object sender, EventArgs e)
        {
            if (IDsListbox.SelectedIndex == -1)
            {
                MessageBox.Show("You must have an ID selected to delete it...");
                return;
            }

            DialogResult ConfirmDelete = MessageBox.Show("Are you sure you want to delete " + IDsListbox.Text + "?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (ConfirmDelete == DialogResult.Yes)
            {
                GlobalVariables.CurrentEntry.items.RemoveAt(GlobalVariables.CurrentEntry.items.FindIndex(x => x.id.Equals(IDsListbox.Text)));

                if (GlobalVariables.CurrentEntry.items.Count() >= GlobalVariables.CurrentEntry.inUse)
                    GlobalVariables.CurrentEntry.isComplete = true;
                else if (GlobalVariables.CurrentEntry.items.Count() < GlobalVariables.CurrentEntry.inUse)
                    GlobalVariables.CurrentEntry.isComplete = false;

                if (GlobalVariables.IncompletesForm != null)
                    GlobalVariables.IncompletesForm.UpdateIncompletes();

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
            using (Admin admin = new Admin())
                if (admin.ShowDialog() == DialogResult.Yes)
                    BatchPrint();
        }

        private void SNIPTableButton_Click(object sender, EventArgs e)
        {
            using (ReviewSNIP snip = new ReviewSNIP())
                snip.ShowDialog();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (IDsListbox.Text != "" && IDsListbox.Text != null)
                GlobalVariables.CurrentEntry.items[GlobalVariables.CurrentEntry.items.FindIndex(x => x.id.Equals(IDsListbox.Text))].notesR14 = R14NotesTextbox.Text;
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

            foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
            {
                if (Entry.nsn.Equals(NSNComboBox.Text) && Entry.doc.Equals(GlobalVariables.CurrentEntry.doc))
                {
                    FoundEntry = GlobalVariables.CurrentInventory.masterR14.entries.Find(x => x.nsn.Equals(NSNComboBox.Text) && x.doc.Equals(GlobalVariables.CurrentEntry.doc));

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
                GlobalVariables.CurrentEntry = FoundEntry;
                UpdateNSNTextboxes();
            }
        }

        private void IDsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateItemTextboxes(GlobalVariables.CurrentEntry.items.Find(x => x.id.Equals(IDsListbox.SelectedValue)));
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

            if (!GlobalVariables.TimeWarpMode)
            {
                var Response = MessageBox.Show("Would you like to save any changes made to the current inventory?", "Save and Exit?", MessageBoxButtons.YesNoCancel);
                if (Response == DialogResult.Yes)
                {
                    ExportFile(typeof(Inventory), GlobalVariables.CurrentInventory, "Inventory", true);
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

            if (!GlobalVariables.TimeWarpMode)
            {
                if (MessageBox.Show("Would you like to save your changes?\nUnsaved Changes will be lost...", "Save Changes?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    ExportFile(typeof(Inventory), GlobalVariables.CurrentInventory, "Inventory", true);

                Inventory tempInventory = ImportInventory(false);
                
                if (tempInventory != null)
                {
                    GlobalVariables.CurrentInventory = tempInventory;

                    TimeWarpSwitch();
                }
            }
            else
            {
                GlobalVariables.CurrentInventory = ImportInventory(true);

                TimeWarpSwitch();
            }
        }

        #endregion

        #endregion

        #region Functions

        #region Data Population

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

            DOCTextbox.Text = GlobalVariables.CurrentEntry.doc;
            AuthTextbox.Text = GlobalVariables.CurrentEntry.auth.ToString();
            InUseTextbox.Text = GlobalVariables.CurrentEntry.inUse.ToString();
            PriceTextbox.Text = GlobalVariables.CurrentEntry.price.ToString();
            PSMTextbox.Text = GlobalVariables.CurrentEntry.psm;
            ASCTextbox.Text = GlobalVariables.CurrentEntry.asc;

            UpdateItemList(0);
        }

        /// <summary>
        /// Populates all textboxes pertaining to an R14 entry's item information.
        /// </summary>
        /// <param name="SelectedItem"></param>
        public void UpdateItemTextboxes(Item SelectedItem)
        {
            CompletedCheckbox.Checked = GlobalVariables.CurrentEntry.isComplete;

            if (GlobalVariables.CurrentEntry.items.Count() != 0)
            {
                if (SelectedItem == null)
                    SelectedItem = GlobalVariables.CurrentEntry.items.Find(x => x.id.Equals(IDsListbox.Text));

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

            GlobalVariables.CurrentEntry.items.ForEach(item =>
            {
                IDsListbox.Items.Add(item.id);
            });

            if (IDsListbox.Items.Count > 0)
                IDsListbox.SelectedIndex = Index;

            TotalFoundTextbox.Text = GlobalVariables.CurrentEntry.items.Count().ToString();
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
        /// Ensures that all R14 entries in the current inventory have appropriate completion categorizations and that the current inventory is named.
        /// </summary>
        public void OptimizeInventory()
        {
            bool modified = false;

            GlobalVariables.CurrentInventory.masterR14.entries.ForEach(entry =>
            {
                if (entry.items.Count() >= entry.inUse && !entry.isComplete)
                {
                    entry.isComplete = true;
                    modified = true;
                }
            });

            if (GlobalVariables.CurrentInventory.name == "" || GlobalVariables.CurrentInventory.name == null)
            {
                GlobalVariables.CurrentInventory.Rename();
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
                if (!GlobalVariables.TimeWarpMode || (GlobalVariables.TimeWarpMode && !LockedControls.Contains(control)))
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

                if (GlobalVariables.CurrentEntry != null && GlobalVariables.CurrentEntry.items.Count > 0)
                {
                    GlobalVariables.CurrentEntry.items.ForEach(item =>
                    {
                        ItemsDataGridView.Rows.Add(new string[] { item.id, item.partNumber, item.serialNumber, item.location, item.notesR14 });
                    });
                }
            }

            ItemsDataGridView.ClearSelection();
            TitleLabel.Focus();
            Application.DoEvents();
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

            if (GlobalVariables.TimeWarpMode)
            {
                GlobalVariables.TimeWarpMode = false;
                QueryPreviousButton.Text = "Query Previous";
            }
            else
            {
                GlobalVariables.TimeWarpMode = true;
                QueryPreviousButton.Text = "Back to Current";
                InventoryNameLabel.Text = GlobalVariables.CurrentInventory.name;
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
            DialogResult confirm = MessageBox.Show("WARNING:\nYou are about to print off " + GlobalVariables.CurrentInventory.masterR14.entries.Count() + " cards...\nAre you sure you would like to continue?", "Batch Print Confirmation", MessageBoxButtons.YesNoCancel);

            if (confirm != DialogResult.No && confirm != DialogResult.Cancel)
            {
                SwitchEditMode();
                GlobalVariables.CurrentInventory.masterR14.entries.ForEach(entry =>
                {
                    NSNComboBox.Items.Clear();

                    GlobalVariables.CurrentEntry = entry;

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
            using (AddSNIP snip = new AddSNIP())
            {
                snip.NewIDTextbox.Text = ID;

                if (snip.ShowDialog(this) == DialogResult.OK)
                    UpdateItemList(GlobalVariables.CurrentEntry.items.Count() - 1);
            }
        }

        /// <summary>
        /// Finds and adds the item to be added to the currently queried R14 entry.
        /// </summary>
        /// <param name="NewID">The ID of the item to be added.</param>
        /// <param name="Obsolete">Whether or not the current inventory is in an obsolete format.</param>
        public void AddItem(string NewID)
        {
            bool Found = false;
            PAMSEntry FoundEntry = new PAMSEntry();
            Item NewItem = new Item();

            if (!NewID.Equals("") && !NewID.Equals(null))
            {
                foreach (R14Entry Entry in GlobalVariables.CurrentInventory.masterR14.entries)
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

                foreach (PAMSEntry Entry in GlobalVariables.CurrentInventory.masterPAMS.entries)
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
                    foreach (PAMSEntry Entry in GlobalVariables.CurrentInventory.masterSNIP.entries)
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

                    GlobalVariables.CurrentEntry.items.Add(NewItem);

                    if (GlobalVariables.CurrentEntry.items.Count() >= GlobalVariables.CurrentEntry.inUse)
                        GlobalVariables.CurrentEntry.isComplete = true;
                    else if (GlobalVariables.CurrentEntry.items.Count() < GlobalVariables.CurrentEntry.inUse)
                        GlobalVariables.CurrentEntry.isComplete = false;

                    if (GlobalVariables.IncompletesForm != null)
                        GlobalVariables.IncompletesForm.UpdateIncompletes();

                    UpdateItemList(GlobalVariables.CurrentEntry.items.Count() - 1);
                }
            }
            else
                StopAutoScan();
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
                fileDialog.FileName = ClassString + " " + GlobalVariables.CurrentInventory.name;
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