using Interpreter_exec.Utils;
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
                throw new FileNotFoundException("Input file not found");

            if (!File.Exists(flags.OutputFile))
                File.Create(flags.OutputFile);

            // Read input file
            StreamReader inputReader = new(flags.InputFile);
            string code = inputReader.ReadToEnd();
            inputReader.Close();

            // Tokenize 
            Tokenizer tokenizer = new(code);

        }
    }
}