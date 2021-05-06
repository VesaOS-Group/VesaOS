namespace VesaOS.System.Users
{
    class UserProfileSystem
    {
        public static string CurrentUser { get; private set; }
        public static UserPermLevel CurrentPermLevel { get; private set; }
        public static bool UserExists(string un)
        {
            return Kernel.config.dictionary.ContainsKey("[Users]"+un);
        }
        public static bool CheckPassword(string un,string psk)
        {
            return Kernel.config.GetValue("Users",un) == psk;
        }
        public static bool Login(string un, string psk)
        {
            if (UserExists(un))
            {
                if (CheckPassword(un, psk))
                {
                    CurrentUser = un;
                    CurrentPermLevel = (UserPermLevel)Kernel.config.GetInteger("UserPerms",un,1);
                    return true;
                }
            }
            return false;
        }
        public static void CreateUser(string un, string psk)
        {
            Kernel.config.SetValue("Users", un, psk);
            Kernel.config.SetValue("UserPerms", un, "2");
        }
    }
    enum UserPermLevel
    {
        Guest = 0,
        User = 1,
        Administrator = 2,
        System = 3,
    }
}
