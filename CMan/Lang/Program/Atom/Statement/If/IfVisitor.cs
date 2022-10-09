using CMan.Lang.Program.Atom.Statement.Expression;

namespace CMan.Lang.Program.Atom.Statement.If {
    public class IfVisitor : CmanParserBaseVisitor<IfAst> {
        private readonly StatementVisitor statementVisitor;

        public IfVisitor(StatementVisitor statementVisitor) {
            this.statementVisitor = statementVisitor;
        }

        public override IfAst VisitIfStmt(CmanParser.IfStmtContext context) {
            var cond = context.@if().expression().Accept(statementVisitor);
            if (!(cond is IExpression condition)) return base.VisitIfStmt(context);
            var trueStatement = context.@if().trueStatement.Accept(statementVisitor);
            return context.@if().falseStatement != null
                ? new IfAst(condition, trueStatement, context.@if().falseStatement.Accept(statementVisitor))
                : new IfAst(condition, trueStatement);
        }
    }
}