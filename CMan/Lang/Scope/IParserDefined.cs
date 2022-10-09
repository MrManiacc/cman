using Antlr4.Runtime;

namespace CMan.Lang.Scope {
    public interface IParserDefined {
        /// <summary>
        /// Assign a definition node to this function for help during compilation.
        /// </summary>
        ParserRuleContext DefinitionNode { get; set; }
    }
}