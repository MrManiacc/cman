using System.Linq;
using CMan.Lang.Program.Atom.Statement.Expression.NoOp;

namespace CMan.Lang.Program.Atom.Statement.Block {
    public class BlockVisitor : CmanParserBaseVisitor<BlockAst> {
        private readonly StatementVisitor statementVisitor;

        public BlockVisitor(StatementVisitor statementVisitor) {
            this.statementVisitor = statementVisitor;
        }

        public override BlockAst VisitBlockStmt(CmanParser.BlockStmtContext context) {
            var block = context.block();
            var statements = block.statement().Select(x => x.Accept(statementVisitor)).ToList();
            if (!statements.Any()) statements.Add(new NoOpExpression());
            return new BlockAst(statements);
        }
    }
}