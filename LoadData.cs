using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    internal class LoadData
    {
        public static void LoadFile(string fileName, Dictionary<string, Account> accountDictionary)
        {
            try
            {
                PrepareData(File.ReadAllLines(fileName), accountDictionary);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"{fileName} was not found. Please check the directory and make sure you have stored the right file in the right location." +
                    $"\nPlease end the program and try again.");
                FileWriter.WriteNewLine($"{fileName} was not found. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private static void PrepareData(string[] fileByLines, Dictionary<string, Account> accountDictionary)
        {
            for (int i = 1; i < fileByLines.Length; i++)
            {
                string[] cell = fileByLines[i].Split(",");
                try
                {
                    Transaction transaction = new Transaction()
                    {
                        Date = DateTime.Parse(cell[0]),
                        FromName = cell[1],
                        ToName = cell[2],
                        Narrative = cell[3],
                        Amount = double.Parse(cell[4])
                    };

                    // Adds to the full transactions list
                    UpdateAccount(accountDictionary, transaction, transaction.FromName, transaction.Amount);
                    UpdateAccount(accountDictionary, transaction, transaction.ToName, -transaction.Amount);
                }
                catch (FormatException ex)
                {
                    string errorLineData = $"Line data - Date: {cell[0]} | From: {cell[1]} | To: {cell[2]} | Narrative: {cell[3]} | Amount: {cell[4]}";

                    Console.WriteLine($"An error occurred on line {i + 1}. This line's data was not imported to the list. \n{errorLineData}" +
                        $"\nIf you would like for this data to be included in the following reports, please close the program, amend this line, and try again.\n\n");
                    FileWriter.WriteNewLine($"Formatting issue with row {i}: {ex.Message} \n{errorLineData}");
                    continue;
                }
            }
        }

        private static void UpdateAccount(Dictionary<string, Account> accountDictionary, Transaction transaction, string name, double amount)
        {
            if (!accountDictionary.ContainsKey(name))
            {
                accountDictionary.Add(name, new Account()
                {
                    AccountBalance = amount,
                    TransactionList = new List<Transaction>() { transaction }
                });
            }
            else
            {
                accountDictionary[name].AccountBalance += amount;
                accountDictionary[name].TransactionList.Add(transaction);
            }
        }
    }
}