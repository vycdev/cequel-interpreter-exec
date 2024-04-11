using Interpreter_lib.Parser;
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
        public static void PrintTree(ISyntaxNode node, string indent = "", bool last = false)
        {
            var regex = new Regex(Regex.Escape(":"));
            string name = "\u001b[39m";

            if(node.GetType() == typeof(Node))
                name += "\x1b[94m" + node.Print();
            else
                name += "\x1b[93m" + regex.Replace(node.Print(), ":\u001b[39m", 1);

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
