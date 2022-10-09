using CMan.Lang.Program.Atom.Statement.Assignment;
using CMan.Lang.Program.Atom.Statement.Block;
using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Program.Atom.Statement.For;
using CMan.Lang.Program.Atom.Statement.If;
using CMan.Lang.Program.Atom.Statement.Return;
using CMan.Lang.Program.Atom.Statement.Variable;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Statement {
    public class StatementVisitor : CmanParserScopedVisitor<IStatement> {
        private readonly ExpressionVisitor expressionVisitor;
        private readonly AssignmentVisitor assignmentVisitor;
        private readonly VariableVisitor variableVisitor;
        private readonly BlockVisitor blockVisitor;
        private readonly ReturnVisitor returnVisitor;
        private readonly IfVisitor ifVisitor;
        private readonly ForVisitor forVisitor;

        public StatementVisitor(IScope scope) : base(scope) {
            expressionVisitor = new ExpressionVisitor(scope);
            assignmentVisitor = new AssignmentVisitor(expressionVisitor);
            variableVisitor = new VariableVisitor(scope, expressionVisitor);
            blockVisitor = new BlockVisitor(this);
            returnVisitor = new ReturnVisitor(expressionVisitor);
            ifVisitor = new IfVisitor(this);
            forVisitor = new ForVisitor(this);
        }

        #region Overrides of CmanParserBaseVisitor<IStatement>
        public override IStatement VisitFunctionCallExpr(CmanParser.FunctionCallExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitVarRefExpr(CmanParser.VarRefExprContext context) =>
            context.Accept(expressionVisitor);
        #endregion

        public override IStatement VisitUnaryExpr(CmanParser.UnaryExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitConditionalExpr(CmanParser.ConditionalExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitValueListExpr(CmanParser.ValueListExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitExpressionList(CmanParser.ExpressionListContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitLiteralExpr(CmanParser.LiteralExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitBinaryExpr(CmanParser.BinaryExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitIfStmt(CmanParser.IfStmtContext context) => context.Accept(ifVisitor);

        public override IStatement VisitAssignmentStmt(CmanParser.AssignmentStmtContext context) =>
            context.Accept(assignmentVisitor);

        public override IStatement VisitVarDeclarationStmt(CmanParser.VarDeclarationStmtContext context) =>
            context.Accept(variableVisitor);

        public override IStatement VisitVariable(CmanParser.VariableContext context) =>
            context.Accept(variableVisitor);

        public override IStatement VisitBlockStmt(CmanParser.BlockStmtContext context) =>
            context.Accept(blockVisitor);

        public override IStatement VisitReturnStmt(CmanParser.ReturnStmtContext context) =>
            context.Accept(returnVisitor);

        public override IStatement VisitForStmt(CmanParser.ForStmtContext context) =>
            context.Accept(forVisitor);
    }
}