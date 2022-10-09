using System.Collections.Generic;
using CMan.Lang.Name;

namespace CMan.Lang.Program.Atom.Module {
    public class ModuleImportAst {
        public List<Identifier> Imports { get; }

        public ModuleImportAst(List<Identifier> imports) {
            Imports = imports;
        }

        public override string ToString() {
            return $"Imports: {string.Join(", ", Imports)}";
        }
    }
}