using System;
using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    internal class Program
    {
        static string path = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\2 - SupportBank\Data Files\Transactions2014.csv";
        static string textFile = File.ReadAllText(path);

        static void Main(string[] args)
        {
            ReadFile();
        }

        static void ReadFile()
        {
            string[] fileByLines = File.ReadAllLines(path);
            Dictionary<string, double> account = new Dictionary<string, double>();

            for (int i = 1; i < fileByLines.Length; i++)
            {
                string[] cell = fileByLines[i].Split(",");
                string fromName = cell[1];
                string toName = cell[2];
                string narrative = cell[3];
                double amount = double.Parse(cell[4]);                

                if (!account.ContainsKey(fromName))
                {
                    account.Add(fromName,amount);
                }
                else
                {
                    account[fromName] += amount;
                }

                if (!account.ContainsKey(toName))
                {
                    account.Add(toName, -amount);
                }
                else
                {
                    account[toName] -= amount;
                }

                /*
                Account fullAccountLineDetails = new Account()
                {
                    date = cell[0],
                    fromName = cell[1],
                    toName = cell[2],
                    narrative = cell[3],
                    amount = double.Parse(cell[4])
                };

                if (!account.ContainsKey(accountLine.fromName))
                {
                    account.Add(cell[1], accountLine);
                }
                */
            }

            ListAllAccounts(account);
        }

        static void ListAllAccounts(Dictionary<string, double> accountBook)
        {
            foreach (var line in accountBook)
            {
                if(line.Value < 0)
                {
                    Console.WriteLine($"{line.Key} is owed {-line.Value:£0.00}");
                }
                else
                {
                    Console.WriteLine($"{line.Key} owes {line.Value:£0.00}");
                }                
            }
        }

        static string ListAccountOutstanding(string name, double amount)
        {
            return $"{name} owes {amount:£0.00}";
        }
    }


}
