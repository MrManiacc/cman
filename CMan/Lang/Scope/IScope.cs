using System.Collections.Generic;

namespace CMan.Lang.Scope {
    public interface IScope {
        /// <summary>
        /// Scopes like a function or a class declaration will have a name to identify the
        /// scope. Other scopes like the global scope or a local scope can have static names
        /// such as "Global" or "Local" respectively 
        /// </summary>
        string GetName();

        /// <summary>
        /// The scope in which this scope is defined in. If this is the root scoop, then the parent would
        /// return null instead of actual parent value.
        /// </summary>
        /// <returns>The enclosing scope or null</returns>
        IScope GetParentScope();

        /// <summary>
        /// Sets the enclosing scope of this current scope to the passed parent value
        /// </summary>
        /// <param name="parent">The enclosing scope of this scope</param>
        void SetParentScope(IScope parent);

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
        void Define(ISymbol symbol);

        /// <summary>
        /// Look up a symbol first in this current scope with the defined name. If the symbol is not found
        /// in this current scope, then we recursively work out way up the enclosing scope tree until we
        /// either find the symbol or can' recurse any more.
        /// </summary>
        /// <param name="name">The name of the symbol</param>
        /// <returns>The symbol or null</returns>
        ISymbol Resolve(string name);

        /// <summary>
        /// Look up a symbol in this current scope with the defined name, does NOT recurse up
        /// the enclosing scope tree. Only Symbols directly in this scope will be resolved through this method.
        /// </summary>
        /// <param name="name">The name of the symbol to be resolved</param>
        /// <returns>The resolved symbol or null</returns>
        ISymbol GetSymbol(string name);

        /// <summary>
        /// Add a nested local scope to this cope. This method is meant to be used like the define method,
        /// but for only non SymbolWithScope, symbols. For example a FunctionSymbol will add a LocalScope for it's block
        /// statement. The for loop would also nest 
        /// </summary>
        /// <param name="scope">The</param>
        void Nest(IScope scope);
        /// <summary>
        /// This will get the outermost scope starting from the current scope by recursively
        /// working up the tree until we can't move to the parent scope anymore.
        /// </summary>
        /// <returns></returns>
        IScope GetRootScope();

            /// <summary>
        /// This will iterate through all of our nested symbols and retrieve any that extend Scoped Symbol
        /// </summary>
        /// <returns>All scoped symbols</returns>
        List<IScope> GetNestedScopedSymbols();

        /// <summary>
        /// This gets all nested scopes including scoped symbols and regular scopes, which would define things
        /// such as a local scope block.
        /// </summary>
        /// <returns>All nested scopes</returns>
        List<IScope> GetNestedScopes();

        /// <summary>
        /// This will return a list of scopes that defines the path to the root scope.
        /// The root scope will be last element while the first element will be this current scope
        /// </summary>
        /// <returns>The path to the root scope</returns>
        List<IScope> GetPathToRootScope();

        /// <summary>
        /// Gets the list of symbols that are in the current scope.
        /// </summary>
        /// <returns></returns>
        List<ISymbol> GetSymbols();

        /// <summary>
        /// This will get all of the symbols, including child scope's symbols as well as our
        /// own scopes symbols.
        /// </summary>
        /// <returns>All downward-accessible symbols</returns>
        List<ISymbol> GetAllSymbols();
    }
}