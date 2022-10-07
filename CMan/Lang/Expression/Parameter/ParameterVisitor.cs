using CMan.Lang.Type;

namespace CMan.Lang.Expression.Parameter {
    public class ParameterVisitor : CmanParserBaseVisitor<ParameterAst> {
        public override ParameterAst VisitParameter(CmanParser.ParameterContext context) {
            var name = context.name().GetText();
            var type = SystemType.Get(context.explicitTypeSignature().type());

            return new ParameterAst(type, name);
        }
    }
}