﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMBLauncher
{
    class Program
    {

        public static int _ID = 4;

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                        return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
