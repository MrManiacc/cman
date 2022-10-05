using System.Collections.Generic;

namespace CMan.Ast.Statement
{
    public class BlockStatement : IStatement
    {
        public List<IStatement> Statements { get; }
        
        public BlockStatement(List<IStatement> statements)
        {
            Statements = statements;
        }

    }
}