using System.Collections.Generic;
using CMan.Ast.Expression;

namespace CMan.Ast.Statement
{
    public class AssignmentStatement : IStatement
    {
        public IExpression Target { get; }
        public IExpression Value { get; }
        
        public AssignmentStatement(IExpression target, IExpression value)
        {
            Target = target;
            Value = value;
        }
    }
}