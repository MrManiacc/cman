using CMan.Lang.Type;

namespace CMan.Lang.Program.Atom.Statement.Expression.Parameter {
    public class ParameterAst {
        public string Name { get; }
        public IType Type { get; }

        public ParameterAst(IType type, string name) {
            Type = type;
            Name = name;
        }

        public override string ToString() {
            return $"Name: {Name}, Type: {Type}";
        }
    }
}