using System;
using System.Collections.Generic;
using System.Text;

namespace LexicalAnalizer_CSharp
{

    public class Parser
    {
        private readonly Scanner scanner = new Scanner();
        public Parser()
        {
            scanner.AnalyzeInput();
        }
        private void PrintError(string grammerName)
        {
            Console.WriteLine($"’{grammerName}’: Illegal  token !");
            Environment.Exit(1);
        }
        private void PrintSuccess()
        {
            Console.WriteLine("The  program is syntactically  correct");
        }
        public void program()
        {
            //<program> -> <declaration2>
            var token = Saver.program.Peek().tokenName;

            switch (token)
            {
                case TokensType.CHAR:
                case TokensType.INT:
                    declaration2();
                    break;
                default:
                    PrintError("program");
                    break;
            }

            PrintSuccess();
        }
        private void declaration2()
        {
            //<declaration2> -> <declaration> <declaration2>
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.CHAR:
                case TokensType.INT:
                    declaration();
                    declaration2();
                    break;
                default:
                    PrintError("declaration2");
                    break;
            }
        }
        private void declaration()
        {
            //< declaration > -> < type > NAME<fun_declaration>
            //< declaration > ->  < var_declaration >

            var token = Saver.program.Dequeue();

            if (!Saver.program.TryDequeue(out token))
            {
                return;
            }

            if (token.tokenName != TokensType.NAME)
            {
                PrintError("declaration");
                return;
            }

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.LPAR:
                    function_declaration();
                    break;
                case TokensType.SEMICOLON:
                    var_declaration();
                    break;
                default:
                    PrintError("declaration");
                    break;
            }
        }
        private void function_declaration()
        {
            //<fun_declaration> -> LPAR <formal_pars> RPAR <block>
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryDequeue(out token))
            {
                return;
            }

            if (token.Item2 != TokensType.LPAR)
            {
                PrintError("function_declaration");
                return;
            }

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.INT:
                case TokensType.CHAR:
                    formal_pars();
                    break;
                case TokensType.RPAR:
                    break;
                default:
                    PrintError("function_declaration");
                    break;
            }

            if (!Saver.program.TryDequeue(out token))
            {
                return;
            }

            if (token.Item2 != TokensType.RPAR)
            {
                PrintError("function_declaration");
                return;
            }

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.LBRACE:
                    block();
                    break;
                default:
                    PrintError("function_declaration");
                    break;
            }
        }
        private void block()
        {
            //<block> -> LBRACE <var_declarations> <statements> RBRACE
            var token = Saver.program.Dequeue();

            var_declarations();
            statements();

            if (!Saver.program.TryDequeue(out token))
            {
                PrintError("block");
                return;
            }

            if (token.tokenName != TokensType.RBRACE)
            {
                PrintError("block");
            }
        }
        private void formal_pars()
        {
            /*
             <formal_pars> -> <formal_par> <formal_pars2>
             <formal_pars> -> ε
             */

            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.INT:
                case TokensType.CHAR:
                    formal_par();
                    formal_pars2();
                    break;
                default:
                    return;
            }

        }
        private void formal_pars2()
        {
            /*
             <formal_pars2> -> COMMA <formal_par><formal_pars2>
             <formal_pars2> -> ε
             */

            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.COMMA:
                    Saver.program.TryDequeue(out token);
                    formal_par();
                    formal_pars2();
                    break;
                default:
                    return;
            }

        }
        private void var_declaration()
        {
            /*
               <var_declaration > -> <type> NAME SEMICOLON
               <var_declaration > -> SEMICOLON
             */
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.INT:
                case TokensType.CHAR:
                    type();
                    break;
                case TokensType.SEMICOLON:
                    Saver.program.TryDequeue(out token);
                    return;
                default:
                    PrintError("var_declaration");
                    break;
            }

            if (!Saver.program.TryDequeue(out token))
            {
                PrintError("var_declaration");
                return;
            }

            switch (token.Item2)
            {
                case TokensType.NAME:
                    break;
                default:
                    PrintError("var_declaration");
                    break;
            }

            if (!Saver.program.TryDequeue(out token))
            {
                PrintError("var_declaration");
                return;
            }

            switch (token.Item2)
            {
                case TokensType.SEMICOLON:
                    break;
                default:
                    PrintError("var_declaration");
                    break;
            }
        }
        private void var_declarations()
        {
            /*
               <var_declarations> -> <var_declaration> <var_declarations>
               <var_declarations> -> ε
             */
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.INT:
                case TokensType.CHAR:
                    var_declaration();
                    var_declarations();
                    break;
            }
        }
        private void statements()
        {
            /*
             <statements> -> <statement> <statements>
             <statements> -> ε
             */
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.NAME:
                case TokensType.RETURN:
                case TokensType.WRITE:
                case TokensType.READ:
                case TokensType.IF:
                case TokensType.WHILE:
                case TokensType.LBRACE:
                    statement();
                    statements();
                    break;
            }
        }
        private void statement()
        {
            /*
             <statement> -> <lexp> ASSIGN <exp> SEMICOLON
             <statement> -> RETURN <exp> SEMICOLON
             <statement> -> <lexp> LPAR <pars> RPAR
             <statement> -> WRITE <exp> SEMICOLON
             <statement> -> READ <exp> SEMICOLON
             <statement> -> IF LPAR <exp> RPAR <statement>
             <statement> -> IF LPAR <exp> RPAR <statement> ELSE <statement>
             <statement> -> WHILE LPAR <exp> RPAR <statement>
             <statement> -> <block>
             */
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.NAME:
                    {
                        lexp();
                        if (!Saver.program.TryDequeue(out token))
                        {
                            return;
                        }

                        switch (token.Item2)
                        {
                            case TokensType.ASSIGN:
                                {
                                    exp();
                                    if (!Saver.program.TryDequeue(out token))
                                    {
                                        return;
                                    }
                                    if (token.Item2 != TokensType.SEMICOLON)
                                    {
                                        PrintError("statement");
                                        return;
                                    }
                                    break;
                                }
                            case TokensType.LPAR:
                                {
                                    pars();
                                    if (!Saver.program.TryDequeue(out token))
                                    {
                                        return;
                                    }
                                    if (token.Item2 != TokensType.RPAR)
                                    {
                                        PrintError("statement");
                                        return;
                                    }
                                    break;
                                }
                        }

                        break;
                    }
                case TokensType.RETURN:
                    {
                        Saver.program.Dequeue();
                        exp();
                        if (!Saver.program.TryDequeue(out token))
                        {
                            return;
                        }
                        if (token.Item2 != TokensType.SEMICOLON)
                        {
                            PrintError("statement");
                            return;
                        }
                        break;
                    }
                case TokensType.WRITE:
                    {
                        Saver.program.Dequeue();
                        exp();
                        if (!Saver.program.TryDequeue(out token))
                        {
                            return;
                        }
                        if (token.Item2 != TokensType.SEMICOLON)
                        {
                            PrintError("statement");
                            return;
                        }
                        break;
                    }
                case TokensType.READ:
                    {
                        Saver.program.Dequeue();
                        exp();
                        if (!Saver.program.TryDequeue(out token))
                        {
                            return;
                        }
                        if (token.Item2 != TokensType.SEMICOLON)
                        {
                            PrintError("statement");
                            return;
                        }
                        break;
                    }
                case TokensType.IF:
                    {
                        Saver.program.Dequeue();
                        if (!Saver.program.TryDequeue(out token))
                        {
                            return;
                        }
                        if (token.Item2 != TokensType.LPAR)
                        {
                            PrintError("statement");
                            return;
                        }
                        exp();
                        if (!Saver.program.TryDequeue(out token))
                        {
                            return;
                        }
                        if (token.Item2 != TokensType.RPAR)
                        {
                            PrintError("statement");
                            return;
                        }
                        statement();
                        if (!Saver.program.TryPeek(out token))
                        {
                            return;
                        }
                        if (token.Item2 == TokensType.ELSE)
                        {
                            Saver.program.Dequeue();
                            statement();
                        }
                        break;
                    }
                case TokensType.WHILE:
                    {
                        Saver.program.Dequeue();
                        if (!Saver.program.TryDequeue(out token))
                        {
                            return;
                        }
                        if (token.Item2 != TokensType.LPAR)
                        {
                            PrintError("statement");
                            return;
                        }
                        exp();
                        if (!Saver.program.TryDequeue(out token))
                        {
                            return;
                        }
                        if (token.Item2 != TokensType.RPAR)
                        {
                            PrintError("statement");
                            return;
                        }
                        statement();
                        break;
                    }
                case TokensType.LBRACE:
                    block();
                    break;
                default:
                    PrintError("statement");
                    break;
            }
        }
        private void pars()
        {
            /*
             <pars> -> <exp> <pars2>
             <pars> -> ε 
             */
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.NAME:
                case TokensType.NOT:
                case TokensType.LPAR:
                case TokensType.NUMER:
                case TokensType.QCHAR:
                case TokensType.LENGTH:
                    exp();
                    pars2();
                    break;
            }
        }
        private void pars2()
        {
            /*
            <pars2> -> COMMA <exp> <pars2>
            <pars2> -> ε 
             */
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryDequeue(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.COMMA:
                    exp();
                    pars2();
                    break;
            }
        }
        private void lexp()
        {
            //<lexp> -> <var> <type2>
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.NAME:
                    var();
                    type2();
                    break;
                default:
                    PrintError("lexp");
                    break;
            }
        }
        private void var()
        {
            //<var> -> NAME
            Saver.program.Dequeue();
        }
        private void type()
        {
            //<type> -> INT <type2>
            //< type > ->CHAR < type2 >

            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryDequeue(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.INT:
                case TokensType.CHAR:
                    type2();
                    break;
                default:
                    PrintError("type");
                    break;
            }
        }
        private void formal_par()
        {
            //<formal_par> -> <type> NAME
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.INT:
                case TokensType.CHAR:
                    type();
                    break;
                default:
                    PrintError("formal_par");
                    break;
            }

            if (!Saver.program.TryDequeue(out token))
            {
                return;
            }

            if (token.Item2 != TokensType.NAME)
            {
                PrintError("formal_par");
                return;
            }
        }
        private void type2()
        {
            //<type2> ->  LBRACK <exp> RBRACK <type2>
            //< type2 > ->ε

            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.LBRACK:
                    Saver.program.TryDequeue(out token);
                    exp();
                    break;
                default:
                    return;
            }

            if (!Saver.program.TryDequeue(out token))
            {
                PrintError("type2");
                return;
            }

            switch (token.Item2)
            {
                case TokensType.RBRACK:
                    type2();
                    break;
                default:
                    PrintError("type2");
                    return;
            }

        }
        private void exp()
        {
            /*
                <exp> -> <exp2>
                <exp> -> LPAR <exp> RPAR
                <exp> -> NUMBER
                <exp> -> NAME LPAR <pars> RPAR
                <exp> -> QCHAR
                <exp> -> LENGTH <exp> 
            */
            var token = (string.Empty, new TokensType());

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.Item2)
            {
                case TokensType.NAME:
                    exp2();
                    break;
                case TokensType.NOT:
                    unop();
                    exp();
                    break;
                case TokensType.LPAR:
                    Saver.program.Dequeue();
                    exp();
                    if (!Saver.program.TryDequeue(out token))
                    {
                        return;
                    }
                    if (token.Item2 != TokensType.RPAR)
                    {
                        PrintError("exp");
                    }
                    break;
                case TokensType.NUMER:
                case TokensType.QCHAR:
                    Saver.program.Dequeue();
                    break;
                case TokensType.LENGTH:
                    Saver.program.Dequeue();
                    exp();
                    break;
                default:
                    PrintError("exp");
                    break;
            }
        }
        private void unop()
        {
            //<unop> -> NOT
            Saver.program.Dequeue();
        }
        private void binop()
        {
            /*
                <binop> -> PLUS
                <binop> -> MINUS
                <binop> -> TIMES
                <binop> -> DIVIDE
                <binop> -> EQUAL
                <binop> -> NEQUAL
                <binop> -> GREATER
                <binop> -> LESS
             */
            Saver.program.Dequeue();
        }
        private void exp2()
        {
           /*
            < exp2 > -> < lexp >
            < exp2 > -> < binop > < exp >
            < exp2 > -> < unop > < exp >
           */
            var token = Saver.program.Dequeue();

            if (!Saver.program.TryPeek(out token))
            {
                return;
            }

            switch (token.tokenName)
            {
                case TokensType.NAME:
                    break;
                case TokensType.PLUS:
                case TokensType.MINUS:
                case TokensType.TIMES:
                case TokensType.DIVIDE:
                case TokensType.EQUAL:
                case TokensType.NEQUAL:
                case TokensType.GREATER:
                case TokensType.LESS:
                    binop();
                    exp();
                    break;
                case TokensType.NOT:
                    unop();
                    if (!Saver.program.TryPeek(out token))
                    {
                        return;
                    }
                    if (token.tokenName == TokensType.ASSIGN)
                    {
                        Saver.program.Dequeue();
                    }
                    exp();
                    break;
                case TokensType.SEMICOLON:
                    break;
            }
        }
    }
}
