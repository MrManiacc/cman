using System.Collections.Generic;
using CMan.Lang.Program.Atom.Statement;
using CMan.Lang.Program.Atom.Statement.Expression.Parameter;
using CMan.Lang.Scope;
using CMan.Lang.Type;

namespace CMan.Lang.Program.Atom.Function {
    public class FunctionVisitor : CmanParserScopedVisitor<FunctionAst> {
        private static readonly ParameterListVisitor parameterListVisitor = new ParameterListVisitor();

        public FunctionVisitor(IScope scope) : base(scope) { }

        public override FunctionAst VisitFunction(CmanParser.FunctionContext context) {
            var sig = context.functionSignature();
            var name = sig.functionName().GetText();
            var parameters = sig.parameterList() != null
                ? sig.parameterList().Accept(parameterListVisitor)
                : new ParameterListAst(new List<ParameterAst>());
            var returnType = sig.returnType != null ? SystemType.Get(sig.returnType.type()) : SystemType.Void;
            var funcSig = new FunctionSignatureAst(name, returnType, parameters);
            var function = new FunctionAst(funcSig);
            Scope.Define(function);
            function.SetScope(Scope);
            var body = context.statement().Accept(new StatementVisitor(function));
            function.Body = body;
            return function;
        }
    }
}