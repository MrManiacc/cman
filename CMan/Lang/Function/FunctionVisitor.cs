namespace CMan.Lang.Function {
    public class FunctionVisitor : CmanParserBaseVisitor<FunctionAst> {
        
        public override FunctionAst VisitFunction(CmanParser.FunctionContext context) {
            return base.VisitFunction(context);
        }
    }
}