using CMan.Lang.Type;

namespace CMan.Lang.Expression.Unary {
    public class UnaryAst : IExpression {
        public IExpression Expression { get; }
        public bool IsPrefix { get; }
        public UnaryOp Op { get; }

        public UnaryAst(IExpression expression, UnaryOp op, bool isPrefix) {
            Expression = expression;
            Op = op;
            IsPrefix = isPrefix;
        }

        public new IType GetType() =>
            Expression.GetType();

        protected bool Equals(UnaryAst other) {
            return Equals(Expression, other.Expression) && IsPrefix == other.IsPrefix && Op == other.Op;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(UnaryAst)) return false;
            return Equals((UnaryAst)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (Expression != null ? Expression.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsPrefix.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Op;
                return hashCode;
            }
        }
    }
}