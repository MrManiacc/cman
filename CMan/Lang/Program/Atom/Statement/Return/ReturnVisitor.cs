using CMan.Lang.Program.Atom.Statement.Expression;

namespace CMan.Lang.Program.Atom.Statement.Return {
    public class ReturnVisitor : CmanParserBaseVisitor<ReturnAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public ReturnVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }
        
        public override ReturnAst VisitReturnStmt(CmanParser.ReturnStmtContext context) =>
            new ReturnAst(context.@return().expression().Accept(expressionVisitor));
    }
}