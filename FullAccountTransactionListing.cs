using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class FullAccountTransactionListing
    {
        public static void ListAll(Dictionary<string,string> dictStringString)
        {
            ListFullAccountTransactions(dictStringString);
        }

        private static void ListFullAccountTransactions(Dictionary<string,string> dictStringString)
        {
            string nameList = "";

            if (dictStringString.Count != 0)
            {
                Console.WriteLine("\nPlease select whose account you want to access from the following:");
                foreach (var name in dictStringString)
                {
                    nameList += $"{name.Key} | ";
                }

                Console.WriteLine(nameList);

                try
                {
                    ListAccountName(dictStringString, Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("\nYou didn't input a valid name. Please try again.");
                    ListAccountName(dictStringString, Console.ReadLine());
                }
            }
            else { Console.WriteLine("\nNo names were found within this document to enable an account search."); }

            Console.WriteLine("\nWould you like to search for another account? If so, type 'Y'.");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                ListFullAccountTransactions(dictStringString);
            }
        }

        private static void ListAccountName(Dictionary<string, string> dictStringString, string name)
        {
            Console.WriteLine(dictStringString[name]);
        }
    }
}
