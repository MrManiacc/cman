using System;
using Antlr4.Runtime;
using CMan.Lang.Type;

namespace CMan.Lang.Scope {
    /// <summary>
    /// Represents anything that can store data and other symbols/scopes.
    /// This would be used for things such as structs, modules, classes, etc.
    /// </summary>
    public abstract class AggregateSymbol : ScopedSymbol, IMemberSymbol, IType, IParserDefined {
        protected AggregateSymbol(string name) : base(name) { }

        #region Overrides of BaseScope
        /// <summary>
        /// Define a symbol in this scope throws exception if the symbol is already declared within this scope.
        /// The definition alters the state of the symbol is two ways:
        /// 
        /// 1. Sets the insertion order number of the symbol
        /// 2. Sets the symbols scope to this current scope.
        ///
        /// The order that symbols are defined must be preserved.  
        /// </summary>
        /// <param name="symbol"></param>
        public override void Define(ISymbol symbol) {
            if (!(symbol is IMemberSymbol))
                throw new InvalidOperationException($"Symbol is {symbol.GetType().FullName} not MemberSymbol");
            base.Define(symbol);
        }
        #endregion

        #region Implementation of IType
        /// <summary>
        /// For now this return the fully qualified named for the name.
        /// </summary>
        public string GetTypeName() => FullyQualifiedName;
        #endregion

        #region Implementation of IMemberSymbol
        /// <summary>
        /// The index that this is stored inside the enclosing aggregate.
        /// </summary>
        /// <returns>Aggregate slot</returns>
        public int GetSlotNumber() =>
            -1; // class definitions do not yield either field or method slots; they are just nested
        #endregion

        #region Implementation of IParserDefined
        /// <summary>
        /// Assign a definition node to this function for help during compilation.
        /// </summary>
        public ParserRuleContext DefinitionNode { get; set; }
        #endregion
    }
}