using System;
using System.Runtime.InteropServices;
using CMan.Lang.Program.Atom.Module;
using CMan.Lang.Program.Atom.Statement.Expression;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom.Statement.Variable {
    public class VariableReferenceVisitor : CmanParserScopedVisitor<VariableReferenceAst> {
        public VariableReferenceVisitor(IScope scope) : base(scope) { }

        #region Overrides of CmanParserBaseVisitor<VariableReferenceVisitor>
        public override VariableReferenceAst VisitVarRefExpr(CmanParser.VarRefExprContext context) {
            var name = context.variableName().GetText();
            if (context.owner == null) {
                Console.WriteLine(Scope.GetPathToRootScope().JoinScopeNames());

                return Resolve(Scope, name);
            }

            var exprVisitor = new ExpressionVisitor(Scope);
            var ownerValue = context.owner.Accept(exprVisitor);
            if (ownerValue is ISymbol sym) {
                return Resolve(sym.GetScope(), name);
            }

            return base.VisitVarRefExpr(context);
        }
        #endregion

        private static VariableReferenceAst Resolve(IScope scope, string name) {
            var variable = scope.Resolve(name);
            return variable switch {
                null => throw new InvalidOperationException(
                    $"Failed to find symbol named {name} in scope {scope.GetPathToRootScope().JoinScopeNames()}"),
                VariableAst varAst => new VariableReferenceAst(varAst),
                VariableSymbol varAst => new VariableReferenceAst(varAst),
                ModuleAst modAst => new VariableReferenceAst(modAst),
                _ => throw new InvalidComObjectException(
                    $"Failed to find variable symbol with name {name}. Found type ${variable.GetType().Name} instead.")
            };
        }
    }
}