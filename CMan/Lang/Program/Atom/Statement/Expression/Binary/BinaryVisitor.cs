using System.Data;

namespace CMan.Lang.Program.Atom.Statement.Expression.Binary {
    public class BinaryVisitor : CmanParserBaseVisitor<BinaryAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public BinaryVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }

        public override BinaryAst VisitBinaryExpr(CmanParser.BinaryExprContext context) {
            var left = context.left.Accept(expressionVisitor);
            var right = context.right.Accept(expressionVisitor);
            var binOp = context.binaryOperator().GetText();
            var op = binOp == "%" ? BinaryOp.Mod :
                binOp == "*" ? BinaryOp.Mul :
                binOp == "/" ? BinaryOp.Div :
                binOp == "-" ? BinaryOp.Sub :
                binOp == "+" ? BinaryOp.Add :
                throw new InvalidExpressionException("Unknown binary operation");
            return new BinaryAst(left, op, right);
        }
    }
}