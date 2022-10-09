using System.Collections.Generic;
using System.Linq;
using CMan.Lang.Name;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Module {
    public class ModuleVisitor : CmanParserScopedVisitor<ModuleAst> {
        public ModuleVisitor(IScope scope) : base(scope) { }

        public override ModuleAst VisitModule(CmanParser.ModuleContext context) {
            var imports = context.moduleImports() != null
                ? context.moduleImports().Accept(ModuleImportVisitor.Instance)
                : new ModuleImportAst(new List<Identifier>());
            var name = context.moduleSignature().Accept(IdentifierVisitor.Instance);
            var module = new ModuleAst(imports, name);
            module.SetScope(Scope);
            Scope.Define(module);
            var visitor = new AtomVisitor(module);
            module.Atoms = context.moduleBody().atom().Select(i => i.Accept(visitor)).ToList();
            return module;
        }
    }
}