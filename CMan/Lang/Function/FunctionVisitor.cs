using CMan.Lang.Expression.Parameter;
using CMan.Lang.Statement;
using CMan.Lang.Type;

namespace CMan.Lang.Function {
    public class FunctionVisitor : CmanParserBaseVisitor<FunctionAst> {
        private static readonly ParameterListVisitor parameterListVisitor = new ParameterListVisitor();
        private readonly StatementVisitor statementVisitor;

        public FunctionVisitor(StatementVisitor statementVisitor) {
            this.statementVisitor = statementVisitor;
        }

        public override FunctionAst VisitFunction(CmanParser.FunctionContext context) {
            var sig = context.functionSignature();
            var name = sig.functionName().GetText();
            var parameters = sig.parameterList().Accept(parameterListVisitor);
            var returnType = SystemType.Get(sig.returnType.type());
            var funcSig = new FunctionSignatureAst(name, returnType, parameters);
            var body = context.statement().Accept(statementVisitor);
            return new FunctionAst(funcSig, body);
        }
    }
}