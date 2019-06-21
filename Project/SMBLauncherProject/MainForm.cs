using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SMBLauncherProject
{
    [System.Serializable]
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Console.WriteLine("MainForm::MainForm");
            CheckForUpdatesForm cfuForm = new CheckForUpdatesForm();
            cfuForm.ShowDialog();

            InitializeComponent();

            Data d = SaveLoad.LoadData();

            if (d != null)
            {
                data = d;

                if (data.ctrls != null)
                {
                    cbUp.Text = data.ctrls.pc.up;
                    cbDown.Text = data.ctrls.pc.down;
                    cbLeft.Text = data.ctrls.pc.left;
                    cbRight.Text = data.ctrls.pc.right;
                    cbJump.Text = data.ctrls.pc.jump;
                    cbSpecial.Text = data.ctrls.pc.special;

                    cbGPJump.SelectedIndex = Data.Ctrls.Gamepad.GetGamepadNumber(data.ctrls.gamepad.jump) - 1;
                    cbGPSpecial.SelectedIndex = Data.Ctrls.Gamepad.GetGamepadNumber(data.ctrls.gamepad.special) - 1;
                    cbUseAnalog.Checked = data.ctrls.gamepad.useAnalog;
                }
            }

            Size size = new Size(pSettings.Width, pSettings.Height - 50);

            pSteamLocation.Location = new Point(3, 3);
            pSteamLocation.Size = size;

            pControls.Location = new Point(3, 3);
            pControls.Size = size;

            pLaunchOptions.Location = new Point(3, 3);
            pLaunchOptions.Size = size;

            if (data.steamLocation.Length == 0)
            {
                // auto search
                AutoSearchForSteamFolder();
            }

            try
            {
                string smbLoc = GetSmbLocation(data.steamLocation);
                if (d == null && Directory.Exists(data.steamLocation) && File.Exists(smbLoc + @"\buttonmap.cfg"))
                {
                    data.ctrls = SaveLoad.LoadControlsFromGameFile(data);

                    cbUp.Text = data.ctrls.pc.up;
                    cbDown.Text = data.ctrls.pc.down;
                    cbLeft.Text = data.ctrls.pc.left;
                    cbRight.Text = data.ctrls.pc.right;
                    cbJump.Text = data.ctrls.pc.jump;
                    cbSpecial.Text = data.ctrls.pc.special;

                    cbGPJump.SelectedIndex = Data.Ctrls.Gamepad.GetGamepadNumber(data.ctrls.gamepad.jump) - 1;
                    cbGPSpecial.SelectedIndex = Data.Ctrls.Gamepad.GetGamepadNumber(data.ctrls.gamepad.special) - 1;
                    cbUseAnalog.Checked = data.ctrls.gamepad.useAnalog;
                }
            }
            catch
            {
                Console.WriteLine("Could not find the smb folder");
            }


            if (data.muted)
                pbMute.BackgroundImage = Properties.Resources.speaker_muted;
            else
                pbMute.BackgroundImage = Properties.Resources.speaker;

            cbDeleteSaveData.Checked = data.deleteDataOnPlay;

            tbSteamLocation.Text = data.steamLocation;

            if (data.launchPrograms == null)
                data.launchPrograms = new List<LaunchProgram>();

            foreach (LaunchProgram lp in data.launchPrograms)
                lvLaunchPrograms.Items.Add(LaunchProgram.GetLVI(lp));

            for (int i = 0; i < data.launchPrograms.Count; i++)
                lvLaunchPrograms.Items[i].Checked = data.launchPrograms[i].enabled;

            cbUseAnalog.CheckedChanged += new EventHandler(CbUseAnalog_CheckedChanged);
            cbDeleteSaveData.CheckedChanged += new EventHandler(CbDeleteSaveData_CheckedChanged);
        }

        private bool AutoSearchForSteamFolder()
        {
            try
            {
                data.steamLocation = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam", "InstallPath", null);
            }
            catch
            {
                try
                {
                    data.steamLocation = (string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", "InstallPath", null);
                }
                catch
                {
                    data.steamLocation = "";
                }
            }

            if (data.steamLocation.Length != 0)
                return true;
            else
                return false;
        }

        private Data data = new Data();

        private string steamExe { get { return data.steamLocation + @"\Steam.exe"; } }

        private string steamLibraryFoldersFile { get { return data.steamLocation + @"\steamapps\libraryfolders.vdf"; } }

        public static string GetSmbLocation(string steamLocation)
        {
            Console.WriteLine("MainForm::GetSmbLocation");
            if (!Directory.Exists(steamLocation))
            {
                return null;
            }

            if (Directory.Exists(steamLocation + @"\steamapps\common\Super Meat Boy"))
            {
                Console.WriteLine(steamLocation + @"\steamapps\common\Super Meat Boy");
                return steamLocation + @"\steamapps\common\Super Meat Boy";
            }
            else
            {
                string[] lines = File.ReadAllLines(steamLocation + @"\steamapps\libraryfolders.vdf");
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    for (int ii = 0; ii < line.Length; ii++)
                    {
                        if (char.IsNumber(line[ii]))
                        {
                            if (!char.IsNumber(line[ii - 1]) && !char.IsNumber(line[ii + 1]))
                            {
                                // ii+2
                                for (int iii = ii + 2; iii < line.Length; iii++)
                                {
                                    if (char.IsLetter(line[iii]))
                                    {
                                        // here, iii = the first letter of a directory
                                        for (int iiii = iii; iiii < line.Length; iiii++)
                                        {
                                            //Console.WriteLine(line[iiii]);
                                            if (char.IsLetter(line[iiii]) && char.IsUpper(line[iiii]))
                                            {
                                                // iiii-1 = end of directory
                                                StringBuilder sb = new StringBuilder();
                                                for (int iiiii = iii; iii < ((iiii - 1) - iii); iiiii++)
                                                {
                                                    if (line[iiiii] == '"')
                                                        break;

                                                    sb.Append(line[iiiii]);
                                                    //Console.WriteLine(sb.ToString());
                                                }
                                                string dir = sb.ToString();
                                                if (Directory.Exists(dir + @"\steamapps\common\Super Meat Boy"))
                                                {
                                                    //Console.WriteLine(dir + @"\steamapps\common\Super Meat Boy");
                                                    return dir + @"\steamapps\common\Super Meat Boy";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("NULL");
            return null;
        }

        private void MenuButton_MouseEnter(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm::MenuButton_MousMenuButton_MouseEntereLeave");

            PictureBox pb = (PictureBox)sender;
            pbArrow.Location = new Point(pb.Location.X - 50, pb.Location.Y);
            pbArrow.Visible = true;
        }
        private void MenuButton_MouseLeave(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm::MenuButton_MouseLeave");
            pbArrow.Visible = false;
        }

        private void ShowSettingsPanel()
        {
            Console.WriteLine("MainForm::ShowSettingsPanel");
            pSettings.Visible = true;
        }
        private void HideSettingsPanel()
        {
            Console.WriteLine("MainForm::HideSettingsPanel");
            pSettings.Visible = false;

            pSteamLocation.Visible = false;
            pControls.Visible = false;
            pLaunchOptions.Visible = false;
        }

        private void ShowSettingsPanel_SteamExe(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm::ShowSettingsPanel_SteamExe");

            if (pbSteamExe.Visible)
                return;

            HideSettingsPanel();

            if (!data.muted)
            {
                Stream str = Properties.Resources.blip2;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            ShowSettingsPanel();
            pSteamLocation.Visible = true;
        }
        private void ShowSettingsPanel_Controls(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm::ShowSettingsPanel_Controls");

            if (pControls.Visible)
                return;

            HideSettingsPanel();

            if (!data.muted)
            {
                Stream str = Properties.Resources.blip2;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            ShowSettingsPanel();
            pControls.Visible = true;
        }

        private void ShowSettingsPanel_LaunchOptions(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm::ShowSettingsPanel_LaunchOptions");

            if (pLaunchOptions.Visible)
                return;

            HideSettingsPanel();

            if (!data.muted)
            {
                Stream str = Properties.Resources.blip2;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            ShowSettingsPanel();
            pLaunchOptions.Visible = true;
        }


        private void Play(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm::Play");

            if (!data.muted)
            {
                Stream str = Properties.Resources.blip2;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            if (!Directory.Exists(data.steamLocation) || !File.Exists(steamExe))
            {
                MessageBox.Show("Steam not found! Please configure your steam location so that the entered value is the folder containing the steam EXE. Please leave the EXE file OUT of your input.", "Error");
                return;
            }

            string smbLoc = GetSmbLocation(data.steamLocation);
            if (data.deleteDataOnPlay && smbLoc != null)
            {
                try
                {//savegame.dat.bak
                    if (File.Exists(smbLoc + @"\UserData\savegame.dat"))
                        File.Delete(smbLoc + @"\UserData\savegame.dat");
                    if (File.Exists(smbLoc + @"\UserData\savegame.dat.bak"))
                        File.Delete(smbLoc + @"\UserData\savegame.dat.bak");
                }
                catch
                {
                    MessageBox.Show("Unable to delete save data. Please ensure that the file is not protected. Try running SMB Launcher as administrator.", "Error");
                    return;
                }
            }

            foreach (LaunchProgram lp in data.launchPrograms)
            {
                try
                {
                    if (lp.enabled)
                        Process.Start(lp.location);
                }
                catch
                {
                    Console.WriteLine("Unable to launch " + lp.location);
                }
            }

            Console.WriteLine("Running SMB...");

            Uri uri = new Uri("steam://rungameid/40800");
            Process process = new Process();

            process.StartInfo.FileName = steamExe;
            process.StartInfo.Arguments = "-applaunch 40800";
            process.Start();

            Close();
        }

        private void Save(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm::Save");

            data.steamLocation = tbSteamLocation.Text;

            if (data.ctrls == null)
                data.ctrls = new Data.Ctrls();

            data.ctrls.pc.up = cbUp.Text;
            data.ctrls.pc.down = cbDown.Text;
            data.ctrls.pc.left = cbLeft.Text;
            data.ctrls.pc.right = cbRight.Text;
            data.ctrls.pc.jump = cbJump.Text;
            data.ctrls.pc.special = cbSpecial.Text;

            data.ctrls.gamepad.jump = cbGPJump.Text;
            data.ctrls.gamepad.special = cbGPSpecial.Text;
            data.ctrls.gamepad.useAnalog = cbUseAnalog.Checked;

            SaveLoad.WriteControlsToGameFile(data);

            for(int i = 0; i < lvLaunchPrograms.Items.Count; i++)
            {
                data.launchPrograms[i].enabled = lvLaunchPrograms.Items[i].Checked;
            }

            HideSettingsPanel();

            SaveLoad.SaveData(data);

            if (!data.muted)
            {
                Stream str = Properties.Resources.coin1;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("MainForm::MainForm_FormClosed");
            SaveLoad.SaveData(data);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MainForm::PictureBox1_MouseDown");
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void LbExit_Click(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm::LbExit_Click");
            Environment.Exit(0);
        }

        private void LblAutoSearch_Click(object sender, EventArgs e)
        {
            bool result = AutoSearchForSteamFolder();

            if (result)
                tbSteamLocation.Text = data.steamLocation;
            else
                MessageBox.Show("Unable to find Steam. You need to install Steam or manually input it's location.", "Error", MessageBoxButtons.OK);
        }

        private void PbPatreon_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.patreon.com/smbl");
        }

        private void ToggleMute(object sender, EventArgs e)
        {
            data.muted = !data.muted;

            if (data.muted)
                pbMute.BackgroundImage = Properties.Resources.speaker_muted;
            else
                pbMute.BackgroundImage = Properties.Resources.speaker;
        }

        private void CbDeleteSaveData_CheckedChanged(object sender, EventArgs e)
        {
            if (!data.muted)
            {
                Stream str = Properties.Resources.blip2;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            data.deleteDataOnPlay = cbDeleteSaveData.Checked;
        }

        private void CbUseAnalog_CheckedChanged(object sender, EventArgs e)
        {
            if (!data.muted)
            {
                Stream str = Properties.Resources.blip2;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            data.ctrls.gamepad.useAnalog = cbUseAnalog.Checked;
        }

        private void LblLivesplitDownload_Click(object sender, EventArgs e)
        {
            Process.Start("http://livesplit.org/downloads/");
        }

        private void PbSpeedrun_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.speedrun.com/smb/");
        }

        private void PbDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/6fWG3XK");
        }

        private void LblMinimise_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PLaunchOptions_Resize(object sender, EventArgs e)
        {
            chName.Width = lvLaunchPrograms.Width / 3;
            chLocation.Width = (lvLaunchPrograms.Width / 3) * 2;
        }

        private void LblLaunchProgramsAdd_Click(object sender, EventArgs e)
        {
            if (!data.muted)
            {
                Stream str = Properties.Resources.blip2;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            LaunchProgramForm lpForm = new LaunchProgramForm();

            if(lpForm.ShowDialog() == DialogResult.OK)
            {
                if (data.launchPrograms == null)
                    data.launchPrograms = new List<LaunchProgram>();

                data.launchPrograms.Add(lpForm.launchProgram);

                lvLaunchPrograms.Items.Clear();

                foreach(LaunchProgram lp in data.launchPrograms)
                    lvLaunchPrograms.Items.Add(LaunchProgram.GetLVI(lp));
            }
        }

        private void RemoveLaunchProgram()
        {
            if (!data.muted)
            {
                Stream str = Properties.Resources.hurt1;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            int index = lvLaunchPrograms.SelectedIndices[0];

            data.launchPrograms.RemoveAt(index);
            lvLaunchPrograms.Items.RemoveAt(index);
        }

        private void LvLaunchPrograms_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Delete || lvLaunchPrograms.SelectedItems.Count == 0)
                return;

            RemoveLaunchProgram();
        }

        private void LblLaunchProgramsRemove_Click(object sender, EventArgs e)
        {
            if (!data.muted)
            {
                Stream str = Properties.Resources.blip2;
                System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
                snd.Play();
            }

            if (lvLaunchPrograms.SelectedItems.Count != 0)
                RemoveLaunchProgram();
        }
    }
}
