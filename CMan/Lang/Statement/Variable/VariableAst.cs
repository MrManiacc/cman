using CMan.Lang.Expression;
using CMan.Lang.Expression.NoOp;
using CMan.Lang.Type;

namespace CMan.Lang.Statement.Variable {
    public class VariableAst : IStatement {
        public string Name { get; }

        public IType Type { get; }

        public IExpression Expression { get; }

        public VariableAst(string name, IType type, IExpression expression) {
            Name = name;
            Type = type;
            Expression = expression;
        }

        public VariableAst(string name, IType type) : this(name, type, new NoOpExpression()) { }

        public VariableAst(string name, IExpression expression) :
            this(name, expression.GetType(), expression) { }
        

        public override string ToString() {
            return $"{nameof(Name)}: {Name}, {nameof(Type)}: {Type}, {nameof(Expression)}: {Expression}";
        }
    }
}