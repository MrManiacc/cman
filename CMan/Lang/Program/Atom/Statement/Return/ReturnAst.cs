using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Program.Atom.Statement.Expression.NoOp;

namespace CMan.Lang.Program.Atom.Statement.Return {
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