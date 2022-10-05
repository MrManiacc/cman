using CMan.Ast.Type;

namespace CMan.Ast.Expression
{
    public class NoOpExpression : IExpression
    {
        public new IType GetType()
        {
            return SystemType.Null;
        }

    }
}