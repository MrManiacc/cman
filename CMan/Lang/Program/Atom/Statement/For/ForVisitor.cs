using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Program.Atom.Statement.Variable;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Statement.For {
    public class ForVisitor : CmanParserScopedVisitor<ForAst> {
        public ForVisitor(IScope scope) : base(scope) { }

        public override ForAst VisitForStmt(CmanParser.ForStmtContext context) {
            var forAst = new ForAst();
            forAst.SetParentScope(Scope);
            Scope.Nest(forAst);
            var statementVisitor = new StatementVisitor(forAst);
            var variable = context.@for().variable().Accept(statementVisitor);
            if (!(variable is VariableAst var)) return base.VisitForStmt(context);
            var condition = context.@for().condition.Accept(statementVisitor);
            if (!(condition is IExpression cond)) return base.VisitForStmt(context);
            var accumulator = context.@for().advancement.Accept(statementVisitor);
            if (!(accumulator is IExpression accum)) return base.VisitForStmt(context);
            var body = context.@for().statement().Accept(statementVisitor);
            forAst.Variable = var;
            forAst.Condition = cond;
            forAst.Accumulator = accum;
            forAst.Body = body;
            return forAst;
        }
    }
}