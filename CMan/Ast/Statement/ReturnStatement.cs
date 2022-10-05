using CMan.Ast.Expression;
using CMan.Ast.Type;

namespace CMan.Ast.Statement
{
    public class ReturnStatement : IStatement
    {
        public IExpression Expression { get; }

        public ReturnStatement(IExpression expression)
        {
            Expression = expression;
        }

        public ReturnStatement() : this(new NoOpExpression())
        {
        }
    }
}