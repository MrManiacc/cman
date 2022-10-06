﻿using CMan.Lang.Expression;

namespace CMan.Lang.Statement.Assignment {
    public class AssignmentVisitor : CmanParserBaseVisitor<AssignmentAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public AssignmentVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }
        
        public override AssignmentAst VisitAssignmentStmt(CmanParser.AssignmentStmtContext context) {
            var owner = context.expression().Accept(expressionVisitor);
            var assignment = context.assignment().expression().Accept(expressionVisitor);
            return new AssignmentAst(owner, assignment);
        }
    }
}