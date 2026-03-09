using System;
using System.Collections.Generic;
using AccountManagementAppService;
using AccountManagementModels;

namespace AccountManagement
{
    internal class Program
    {
        static List<string> accesslogs = new List<string>();
        static List<string> usernames = new List<string>();
        static List<string> passwords = new List<string>();

        static AccountAppService accountAppService = new AccountAppService(); 

        static void Main(string[] args)
        {
            Console.WriteLine("ACCOUNT MANAGEMENT SYSYEM");

            bool isLogin = ShowLoginOption();

            while (isLogin)
            {
                Login();

                isLogin = ShowLoginOption();
            }
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

                bool isMatched = accountAppService.Authenticate(usernameInput, passwordInput);

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

        static void UpdateUser()
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

        static bool ValidateUserName(string username)
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

        static void AddUser()
        {
            Console.WriteLine("ADDING USER: Enter the necessary information");
            Console.Write("username: ");
            string username = Console.ReadLine();
            Console.Write("password: ");
            string password = Console.ReadLine();

            Account newAccount = new Account { AccountId = Guid.NewGuid(), Username = username, Password = password};

            accountAppService.Register(newAccount);

            Console.WriteLine($"Successfully added user {newAccount.AccountId}");
            AdminMenu();
        }

        static void ViewUsers()
        {
            Console.WriteLine("\nHere are the list of users.. ");

            var accounts = accountAppService.GetAccounts();

            foreach (var account in accounts)
            {
                Console.WriteLine($"ID: {account.AccountId} username: {account.Username}, password: {account.Password}");

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