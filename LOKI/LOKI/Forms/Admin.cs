using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace LOKI.Forms
{
    public partial class Admin : Form
    {
        public List<Control> AdminControls;
        public Bitmap PrintingCard;

        public Admin()
        {
            InitializeComponent();

            AdminControls = new List<Control>()
            {
                IOGroupBox,
                SpecialGroupBox,
                CloseButton,
                WaitLabel
            };
        }

        #region Controls

        private void ExportPAMSButton_Click(object sender, EventArgs e)
        {
            ExportFile(typeof(PAMS), GlobalVariables.CurrentInventory.masterPAMS, "PAMS", false);
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
                ExportFile(typeof(PAMS), GlobalVariables.CurrentInventory.masterPAMS, "PAMS", false);

                TempPAMS = new PAMS();

                TempPAMS = ImportPAMS();

                if (TempPAMS != null)
                    GlobalVariables.CurrentInventory.masterPAMS = TempPAMS;
            }
            else
            {
                TempPAMS = new PAMS();

                TempPAMS = ImportPAMS();

                if (TempPAMS != null)
                    GlobalVariables.CurrentInventory.masterPAMS = TempPAMS;
            }
        }

        private void ExportR14Button_Click(object sender, EventArgs e)
        {
            ExportFile(typeof(R14), GlobalVariables.CurrentInventory.masterR14, "R14", false);
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
                ExportFile(typeof(R14), GlobalVariables.CurrentInventory.masterR14, "R14", false);

                TempR14 = new R14();

                TempR14 = ImportR14();

                if (TempR14 != null)
                    GlobalVariables.CurrentInventory.masterR14 = TempR14;
            }
            else
            {
                GlobalVariables.CurrentInventory.masterR14 = new R14();

                TempR14 = new R14();

                TempR14 = ImportR14();

                if (TempR14 != null)
                    GlobalVariables.CurrentInventory.masterR14 = TempR14;
            }
        }

        private void NewInventoryButton_Click(object sender, EventArgs e)
        {
            ExportFile(typeof(Inventory), GlobalVariables.CurrentInventory, "Inventory", false);

            StartNewInventory();
        }

        private void AdminCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BatchPrintButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExcelExportButton_Click(object sender, EventArgs e)
        {
            AdminWait();
            ExportToExcel();
            AdminWait();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Exports and archives either an R14 or PAMS object to an appropriate XML file.
        /// </summary>
        /// <param name="ClassType">The type of object being exported (Inventory, PAMS, or R14).</param>
        /// <param name="Item">The object to be exported.</param>
        /// <param name="ClassString">The string representation of the type of the object to be exported ("Inventory", "PAMS", or "R14").</param>
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

            GlobalVariables.CurrentInventory.masterR14.entries.ForEach(entry =>
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

            foreach (Excel.Worksheet sheet in workbook.Worksheets)
            {
                for (int i = 1; i <= 5; i++)
                    sheet.Columns[i].AutoFit();
            }

            ExcelApp.Visible = true;
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
        /// Initiates a new inventory and establishes a saved inventory file.
        /// </summary>
        public void StartNewInventory()
        {
            GlobalVariables.CurrentInventory = new Inventory();

            GlobalVariables.CurrentInventory.masterPAMS = new PAMS();
            GlobalVariables.CurrentInventory.masterR14 = new R14();
            GlobalVariables.CurrentInventory.masterSNIP = new PAMS();

            MessageBox.Show("Please choose the R14 file you would like to start the new inventory with...");
            GlobalVariables.CurrentInventory.masterR14 = ImportR14();

            if (GlobalVariables.CurrentInventory.masterR14 == null)
            {
                MessageBox.Show("You must select a valid file for the R14 file...\n\nThe program will now exit.");
                System.Environment.Exit(0);
            }

            MessageBox.Show("Please choose the PAMS file you would like to start the new inventory with...");
            GlobalVariables.CurrentInventory.masterPAMS = ImportPAMS();

            if (GlobalVariables.CurrentInventory.masterPAMS == null)
            {
                MessageBox.Show("You must select a valid file for the PAMS file...\n\nThe program will now exit.");
                System.Environment.Exit(0);
            }

            GlobalVariables.CurrentInventory.masterSNIP = new PAMS();
            GlobalVariables.CurrentInventory.Rename();

            ExportFile(typeof(Inventory), GlobalVariables.CurrentInventory, "Inventory", true);
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
        /// Hides or shows all Admin controls while a particularly lengthy operation is performed.
        /// </summary>
        public void AdminWait()
        {
            AdminControls.ForEach(control =>
            {
                if (control.Visible)
                    control.Visible = false;
                else
                    control.Visible = true;
            });

            Application.DoEvents();
        }

        #endregion
    }
}
