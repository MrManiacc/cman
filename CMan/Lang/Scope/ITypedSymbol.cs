using CMan.Lang.Type;

namespace CMan.Lang.Scope {
    /// <summary>
    /// The typed symbol interface
    /// </summary>
    public interface ITypedSymbol {
        /// <summary>
        /// Gets or sets the value of the type
        /// </summary>
        IType Type { get; set; }
    }
}