using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMan.Lang.Name;
using CMan.Lang.Util;

namespace CMan.Lang.Scope {
    /// <summary>
    /// The base scope class
    /// </summary>
    /// <seealso cref="IScope"/>
    public abstract class BaseScope : IScope {
        //All of our named symbols (functions, variables, etc.)
        /// <summary>
        /// The symbol
        /// </summary>
        private readonly Dictionary<string, ISymbol> symbols = new Dictionary<string, ISymbol>();

        //Represents all nested scopes that are not also symbols, like a local-scope for a block.
        /// <summary>
        /// The scope
        /// </summary>
        private readonly List<IScope> nestedScopes = new List<IScope>();

        //The parent of this scope
        /// <summary>
        /// The enclosing scope
        /// </summary>
        protected IScope EnclosingScope;

        /// <summary>
        /// Gets the name
        /// </summary>
        /// <returns>The string</returns>
        public abstract string GetName();

        /// <summary>
        /// Gets the parent scope
        /// </summary>
        /// <returns>The scope</returns>
        public IScope GetParentScope() => EnclosingScope;

        /// <summary>
        /// Sets the parent scope using the specified parent
        /// </summary>
        /// <param name="parent">The parent</param>
        public void SetParentScope(IScope parent) => EnclosingScope = parent;

        /// <summary>
        /// Defines the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <exception cref="InvalidOperationException">Duplicate symbol named: {symbol.GetName()}</exception>
        public virtual void Define(ISymbol symbol) {
            if (symbols.ContainsKey(symbol.GetName()))
                throw new InvalidOperationException($"Duplicate symbol named: {symbol.GetName()}");

            symbol.SetScope(this);
            symbol.SetInsertSlot(symbols.Count);
            symbols[symbol.GetName()] = symbol;
        }

        /// <summary>
        /// Resolves the name
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>The symbol</returns>
        public ISymbol Resolve(string name) =>
            symbols.ContainsKey(name) ? symbols[name] :
            (name == GetName() && this is ISymbol) ? this as ISymbol : GetParentScope().Resolve(name);

        /// <summary>
        /// Gets the root scope
        /// </summary>
        /// <returns>The scope</returns>
        public IScope GetRootScope() {
            IScope scope = this;
            while (scope.GetParentScope() != null) scope = scope.GetParentScope();
            return scope;
        }

        /// <summary>
        /// Gets the path to root scope
        /// </summary>
        /// <returns>The scopes</returns>
        public List<IScope> GetPathToRootScope() {
            var scopes = new List<IScope>();
            IScope scope = this;
            while (scope != null) {
                scopes.Add(scope);
                scope = scope.GetParentScope();
            }

            return scopes;
        }

        /// <summary>
        /// Look up a symbol in this current scope with the defined name, does NOT recurse up
        /// the enclosing scope tree. Only Symbols directly in this scope will be resolved through this method.
        /// </summary>
        /// <param name="name">The name of the symbol to be resolved</param>
        /// <returns>The resolved symbol or null</returns>
        public ISymbol GetSymbol(string name) => symbols[name];

        /// <summary>
        /// Add a nested local scope to this cope. This method is meant to be used like the define method,
        /// but for only non SymbolWithScope, symbols. For example a FunctionSymbol will add a LocalScope for it's block
        /// statement. The for loop would also nest 
        /// </summary>
        /// <param name="scope">The</param>
        /// <exception cref="InvalidOperationException">ScopedSymbol named '{scope.GetName()}' must be added via define()</exception>
        public void Nest(IScope scope) {
            if (scope is ScopedSymbol)
                throw new InvalidOperationException(
                    $"ScopedSymbol named '{scope.GetName()}' must be added via define()");
            nestedScopes.Add(scope);
        }

        /// <summary>
        /// This will iterate through all of our nested symbols and retrieve any that extend Scoped Symbol
        /// </summary>
        /// <returns>All scoped symbols</returns>
        public List<IScope> GetNestedScopedSymbols() =>
            GetSymbols().InstancesOf<IScope>();

        /// <summary>
        /// Gets the nested scopes
        /// </summary>
        /// <returns>A list of i scope</returns>
        public List<IScope> GetNestedScopes() =>
            GetNestedScopedSymbols()
                .AddAll(nestedScopes);

        /// <summary>
        /// Gets the symbols
        /// </summary>
        /// <returns>A list of i symbol</returns>
        public List<ISymbol> GetSymbols() => symbols.Values.ToList();

        /// <summary>
        /// Gets the all symbols
        /// </summary>
        /// <returns>The syms</returns>
        public List<ISymbol> GetAllSymbols() {
            var syms = new List<ISymbol>();
            syms.AddRange(GetSymbols());
            foreach (var sym in symbols.Values) {
                if (sym is IScope scope) {
                    syms.AddRange(scope.GetAllSymbols());
                }
            }

            return syms;
        }

        #region Equality members
        protected bool Equals(BaseScope other) {
            return Equals(symbols, other.symbols) && Equals(nestedScopes, other.nestedScopes) &&
                   Equals(EnclosingScope, other.EnclosingScope);
        }

        public override bool Equals(object obj) {
            return ReferenceEquals(this, obj) || obj is BaseScope other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (symbols != null ? symbols.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (nestedScopes != null ? nestedScopes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EnclosingScope != null ? EnclosingScope.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(BaseScope left, BaseScope right) {
            return Equals(left, right);
        }

        public static bool operator !=(BaseScope left, BaseScope right) {
            return !Equals(left, right);
        }
        #endregion
    }
}