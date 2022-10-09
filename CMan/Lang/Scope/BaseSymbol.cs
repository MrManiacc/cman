using System;
using Antlr4.Runtime;
using CMan.Lang.Type;

namespace CMan.Lang.Scope {
    public abstract class BaseSymbol : ISymbol, IParserDefined, ITypedSymbol {
        protected readonly string Name;
        protected IScope Scope;
        protected int LexicalOrder;

        protected BaseSymbol(string name) {
            this.Name = name;
        }

        public string FullyQualifiedName {
            get {
                if (Scope == null)
                    throw new InvalidOperationException(
                        $"Attempted to retrieve qualified name from a  scope-less symbol, named: {Name}");
                return Scope.GetPathToRootScope().JoinScopeNames() + $".{Name}";
            }
        }

        #region Implementation of ISymbol
        /// <summary>
        /// Every symbol should be able to be identified at compile time. We are able
        /// to reference symbols via their name/scope.
        /// </summary>
        /// <returns>The identifier for this symbol</returns>
        public string GetName() => Name;

        /// <summary>
        /// The scope that this symbol resides in.
        /// </summary>
        /// <returns>Enclosing scope</returns>
        public IScope GetScope() => Scope;

        /// <summary>
        /// Set the current enclosing scope of this symbol
        /// </summary>
        public void SetScope(IScope scope) => Scope = scope;

        /// <summary>
        /// The insertion order of this symbol. This may be used during bytecode generation
        /// </summary>
        /// <returns></returns>
        public int GetInsertSlot() => LexicalOrder;

        /// <summary>
        /// Sets the position that the symbol should be inserted into the scope.
        /// </summary>
        /// <param name="slot">the slot of the symbol</param>
        public void SetInsertSlot(int slot) => LexicalOrder = slot;
        #endregion

        #region Implementation of IParserDefined
        /// <summary>
        /// Assign a definition node to this function for help during compilation.
        /// </summary>
        public ParserRuleContext DefinitionNode { get; set; }
        #endregion

        #region Implementation of ITypedSymbol
        public IType Type { get; set; }
        #endregion
    }
}