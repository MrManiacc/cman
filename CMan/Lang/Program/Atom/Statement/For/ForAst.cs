using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Program.Atom.Statement.Variable;

namespace CMan.Lang.Program.Atom.Statement.For {
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