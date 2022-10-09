using Antlr4.Runtime;
using CMan.Lang.Type;

namespace CMan.Lang.Scope {
    /// <summary>
    /// The function symbol class
    /// </summary>
    /// <seealso cref="ScopedSymbol"/>
    /// <seealso cref="ITypedSymbol"/>
    /// <seealso cref="IParserDefined"/>
    public class FunctionSymbol : ScopedSymbol, ITypedSymbol, IParserDefined {
     
           
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionSymbol"/> class
        /// </summary>
        /// <param name="name">The name</param>
        public FunctionSymbol(string name) : base(name) { }
        
        #region Implementation of ITypedSymbol
        /// <summary>
        /// Gets or sets the value of the type
        /// </summary>
        public IType Type { get; set; }
        #endregion

        #region Implementation of IParserDefined
        /// <summary>
        /// Gets or sets the value of the definition node
        /// </summary>
        public ParserRuleContext DefinitionNode { get; set; }
        #endregion
     
    }
}