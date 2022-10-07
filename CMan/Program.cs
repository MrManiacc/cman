using System;
using System.Linq;
using Antlr4.Runtime;
using CMan.Lang.Program;
using CMan.Lang.Statement.Assignment;
using CMan.Lang.Statement.Variable;

namespace CMan {
    internal static class Program {
        private const string Script = @"
            let threshold = 69
            if(70 > 50)
                threshold = 5
        ";
        
        public static void Main(string[] args) {
            var parser = Setup(Script);
            var program = BuildAst(parser);
            var variables = program.Statements.OfType<AssignmentAst>();
            foreach (var variable in variables) {
                Console.WriteLine(variable);
            }
        }

        private static CmanParser Setup(string input) {
            var inputStream = new AntlrInputStream(Script);
            var lexer = new CmanLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            return new CmanParser(tokenStream);
        }

        private static ProgramAst BuildAst(CmanParser parser) {
            var visitor = new ProgramVisitor();
            return parser.program().Accept(visitor);
        }
    }
}