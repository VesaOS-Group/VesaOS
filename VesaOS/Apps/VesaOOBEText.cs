using System;
using VesaOS.System.Users;

namespace VesaOS.Apps
{
    class VesaOOBEText
    {
        public static void UserAccountSetup()
        {
            Console.WriteLine("You need to setup a user account.");
            Console.Write("Username: ");
            string un = Console.ReadLine();
            Console.Write("Password: ");
            string psk = Console.ReadLine();
            UserProfileSystem.CreateUser(un,psk);
            Kernel.Reboot();
        }
    }
}
