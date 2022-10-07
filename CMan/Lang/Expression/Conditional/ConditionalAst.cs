using System;
using CMan.Lang.Type;

namespace CMan.Lang.Expression.Conditional {
    public class ConditionalAst : IExpression {
        private readonly IType type;
        public IExpression Left { get; }
        public ConditionalOp Op { get; }
        public IExpression Right { get; }
        

        public ConditionalAst(IExpression left, ConditionalOp op, IExpression right) {
            Left = left;
            Right = right;
            Op = op;
            if (!left.GetType().Equals(right.GetType()))
                throw new InvalidOperationException("Attempted to do conditional between different typed expressions.");
            type = left.GetType();
        }

        public new IType GetType() => type;
    }
}