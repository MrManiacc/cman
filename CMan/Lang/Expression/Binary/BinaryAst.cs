using System;
using CMan.Lang.Type;

namespace CMan.Lang.Expression.Binary {
    public class BinaryAst : IExpression {
        public IExpression Left { get; }
        public BinaryOp Op { get; }
        public IExpression Right { get; }


        public BinaryAst(IExpression left, BinaryOp op, IExpression right) {
            Left = left;
            Right = right;
            Op = op;
            if (!Equals(Left.GetType().GetTypeName(), Right.GetType().GetTypeName()))
                throw new InvalidOperationException("Tried to do binary operation between incompatible types!");
        }

        public new IType GetType()
            => Left.GetType();
    }
}