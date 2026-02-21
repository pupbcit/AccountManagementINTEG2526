using System;
using System.Collections.Generic;

namespace AccountManagement
{
    internal class Program
    {
        static List<string> accesslogs = new List<string>();
        static string[] usernames = new string[3];
        static string[] passwords = new string[3];

        static void Main(string[] args)
        {
            Console.WriteLine("ACCOUNT MANAGEMENT SYSYEM");

            PopulateDefaultAccounts();

            bool isLogin = UserLogin();

            while (isLogin)
            {
                for (int i = 0; i < 3; i++)
                {
                    bool isMatched = InputCredentials();

                    if (isMatched)
                    {
                        Console.WriteLine("Login Successful!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Credentials.");
                        continue;
                    }
                }

                isLogin = UserLogin();
            }
        }

        static void PopulateDefaultAccounts()
        {
            usernames[0] = "admin";
            usernames[1] = "superuser";
            usernames[2] = "guest";

            passwords[0] = "admin123!";
            passwords[1] = "superuser123!";
            passwords[2] = "guest123!";
        }

        static bool UserLogin()
        {
            Console.Write("Do you want to login? y/n: ");
            bool isLogin = false;
            string loginInput = Console.ReadLine();

            switch (loginInput)
            {
                case "y":
                    isLogin = true;
                    break;
                case "n":
                    isLogin = false;
                    break;
                default:
                    Console.WriteLine("Incorrect input. The system will exit.");
                    Environment.Exit(0);
                    break;
            }

            return isLogin;
        }

        static void DisplayLogs()
        {
            foreach (var log in accesslogs)
            {
                Console.WriteLine(log);
            }
        }

        static bool InputCredentials()
        {
            Console.Write("Enter username: ");
            string usernameInput = Console.ReadLine();
            Console.Write("Enter password: ");
            string passwordInput = Console.ReadLine();

            bool isMatched = false;

            for (int x = 0; x < passwords.Length; x++)
            {
                if (usernameInput == usernames[x] && passwordInput == passwords[x])
                {
                    isMatched = true;
                    break;
                }
                else
                {
                    isMatched = false;
                }
            }

            AddAccessLogs(usernameInput, passwordInput, isMatched);

            return isMatched;
        }

        static void AddAccessLogs(string username, string password, bool status)
        {
            accesslogs.Add($"username: {username}, password: {password}, Is Successful?: {status}");
        }
    }
}