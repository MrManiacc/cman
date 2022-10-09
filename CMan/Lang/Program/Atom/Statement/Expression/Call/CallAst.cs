using System;
using CMan.Lang.Program.Atom.Function;
using CMan.Lang.Program.Atom.Statement.Expression.ValueList;
using CMan.Lang.Scope;
using CMan.Lang.Type;

namespace CMan.Lang.Program.Atom.Statement.Expression.Call {
    public class CallAst : SymbolReference, IExpression {
        public FunctionAst Function { get; }

        public ValueListAst Arguments { get; }

        public CallAst(FunctionAst function, ValueListAst arguments) : base(function) {
            Function = function;
            Arguments = arguments;
        }
        #region Implementation of IExpression
        public new IType GetType() => Function.Type;
        #endregion
        
    }
}