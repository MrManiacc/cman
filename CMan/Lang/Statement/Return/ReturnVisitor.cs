using CMan.Lang.Expression;

namespace CMan.Lang.Statement.Return {
    public class ReturnVisitor : CmanParserBaseVisitor<ReturnAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public ReturnVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }
        
        public override ReturnAst VisitReturnStmt(CmanParser.ReturnStmtContext context) =>
            new ReturnAst(context.@return().expression().Accept(expressionVisitor));
    }
}