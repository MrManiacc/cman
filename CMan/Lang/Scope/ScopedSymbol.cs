namespace CMan.Lang.Scope {
    public abstract class ScopedSymbol : BaseScope, ISymbol, IScope {
        private readonly string name;
        private int index;

        protected ScopedSymbol(string name) {
            this.name = name;
        }

        public override string GetName() => name;

        public IScope GetScope() => GetParentScope();

        public void SetScope(IScope scope) => SetParentScope(scope);
        
        public int GetInsertSlot() => index;
    
        public void SetInsertSlot(int slot) => index = slot;
                
        protected bool Equals(ScopedSymbol other) {
            return name == other.name;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType().IsSubclassOf(typeof(ISymbol))
                   && Equals(((ScopedSymbol)obj).name, name);
        }

        public override int GetHashCode() {
            return (name != null ? name.GetHashCode() : 0);
        }
    }
}