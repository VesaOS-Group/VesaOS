using System;
using System.IO;

namespace VesaOS.Win32
{
    class fileapi
    {
        public static FileStream CreateFileA(string lpFileName)
        {
            return File.Open(lpFileName, FileMode.OpenOrCreate);
        }
        public static FileStream CreateFileW(string lpFileName)
        {
            return File.Open(lpFileName, FileMode.Create);
        }

        public static bool WriteFile(string lpFileName, string lpFileContents)
        {
            try
            {
                File.WriteAllText(lpFileName, lpFileContents);
                return true;
            } catch
            {
                return false;
            }
        }

        public static string ReadFile(string lpFileName)
        {
            try
            {
                return File.ReadAllText(lpFileName);
            }
            catch
            {
                throw new Exception("File not found.");
            }
        }

        public static bool DeleteFile(string lpFileName)
        {
            try
            {
                File.Delete(lpFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteFileA(string lpFileName)
        {
            return DeleteFile(lpFileName);
        }

        public static bool DeleteFileW(string lpFileName)
        {
            return DeleteFile(lpFileName);
        }
        
        public static string GetDirectory()
        {
            return Kernel.CurrentVol + @":\" + Kernel.CurrentDir;
        }
    }
}
