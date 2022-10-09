using System.Collections.Generic;
using CMan.Lang.Program.Atom;
using CMan.Lang.Scope;

namespace CMan.Lang.Program {
    public class ProgramAst : BaseScope {
        public List<AtomAst> Atoms { get; internal set; }

        public ProgramAst(List<AtomAst> atoms) =>
            Atoms = atoms;

        public ProgramAst() {}
        public override string ToString() => $"Atoms: {string.Join(", ", Atoms)}";

        
        #region Overrides of BaseScope
        public override string GetName() => "program";
        #endregion
    }
}