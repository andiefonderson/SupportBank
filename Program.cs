using System;
using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    internal class Program
    {
        static string fileFor2014 = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\2 - SupportBank\Data Files\Transactions2014.csv";
        static string fileFor2015 = @"C:\Users\Andrea.Fonderson\OneDrive - IRIS Software Group\Corndel Bootcamp\C Sharp Bootcamp\2 - SupportBank\Data Files\DodgyTransactions2015.csv";
        static string[] fileByLines = File.ReadAllLines(fileFor2014);

        static Dictionary<string, double> accountMoneyOwedOnly = new Dictionary<string, double>();
        static Dictionary<string, string> accountFullDetails = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            ProgramStartUp();
        }

        static void ProgramStartUp()
        {
            LoadData();
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

        static void LoadData()
        {       
            FileWriter.ClearLog();

            for (int i = 1; i < fileByLines.Length; i++)
            {
                string[] cell = fileByLines[i].Split(",");
                try
                {                    
                    DateTime date = DateTime.Parse(cell[0]);
                    string fromName = cell[1];
                    string toName = cell[2];
                    string narrative = cell[3];
                    double amount = double.Parse(cell[4]);

                    string accountLineText = $"{date:d}: [Incoming] Paid {amount:C} by {toName} for {narrative}";
                    string accountPaidOtherLineText = $"{date:d}: [Outgoing] Paid {amount:C} to {fromName} for {narrative}";

                    ListAmountOutstanding.CalculateAmountsOwed(accountFullDetails, accountMoneyOwedOnly, fromName, toName, amount, accountLineText, accountPaidOtherLineText);
                }
                catch (FormatException ex)
                {
                    string errorLineData = $"Line data - Date: {cell[0]} | From: {cell[1]} | To: {cell[2]} | Narrative: {cell[3]} | Amount: {cell[4]}";

                    Console.WriteLine($"An error occurred on line {i}. This line's data was not imported to the list. \n{errorLineData}" +
                        $"\nIf you would like for this data to be included in the following reports, please close the program, amend this line, and try again.\n\n");
                    FileWriter.WriteNewLine($"Formatting issue with row {i}: {ex.Message} \n{errorLineData}");
                    continue;
                }catch (Exception ex)
                {
                    FileWriter.WriteNewLine($"Issue with row {i}: {ex.Message}" +
                        $"\nLine data - Date: {cell[0]} | From: {cell[1]} | To: {cell[2]} | Narrative: {cell[3]} | Amount: {cell[4]}\n");
                    continue;
                }
            }       
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
