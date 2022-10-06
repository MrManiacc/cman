using System;
using System.Linq;
using CMan.Lang.Statement;

namespace CMan.Lang.Program {
    public class ProgramVisitor : CmanParserBaseVisitor<ProgramAst> {
        private static readonly StatementVisitor StatementVisitor = new StatementVisitor();

        public override ProgramAst VisitProgram(CmanParser.ProgramContext context) {
            var statements = context.statement()
                .Select(x => x.Accept(StatementVisitor)).ToList();
            return new ProgramAst(statements);
        }
    }
}