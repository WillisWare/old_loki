using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOKI.Forms
{
    public partial class Incompletes : Form
    {
        public Incompletes()
        {
            InitializeComponent();

            GlobalVariables.IncompletesForm = this;

            UpdateIncompletes();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            GlobalVariables.IncompletesForm = null;

            this.Close();
        }

        public void UpdateIncompletes()
        {
            IncompleteDatagridview.Rows.Clear();

            foreach (R14Entry entry in GlobalVariables.CurrentInventory.masterR14.entries)
                if (entry.isComplete == false)
                    IncompleteDatagridview.Rows.Add(new string[] { entry.nsn, entry.inUse.ToString(), entry.items.Count().ToString() });

            IncompleteTotalTextbox.Text = (IncompleteDatagridview.Rows.Count - 1).ToString();
        }
    }
}
