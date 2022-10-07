namespace CMan.Lang.Scope {
    public interface ISymbol {
        /// <summary>
        /// Every symbol should be able to be identified at compile time. We are able
        /// to reference symbols via their name/scope.
        /// </summary>
        /// <returns>The identifier for this symbol</returns>
        string GetName();

        /// <summary>
        /// The scope that this symbol resides in.
        /// </summary>
        /// <returns>Enclosing scope</returns>
        IScope GetScope();

        /// <summary>
        /// Set the current enclosing scope of this symbol
        /// </summary>
        void SetScope(IScope scope);

        /// <summary>
        /// The insertion order of this symbol. This may be used during bytecode generation
        /// </summary>
        /// <returns></returns>
        int GetInsertSlot();

        /// <summary>
        /// Sets the position that the symbol should be inserted into the scope.
        /// </summary>
        /// <param name="slot">the slot of the symbol</param>
        void SetInsertSlot(int slot);
        
    }
}