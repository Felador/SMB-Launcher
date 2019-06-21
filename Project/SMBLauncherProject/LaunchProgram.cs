using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMBLauncherProject
{
    [System.Serializable]
    public class LaunchProgram
    {
        public LaunchProgram()
        {
            this.id = idMax;
            idMax++;
        }

        public string location;
        public string name;
        public bool enabled = true;

        private int id;

        public int GetID()
        {
            return id;
        }

        public static int idMax;

        public static ListViewItem GetLVI(LaunchProgram launchProgram)
        {
            ListViewItem lvi = new ListViewItem(launchProgram.name);
            lvi.Checked = launchProgram.enabled;
            lvi.SubItems.Add(launchProgram.location);
            return lvi;
        }
    }
}
