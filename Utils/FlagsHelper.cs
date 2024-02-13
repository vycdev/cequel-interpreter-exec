using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interpreter_lib.Utils;
using Interpreter_lib.Tokenizer;

namespace Interpreter_exec.Utils
{
    internal class FlagsHelper
    {
        public string InputFile { get; set; } = "input.ceq";
        public string OutputFile { get; set; } = string.Empty;
        public bool Verbose { get; set; }
        public bool Debug { get; set; }
        public bool Help { get; set; }
        public TokensLanguage Language { get; set; } = Languages.romanian;
 
        public FlagsHelper(string[] args) 
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-i":
                    case "--input":
                        InputFile = args[++i];
                        if(InputFile.Contains("\"") || InputFile.Contains("'"))
                        {
                            Console.WriteLine("Invalid input file name");
                            Environment.Exit(1);
                        }
                        break;
                    case "-o":
                    case "--output":
                        OutputFile = args[++i];
                        if (OutputFile.Contains("\"") || OutputFile.Contains("'"))
                        {
                            Console.WriteLine("Invalid output file name");
                            Environment.Exit(1);
                        }
                        break;
                    case "-v":
                    case "--verbose":
                        Verbose = true;
                        break;
                    case "-d":
                    case "--debug":
                        Debug = true;
                        break;
                    case "-h":
                    case "-?":
                    case "--help":
                        Help = true;
                        break;
                    case "-l":
                    case "--language":
                        if (args[++i] == "en")
                            Language = Languages.english;
                        if (args[i] == "ro")
                            Language = Languages.romanian;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
