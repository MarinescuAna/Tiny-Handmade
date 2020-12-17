using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LexicalAnalizer_CSharp
{
    public static class Printer
    {
        private static StreamWriter outFile =( new FileInfo(Constants.OutputFilePath)).CreateText();

        public static void WriteLine(string token, string tokenName, string value)
        {
            Console.WriteLine($"TOKEN [\ntLexeme = '{token}'\ntToken = {tokenName}");
            outFile.WriteLine($"TOKEN [\ntLexeme = '{token}'\ntToken = {tokenName}");
            if (!string.IsNullOrEmpty(value))
            {
                Console.WriteLine($"tValue = {token}");
                outFile.WriteLine($"tValue = {token}");
            }

            outFile.WriteLine("]\n");
            Console.WriteLine("]\n");
        }
        public static void CloseFile()
        {
            outFile.Close();
        }
    }
}
