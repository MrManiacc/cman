using CMan.Lang.Expression;
using CMan.Lang.Statement.Assignment;
using CMan.Lang.Statement.Block;
using CMan.Lang.Statement.Return;
using CMan.Lang.Statement.Variable;

namespace CMan.Lang.Statement {
    public class StatementVisitor : CmanParserBaseVisitor<IStatement> {
        private readonly ExpressionVisitor expressionVisitor;
        private readonly AssignmentVisitor assignmentVisitor;
        private readonly VariableVisitor variableVisitor;
        private readonly BlockVisitor blockVisitor;
        private readonly ReturnVisitor returnVisitor;

        public StatementVisitor() {
            expressionVisitor = new ExpressionVisitor();
            assignmentVisitor = new AssignmentVisitor(expressionVisitor);
            variableVisitor = new VariableVisitor(expressionVisitor);
            blockVisitor = new BlockVisitor(this);
            returnVisitor = new ReturnVisitor(expressionVisitor);
        }

        public override IStatement VisitLiteralExpr(CmanParser.LiteralExprContext context) =>
            context.Accept(expressionVisitor);

        public override IStatement VisitAssignmentStmt(CmanParser.AssignmentStmtContext context) =>
            context.Accept(assignmentVisitor);

        public override IStatement VisitVarDeclarationStmt(CmanParser.VarDeclartionStmtContext context) =>
            context.Accept(variableVisitor);

        public override IStatement VisitBlockStmt(CmanParser.BlockStmtContext context) =>
            context.Accept(blockVisitor);

        public override IStatement VisitReturnStmt(CmanParser.ReturnStmtContext context) =>
            context.Accept(returnVisitor);
    }
}