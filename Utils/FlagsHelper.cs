using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter_exec.Utils
{
    internal class FlagsHelper
    {
        public string InputFile { get; set; } = "input.ceq";
        public string OutputFile { get; set; } = string.Empty;
        public bool Verbose { get; set; }
        public bool Debug { get; set; }
        public bool Help { get; set; }
 
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
                            throw new ArgumentException("Invalid input file name");
                        break;
                    case "-o":
                    case "--output":
                        OutputFile = args[++i];
                        if (OutputFile.Contains("\"") || OutputFile.Contains("'"))
                            throw new ArgumentException("Invalid output file name");
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
                    default:
                        break;
                }
            }
        }
    }
}
