using Cosmos.HAL;
using System;
using System.IO;
using VesaOS.System.Network.HTTP;
using Cosmos.System.Graphics;
using VesaOS.System.Graphics.UI;
using VesaOS.Win32;
using VesaOS.System.Graphics;

namespace VesaOS.System.Terminal
{
    class Shell
    {
        public static Core.Process ShellProcess = new Core.Process();
        public static Network.NTPClient nTP = new Network.NTPClient();
        private static void DiskPart(string[] cmd)
        {
            switch (cmd[1].ToLower())
            {
                case "format":

                default:
                    Console.WriteLine("diskpart: Invalid command.");
                    break;
            }
        }
        public static void Exec(string cmdline)
        {
            string[] cmd = cmdline.Split(" ");
            switch (cmd[0].ToLower())
            {
                case "timesync":
                    if (Kernel.BootMode == 0 || Kernel.BootMode == 2)
                    {
                        Console.WriteLine(nTP.GetNetworkTime().ToString());
                    }
                    else
                    {
                        Console.WriteLine("Networking is disabled!");
                    }
                    break;
                case "md":
                case "mkdir":
                    if (Kernel.BootMode == 0 || Kernel.BootMode == 3)
                    {
                        Directory.CreateDirectory(Kernel.CurrentVol + @":\" + Kernel.CurrentDir + "\\" + cmd[1]);
                    }
                    else
                    {
                        Console.WriteLine("Filesystem not enabled!");
                    }
                    break;
                case "rd":
                case "rmdir":
                    if (Kernel.BootMode == 0 || Kernel.BootMode == 3)
                    {
                        Directory.Delete(Kernel.GetFullPath(cmd[1]), true);
                    }
                    else
                    {
                        Console.WriteLine("Filesystem not enabled!");
                    }
                    break;
                case "del":
                    if (Kernel.BootMode == 0 || Kernel.BootMode == 3)
                    {
                        fileapi.DeleteFile(Kernel.GetFullPath(cmd[1]));
                    }
                    else
                    {
                        Console.WriteLine("Filesystem not enabled!");
                    }
                    break;
                case "dir":
                case "ls":
                    if (Kernel.BootMode == 0 || Kernel.BootMode == 3)
                    {
                        string[] filePaths = Directory.GetFiles(Kernel.CurrentVol + @":\" + Kernel.CurrentDir);
                        var drive = new DriveInfo(Kernel.CurrentVol);
                        Console.WriteLine("Volume in drive 0 is " + $"{drive.VolumeLabel}");
                        Console.WriteLine("Directory of " + Kernel.CurrentVol + @":\" + Kernel.CurrentDir);
                        Console.WriteLine("\n");
                        if (filePaths.Length == 0 && Directory.GetDirectories(Kernel.CurrentVol + @":\" + Kernel.CurrentDir).Length == 0)
                        {
                            Console.WriteLine("File Not Found");
                        }
                        else
                        {
                            for (int i = 0; i < filePaths.Length; ++i)
                            {
                                string path = filePaths[i];
                                Console.WriteLine(Path.GetFileName(path));
                            }
                            foreach (var d in Directory.GetDirectories(Kernel.CurrentVol + @":\" + Kernel.CurrentDir))
                            {
                                var dir = new DirectoryInfo(d);
                                var dirName = dir.Name;

                                Console.WriteLine(dirName + " <DIR>");
                            }
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine("        " + $"{drive.TotalSize}" + " bytes");
                        Console.WriteLine("        " + $"{drive.AvailableFreeSpace}" + " bytes free");
                    }
                    else
                    {
                        Console.WriteLine("Filesystem not enabled!");
                    }
                    break;
                case "writefile":
                    if (Kernel.BootMode == 0 || Kernel.BootMode == 3)
                    {
                        string contents = cmdline.Substring(cmd[0].Length + cmd[1].Length + 2);
                        fileapi.WriteFile(fileapi.GetDirectory() + "\\" + cmd[1], contents);
                    }
                    else
                    {
                        Console.WriteLine("Filesystem not enabled!");
                    }
                    break;
                case "cat":
                    Console.WriteLine(fileapi.ReadFile(fileapi.GetDirectory() + cmd[1]));
                    break;
                case "cd":
                    // todo: make this not stupid
                    #region messy code here
                    if (cmd[1] == "..")
                    {
                        if (Kernel.CurrentDir == "")
                        {
                            break;
                        }
                        char currletter = Kernel.CurrentDir[Kernel.CurrentDir.Length - 1];
                        while (!(currletter == "\\".ToCharArray()[0]))
                        {
                            Kernel.CurrentDir = Kernel.CurrentDir.Remove(Kernel.CurrentDir.Length - 1);
                            if (Kernel.CurrentDir.Length == 0) { break; }
                            currletter = Kernel.CurrentDir[Kernel.CurrentDir.Length - 1];
                        }
                        if (Kernel.CurrentDir.Length == 0) { break; }
                        Kernel.CurrentDir = Kernel.CurrentDir.Remove(Kernel.CurrentDir.Length - 1);
                        break;
                    }
                    string bdir = Kernel.CurrentDir;
                    if (Kernel.CurrentDir == "")
                    {
                        Kernel.CurrentDir += cmd[1];
                    }
                    else
                    {
                        Kernel.CurrentDir += "\\" + cmd[1];
                    }
                    if (!Directory.Exists(Kernel.CurrentVol + ":\\" + Kernel.CurrentDir))
                    {
                        Kernel.CurrentDir = bdir;
                        Console.WriteLine("Directory not found!");
                    }
                    break;
                #endregion
                case "format":
                    if (Users.UserProfileSystem.CurrentPermLevel == Users.UserPermLevel.Guest)
                    {
                        Console.WriteLine("You do not have permission to do this!");
                        break;
                    }
                    if (Kernel.BootMode == 0 || Kernel.BootMode == 3)
                    {
                        try
                        {
                            Console.WriteLine("Formatting...");
                            Cosmos.System.FileSystem.VFS.VFSManager.Format(cmd[1], "fat32", true);
                            Console.Write("Enter volume label: ");
                            string label = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(label))
                            {
                                label = "Local Disk";
                            }
                            Cosmos.System.FileSystem.VFS.VFSManager.SetFileSystemLabel(cmd[1], label);
                            Console.WriteLine("Formatted.");
                        }
                        catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
                    }
                    else
                    {
                        Console.WriteLine("Filesystem not enabled!");
                    }
                    break;
                case "gmode":
                    if (Kernel.BootMode == 0)
                    {
                        Graphics.WindowManager.Init();
                        /*Window window = new Window();
                        Button button = new Button();
                        button.Width = 100;
                        button.Height = 50;
                        button.Color = VGAColor.White;
                        button.HoverColor = VGAColor.Gray;
                        button.ClickColor = VGAColor.Blue;
                        button.X = 5;
                        button.Y = 5;
                        window.UIElements.Add(button);
                        WindowManager.ShowWindow(window);*/
                    }
                    else
                    {
                        Console.WriteLine("Graphics mode not supported!");
                    }
                    break;
                case "":
                    break;
                case "crash":
                    throw new FatalException();
                case "update":
                    //i need to put this in here - eli310
                    break;
                case "date":
                    Console.WriteLine(RTC.DayOfTheMonth.ToString()+"/"+RTC.Month.ToString()+"/"+RTC.Century.ToString()+RTC.Year.ToString());
                    break;
                case "time":
                    Console.WriteLine(RTC.Hour.ToString() + ":" + RTC.Minute.ToString());
                    break;
                case "wget":
                    // why does networking throw a cpu exception
                    Console.WriteLine(HTTPClient.Get(cmd[1]));
                    break;
                case "diskpart":
                    DiskPart(cmd);
                    break;
                case "currentuser":
                case "whoami":
                case "cu":
                    Console.WriteLine(Users.UserProfileSystem.CurrentUser);
                    break;
                case "edit":
                case "miv":
                    /*if (cmd.Length == 1)
                    {
                        Apps.MIV.miv(null);
                        break;
                    }
                    File.WriteAllText(Kernel.GetFullPath(cmd[1]),Apps.MIV.miv(File.ReadAllText(Kernel.GetFullPath(cmd[1]))));*/
                    break;
                case "lua":
                    Kernel.RunProgram(cmd[1],ProgramType.Lua);
                    break;
                default:
                    if (Kernel.BootMode == 0 || Kernel.BootMode == 3)
                    {
                        if (cmd[0].EndsWith(":") && cmd[0].Length == 2)
                        {
                            try
                            {
                                Directory.GetFiles(cmd[0] + "\\");
                                Kernel.CurrentVol = cmd[0][0].ToString();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Could not change drive!");
                            }
                            break;
                        }
                    }
                    Console.WriteLine("Command not found!");
                    break;
            }
        }
    }
}
namespace VesaOS.System
{
    class FatalException : Exception
    {
        public FatalException(string additionalData) : base("FatalException: " + additionalData) { }
        public FatalException() : base("FatalException") { }
    }
}
