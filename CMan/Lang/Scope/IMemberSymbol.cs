namespace CMan.Lang.Scope {
    public interface IMemberSymbol : ISymbol {
        /// <summary>
        /// The index that this is stored inside the enclosing aggregate.
        /// </summary>
        /// <returns>Aggregate slot</returns>
        int GetSlotNumber();
    }
}