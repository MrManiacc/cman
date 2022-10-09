using System;
using CMan.Lang.Program.Atom.Statement;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Function {
    public class FunctionAst : FunctionSymbol {
        public FunctionSignatureAst Signature { get; }
        public IStatement Body { get; set; }

        public FunctionAst(FunctionSignatureAst signature, IStatement body) : base(signature.Name) {
            Signature = signature;
            Body = body;
            Type = Signature.ReturnType;
            DefineParameters(signature);
        }

        public FunctionAst(FunctionSignatureAst signature) : base(signature.Name) {
            Signature = signature;
            Type = signature.ReturnType;
            DefineParameters(signature);
        }

        private void DefineParameters(FunctionSignatureAst sig) {
            var parms = sig.ParameterList.Parameters;
            foreach (var param in parms) {
                Define(new VariableSymbol(param.Name, param.Type));
                Console.WriteLine(
                    $"Defined param as variable named {param.Name} with type {param.Type.GetTypeName()} in {FullyQualifiedName}");
            }
            
        }

        public override string ToString() {
            return $"Signature: {Signature}, Body: {Body}";
        }
    }
}