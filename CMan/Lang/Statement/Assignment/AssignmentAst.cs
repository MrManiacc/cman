using CMan.Lang.Expression;

namespace CMan.Lang.Statement.Assignment {
    public class AssignmentAst : IStatement {
        public IExpression Target { get; }
        public IExpression Value { get; }

        public AssignmentAst(IExpression target, IExpression value) {
            Target = target;
            Value = value;
        }

        public override string ToString() {
            return $"{nameof(Target)}: {Target}, {nameof(Value)}: {Value}";
        }
    }
}