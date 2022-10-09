using System.Linq;

namespace CMan.Lang.Program.Atom.Statement.Expression.Parameter {
    public class ParameterListVisitor : CmanParserBaseVisitor<ParameterListAst> {
        private readonly ParameterVisitor parameterVisitor = new ParameterVisitor();
        
        public override ParameterListAst VisitParameterList(CmanParser.ParameterListContext context)
            => new ParameterListAst(context.parameter().Select(p => p.Accept(parameterVisitor)).ToList());
    }
}