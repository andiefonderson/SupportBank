using System;
using System.Collections.Generic;
using System.Globalization;

namespace SupportBank
{
    internal class Program
    {
        static string fileFor2014 = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\2 - SupportBank\Data Files\Transactions2014.csv";
        static string fileFor2015 = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\2 - SupportBank\Data Files\DodgyTransactions2015.csv";

        static Dictionary<string, Account> accountDetails = new Dictionary<string, Account>();
        static void Main(string[] args)
        {
            StartUp();
        }

        static void StartUp()
        {
            LoadData.LoadFile(fileFor2014, accountDetails);
            MainMenu();
        }

        static void MainMenu()
        {
            Console.WriteLine("Please select the number of the task you want to carry out." +
                "\n1) List All Account Balances" +
                "\n2) List Account Transactions");

            switch (Console.ReadLine())
            {
                case "1":
                    ListAccounts.AllAccounts(accountDetails);
                    break;
                case "2":
                    ListAccounts.AccountTransactions(accountDetails);
                    break;
                default:
                    Console.WriteLine("That was not a valid choice. Please try again.");
                    MainMenu();
                    break;
            }

            ReturnToMainMenu();
        }

        static void ReturnToMainMenu()
        {
            Console.WriteLine("\nWould you like to do something else? " +
                "\nIf so, type 'Y'. Otherwise type any key to close.");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                MainMenu();
            }
        }                
    }
}
