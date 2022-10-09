using System.Linq;
using CMan.Lang.Name;

namespace CMan.Lang.Program.Atom.Module {
    public class ModuleImportVisitor : CmanParserBaseVisitor<ModuleImportAst> {
        
        public static ModuleImportVisitor Instance { get; } = new ModuleImportVisitor();

        static ModuleImportVisitor() { }
        private ModuleImportVisitor() { }
        public override ModuleImportAst VisitModuleImports(CmanParser.ModuleImportsContext context) =>
            new ModuleImportAst(context.qualifiedName().Select(i => i.Accept(IdentifierVisitor.Instance)).ToList());
    }
}