using interpreter_exec.Utils;
using Interpreter_exec.Utils;
using Interpreter_lib.Evaluator;
using Interpreter_lib.Parser;
using Interpreter_lib.Tokenizer;
using Interpreter_lib.Utils;

namespace Interpreter_exec
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FlagsHelper flags = new(args);
            DateTime? executionStart = null;
            DateTime? executionEnd = null;
            TimeSpan? totalExecutionTime = null; 

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
            executionStart = DateTime.Now;

            Tokenizer tokenizer = new(code, flags.Language);
            
            executionEnd = DateTime.Now;

            if (flags.Debug)
            {
                Printer.PrintTokens(tokenizer.Tokens);

            }
            
            if (flags.Verbose)
            {
                Console.WriteLine("\u001b[39m\u001b[94mTokenization time: \u001b[39m" + (executionEnd - executionStart).Value.ToString("c"));
                totalExecutionTime = (executionEnd - executionStart).Value;
            }

            try
            {
                // Parse
                executionStart = DateTime.Now;
                
                Parser parser = new(tokenizer.Tokens);
                parser.Parse();

                executionEnd = DateTime.Now;

                // Print tree
                if (flags.Debug)
                {
                    Console.WriteLine("\nSyntax tree:");
                    Printer.PrintTree(parser.GetTree());
                    Console.WriteLine("\n");
                }

                if (flags.Verbose)
                {
                    Console.WriteLine("\u001b[39m\u001b[94mParsing time: \u001b[39m" + (executionEnd - executionStart).Value.ToString("c"));
                    totalExecutionTime += (executionEnd - executionStart).Value;
                }

                // Execute
                executionStart = DateTime.Now;

                Evaluator evaluator = new();
                evaluator.Evaluate(parser.GetTree());
                
                executionEnd = DateTime.Now;

                if (flags.Verbose)
                {
                    Console.WriteLine("\n\n\u001b[39m\u001b[94mExecution time: \u001b[39m" + (executionEnd - executionStart).Value.ToString("c"));
                    totalExecutionTime += (executionEnd - executionStart).Value;
                    Console.WriteLine("\u001b[39m\u001b[94mTotal execution time: \u001b[39m" + totalExecutionTime.Value.ToString("c"));
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }

        }
    }
}