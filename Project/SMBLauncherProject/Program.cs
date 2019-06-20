using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMBLauncherProject
{
    static class Program
    {

        public static int _ID = 5;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
        public static bool CheckForInternetConnection()
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("8.8.8.8", 1000);

            return reply.Status == IPStatus.Success ? true : false;
        }
    }
}
