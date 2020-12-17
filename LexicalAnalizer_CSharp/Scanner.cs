using System;
using System.Collections.Generic;
using System.Text;

namespace LexicalAnalizer_CSharp
{
    public class Scanner
    {
        private System.IO.StreamReader file = new System.IO.StreamReader(Constants.InputFilePath);
        private List<string> contentRead = new List<string>();
        private Tokens Tokens = new Tokens();

        public void ReadContent()
        {
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                contentRead.Add(line);
            }
        }
        private bool IsNotLetter(char character)
        {
            return character < Constants.a || character > Constants.z;
        }
        private bool IsNumber(char character)
        {
            return Constants.NumbersFrom0To9.Contains(character);
        }
        private bool IsCommentEnd(string end) {
            return end == Constants.EndComment;
        }
        private bool IsCommentStart(string start)
        {
            return start == Constants.StartComment;
        }
        public void AnalyzeInput()
        {
            var construct = new StringBuilder();
            var isComment = false;
            foreach (var line in contentRead)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                for (var index = 0; index < line.Length; index++)
                {
                    if (string.IsNullOrWhiteSpace(line[index].ToString()))
                    {
                        continue;
                    }

                    if (isComment)
                    {
                        do {
                            construct.Append(line[index++]);
                        } while (index < line.Length && !IsCommentEnd($"{line[index - 1]}{line[index]}"));

                        if(index < line.Length && IsCommentEnd($"{line[index - 1]}{line[index]}"))
                        {
                            construct.Append(line[index]);
                            Tokens.IdentifyComment(construct.ToString());
                            construct.Length = 0;
                            isComment = false;
                        }
                        else
                        {
                            construct.Append(Constants.NewLine);
                        }
                        continue;
                    }
                    if (index<line.Length-1 && IsCommentStart($"{line[index]}{line[index+1]}"))
                    {
                        isComment = true;
                        do
                        {
                            construct.Append(line[index++]);
                        } while (index < line.Length && !IsCommentEnd($"{line[index - 1]}{line[index]}"));

                        if (index!=line.Length && IsCommentEnd($"{line[index - 1]}{line[index]}"))
                        {
                            construct.Append(line[index]);
                            Tokens.IdentifyComment(construct.ToString());
                            construct.Length = 0;
                            isComment = false;
                        }
                        else
                        {
                            construct.Append(Constants.NewLine);
                        }

                        continue;
                    }

                    if (IsNotLetter(line[index]) && line[index] != Constants.Underscore)
                    {
                        if (line[index] == Constants.Apostrophe)
                        {
                            index=Tokens.IdentifyQchar(line,index);
                            continue;
                        }
                        if (IsNumber(line[index]))
                        {
                            index = Tokens.IdentifyNumbers(line, index);
                            continue;
                        }
                        if (line[index] == Constants.Assign && line[index + 1] == Constants.Assign)
                        {
                            Tokens.IdentifyEqual(Constants.Equal);
                            index++;
                            continue;
                        }

                        Tokens.IdentifySimbols(line[index].ToString());
                        continue;
                        
                    }
                    else
                    {
                        while ((!IsNotLetter(line[index]) || 
                            line[index] == Constants.Underscore || 
                            IsNumber(line[index])) && 
                            index < line.Length)
                        {
                            construct.Append(line[index++]);
                        }

                        if (!Tokens.TryGetValueForStatement(construct.ToString()))
                        {
                            Tokens.IdentifyNames(construct.ToString());
                        }

                        construct.Length = 0;
                        index--;
                    }
                }
            }

            if (isComment)
            {
                Tokens.IdentifyComment(construct.ToString());
            }


            Printer.CloseFile();
        }



    }
}
