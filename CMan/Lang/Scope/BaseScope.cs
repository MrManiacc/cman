using System;
using System.Collections.Generic;
using System.Linq;

namespace CMan.Lang.Scope {
    public abstract class BaseScope : IScope {
        //All of our named symbols (functions, variables, etc.)
        private readonly Dictionary<string, ISymbol> symbols = new Dictionary<string, ISymbol>();

        //Represents all nested scopes that are not also symbols, like a local-scope for a block.
        private readonly List<IScope> nestedScopes = new List<IScope>();

        //The parent of this scope
        private IScope enclosingScope;


        public abstract string GetName();

        public IScope GetParentScope() => enclosingScope;

        public void SetParentScope(IScope parent) => enclosingScope = parent;

        public void Define(ISymbol symbol) {
            if (symbols.ContainsKey(symbol.GetName()))
                throw new InvalidOperationException($"Duplicate symbol named: {symbol.GetName()}");

            symbol.SetScope(this);
            symbol.SetInsertSlot(symbols.Count);
            symbols[symbol.GetName()] = symbol;
        }

        public ISymbol Resolve(string name) =>
            symbols[name] ?? GetParentScope()?.Resolve(name);


        public IScope GetRootScope() {
            IScope scope = this;
            while (scope.GetParentScope() != null) scope = scope.GetParentScope();
            return scope;
        }

        public List<IScope> GetPathToRootScope() {
            var scopes = new List<IScope>();
            IScope scope = this;
            while (scope != null) {
                scopes.Add(scope);
                scope = scope.GetParentScope();
            }

            return scopes;
        }

        public ISymbol GetSymbol(string name) => symbols[name];

        public void Nest(IScope scope) {
            // if(scope)
        }

        public List<IScope> GetNestedScopedSymbols() {
            throw new System.NotImplementedException();
        }

        public List<IScope> GetNestedScopes() =>
            GetSymbols().FindAll(s => s is IScope).Select(s => s as IScope).ToList();


        public List<ISymbol> GetSymbols() => symbols.Values.ToList();

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
    }
}