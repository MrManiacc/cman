using System.Collections.Generic;
using CMan.Lang.Name;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Module {
    public class ModuleAst : ScopedSymbol {
        public ModuleImportAst Imports { get; }
        public Identifier Name { get; }
        public List<AtomAst> Atoms { get; internal set; }

        public ModuleAst(ModuleImportAst imports, Identifier name, List<AtomAst> atoms) : base(name.Name) {
            Imports = imports;
            Name = name;
            Atoms = atoms;
        }

        public ModuleAst(ModuleImportAst imports, Identifier name) : base(name.Name) {
            Imports = imports;
            Name = name;
        }

        public override string ToString() {
            return $"Imports: {Imports}, Name: {Name}, Atoms: {string.Join(", ", Atoms)}";
        }
    }
}