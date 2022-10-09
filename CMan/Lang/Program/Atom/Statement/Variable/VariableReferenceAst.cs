using CMan.Lang.Program.Atom.Function;
using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Scope;
using CMan.Lang.Type;

namespace CMan.Lang.Program.Atom.Statement.Variable {
    public class VariableReferenceAst : SymbolReference, IExpression {
        public VariableReferenceAst(ISymbol variable) : base(variable) { }

        #region Implementation of IExpression
        public new IType GetType() => Symbol is ITypedSymbol typed ? typed.Type : SystemType.Void;
        #endregion
    }
}