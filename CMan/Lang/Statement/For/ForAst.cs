using CMan.Lang.Expression;
using CMan.Lang.Statement.Variable;

namespace CMan.Lang.Statement.For {
    public class ForAst : IStatement {
        public VariableAst Variable { get; }
        public IExpression Condition { get; }
        public IExpression Accumulator { get; }
        public IStatement Body { get; }

        public ForAst(VariableAst variable, IExpression condition, IExpression accumulator, IStatement body) {
            Variable = variable;
            Condition = condition;
            Accumulator = accumulator;
            Body = body;
        }
    }
}