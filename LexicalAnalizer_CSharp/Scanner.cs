using System;
using System.Collections.Generic;
using System.Text;

namespace LexicalAnalizer_CSharp
{
    public class Scanner
    {
        private readonly System.IO.StreamReader file = new System.IO.StreamReader(Constants.InputFilePath);
        private readonly List<string> contentRead = new List<string>();
        private readonly Tokens Tokens = new Tokens();

        private void ReadContent()
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
        public void AnalyzeInput()
        {
            // read the input and add the each line of the content into contentRead
            ReadContent();

            var construct = new StringBuilder();

            // analize each line to identify the symbols
            foreach (var line in contentRead)
            {
                // skip the empty lines
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (Tokens.IdentifyComment(line))
                {
                    continue;
                }

                // take a line and analize the content 
                for (var index = 0; index < line.Length; index++)
                {
                    //skip the spaces
                    if (string.IsNullOrWhiteSpace(line[index].ToString()))
                    {
                        continue;
                    }


                    if (index<line.Length+1 && line[index]==Constants.Slash && line[index+1]==Constants.Slash)
                    {
                        var splitComment = line.Split("//");
                        Tokens.IdentifyComment(splitComment[1].Insert(0, "//"));
                        break;
                    }

                    if (IsNotLetter(line[index]) && line[index] != Constants.Underscore)
                    {
                        if (line[index] == Constants.Apostrophe)
                        {
                            index = Tokens.IdentifyQchar(line, index);
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

        }
    }
}
