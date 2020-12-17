using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LexicalAnalizer_CSharp
{
    public class Tokens
    {
        private Dictionary<string, string> tokensSymbols = new Dictionary<string, string>();
        private Dictionary<string, string> tokensStatements = new Dictionary<string, string>();
        public  Tokens()
        {
            tokensStatements.Add("int", "INT");
            tokensStatements.Add("return", "RETURN"); 
            tokensSymbols.Add("}", "RBRACE");
            tokensSymbols.Add("{", "LBRACE");
            tokensSymbols.Add(";", "SEMICOLON");
            tokensSymbols.Add("*", "TIMES");
            tokensStatements.Add("write", "WRITE");
            tokensSymbols.Add("!", "NOT");
            tokensStatements.Add("if", "IF");
            tokensSymbols.Add("(", "LPAR");
            tokensSymbols.Add(")", "RPAR");
            tokensSymbols.Add("[", "LBRACK");
            tokensSymbols.Add("]", "RBRACK");
            tokensSymbols.Add(",", "COMMA");
            tokensSymbols.Add("/", "DIVIDE");
            tokensStatements.Add("read", "READ");
            tokensStatements.Add("length", "LENGTH");
            tokensStatements.Add("else", "ELSE");
            tokensSymbols.Add("+", "PLUS");
            tokensSymbols.Add("==", "EQUAL");
            tokensSymbols.Add(">", "GREATER");
            tokensSymbols.Add("<", "LESS");
            tokensStatements.Add("while", "WHILE");
            tokensSymbols.Add("!=", "NEQUAL");
            tokensSymbols.Add("=", "ASSIGN");
            tokensSymbols.Add("-", "MINUS");
            tokensStatements.Add("char", "CHAR");
        }
        public int IdentifyNumbers(string line, int indexStart )
        {
            var number = new StringBuilder();
            while(indexStart<line.Length && Constants.NumbersFrom0To9.Contains(line[indexStart]))
            {
                number.Append(line[indexStart++]);
            }
            Printer.WriteLine(number.ToString(), Constants.Number, number.ToString());
            return indexStart-1;
        }
        public void IdentifyEqual(string equal)
        {
            Printer.WriteLine(equal, tokensSymbols[equal], string.Empty);
        }
        public void IdentifySimbols(string key)
        { 
            if (tokensSymbols.ContainsKey(key))
            {
                Printer.WriteLine(key, tokensSymbols[key], string.Empty);
            }
        }
        public bool TryGetValueForStatement(string key)
        {
            if (tokensStatements.ContainsKey(key))
            {
                Printer.WriteLine(key, tokensStatements[key], string.Empty);
                return true;
            }
            return false;
        }
        public void IdentifyNames(string value)
        {
            if (Regex.Match(value, Constants.NamePattern).Length == value.Length)
            {
                Printer.WriteLine(value, Constants.Name, string.Empty);
            }
            else
            {
                Printer.WriteLine(value, Constants.Exception, string.Empty);
            }
        }
        public int IdentifyQchar(string line, int index)
        {
            var construct = new StringBuilder();
            do
            {
                construct.Append(line[index++]);

            } while (index < line.Length && line[index] != Constants.Apostrophe);
            construct.Append(line[index++]);

            if (construct.ToString().StartsWith(Constants.Apostrophe) && 
                construct.ToString().EndsWith(Constants.Apostrophe) && 
                construct.ToString().Length==3)
            {
                Printer.WriteLine(construct.ToString(), Constants.Qchar, construct.ToString());
            }
            else
            {
                Printer.WriteLine(construct.ToString(), Constants.Exception, string.Empty);
            }

            return index-1;
        }
        public void IdentifyComment(string comment)
        {
            if (comment.Contains(Constants.StartComment) && comment.Contains(Constants.EndComment))
            {
                Printer.WriteLine(comment, Constants.Comment, string.Empty);
            }
            else
            {
                Printer.WriteLine(comment, Constants.Exception, string.Empty);
            }
        }
    }
}
