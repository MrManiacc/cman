using CMan.Lang.Program.Atom.Statement.Expression.Binary;
using CMan.Lang.Program.Atom.Statement.Expression.Call;
using CMan.Lang.Program.Atom.Statement.Expression.Conditional;
using CMan.Lang.Program.Atom.Statement.Expression.Literal;
using CMan.Lang.Program.Atom.Statement.Expression.Unary;
using CMan.Lang.Program.Atom.Statement.Expression.ValueList;
using CMan.Lang.Program.Atom.Statement.Variable;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Statement.Expression {
    public class ExpressionVisitor : CmanParserScopedVisitor<IExpression> {
        private readonly LiteralVisitor literalVisitor;
        private readonly ValueListVisitor valueListVisitor;
        private readonly BinaryVisitor binaryVisitor;
        private readonly ConditionalVisitor conditionalVisitor;
        private readonly UnaryVisitor unaryVisitor;
        private readonly VariableReferenceVisitor variableReferenceVisitor;
        private readonly CallVisitor callVisitor;

        public ExpressionVisitor(IScope scope) : base(scope) {
            literalVisitor = new LiteralVisitor();
            valueListVisitor = new ValueListVisitor(scope);
            binaryVisitor = new BinaryVisitor(this);
            conditionalVisitor = new ConditionalVisitor(this);
            unaryVisitor = new UnaryVisitor(this);
            variableReferenceVisitor = new VariableReferenceVisitor(scope);
            callVisitor = new CallVisitor(Scope);
        }

        #region Overrides of CmanParserBaseVisitor<IExpression>
        public override IExpression VisitFunctionCallExpr(CmanParser.FunctionCallExprContext context) =>
            context.Accept(callVisitor);

        public override IExpression VisitVarRefExpr(CmanParser.VarRefExprContext context) =>
            context.Accept(variableReferenceVisitor);

        public override IExpression VisitUnaryExpr(CmanParser.UnaryExprContext context) =>
            context.Accept(unaryVisitor);

        public override IExpression VisitConditionalExpr(CmanParser.ConditionalExprContext context) =>
            context.Accept(conditionalVisitor);

        public override IExpression VisitBinaryExpr(CmanParser.BinaryExprContext context) =>
            context.Accept(binaryVisitor);

        public override IExpression VisitLiteralExpr(CmanParser.LiteralExprContext context) =>
            context.Accept(literalVisitor);

        public override IExpression VisitValueListExpr(CmanParser.ValueListExprContext context) =>
            context.Accept(valueListVisitor);

        public override IExpression VisitExpressionList(CmanParser.ExpressionListContext context) =>
            context.Accept(valueListVisitor);
        #endregion
    }
}