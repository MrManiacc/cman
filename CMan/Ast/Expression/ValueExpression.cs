using CMan.Ast.Type;

namespace CMan.Ast.Expression
{
    public class ValueExpression : IExpression
    {
        public string Value { get; }
        private readonly IType _type;
    
        public ValueExpression(IType type, string value)
        {
            _type = type;
            Value = value;
        }

        public new IType GetType()
        {
            return _type;
        }
    }
}