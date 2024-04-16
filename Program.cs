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
                Printer.PrintTokens(tokenizer.Tokens);
            }

            try
            {
                // Parse
                Parser parser = new(tokenizer.Tokens);
                parser.Parse();

                // Print tree
                if (flags.Debug)
                {
                    Console.WriteLine("\nSyntax tree:");
                    Printer.PrintTree(parser.GetTree());
                }
            }
            catch (ParsingException ex)
            {
                // TODO: Make the error messages nicer and put them inside the ParsingException class
                Console.WriteLine("\nParsing exception:");

                if(ex.Rule == null)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

                Console.WriteLine("Rule: ");    
                Console.WriteLine(ex.Rule._rule);

                Console.WriteLine("Expected tokens: ");
                foreach (var token in ex.Rule._expectedTokens)
                {
                    Console.WriteLine(token);
                }

                if(ex.Rule._currentTokenIndex < ex.Rule._tokens.Count)
                {
                    Console.WriteLine("Current token: ");
                    Console.WriteLine(ex.Rule._tokens[ex.Rule._currentTokenIndex].Type);
                    Console.WriteLine(ex.Rule._tokens[ex.Rule._currentTokenIndex].Value);
                }

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}