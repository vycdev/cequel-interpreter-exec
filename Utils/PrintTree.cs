using Interpreter_lib.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter_exec.Utils
{

    public static class PrintTree
    {
        public static void Print(Node tree, string indent = "", bool last = false)
        {
            Console.Write(indent + "+- " + tree.GetRule().ToString() + " | (" + string.Join(",", tree.GetTokens().Select(a => "[" + a.Type.ToString() + "," + a.Value + "]")) + ")");
            indent += last ? "   " : "|  ";

            for (int i = 0; i < tree.GetNodes().Count; i++)
            {
                Print(tree.GetNodes()[i], indent, i == tree.GetNodes().Count - 1);
            }
        }
    }
}
