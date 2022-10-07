using CMan.Lang.Expression.Parameter;
using CMan.Lang.Type;

namespace CMan.Lang.Function {
    public class FunctionSignatureAst {
        public string Name { get; }

        public IType ReturnType { get; }

        public ParameterListAst ParameterList { get; }
        
        public FunctionSignatureAst(string name, IType returnType, ParameterListAst parameterList) {
            Name = name;
            ReturnType = returnType;
            ParameterList = parameterList;
        }


        public override string ToString() {
            return $"Name: {Name}, ReturnType: {ReturnType}, ParameterList: {ParameterList}";
        }
    }
}