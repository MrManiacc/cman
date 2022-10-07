using System.Collections.Generic;
using CMan.Lang.Function;
using CMan.Lang.Statement;

namespace CMan.Lang.Program {
    public class ProgramAst {
        public List<FunctionAst> Functions { get; }
        public List<IStatement> Statements { get; }

        public ProgramAst(List<FunctionAst> functions, List<IStatement> statements) {
            Functions = functions;
            Statements = statements;
        }

        public override string ToString() {
            return $"{nameof(Functions)}: {string.Join(",", Functions)}, {nameof(Statements)}: {string.Join(",", Statements)}";
        }
    }
}