using System;
using System.Collections.Generic;
using System.Text;

namespace LexicalAnalizer_CSharp
{
    public static class Constants
    {
        public static readonly string NumbersFrom0To9 = "0123456789";
        public static readonly string InputFilePath = @"C:\Users\marin\OneDrive\Documente\GitHub\Tiny-Handmade\LexicalAnalizer_CSharp\In.txt";
        public static readonly string OutputFilePath = @"C:\Users\marin\OneDrive\Documente\GitHub\Tiny-Handmade\LexicalAnalizer_CSharp\Out.txt";
        public static readonly string NamePattern = @"[a-zA-Z]([a-zA-Z]|[0-9]|[_])*";
        public static readonly string Exception = "Invalid expression!!";
        public static readonly string Number = "NUMBER";
        public static readonly string Qchar = "QCHAR";
        public static readonly string Name = "NAME";
        public static readonly string Comment = "COMMENT";
        public static readonly char a = 'a';
        public static readonly char z = 'z';
        public static readonly char Assign = '=';
        public static readonly string Equal = "==";
        public static readonly char Apostrophe = '\'';
        public static readonly char Underscore = '_';
        public static readonly char Slash = '/';
        public static readonly char Star = '*';
        public static readonly char NewLine = '\n';
        public static readonly string StartComment = "/*";
        public static readonly string EndComment = "*/";
    }
}
