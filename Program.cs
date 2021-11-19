using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class Program
    {
        static string fileFor2014 = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\2 - SupportBank\Data Files\Transactions2014.csv";
        static string fileFor2015 = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\2 - SupportBank\Data Files\DodgyTransactions2015.csv";
        

        static Dictionary<string, double> accountMoneyOwedOnly = new Dictionary<string, double>();
        static Dictionary<string, string> accountFullDetails = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            ProgramStartUp();
        }

        static void ProgramStartUp()
        {
            LoadData.ExtractData(fileFor2014, accountFullDetails, accountMoneyOwedOnly);
            if(accountFullDetails.Count > 0)
            {
                MainMenu();
            }            
        }
        static void MainMenu()
        {
            Console.WriteLine("Please select the number of the task you want to carry out." +
                "\n1) List All Account Balances" +
                "\n2) List Account Transactions");

            switch (Console.ReadLine())
            {
                case "1":
                    ListAmountOutstanding.ListAllAccounts(accountMoneyOwedOnly);
                    break;
                case "2":
                    FullAccountTransactionListing.ListAll(accountFullDetails);
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
