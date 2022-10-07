using CMan.Lang.Expression.Binary;
using CMan.Lang.Expression.Conditional;
using CMan.Lang.Expression.Literal;
using CMan.Lang.Expression.ValueList;

namespace CMan.Lang.Expression {
    public class ExpressionVisitor : CmanParserBaseVisitor<IExpression> {
        private readonly LiteralVisitor literalVisitor;
        private readonly ValueListVisitor valueListVisitor;
        private readonly BinaryVisitor binaryVisitor;
        private readonly ConditionalVisitor conditionalVisitor;

        public ExpressionVisitor() {
            literalVisitor = new LiteralVisitor();
            valueListVisitor = new ValueListVisitor(this);
            binaryVisitor = new BinaryVisitor(this);
            conditionalVisitor = new ConditionalVisitor(this);
        }

        public override IExpression VisitConditionalExpr(CmanParser.ConditionalExprContext context) =>
            context.Accept(conditionalVisitor);

        public override IExpression VisitBinaryExpr(CmanParser.BinaryExprContext context) =>
            context.Accept(binaryVisitor);

        public override IExpression VisitLiteralExpr(CmanParser.LiteralExprContext context) =>
            context.Accept(literalVisitor);

        public override IExpression VisitValueListExpr(CmanParser.ValueListExprContext context) =>
            context.Accept(valueListVisitor);
    }
}