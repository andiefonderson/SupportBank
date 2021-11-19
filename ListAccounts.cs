using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    internal class ListAccounts
    {
        public static void AllAccounts(Dictionary<string,Account> accountDetails)
        {
            foreach (var account in accountDetails)
            {
                if (account.Value.AccountBalance > 0)
                {
                    Console.WriteLine($"{account.Key} owes {account.Value.AccountBalance:C}!!");
                }
                else
                {
                    Console.WriteLine($"{account.Key} is owed {-account.Value.AccountBalance:C}.");
                }

            }
        }

        public static void AccountTransactions(Dictionary<string, Account> accountDetails)
        {
            string nameList = "";
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;

            if (accountDetails.Count != 0)
            {
                Console.WriteLine("\nPlease select whose account you want to access from the following:");
                foreach (var name in accountDetails)
                {
                    nameList += $"{name.Key} | ";
                }

                Console.WriteLine(nameList);

                GetTransactionList(textInfo.ToTitleCase(Console.ReadLine()), accountDetails);
            }
            else { Console.WriteLine("\nNo names were found within this document to enable an account search."); }

            Console.WriteLine("\nWould you like to search for another account? If so, type 'Y'.");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                AccountTransactions(accountDetails);
            }
        }

        private static void GetTransactionList(string name, Dictionary<string, Account> accountDetails)
        {
            TextInfo textInfo = new CultureInfo("en-GB", false).TextInfo;

            try
            {
                List<Transaction> transactions = accountDetails[name].TransactionList;

                Console.WriteLine($"\nAccount Transactions Statement for {name}");
                foreach (var transactionLine in transactions)
                {
                    if (transactionLine.FromName == name)
                    {
                        Console.WriteLine($"{transactionLine.Date:d}: [Incoming] Paid {transactionLine.Amount:C} by {transactionLine.ToName} for {transactionLine.Narrative}");
                    }
                    else
                    {
                        Console.WriteLine($"{transactionLine.Date:d}: [Outgoing] Paid {transactionLine.Amount:C} to {transactionLine.FromName} for {transactionLine.Narrative}");
                    }
                }
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("\nYou didn't input a valid name. Please try again.");
                GetTransactionList(textInfo.ToTitleCase(Console.ReadLine()), accountDetails);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("\nPlease only input letters and type the name exactly as stated.");
                GetTransactionList(textInfo.ToTitleCase(Console.ReadLine()), accountDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nYour input was invalid; please try typing the name exactly as stated.");
                GetTransactionList(textInfo.ToTitleCase(Console.ReadLine()), accountDetails);
            }
        }
    }
}
