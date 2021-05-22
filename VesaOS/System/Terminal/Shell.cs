using ICSharpCode.SharpZipLib.Tar;
using System;
using System.IO;
using VesaOS.Win32;

namespace VesaOS.System.Terminal
{
    class Shell
    {
        public static Network.NTPClient nTP = new Network.NTPClient();
        public static void Exec(string cmdline)
        {
            string[] cmd = cmdline.Split(" ");
            switch (cmd[0].ToLower())
            {
                case "time":
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
                case "untar":
                    
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
