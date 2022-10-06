using CMan.Lang.Expression.Literal;
using CMan.Lang.Expression.ValueList;

namespace CMan.Lang.Expression {
    public class ExpressionVisitor : CmanParserBaseVisitor<IExpression> {
        private readonly LiteralVisitor literalVisitor;
        private readonly ValueListVisitor valueListVisitor;
    
        public ExpressionVisitor() {
            literalVisitor = new LiteralVisitor();
            valueListVisitor = new ValueListVisitor(this);
        }

        public override IExpression VisitLiteralExpr(CmanParser.LiteralExprContext context) =>
            context.Accept(literalVisitor);

        public override IExpression VisitValueListExpr(CmanParser.ValueListExprContext context) =>
            context.Accept(valueListVisitor);
    }
}