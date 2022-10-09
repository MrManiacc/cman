namespace CMan.Lang.Scope {
    public abstract class SymbolReference : ISymbol {
        protected ISymbol Symbol { get; }

        protected SymbolReference(ISymbol symbol) {
            Symbol = symbol;
        }

        #region Implementation of ISymbol
        public bool IsScope() => Symbol is IScope;
        public string GetName() => Symbol.GetName();

        public virtual IScope GetScope() => IsScope() ? Symbol as IScope : Symbol.GetScope();

        public virtual void SetScope(IScope scope) {
            if (!IsScope())
                Symbol.SetScope(scope);
        }

        public int GetInsertSlot() => Symbol.GetInsertSlot();

        public void SetInsertSlot(int slot) => Symbol.SetInsertSlot(slot);
        #endregion
    }
}