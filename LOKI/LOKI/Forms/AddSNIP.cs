using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOKI
{
    public partial class AddSNIP : Form
    {
        public AddSNIP()
        {
            

            InitializeComponent();
        }



        private void SNIPCancelButton_Click(object sender, EventArgs e)
        {
            /*  There should probably be some sort of
             *  "Exit Without Saving?" prompt here,
             *  but the EMO monitors haven't said anything
             *  about needing it.  Soooooo...   */

            this.Close();
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

            GlobalVariables.CurrentInventory.masterSNIP.entries.Add(newEntry);

            newItem.id = newEntry.id;
            newItem.nomenclature = newEntry.nomenclature;
            newItem.partNumber = newEntry.partNumber;
            newItem.serialNumber = newEntry.serialNumber;
            newItem.location = newEntry.location;
            newItem.exchange = false;
            newItem.notesPAMS = newEntry.notes;
            newItem.notesR14 = "";

            GlobalVariables.CurrentEntry.items.Add(newItem);

            if (GlobalVariables.CurrentEntry.items.Count() == GlobalVariables.CurrentEntry.inUse)
                GlobalVariables.CurrentEntry.isComplete = true;
            else if (GlobalVariables.CurrentEntry.items.Count() != GlobalVariables.CurrentEntry.inUse)
                GlobalVariables.CurrentEntry.isComplete = false;

            this.Close();
        }
    }
}
