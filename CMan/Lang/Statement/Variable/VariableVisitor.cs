﻿using System;
using CMan.Lang.Expression;
using CMan.Lang.Expression.NoOp;
using CMan.Lang.Type;

namespace CMan.Lang.Statement.Variable {
    internal class VariableVisitor : CmanParserBaseVisitor<VariableAst> {
        private ExpressionVisitor expressionVisitor;

        public VariableVisitor(ExpressionVisitor expressionVisitor) {
            this.expressionVisitor = expressionVisitor;
        }

        public override VariableAst VisitVarDeclarationStmt(CmanParser.VarDeclartionStmtContext context) {
            var variable = context.variable();
            var name = variable.name().GetText();
            var declaration = variable.variableDeclartion();
            var child = declaration.GetChild(0);
            IExpression expr = new NoOpExpression();
            IType type = SystemType.Null;
            switch (child) {
                case CmanParser.AssignmentContext ass:
                    expr = ass.expression().Accept(expressionVisitor);
                    type = expr.GetType();
                    break;
                case CmanParser.ExplicitTypeSignatureContext sig: {
                    type = SystemType.Get(sig.type().GetText());
                    if (type.Equals(SystemType.Null)) type = new UserType(sig.type().GetText());
                    break;
                }
                case CmanParser.ExplicitAssignmentSignatureContext exp: {
                    expr = exp.assignment().expression().Accept(expressionVisitor);
                    type = expr.GetType();
                    break;
                }
            }

            return new VariableAst(name, type, expr);
        }
    }
}