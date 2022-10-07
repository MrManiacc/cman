using System.Collections.Generic;

namespace CMan.Lang.Statement.Block {
    public class BlockAst : IStatement {
        public List<IStatement> Statements { get; }

        public BlockAst(List<IStatement> statements) {
            Statements = statements;
        }

        public override string ToString() {
            return $"{nameof(Statements)}: {string.Join(", ", Statements)}";
        }
    }
}