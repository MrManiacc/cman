using System.Linq;
using CMan.Lang.Name;

namespace CMan.Lang.Scope {
    /// <summary>
    /// The scoped symbol class
    /// </summary>
    /// <seealso cref="BaseScope"/>
    /// <seealso cref="ISymbol"/>
    /// <seealso cref="IScope"/>
    public abstract class ScopedSymbol : BaseScope, ISymbol, IScope {
        /// <summary>
        /// The name
        /// </summary>
        private readonly string name;
        /// <summary>
        /// The index
        /// </summary>
        private int index;

        /// <summary>
        /// Return the name prefixed with the name of its enclosing scope
        /// using '.' (dot) as the scope separator. 
        /// </summary>
        public string QualifiedName => EnclosingScope.GetName() + Identifier.Separator + name;

        /// <summary>
        /// Return the fully qualified name includes all scopes from the root down
        /// to this particular symbol.
        /// </summary>
        public string FullyQualifiedName => GetPathToRootScope().JoinScopeNames();

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedSymbol"/> class
        /// </summary>
        /// <param name="name">The name</param>
        protected ScopedSymbol(string name) {
            this.name = name;
        }

        /// <summary>
        /// Describes whether this instance equals
        /// </summary>
        /// <param name="obj">The obj</param>
        /// <returns>The bool</returns>
        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType().IsSubclassOf(typeof(ISymbol))
                   && Equals(((ScopedSymbol)obj).name, name);
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>The int</returns>
        public override int GetHashCode() {
            return (name.GetHashCode());
        }

        #region Implementation of ISymbol
        /// <summary>
        /// Every symbol should be able to be identified at compile time. We are able
        /// to reference symbols via their name/scope.
        /// </summary>
        /// <returns>The identifier for this symbol</returns>
        public override string GetName() => name;

        /// <summary>
        /// The scope that this symbol resides in.
        /// </summary>
        /// <returns>Enclosing scope</returns>
        public IScope GetScope() => GetParentScope();

        /// <summary>
        /// Sets the scope using the specified scope
        /// </summary>
        /// <param name="scope">The scope</param>
        public void SetScope(IScope scope) => SetParentScope(scope);

        /// <summary>
        /// Gets the insert slot
        /// </summary>
        /// <returns>The int</returns>
        public int GetInsertSlot() => index;

        /// <summary>
        /// Sets the insert slot using the specified slot
        /// </summary>
        /// <param name="slot">The slot</param>
        public void SetInsertSlot(int slot) => index = slot;
        #endregion
    }
}