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
        }

        public IType GetType() {
            throw new System.NotImplementedException();
        }
    }
}