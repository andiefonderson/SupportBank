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
        public static void WriteNewLine(string errorLine)
        {
            File.AppendAllText(@"\TestOutputFiles\SupportBankErrorLog.txt", String.Format($"{errorLine}"));
        }

        public static void ClearLog()
        {
            File.WriteAllText(@"\TestOutputFiles\SupportBankErrorLog.txt", "");
        }
    }
}
