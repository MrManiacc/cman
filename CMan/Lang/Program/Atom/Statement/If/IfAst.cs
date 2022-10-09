using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Program.Atom.Statement.Expression.NoOp;

namespace CMan.Lang.Program.Atom.Statement.If {
    public class IfAst : IStatement {
        public IExpression Condition { get; }
        public IStatement TrueStatement { get; }
        public IStatement FalseStatement { get; }

        public IfAst(IExpression condition, IStatement trueStatement, IStatement falseStatement) {
            Condition = condition;
            TrueStatement = trueStatement;
            FalseStatement = falseStatement;
        }

        public IfAst(IExpression condition, IStatement trueStatement) : this(condition, trueStatement,
            new NoOpExpression()) { }

        public override string ToString() {
            return $"{nameof(Condition)}: {Condition}, {nameof(TrueStatement)}: {TrueStatement}, {nameof(FalseStatement)}: {FalseStatement}";
        }
    }
}