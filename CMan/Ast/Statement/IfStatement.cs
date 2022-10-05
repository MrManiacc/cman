using CMan.Ast.Expression;

namespace CMan.Ast.Statement
{
    public class IfStatement : IStatement
    {
        public IExpression Condition { get; }
        public IStatement TrueStatement { get; }
        public IStatement FalseStatement { get; }

        public IfStatement(IExpression condition, IStatement trueStatement, IStatement falseStatement)
        {
            Condition = condition;
            TrueStatement = trueStatement;
            FalseStatement = falseStatement;
        }

        public IfStatement(IExpression condition, IStatement trueStatement) : this(condition, trueStatement,
            new NoOpExpression())
        {
        }
    }
}