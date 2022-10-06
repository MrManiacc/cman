using CMan.Lang.Expression;
using CMan.Lang.Expression.NoOp;

namespace CMan.Lang.Statement.Return {
    public class ReturnAst : IStatement {
        public IExpression Expression { get; }

        public ReturnAst(IExpression expression) {
            Expression = expression;
        }

        public ReturnAst() : this(new NoOpExpression()) { }

        public override string ToString() {
            return $"{nameof(Expression)}: {Expression}";
        }
    }
}