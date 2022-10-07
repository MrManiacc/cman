using System.Data;
using CMan.Lang.Expression.ValueList;

namespace CMan.Lang.Expression.Binary {
    public class BinaryVisitor : CmanParserBaseVisitor<BinaryAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public BinaryVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }

        public override BinaryAst VisitBinaryExpr(CmanParser.BinaryExprContext context) {
            var left = context.left.Accept(expressionVisitor);
            var right = context.right.Accept(expressionVisitor);
            var binOp = context.binaryOperator();
            var op = !binOp.GetRuleContext<CmanParser.ModulusOpContext>(0).IsEmpty ? BinaryOp.Mod :
                !binOp.GetRuleContext<CmanParser.MultiplyOpContext>(0).IsEmpty ? BinaryOp.Mul :
                !binOp.GetRuleContext<CmanParser.DivideOpContext>(0).IsEmpty ? BinaryOp.Div :
                !binOp.GetRuleContext<CmanParser.MinusOpContext>(0).IsEmpty ? BinaryOp.Sub :
                !binOp.GetRuleContext<CmanParser.PlusOpContext>(0).IsEmpty ? BinaryOp.Add :
                throw new InvalidExpressionException("Unknown binary operation");
            return new BinaryAst(left, op, right);
        }
    }
}