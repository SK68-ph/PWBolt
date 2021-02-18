using System;
using PWBolt.Properties;
using PWBolt.Network;
using System.Diagnostics;
using System.Threading;
using System.Linq;

namespace PWBolt
{
    class Program
    {
        static void Main(string[] args)
        {
            
            PWMenu menu = new PWMenu();
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Resources.menuStr + "\n");
                Console.WriteLine("Press 1 to login\n");
                Console.WriteLine("Press 2 to register\n");
                Console.WriteLine("Press Q to exit\n");
                Console.Write("Type Option :");
                var option = Console.ReadKey();

                if (option.KeyChar == '1')
                {
                    menu.LoginMenu();
                }
                else if (option.KeyChar == '2')
                {
                    menu.RegisterMenu();
                }
                else if (option.KeyChar == 'q' || option.KeyChar == 'Q')
                    break;
            }
        }
    }
    class PWMenu
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void RegisterMenu()
        {
            string tmpUser = null;
            string tmpBoltPass = null;
            while (string.IsNullOrWhiteSpace(tmpUser) | string.IsNullOrWhiteSpace(tmpBoltPass))
            {
                Console.Clear();
                Console.WriteLine(Resources.menuStr + "\n");
                Console.Write("Register PWBolt Account");
                Console.Write("\nUser: ");
                tmpUser = Console.ReadLine();
                Console.Write("\nBoltPass: ");
                tmpBoltPass = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(tmpUser) | string.IsNullOrWhiteSpace(tmpBoltPass))
                {
                    Console.WriteLine("Empty Field. Please provide proper information");
                    Console.ReadKey();
                }
                else
                    break;
            }
            WebServer.RegisterPWBolt(tmpUser, tmpBoltPass);
            Console.ReadKey();
        }

        public void LoginMenu() 
        {
            string tmpUser = null;
            string tmpBoltPass = null;
            while (string.IsNullOrWhiteSpace(tmpUser) | string.IsNullOrWhiteSpace(tmpBoltPass))
            {
                Console.Clear();
                Console.WriteLine(Resources.menuStr + "\n");
                Console.WriteLine("LogIn PWBolt Account");
                Console.Write("\nUser: ");
                tmpUser = Console.ReadLine();
                Console.Write("\nBoltPass: ");
                tmpBoltPass = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(tmpUser) | string.IsNullOrWhiteSpace(tmpBoltPass))
                {
                    Console.WriteLine("Empty Field. Please provide proper information");
                    Console.ReadKey();
                }
                else
                    break;
            }
            WebServer.LoginPWBolt(tmpUser,tmpBoltPass);
            if (WebServer.IsLoggedIn)
            {
                MainMenu();
            }
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Resources.menuStr + "\n");
                Console.WriteLine("Press 1 to Display accounts\n");
                Console.WriteLine("Press 2 to Add account\n");
                Console.WriteLine("Press 3 to Edit account\n");
                Console.WriteLine("Press 4 to Delete account\n");
                Console.WriteLine("Press Q to Logout\n");
                Console.Write("Type Option :");
                var option = Console.ReadKey();

                if (option.KeyChar == '1')
                {
                    DisplayAccountsMenu();
                }
                else if (option.KeyChar == '2')
                {
                    AddAccountMenu();
                }
                else if (option.KeyChar == '3')
                {
                    EditAccountMenu();
                }
                else if (option.KeyChar == '4')
                {
                    DeleteAccountMenu();
                }
                else if (option.KeyChar == 'q' || option.KeyChar == 'Q')
                {

                    break;
                }
                    
            }
        }

        public void AddAccountMenu()
        {
            string tmpWebSite = null;
            string tmpUsername = null;
            string tmpPass = null;
            while (string.IsNullOrWhiteSpace(tmpWebSite) | string.IsNullOrWhiteSpace(tmpUsername) | string.IsNullOrWhiteSpace(tmpPass))
            {
                Console.Clear();
                Console.WriteLine(Resources.menuStr + "\n");
                Console.Write("Add new account\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nWebsite : ");
                tmpWebSite = Console.ReadLine();
                Console.Write("\nUser: ");
                tmpUsername = Console.ReadLine();
                Console.WriteLine("\n type -r to generate random password ");
                Console.Write("Password: ");
                tmpPass = Console.ReadLine();
                if (tmpPass.Equals("-r"))
                    tmpPass = RandomString(24);
                Console.ResetColor();
                if (string.IsNullOrWhiteSpace(tmpWebSite) | string.IsNullOrWhiteSpace(tmpUsername) | string.IsNullOrWhiteSpace(tmpPass))
                {
                    Console.WriteLine("Empty Field. Please provide proper information");
                    Console.ReadKey();
                }
                else
                    break;
            }
            WebServer.bolt_AddAccount(tmpWebSite, tmpUsername, tmpPass);
            Console.Write("\nPress any key to continue ");
            Console.ReadKey();
        }

        public void DisplayAccountsMenu()
        {
            Console.Clear();
            Console.WriteLine(Resources.menuStr + "\n");
            Console.Write("Accounts\n");
            WebServer.bolt_DisplayAccount();
            Console.Write("\nPress any key to continue ");
            Console.ReadKey();
        }

        public void DeleteAccountMenu()
        {
            string id = null;
            while (string.IsNullOrWhiteSpace(id))
            {
                Console.Clear();
                Console.WriteLine(Resources.menuStr + "\n");
                Console.WriteLine("Delete Account.");
                WebServer.bolt_DisplayAccount();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nEnter Selected ID to delete account :");
                id = Console.ReadLine();
                Console.ResetColor();
            }
            WebServer.bolt_DeleteAccount(id);
            Console.Clear();
            Console.WriteLine(Resources.menuStr + "\n");
            WebServer.bolt_DisplayAccount();
            Console.Write("\nPress any key to continue ");
            Console.ReadKey();
        }

        public void EditAccountMenu()
        {
            string tmpWebSite = null;
            string tmpUsername = null;
            string tmpPass = null;
            string id = null;
            while (string.IsNullOrWhiteSpace(tmpWebSite) | string.IsNullOrWhiteSpace(tmpUsername) | string.IsNullOrWhiteSpace(tmpPass) | string.IsNullOrWhiteSpace(id))
            {
                Console.Clear();
                Console.WriteLine(Resources.menuStr + "\n");
                Console.Write("Update account\n");
                WebServer.bolt_DisplayAccount();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nEnter Selected ID to delete account :");
                id = Console.ReadLine();
                Console.Write("\nWebsite : ");
                tmpWebSite = Console.ReadLine();
                Console.Write("\nUser: ");
                tmpUsername = Console.ReadLine();
                Console.Write("\nPassword: ");
                tmpPass = Console.ReadLine();
                Console.ResetColor();
                if (string.IsNullOrWhiteSpace(tmpWebSite) | string.IsNullOrWhiteSpace(tmpUsername) | string.IsNullOrWhiteSpace(tmpPass) | string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("Empty Field. Please provide proper information");
                    Console.ReadKey();
                }
                else
                    break;
            }
            WebServer.bolt_EditAccount(id,tmpWebSite, tmpUsername, tmpPass);
            Console.Write("\nPress any key to continue ");
            Console.ReadKey();
        }
    }
}

// Procedural Method 
//PWBOLT_register.php
// INSERT INTO tbl_users(username,passbolt,bolt4pin) VALUES(P_username,P_passbolt,P_boltpin)
