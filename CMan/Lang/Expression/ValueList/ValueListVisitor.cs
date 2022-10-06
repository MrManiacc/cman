using System.Linq;

namespace CMan.Lang.Expression.ValueList {
    public class ValueListVisitor : CmanParserBaseVisitor<ValueListAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public ValueListVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }

        public override ValueListAst VisitValueListExpr(CmanParser.ValueListExprContext context) =>
            new ValueListAst(context.valueList().expression().Select(ctx => ctx.Accept(expressionVisitor)).ToList());
    }
}