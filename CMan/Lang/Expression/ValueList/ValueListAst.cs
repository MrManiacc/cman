using System.Collections.Generic;
using System.Linq;
using CMan.Lang.Type;

namespace CMan.Lang.Expression.ValueList {
    public class ValueListAst : IExpression {
        public List<IExpression> Values { get; }
        private readonly IType type;

        public ValueListAst(List<IExpression> values, IType type) {
            Values = values;
            this.type = type;
        }

        public ValueListAst(List<IExpression> values) : this(values, values[0].GetType()) { }
    
        public new IType GetType() => type;

        public override string ToString() {
            return $"{nameof(type)}: {type}, {nameof(Values)}: {string.Join(", ", Values)}";
        }
    }
}