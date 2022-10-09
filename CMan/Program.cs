using System;
using System.Linq;
using Antlr4.Runtime;
using CMan.Lang.Program;
using CMan.Lang.Util;

namespace CMan {
    internal static class Program {
        private const string Script = @"
           let rootTest = 1
            mod foo {
                fn testing(): int {
                    ret 5
                }
                
                mod blah{
                   let test = (testing() + 5) - rootTest
                }
            }  
            
           

        ";

        public static void Main(string[] args) {
            var test = new object[] { "test", "test2", 5, 6, "Blah" };
            
            var parser = Setup(Script);
            var program = BuildAst(parser);
            var symbols = program.GetAllSymbols();
            Console.WriteLine(symbols.JoinToString());
            // var variables = program.Statements.OfType<AssignmentAst>();
            Console.WriteLine(program);
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