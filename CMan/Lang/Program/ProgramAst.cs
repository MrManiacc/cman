using System.Collections.Generic;
using CMan.Lang.Statement;

namespace CMan.Lang.Program {
    public class ProgramAst {
        public List<IStatement> Statements { get; }

        public ProgramAst(List<IStatement> statements) {
            Statements = statements;
        }

        public override string ToString() {
            return $"{nameof(Statements)}: {string.Join(",", Statements)}";
        }
    }
}