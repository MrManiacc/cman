using CMan.Lang.Expression.Parameter;
using CMan.Lang.Statement;
using CMan.Lang.Type;

namespace CMan.Lang.Function {
    public class FunctionAst {
        public FunctionSignatureAst Signature { get; }
        public IStatement Body { get; }

        public FunctionAst(FunctionSignatureAst signature, IStatement body) {
            Signature = signature;
            Body = body;
        }

        public override string ToString() {
            return $"Signature: {Signature}, Body: {Body}";
        }
    }
}