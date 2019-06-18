using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMBLauncherv2
{
    public static class SaveLoad
    {

        public static void SaveData(Data data)
        {
            Console.WriteLine("SaveLoad::SaveData");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.UserAppDataPath + ".dat");
            bf.Serialize(file, data);
            file.Close();
        }

        public static void WriteControlsToGameFile(Data data)
        {
            Console.WriteLine("SaveLoad::WriteControlsToGameFile");
            string smbloc = MainForm.GetSmbLocation(data.steamLocation);

            if (smbloc == null)
            {
                MessageBox.Show("Super Meat Boy was not found. Please ensure that the steam location was set correctly and Super Meat Boy is installed through Steam.", "Error");
                return;
            }

            using (FileStream fs = File.Create(smbloc + @"\buttonmap.cfg"))
            {
                using (StringReader sr = new StringReader(data.ctrls.ToString()))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        byte[] bytes = new UTF8Encoding(false).GetBytes(line.ToLower());
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
            }

        }

        public static Data LoadData()
        {
            Console.WriteLine("SaveLoad::LoadData");
            if (File.Exists(Application.UserAppDataPath + ".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.UserAppDataPath + ".dat", FileMode.Open);
                Data data = (Data)bf.Deserialize(file);
                file.Close();

                return data;
            }

            return null;
        }

        public static Data.Ctrls LoadControlsFromGameFile(Data data)
        {
            Console.WriteLine("SaveLoad::LoadControlsFromGameFile");
            Data.Ctrls ctrls = new Data.Ctrls();

            using (FileStream fs = File.OpenRead(MainForm.GetSmbLocation(data.steamLocation) + @"\buttonmap.cfg"))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    //Console.WriteLine(temp.GetString(b));
                    List<string> controlList = new List<string>();

                    StringBuilder sb = new StringBuilder();
                    bool isPartOfString = false;
                    foreach(char c in temp.GetString(b))
                    {
                        if (isPartOfString && c != '"')
                            sb.Append(c);
                        else if (isPartOfString && c == '"')
                        {
                            isPartOfString = false;
                            controlList.Add(sb.ToString());
                            sb = new StringBuilder();
                        }
                        else if (!isPartOfString && c == '"')
                            isPartOfString = true;
                    }

                    if(controlList.Count != 9)
                    {
                        Console.WriteLine("Controls not found");
                        return null;
                    }

                    ctrls.pc.up = controlList[0];
                    ctrls.pc.down = controlList[1];
                    ctrls.pc.left = controlList[2];
                    ctrls.pc.right = controlList[3];
                    ctrls.pc.jump = controlList[4];
                    ctrls.pc.special = controlList[5];

                    int gamepadJump = 1;
                    int gamepadSpecial = 2;

                    if(!int.TryParse(controlList[6], out gamepadJump))
                    {
                        Console.WriteLine("invalid gampad jump");
                        return null;
                    }

                    if (!int.TryParse(controlList[7], out gamepadSpecial))
                    {
                        Console.WriteLine("invalid gampad special;");
                        return null;
                    }

                    ctrls.gamepad.jump = Data.Ctrls.Gamepad.GetGamepadText(gamepadJump);
                    ctrls.gamepad.special = Data.Ctrls.Gamepad.GetGamepadText(gamepadSpecial);

                    if(!bool.TryParse(controlList[8], out ctrls.gamepad.useAnalog))
                    {
                        Console.WriteLine("invalid useanalog");
                        return null;
                    }
                }
            }

            return ctrls;
        }

    }
}
