using CMan.Lang.Type;

namespace CMan.Lang.Scope {
    public  class VariableSymbol : BaseSymbol {
        public VariableSymbol(string name, IType type) : base(name) {
            Type = type;
        }
    }
}