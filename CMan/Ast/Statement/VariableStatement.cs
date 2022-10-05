using CMan.Ast.Expression;
using CMan.Ast.Type;

namespace CMan.Ast.Statement
{
    public class VariableDeclaration : IStatement
    {
        public string Name { get; }

        public IType Type { get; }

        public IExpression Expression { get; }

        public VariableDeclaration(string name, IType type, IExpression expression)
        {
            Name = name;
            Type = type;
            Expression = expression;
        }

        public VariableDeclaration(string name, IType type) : this(name, type, new NoOpExpression())
        {
        }

        public VariableDeclaration(string name, IExpression expression) : this(name, expression.GetType(), expression)
        {
        }
    }
}