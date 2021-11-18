using System;
using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    internal class Program
    {
        static string path = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\2 - SupportBank\Data Files\Transactions2014.csv";
        static string[] fileByLines = File.ReadAllLines(path);

        static Dictionary<string, double> accountMoneyOwedOnly = new Dictionary<string, double>();
        static Dictionary<string, string> accountFullDetails = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            ProgramStartUp();
        }

        static void ProgramStartUp()
        {       
            for (int i = 1; i < fileByLines.Length; i++)
            {
                string[] cell = fileByLines[i].Split(",");
                string date = cell[0];
                string fromName = cell[1];
                string toName = cell[2];
                string narrative = cell[3];
                double amount = double.Parse(cell[4]);

                string accountLineText = $"{date}: [Incoming] Paid {amount:C} by {toName} for {narrative}";
                string accountPaidOtherLineText = $"{date}: [Outgoing] Paid {amount:C} to {fromName} for {narrative}";

                TotalOwedMoney(fromName, amount);
                TotalOwedMoney(toName, -amount);
                ListAccountDetails(fromName, accountLineText);
                ListAccountDetails(toName, accountPaidOtherLineText);
            }

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
                    ListAllAccounts(accountMoneyOwedOnly);
                    break;
                case "2":
                    ListFullAccountTransactions();
                    break;
                default:
                    Console.WriteLine("That was not a valid choice. Please try again.");
                    MainMenu();
                    break;
            }

            ReturnToMainMenu();
        }

        static void ListFullAccountTransactions()
        {
            string nameList = "";

            if (accountFullDetails.Count != 0)
            {
                Console.WriteLine("\nPlease select whose account you want to access from the following:");
                foreach (var name in accountFullDetails)
                {
                    nameList += $"{name.Key} | ";
                }

                Console.WriteLine(nameList);

                try
                {
                    ListAccountName(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("\nYou didn't input a valid name. Please try again.");
                    ListAccountName(Console.ReadLine());
                }
            }
            else { Console.WriteLine("\nNo names were found within this document to enable an account search."); }

            Console.WriteLine("\nWould you like to search for another account? If so, type 'Y'.");
            if(Console.ReadLine().ToUpper() == "Y")
            {
                ListFullAccountTransactions();
            }

        }

        private static void ListAccountDetails(string name, string lineText)
        {
            if (!accountFullDetails.ContainsKey(name))
            {
                accountFullDetails.Add(name, $"{name} Statement \n{lineText}");
            }
            else { accountFullDetails[name] += $"\n{lineText}"; }
        }

        private static void TotalOwedMoney(string name, double amount)
        {
            if (!accountMoneyOwedOnly.ContainsKey(name))
            {
                accountMoneyOwedOnly.Add(name, amount);
            }
            else
            {
                accountMoneyOwedOnly[name] += amount;
            }
        }

        static void ListAllAccounts(Dictionary<string, double> accountBook)
        {
            foreach (var line in accountBook)
            {
                if(line.Value < 0)
                {
                    Console.WriteLine($"{line.Key} is owed {-line.Value:C}.");
                }
                else
                {
                    Console.WriteLine($"{line.Key} owes {line.Value:C}!!");
                }                
            }
        }

        static void ListAccountName(string name)
        {
            Console.WriteLine(accountFullDetails[name]);
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
