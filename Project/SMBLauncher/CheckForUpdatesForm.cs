using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace SMBLauncher
{
    public partial class CheckForUpdatesForm : Form
    {
        public CheckForUpdatesForm()
        {
            InitializeComponent();            
        }

        string filePath = "";
        string webPath = "";

        public void RunCheck(object sender, EventArgs e)
        {
            string url = "https://tomnaughton.net/projects/smbl/v2/";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string html = reader.ReadToEnd();
                    Regex regex = new Regex(GetDirectoryListingRegexForUrl(url));
                    MatchCollection matches = regex.Matches(html);
                    Console.WriteLine("Matches: " + matches.Count);
                    if (matches.Count > Program._ID)
                    {
                        lblStatus.Text = "New version found";

                        if (MessageBox.Show("New version found. Download now?", "Update", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            webPath = "https://tomnaughton.net/projects/smbl/v2/" + matches[matches.Count - 1].Groups["name"];
                            filePath = KnownFolders.GetPath(KnownFolder.Downloads) + @"\" + matches[matches.Count - 1].Groups["name"];
                            startDownload();
                        }
                        else
                            Close();
                    }
                    else
                    {
                        lblStatus.Text = "No new version";
                        Close();
                    }
                }
            }
        }

        public static string GetDirectoryListingRegexForUrl(string url)
        {
            if (url.Equals("https://tomnaughton.net/projects/smbl/v2/"))
            {
                return "<a href=\".*\">(?<name>.*)</a>";
            }
            throw new NotSupportedException();
        }

        private void startDownload()
        {
            Thread thread = new Thread(() => {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(webPath), filePath);
            });
            thread.Start();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (!this.IsHandleCreated)
                this.CreateControl();

            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                lblStatus.Text = "Downloaded " + string.Format("{0:P2}.", (decimal)e.BytesReceived / (decimal)e.TotalBytesToReceive);
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate {
                lblStatus.Text = "Completed";
                System.Diagnostics.Process.Start(filePath);
                Environment.Exit(0);
            });
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
