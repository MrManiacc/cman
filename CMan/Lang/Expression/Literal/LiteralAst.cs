using CMan.Lang.Type;

namespace CMan.Lang.Expression.Literal {
    /// <summary>
    /// Represents a literal value, could be an Whole Number,Decimal Number, String, Boolean, Char
    /// </summary>
    public class LiteralAst : IExpression {
        public string Value { get; }
        private readonly IType type;

        public LiteralAst(IType type, string value) {
            this.type = type;
            Value = value;
        }

        public new IType GetType() {
            return type;
        }

        public override string ToString() {
            return $"{nameof(type)}: {type}, {nameof(Value)}: {Value}";
        }
    }
}