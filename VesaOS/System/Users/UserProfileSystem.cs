using System.IO;
namespace VesaOS.System.Users
{
    public class UserProfileSystem
    {
        public static string CurrentUser { get; private set; }
        public static UserPermLevel CurrentPermLevel { get; private set; }
        public static bool UserExists(string un)
        {
            return File.Exists(@"0:\Users\"+un+@"\VUSERC");
        }
        public static bool CheckPassword(string un,string psk)
        {
            return File.ReadAllLines(@"0:\Users\" + un + @"\VUSERC")[0] == psk;
        }
        public static bool Login(string un, string psk)
        {
            if (UserExists(un))
            {
                if (CheckPassword(un, psk))
                {
                    CurrentUser = un;
                    CurrentPermLevel = (UserPermLevel)int.Parse(File.ReadAllLines(@"0:\Users\" + un + @"\VUSERC")[1]);
                    return true;
                }
            }
            return false;
        }
        public static void CreateUser(string un, string psk)
        {
            if (!Directory.Exists(@"0:\Users"))
            {
                Directory.CreateDirectory(@"0:\Users");
            }
            Directory.CreateDirectory(@"0:\Users\"+un);
            File.WriteAllLines(@"0:\Users\" + un + @"\VUSERC",new string[]{ psk, "2" });
        }
    }
    public enum UserPermLevel
    {
        Guest = 0,
        User = 1,
        Administrator = 2,
        System = 3,
    }
}
