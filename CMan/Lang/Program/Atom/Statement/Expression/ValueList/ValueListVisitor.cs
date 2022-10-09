using System.Linq;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Statement.Expression.ValueList {
    public class ValueListVisitor : CmanParserScopedVisitor<ValueListAst> {
        public ValueListVisitor(IScope scope) : base(scope) { }

        public override ValueListAst VisitValueListExpr(CmanParser.ValueListExprContext context) {
            var expressionVisitor = new ExpressionVisitor(Scope);
            return new ValueListAst(context.valueList().expression().Select(ctx => ctx.Accept(expressionVisitor))
                .ToList());
        }

        #region Overrides of CmanParserBaseVisitor<ValueListAst>
        public override ValueListAst VisitExpressionList(CmanParser.ExpressionListContext context) {
            var expressionVisitor = new ExpressionVisitor(Scope);
            return new ValueListAst(context.expression().Select(ctx => ctx.Accept(expressionVisitor))
                .ToList());
        }
        #endregion
    }
}