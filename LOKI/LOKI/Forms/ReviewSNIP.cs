using System;
using System.Windows.Forms;

namespace LOKI.Forms
{
    public partial class ReviewSNIP : Form
    {
        public bool Initialization = true;

        public ReviewSNIP()
        {
            InitializeComponent();

            UpdateSNIPList();

            if (GlobalVariables.TimeWarpMode)
            {
                SNIPDatagridview.ReadOnly = true;
            }
            else
            {
                SNIPDatagridview.ReadOnly = false;
            }

            Initialization = false;
        }

        public void UpdateSNIPList()
        {
            SNIPDatagridview.Rows.Clear();

            foreach (PAMSEntry thisEntry in GlobalVariables.CurrentInventory.masterSNIP.entries)
                SNIPDatagridview.Rows.Add(new string[] { thisEntry.id, thisEntry.partNumber, thisEntry.serialNumber, thisEntry.nomenclature });

            SNIPTotalTextbox.Text = (SNIPDatagridview.Rows.Count - 1).ToString();
        }

        private void SNIPDatagridview_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!GlobalVariables.TimeWarpMode)
            {
                if (SNIPDatagridview.SelectedCells.Count != 0)
                {
                    DataGridViewCell changedCell = SNIPDatagridview.SelectedCells[0];

                    if (changedCell.ColumnIndex == 0)
                        GlobalVariables.CurrentInventory.masterSNIP.entries[changedCell.RowIndex].id = changedCell.Value.ToString();
                    else if (changedCell.ColumnIndex == 1)
                        GlobalVariables.CurrentInventory.masterSNIP.entries[changedCell.RowIndex].partNumber = changedCell.Value.ToString();
                    else if (changedCell.ColumnIndex == 2)
                        GlobalVariables.CurrentInventory.masterSNIP.entries[changedCell.RowIndex].serialNumber = changedCell.Value.ToString();
                    else
                        GlobalVariables.CurrentInventory.masterSNIP.entries[changedCell.RowIndex].nomenclature = changedCell.Value.ToString();

                    MessageBox.Show("SNIP entry updated!");
                }
            }
            else if (!Initialization)
            {
                MessageBox.Show("You can't edit the SNIP table entries while viewing a previous inventory!");

                UpdateSNIPList();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}