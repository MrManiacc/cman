using System;

namespace CMan.Lang.Program.Atom.Statement.Expression.Unary {
    public class UnaryVisitor : CmanParserBaseVisitor<UnaryAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public UnaryVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }

        public override UnaryAst VisitUnaryExpr(CmanParser.UnaryExprContext context) {
            var expression = context.expression().Accept(expressionVisitor);
            var unary = context.@operator;
            var op = unary.NOT() != null ? UnaryOp.Not :
                unary.PLUS().Length == 2 ? UnaryOp.PlusPlus :
                unary.MINUS().Length == 2 ? UnaryOp.MinusMinus :
                throw new InvalidOperationException($"Unknown unary operator {unary.GetText()}");
            var prefix = context.expression().SourceInterval.StartsAfter(unary.SourceInterval);

            return new UnaryAst(expression, op, prefix);
        }
    }
}