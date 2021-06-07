using System;
using System.Collections.Generic;
using IL2CPU.API.Attribs;
using VesaOS;
using VesaOS.System.Users;

namespace VesaOS_Plugs
{
    [Plug(Target = typeof(global::System.Environment))]
    public class EnviromentImpl
    {
        public static string NewLine { get { return "\n"; } }
        public static bool Is64BitOperatingSystem { get { return false; } }
        private static Dictionary<string, string> envvars = new Dictionary<string, string>();
        public static string CurrentDirectory {
            get { return Kernel.CurrentVol + ":\\" + Kernel.CurrentDir; }
            set { SetKernelVarsCurrentDir(value); } 
        }
        private static void SetKernelVarsCurrentDir(string value)
        {
            string[] split = value.Split("\\");
            string vol = split[0].Remove(1);
            Kernel.CurrentVol = vol;
            //TODO: set current dir
        }
        public static string GetFolderPath(Environment.SpecialFolder folder)
        {
            switch (folder)
            {
                case Environment.SpecialFolder.ApplicationData:
                    break;
                case Environment.SpecialFolder.CommonApplicationData:
                    break;
                case Environment.SpecialFolder.LocalApplicationData:
                    break;
                case Environment.SpecialFolder.Cookies:
                    break;
                case Environment.SpecialFolder.Desktop:
                    return @"0:\Users\" + UserProfileSystem.CurrentUser + @"\Desktop";
                case Environment.SpecialFolder.Favorites:
                    break;
                case Environment.SpecialFolder.History:
                    break;
                case Environment.SpecialFolder.InternetCache:
                    break;
                case Environment.SpecialFolder.Programs:
                    break;
                case Environment.SpecialFolder.MyComputer:
                    break;
                case Environment.SpecialFolder.MyMusic:
                    return @"0:\Users\" + UserProfileSystem.CurrentUser + @"\Music";
                case Environment.SpecialFolder.MyPictures:
                    return @"0:\Users\" + UserProfileSystem.CurrentUser + @"\Pictures";
                case Environment.SpecialFolder.MyVideos:
                    return @"0:\Users\" + UserProfileSystem.CurrentUser + @"\Videos";
                case Environment.SpecialFolder.Recent:
                    break;
                case Environment.SpecialFolder.SendTo:
                    break;
                case Environment.SpecialFolder.StartMenu:
                    break;
                case Environment.SpecialFolder.Startup:
                    break;
                case Environment.SpecialFolder.System:
                    return @"0:\Windows\System32";
                case Environment.SpecialFolder.Templates:
                    break;
                case Environment.SpecialFolder.DesktopDirectory:
                    break;
                case Environment.SpecialFolder.MyDocuments:
                    return @"0:\Users\" + UserProfileSystem.CurrentUser + @"\Documents";
                case Environment.SpecialFolder.AdminTools:
                    break;
                case Environment.SpecialFolder.CDBurning:
                    break;
                case Environment.SpecialFolder.CommonAdminTools:
                    break;
                case Environment.SpecialFolder.CommonDocuments:
                    break;
                case Environment.SpecialFolder.CommonMusic:
                    break;
                case Environment.SpecialFolder.CommonOemLinks:
                    break;
                case Environment.SpecialFolder.CommonPictures:
                    break;
                case Environment.SpecialFolder.CommonStartMenu:
                    break;
                case Environment.SpecialFolder.CommonPrograms:
                    break;
                case Environment.SpecialFolder.CommonStartup:
                    break;
                case Environment.SpecialFolder.CommonDesktopDirectory:
                    break;
                case Environment.SpecialFolder.CommonTemplates:
                    break;
                case Environment.SpecialFolder.CommonVideos:
                    break;
                case Environment.SpecialFolder.Fonts:
                    return @"0:\Windows\Fonts";
                case Environment.SpecialFolder.NetworkShortcuts:
                    break;
                case Environment.SpecialFolder.PrinterShortcuts:
                    break;
                case Environment.SpecialFolder.UserProfile:
                    break;
                case Environment.SpecialFolder.ProgramFiles:
                case Environment.SpecialFolder.ProgramFilesX86:
                    return @"0:\Programs";
                case Environment.SpecialFolder.CommonProgramFilesX86:
                case Environment.SpecialFolder.CommonProgramFiles:
                    return @"0:\Programs\Common";
                case Environment.SpecialFolder.Resources:
                    break;
                case Environment.SpecialFolder.LocalizedResources:
                    break;
                case Environment.SpecialFolder.SystemX86:
                    return @"0:\Windows\SysWOW64";
                case Environment.SpecialFolder.Windows:
                    return @"0:\Windows";
                default:
                    break;
            }
            return @"0:\";
        }
        public static void Exit(int exitCode)
        {

        }
        public static string GetEnvironmentVariable(string variable)
        {
            return envvars[variable];
        }
        public static void SetEnvironmentVariable(string variable, string value)
        {
            envvars[variable] = value;
        }
    }
}
