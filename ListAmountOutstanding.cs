using System;
using System.Collections.Generic;

namespace SupportBank
{
    internal class ListAmountOutstanding
    {
        public static void ListAllAccounts(Dictionary<string, double> accountBook)
        {
            foreach (var line in accountBook)
            {
                if (line.Value < 0)
                {
                    Console.WriteLine($"{line.Key} is owed {-line.Value:C}.");
                }
                else
                {
                    Console.WriteLine($"{line.Key} owes {line.Value:C}!!");
                }
            }
        }

        public static void CalculateAmountsOwed(Dictionary<string, string> dictStringString, Dictionary<string,double> dictStringDouble, string fromName, string toName, double amount, string owedText, string paidOutText)
        {
            TotalOwedMoney(dictStringDouble, fromName, amount);
            TotalOwedMoney(dictStringDouble, toName, -amount);
            ListAccountDetails(dictStringString, fromName, owedText);
            ListAccountDetails(dictStringString, toName, paidOutText);
        }

        private static void ListAccountDetails(Dictionary<string,string> dictStringString, string name, string lineText)
        {
            if (!dictStringString.ContainsKey(name))
            {
                dictStringString.Add(name, $"{name} Statement \n{lineText}");
            }
            else { dictStringString[name] += $"\n{lineText}"; }
        }

        private static void TotalOwedMoney(Dictionary<string, double> dictStringDouble, string name, double amount)
        {
            if (!dictStringDouble.ContainsKey(name))
            {
                dictStringDouble.Add(name, amount);
            }
            else
            {
                dictStringDouble[name] += amount;
            }
        }
    }
}
