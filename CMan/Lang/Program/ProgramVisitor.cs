using System;
using System.Linq;
using CMan.Lang.Function;
using CMan.Lang.Statement;

namespace CMan.Lang.Program {
    public class ProgramVisitor : CmanParserBaseVisitor<ProgramAst> {
        private static readonly StatementVisitor StatementVisitor = new StatementVisitor();
        private static readonly FunctionVisitor FunctionVisitor = new FunctionVisitor(StatementVisitor);

        public override ProgramAst VisitProgram(CmanParser.ProgramContext context)
            => new ProgramAst(context.function()
                    .Select(x => x.Accept(FunctionVisitor)).ToList(),
                context.statement()
                    .Select(x => x.Accept(StatementVisitor)).ToList());
    }
}