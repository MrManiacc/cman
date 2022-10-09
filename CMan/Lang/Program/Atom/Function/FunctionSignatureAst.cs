using CMan.Lang.Program.Atom.Statement.Expression.Parameter;
using CMan.Lang.Type;

namespace CMan.Lang.Program.Atom.Function {
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

        #region Equality members
        protected bool Equals(FunctionSignatureAst other) {
            return Name == other.Name && Equals(ReturnType, other.ReturnType) && Equals(ParameterList, other.ParameterList);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FunctionSignatureAst)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ParameterList != null ? ParameterList.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(FunctionSignatureAst left, FunctionSignatureAst right) {
            return Equals(left, right);
        }

        public static bool operator !=(FunctionSignatureAst left, FunctionSignatureAst right) {
            return !Equals(left, right);
        }
        #endregion
    }
}