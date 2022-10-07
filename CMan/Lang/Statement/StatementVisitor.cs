using CMan.Lang.Expression;
using CMan.Lang.Statement.Assignment;
using CMan.Lang.Statement.Block;
using CMan.Lang.Statement.For;
using CMan.Lang.Statement.If;
using CMan.Lang.Statement.Return;
using CMan.Lang.Statement.Variable;

namespace CMan.Lang.Statement {
    public class StatementVisitor : CmanParserBaseVisitor<IStatement> {
        private readonly ExpressionVisitor expressionVisitor;
        private readonly AssignmentVisitor assignmentVisitor;
        private readonly VariableVisitor variableVisitor;
        private readonly BlockVisitor blockVisitor;
        private readonly ReturnVisitor returnVisitor;
        private readonly IfVisitor ifVisitor;
        private readonly ForVisitor forVisitor;

        public StatementVisitor() {
            expressionVisitor = new ExpressionVisitor();
            assignmentVisitor = new AssignmentVisitor(expressionVisitor);
            variableVisitor = new VariableVisitor(expressionVisitor);
            blockVisitor = new BlockVisitor(this);
            returnVisitor = new ReturnVisitor(expressionVisitor);
            ifVisitor = new IfVisitor(this);
            forVisitor = new ForVisitor(this);
        }

        public override IStatement VisitUnaryExpr(CmanParser.UnaryExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitConditionalExpr(CmanParser.ConditionalExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitValueListExpr(CmanParser.ValueListExprContext context) =>
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