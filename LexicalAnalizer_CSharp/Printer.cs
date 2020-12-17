using System;
using System.Collections.Generic;
using System.Text;

namespace LexicalAnalizer_CSharp
{
    public static class Printer
    {
        //private System.IO.StreamWriter  outFile = new System.IO.StreamWriter(@"C:\Users\marin\OneDrive\Documente\GitHub\LexicalAnalizer_CSharp\In.txt");

        public static void WriteLine(string token, string tokenName, string value)
        {
            Console.WriteLine($"TOKEN [\ntLexeme = '{token}'\ntToken = {tokenName}");

            if (!string.IsNullOrEmpty(value))
            {
                Console.WriteLine($"tValue = {token}");
            }
            
            Console.WriteLine("]\n");
        }
    }
}
