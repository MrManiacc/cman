using System;

namespace CMan.Lang.Program.Atom.Statement.Expression.Conditional {
    public class ConditionalVisitor : CmanParserBaseVisitor<ConditionalAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public ConditionalVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }

        public override ConditionalAst VisitConditionalExpr(CmanParser.ConditionalExprContext context) {
            var left = context.left.Accept(expressionVisitor);
            var cond = context.condtionalOperator();
            var right = context.right.Accept(expressionVisitor);
            var op = cond.GREATER_THAN() != null ? ConditionalOp.GreaterThan :
                cond.LESS_THAN() != null ? ConditionalOp.LessThan :
                cond.EQUAL_TO() != null ? ConditionalOp.EqualTo :
                cond.NOT_EQUAL_TO() != null ? ConditionalOp.NotEqualTo :
                cond.GREATER_THAN_OR_EQUAL() != null ? ConditionalOp.GreaterThanOrEqualTo :
                cond.LESS_THAN_OR_EQUAL() != null ? ConditionalOp.LessThanOrEqualTo :
                cond.AND() != null ? ConditionalOp.And :
                cond.OR() != null ? ConditionalOp.Or :
                throw new InvalidOperationException($"Found invalid conditional operator: {cond.GetText()}");
            return new ConditionalAst(left, op, right);
        }
    }
}