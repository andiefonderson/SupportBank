using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    internal class FileWriter
    {
        public static void WriteNewLine(string logLine)
        {
            File.AppendAllText(@"\TestOutputFiles\SupportBankErrorLog.txt", String.Format($"{logLine}\n"));
        }

        public static void ClearLog()
        {
            File.WriteAllText(@"\TestOutputFiles\SupportBankErrorLog.txt", "");
        }
    }
}
