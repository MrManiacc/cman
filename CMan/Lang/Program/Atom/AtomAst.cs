using CMan.Lang.Program.Atom.Function;
using CMan.Lang.Program.Atom.Module;
using CMan.Lang.Program.Atom.Statement;

namespace CMan.Lang.Program.Atom {
    public class AtomAst {
        public ModuleAst Module { get; }
        public FunctionAst Function { get; }
        public IStatement Statement { get; }

        public AtomAst(ModuleAst module) {
            Module = module;
        }

        public AtomAst(FunctionAst function) {
            Function = function;
        }

        public AtomAst(IStatement statement) {
            Statement = statement;
        }

        public override string ToString() {
            if (Module != null) return $"Module: {Module}";
            if (Function != null) return $"Function: {Function}";
            if (Statement != null) return $"Statement: {Statement}";
            return "Undefined";
        }
    }
}