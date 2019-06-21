using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBLauncherProject
{
    [System.Serializable]
    public class Data
    {
        public string steamLocation = "";
        public bool muted = false;

        public bool deleteDataOnPlay = false;

        public List<LaunchProgram> launchPrograms;

        public Ctrls ctrls = new Ctrls();

        [System.Serializable]
        public class Ctrls
        {
            public PC pc = new PC();
            public Gamepad gamepad = new Gamepad();

            [System.Serializable]
            public class PC
            {
                public string up;
                public string down;
                public string left;
                public string right;
                public string jump;
                public string special;
            }

            [System.Serializable]
            public class Gamepad
            {
                public static int GetGamepadNumber(string text)
                {
                    Console.WriteLine("Data::GetGamepadNumber");
                    switch (text)
                    {
                        case "A / CROSS":
                            return 1;
                        case "B / CIRCLE":
                            return 2;
                        case "X / SQUARE":
                            return 3;
                        case "Y / TRIANGLE":
                            return 4;
                        case "LB":
                            return 5;
                        case "RB":
                            return 6;
                    }
                    return 1;
                }
                public static string GetGamepadText(int val)
                {
                    Console.WriteLine("Data::GetGamepadText");
                    switch (val)
                    {
                        case 1:
                            return "A / CROSS";
                        case 2:
                            return "B / CIRCLE";
                        case 3:
                            return "X / SQUARE";
                        case 4:
                            return "Y / TRIANGLE";
                        case 5:
                            return "LB";
                        case 6:
                            return "RB";
                    }
                    return "";
                }

                public string jump;
                public string special;
                public bool useAnalog;
            }

            public override string ToString()
            {
                Console.WriteLine("Data::ToString");
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("keyboard");
                sb.AppendLine("{");
                sb.AppendLine("up=\"" + pc.up + "\";");
                sb.AppendLine("down = \"" + pc.down + "\";");
                sb.AppendLine("left = \"" + pc.left + "\";");
                sb.AppendLine("right = \"" + pc.right + "\";");
                sb.AppendLine("jump = \"" + pc.jump + "\";");
                sb.AppendLine("special = \"" + pc.special + "\";");
                sb.AppendLine("}");
                sb.AppendLine("");
                sb.AppendLine("gamepad");
                sb.AppendLine("{");
                sb.AppendLine("jump=\"" + Gamepad.GetGamepadNumber(gamepad.jump) + "\";");
                sb.AppendLine("special=\"" + Gamepad.GetGamepadNumber(gamepad.special) + "\";");
                sb.AppendLine("useanalog=\"" + gamepad.useAnalog + "\";");
                sb.AppendLine("}");

                return sb.ToString();
            }
        }
    }
}
