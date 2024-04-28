using Interpreter_lib.Parser;
using Interpreter_lib.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace interpreter_exec.Utils
{

    public static class Printer
    {
        public static void PrintTokens(List<Token> tokens)
        {
            Console.WriteLine(String.Format("|{0,-30}|{1,-30}|{2,-10}|", "Type", "Value", "Line"));
            Console.WriteLine(String.Format("|{0,-30}|{0,-30}|{1,-10}|", "------------------------------", "----------"));
            foreach (Token token in tokens)
            {
                if (token.Value.Length > 30)
                {
                    var lines = token.Value.ReplaceLineEndings("\\n").SplitInParts(30);
                    Console.WriteLine(String.Format("|{0,-30}|{1,-30}|{2,-10}|", token.Type, lines.First(), token.Line));
                    foreach (string line in lines.Skip(1))
                        Console.WriteLine(String.Format("|{0,-30}|{1,-30}|{2,-10}|", "", line, ""));
                }
                else
                    Console.WriteLine(String.Format("|{0,-30}|{1,-30}|{2,-10}|", token.Type, token.Value, token.Line));
            }
            Console.WriteLine("\u001b[39m\u001b[94mNumber of tokens: \u001b[39m" + tokens.Count());
        }

        public static void PrintTree(ISyntaxNode node, string indent = "", bool last = false)
        {
            var regexColon = new Regex(Regex.Escape(":"));
            var regexSeparator = new Regex(Regex.Escape("|"));
            string name = "\u001b[39m";

            if(node.GetType() == typeof(Node))
            {
                name += "\x1b[94m" + node.Print();
                name = regexSeparator.Replace(name, "\u001b[39m|\u001b[92m", 1);
                name = regexColon.Replace(name, ":\u001b[39m");
            }
            else
            {
                name += "\x1b[93m" + regexColon.Replace(node.Print(), ":\u001b[39m");
                name = regexSeparator.Replace(name, "\u001b[39m|\u001b[92m", 1);
            }

            name += "\u001b[39m";

            Console.Write("\n" + indent + "+- " + name);
            indent += last ? "   " : "|  ";

            if(node.GetType() == typeof(Node))
                for (int i = 0; i < ((Node)node).GetSyntaxNodes().Count; i++)
                {
                    PrintTree(((Node)node).GetSyntaxNodes()[i], indent, i == ((Node)node).GetSyntaxNodes().Count - 1);
                }
        }
    }
}
