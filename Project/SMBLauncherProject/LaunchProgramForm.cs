using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMBLauncherProject
{
    public partial class LaunchProgramForm : Form
    {
        public LaunchProgramForm()
        {
            InitializeComponent();
        }

        public LaunchProgram launchProgram = new LaunchProgram();

        private void LblDone_Click(object sender, EventArgs e)
        {
            if(tbName.Text.Length == 0)
            {
                MessageBox.Show("Please enter a name.", "Error", MessageBoxButtons.OK);
                return;
            }

            if(tbLocation.Text.Length == 0)
            {
                MessageBox.Show("Please enter a location.", "Error", MessageBoxButtons.OK);
                return;
            }

            if(!System.IO.File.Exists(tbLocation.Text))
            {
                MessageBox.Show("Program not found. Please include the program in the location text.", "Error", MessageBoxButtons.OK);
                return;
            }

            launchProgram.name = tbName.Text;
            launchProgram.location = tbLocation.Text;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
