using CMan.Lang.Type;

namespace CMan.Lang.Program.Atom.Statement.Expression.NoOp {
    public class NoOpExpression : IExpression {
        private readonly IType type;

        public NoOpExpression(IType type) {
            this.type = type;
        }
        
        public NoOpExpression() : this(SystemType.Void) { }

        public new IType GetType() {
            return type;
        }

        public override string ToString() {
            return $"{nameof(type)}: {type}";
        }
    }
}