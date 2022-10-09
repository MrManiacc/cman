using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Program.Atom.Statement.Variable;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Statement.For {
    public class ForAst : BaseScope, IStatement {
        public VariableAst Variable { get;  set;}
        public IExpression Condition { get; set; }
        public IExpression Accumulator { get;  set; }
        public IStatement Body { get; set; }

  

        #region Overrides of BaseScope
        public override string GetName() => "for";
        #endregion
    }
}