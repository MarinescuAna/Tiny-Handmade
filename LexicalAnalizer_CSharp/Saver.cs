using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LexicalAnalizer_CSharp
{
    public enum TokensType
    {
        INT,
        RETURN,
        RBRACE,
        LBRACE,
        SEMICOLON,
        TIMES,
        WRITE,
        NOT,
        IF,
        LPAR,
        RPAR,
        LBRACK,
        RBRACK,
        COMMA,
        DIVIDE,
        READ,
        LENGTH,
        ELSE,
        PLUS,
        EQUAL,
        GREATER,
        LESS,
        WHILE,
        NEQUAL,
        ASSIGN,
        MINUS,
        CHAR,
        NUMER,
        NAME,
        QCHAR
    };
    public static class Saver
    {
        public static Queue<(string token, TokensType tokenName)> program = new Queue<(string token, TokensType tokenName)>();
        public static void WriteLine(string token, string tokenName)
        {
            switch (tokenName)
            {
                case "INT":
                    program.Enqueue((token, TokensType.INT));
                    break;
                case "RETURN":
                    program.Enqueue((token, TokensType.RETURN));
                    break;
                case "RBRACE":
                    program.Enqueue((token, TokensType.RBRACE));
                    break;
                case "LBRACE":
                    program.Enqueue((token, TokensType.LBRACE));
                    break;
                case "SEMICOLON":
                    program.Enqueue((token, TokensType.SEMICOLON));
                    break;
                case "TIMES":
                    program.Enqueue((token, TokensType.TIMES));
                    break;
                case "WRITE":
                    program.Enqueue((token, TokensType.WRITE));
                    break;
                case "NOT":
                    program.Enqueue((token, TokensType.NOT));
                    break;
                case "IF":
                    program.Enqueue((token, TokensType.IF));
                    break;
                case "LPAR":
                    program.Enqueue((token, TokensType.LPAR));
                    break;
                case "RPAR":
                    program.Enqueue((token, TokensType.RPAR));
                    break;
                case "LBRACK":
                    program.Enqueue((token, TokensType.LBRACK));
                    break;
                case "RBRACK":
                    program.Enqueue((token, TokensType.RBRACK));
                    break;
                case "COMMA":
                    program.Enqueue((token, TokensType.COMMA));
                    break;
                case "DIVIDE":
                    program.Enqueue((token, TokensType.DIVIDE));
                    break;
                case "READ":
                    program.Enqueue((token, TokensType.READ));
                    break;
                case "LENGTH":
                    program.Enqueue((token, TokensType.LENGTH));
                    break;
                case "ELSE":
                    program.Enqueue((token, TokensType.ELSE));
                    break;
                case "PLUS":
                    program.Enqueue((token, TokensType.PLUS));
                    break;
                case "EQUAL":
                    program.Enqueue((token, TokensType.EQUAL));
                    break;
                case "GREATER":
                    program.Enqueue((token, TokensType.GREATER));
                    break;
                case "LESS":
                    program.Enqueue((token, TokensType.LESS));
                    break;
                case "WHILE":
                    program.Enqueue((token, TokensType.WHILE));
                    break;
                case "NEQUAL":
                    program.Enqueue((token, TokensType.NEQUAL));
                    break;
                case "ASSIGN":
                    program.Enqueue((token, TokensType.ASSIGN));
                    break;
                case "MINUS":
                    program.Enqueue((token, TokensType.MINUS));
                    break;
                case "CHAR":
                    program.Enqueue((token, TokensType.CHAR));
                    break;
                case "NUMBER":
                    program.Enqueue((token, TokensType.NUMER));
                    break;
                case "NAME":
                    program.Enqueue((token, TokensType.NAME));
                    break;
                case "QCHAR":
                    program.Enqueue((token, TokensType.QCHAR));
                    break;
            }
        }
    }
}
