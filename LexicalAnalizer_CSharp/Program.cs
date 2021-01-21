using System;

namespace LexicalAnalizer_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            parser.program();

            //foreach(var stri in Saver.program){
            //    Console.WriteLine(stri.token + " -> " + stri.tokenName.ToString());
            //}
        }
    }
}
