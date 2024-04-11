using interpreter_exec.Utils;
using Interpreter_exec.Utils;
using Interpreter_lib.Parser;
using Interpreter_lib.Tokenizer;

namespace Interpreter_exec
{
    public class Program
    {
        public static void Main(string[] args)
        {

            FlagsHelper flags = new(args);
            if (flags.Help)
            {
                StreamReader reader = new("help.txt");
                string help = reader.ReadToEnd();
                reader.Close();
                Console.Write(help);    

                return;
            }

            if (flags.Verbose)
            {
                Console.WriteLine("Input file: " + flags.InputFile);
                Console.WriteLine("Output file: " + flags.OutputFile);
                Console.WriteLine("Verbose: " + flags.Verbose);
                Console.WriteLine("Debug: " + flags.Debug);
            }

            // Check if input file exists
            if (!File.Exists(flags.InputFile))
            {
                Console.WriteLine($"Input file \"{flags.InputFile}\" not found.");
                Environment.Exit(1);
            }

            if (!string.IsNullOrEmpty(flags.OutputFile) && !File.Exists(flags.OutputFile))
                File.Create(flags.OutputFile);

            // Read input file
            StreamReader inputReader = new(flags.InputFile);
            string code = inputReader.ReadToEnd();
            inputReader.Close();

            // Tokenize 
            Tokenizer tokenizer = new(code, flags.Language);

            if (flags.Debug)
            {
                Console.WriteLine(String.Format("|{0,-30}|{0,-30}|", "Type", "Value"));
                Console.WriteLine(String.Format("|{0,-30}|{0,-30}|", "------------------------------", "------------------------------"));
                foreach (Token token in tokenizer.Tokens)
                {
                    if(token.Value.Length > 30)
                    {
                        var lines = token.Value.ReplaceLineEndings("\\n").SplitInParts(30);
                        Console.WriteLine(String.Format("|{0,-30}|{1,-30}|", token.Type, lines.First()));
                        foreach (string line in lines.Skip(1))
                            Console.WriteLine(String.Format("|{0,-30}|{1,-30}|", "", line));
                    }
                    else
                        Console.WriteLine(String.Format("|{0,-30}|{1,-30}|", token.Type, token.Value));
                }
                Console.WriteLine("Number of tokens: " + tokenizer.Tokens.Count());
            }


            // Parse
            Parser parser = new(tokenizer.Tokens);
            parser.Parse();

            // Print tree
            if (flags.Debug)
            {
                Console.WriteLine("\nSyntax tree:");
                PrintTree.Print(parser.GetTree());
            }
        }
    }
}