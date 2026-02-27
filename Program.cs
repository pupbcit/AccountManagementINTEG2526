using System;
using System.Collections.Generic;

namespace AccountManagement
{
    internal class Program
    {
        static List<string> accesslogs = new List<string>();
        static List<string> usernames = new List<string>();
        static List<string> passwords = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("ACCOUNT MANAGEMENT SYSYEM");

            PopulateDefaultAccounts();

            bool isLogin = ShowLoginOption();

            while (isLogin)
            {
                Login();

                isLogin = ShowLoginOption();
            }
        }

        static void PopulateDefaultAccounts()
        {
            usernames.Add("admin");
            usernames.Add("user");
            usernames.Add("guest");
            passwords.Add("admin123!");
            passwords.Add("user123!");
            passwords.Add("guest123!");
        }

        static bool ShowLoginOption()
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

        static void Login()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter username: ");
                string usernameInput = Console.ReadLine();
                Console.Write("Enter password: ");
                string passwordInput = Console.ReadLine();
                bool isMatched = false;

                for (int x = 0; x < passwords.Count; x++)
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
                
                if (isMatched)
                {
                    Console.WriteLine("Login Successful!");

                    switch (usernameInput)
                    {
                        case "admin":
                            AdminMenu();
                            break;
                        case "user":
                            break;
                        case "guest":
                            break;
                        default:
                            break;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect Credentials.");
                }
            }


        }

        static void AddAccessLogs(string username, string password, bool status)
        {
            accesslogs.Add($"username: {username}, password: {password}, Is Successful?: {status}");
        }

        static void AdminMenu()
        {

            Console.WriteLine("\n----------\n ADMIN MENU:");
            string[] superuseroptions = new string[] { "View Users", "Add User", "Update User", "Delete User", "Display Logs" };
            ShowOptions(superuseroptions);

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ViewUsers();
                    break;
                case "2":
                    AddUser();
                    break;
                case "3":
                    UpdateUser();
                    break;
                case "4":
                    //DeleteUser();
                    break;
                case "5":
                    DisplayLogs();
                    break;
                default:
                    Console.WriteLine("Invalid.");
                    break;
            }
        }

        private static void UpdateUser()
        {
            Console.WriteLine("UPDATE USER: ");
            Console.Write("Enter the username of the user you want to update: ");
            string findUser = Console.ReadLine();

            for (int i = 0; i < usernames.Count; i++)
            {
                if (usernames[i] == findUser)
                {
                    Console.WriteLine("enter new username: ");
                    string newusername = Console.ReadLine();
                    Console.WriteLine("enter new password: ");
                    string newpassword = Console.ReadLine();

                    if (ValidateUserName(newusername))
                    {
                        usernames[i] = newusername;
                        passwords[i] = newpassword;
                    }
                    else {
                        Console.WriteLine("user name already exists.");
                    }

                }
            }

            AdminMenu();
        }

        private static bool ValidateUserName(string username)
        {
            bool valid = true;
            foreach(var un in usernames)
            {
                if (un == username)
                {
                    valid = false;
                }
            }
            return valid;
        }

        private static void AddUser()
        {
            Console.WriteLine("ADDING USER: Enter the necessary information");
            Console.Write("username: ");
            string username = Console.ReadLine();
            Console.Write("password: ");
            string password = Console.ReadLine();
            usernames.Add(username);
            passwords.Add(password);
            Console.WriteLine($"Successfully added user {username}");
            AdminMenu();
        }

        private static void ViewUsers()
        {
            Console.WriteLine("\nHere are the list of users.. ");
            
            for (int i = 0; i < usernames.Count; i++)
            {
                Console.WriteLine($"[{i+1}] username: {usernames[i]}, password: {passwords[i]}");
            }

            AdminMenu();
        }


        static void ShowOptions(string[] options)
        {
            for (int x = 0;x < options.Length;x++)
            {
                Console.WriteLine($"[{x+1}] {options[x]}");
            }
            Console.Write("Enter the number of your option: ");
        }
    }
}