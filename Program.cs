using System;

namespace AccountManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ACCOUNT MANAGEMENT SYSYEM");

            string username = "superuser";
            string password = "superuser123!";

            Console.Write("Enter username: ");
            string usernameInput = Console.ReadLine();
            Console.Write("Enter password: ");
            string passwordInput = Console.ReadLine();

            if (usernameInput == username && passwordInput == password)
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Incorrect credentials.");
            }
        }
    }
}
