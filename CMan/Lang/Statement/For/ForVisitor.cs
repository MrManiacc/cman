using CMan.Lang.Expression;
using CMan.Lang.Statement.Variable;

namespace CMan.Lang.Statement.For {
    public class ForVisitor : CmanParserBaseVisitor<ForAst> {
        private readonly StatementVisitor statementVisitor;

        public ForVisitor(StatementVisitor statementVisitor) {
            this.statementVisitor = statementVisitor;
        }

        public override ForAst VisitForStmt(CmanParser.ForStmtContext context) {
            var variable = context.@for().variable().Accept(statementVisitor);
            if (!(variable is VariableAst var)) return base.VisitForStmt(context);
            var condition = context.@for().condition.Accept(statementVisitor);
            if (!(condition is IExpression cond)) return base.VisitForStmt(context);
            var accumulator = context.@for().advancement.Accept(statementVisitor);
            if (!(accumulator is IExpression accum)) return base.VisitForStmt(context);
            var body = context.@for().statement().Accept(statementVisitor);
            return new ForAst(var, cond, accum, body);
        }
    }
}