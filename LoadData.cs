using System;
using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    internal class LoadData
    {

        static public void ExtractData(string fileName, Dictionary<string, string> dictStringString, Dictionary<string, double> dictStringDouble)
        {
            try
            {
                FileWriter.ClearLog();
                FileWriter.WriteNewLine($"[{DateTime.Now}] Attempting to load in '${fileName}'");
                string[] fileByLines = File.ReadAllLines(fileName);
                FileWriter.WriteNewLine($"[{DateTime.Now}] Load successful.");

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

                        ListAmountOutstanding.CalculateAmountsOwed(dictStringString, dictStringDouble, fromName, toName, amount, accountLineText, accountPaidOtherLineText);
                    }
                    catch (FormatException ex)
                    {
                        string errorLineData = $"Line data - Date: {cell[0]} | From: {cell[1]} | To: {cell[2]} | Narrative: {cell[3]} | Amount: {cell[4]}";

                        Console.WriteLine($"An error occurred on line {i + 1}. This line's data was not imported to the list. \n{errorLineData}" +
                            $"\nIf you would like for this data to be included in the following reports, please close the program, amend this line, and try again.\n\n");
                        FileWriter.WriteNewLine($"Formatting issue with row {i}: {ex.Message} \n{errorLineData}");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        FileWriter.WriteNewLine($"Issue with row {i + 1}: {ex.Message}" +
                            $"\nLine data - Date: {cell[0]} | From: {cell[1]} | To: {cell[2]} | Narrative: {cell[3]} | Amount: {cell[4]}\n");
                        continue;
                    }
                }
            }
            catch (FileNotFoundException ex)
            {                
                Console.WriteLine($"{fileName} was not found. Please check the directory and make sure you have stored the right file in the right location." +
                    $"\nPlease end the program and try again.");
                FileWriter.WriteNewLine($"{fileName} was not found. {ex.Message}");
            }catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} Please close the program and try again.");
                FileWriter.WriteNewLine($"The following error was thrown: {ex.Message}");
            }
        }
    }
}