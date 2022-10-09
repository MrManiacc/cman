using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Program.Atom.Statement.Expression.NoOp;
using CMan.Lang.Scope;
using CMan.Lang.Type;

namespace CMan.Lang.Program.Atom.Statement.Variable {
    internal class VariableVisitor : CmanParserScopedVisitor<VariableAst> {
        private readonly ExpressionVisitor expressionVisitor;

        public VariableVisitor(IScope scope, ExpressionVisitor expressionVisitor) : base(scope) {
            this.expressionVisitor = expressionVisitor;
        }

        public override VariableAst VisitVariable(CmanParser.VariableContext context) {
            var name = context.name().GetText();
            var declaration = context.variableDeclartion();
            var child = declaration.GetChild(0);
            IExpression expr = new NoOpExpression();
            IType type = SystemType.Null;
            switch (child) {
                case CmanParser.AssignmentContext ass:
                    expr = ass.expression().Accept(expressionVisitor);
                    type = expr.GetType();
                    break;
                case CmanParser.ExplicitTypeSignatureContext sig: {
                    type = SystemType.Get(sig.type());
                    break;
                }
                case CmanParser.ExplicitAssignmentSignatureContext exp: {
                    expr = exp.assignment().expression().Accept(expressionVisitor);
                    type = expr.GetType();
                    break;
                }
            }

            var variable = new VariableAst(name, type, expr) {
                DefinitionNode = context
            };
            variable.SetScope(Scope);
            Scope.Define(variable);
            return variable;
        }

        public override VariableAst VisitVarDeclarationStmt(CmanParser.VarDeclarationStmtContext context) =>
            VisitVariable(context.variable());
    }
}