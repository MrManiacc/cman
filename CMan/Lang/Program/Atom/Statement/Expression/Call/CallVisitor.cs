using System.Collections.Generic;
using System.Linq;
using CMan.Lang.Program.Atom.Function;
using CMan.Lang.Program.Atom.Statement.Expression.ValueList;
using CMan.Lang.Scope;
using CMan.Lang.Type;

namespace CMan.Lang.Program.Atom.Statement.Expression.Call {
    public class CallVisitor : CmanParserScopedVisitor<CallAst> {
        public CallVisitor(IScope scope) : base(scope) { }

        #region Overrides of CmanParserBaseVisitor<CallAst>
        public override CallAst VisitFunctionCallExpr(CmanParser.FunctionCallExprContext context) {
            var owner = context.owner;
            var name = context.functionName().GetText();
            var valueListVisitor = new ValueListVisitor(Scope);
            var arguments = new ValueListAst(new List<IExpression>(), SystemType.Void);
            var types = new List<IType>();
            if (context.call().expressionList() != null) {
                arguments = context.call().expressionList().Accept(valueListVisitor);
                types = arguments.Values.Select(val => val.GetType()).ToList();
            }

            if (owner == null) {
                var function = Scope.ResolveFunction(name);
                return new CallAst(function, arguments);
            }

            var ownerExpr = owner.Accept(new ExpressionVisitor(Scope));

            if (ownerExpr is ISymbol sym) {
                return new CallAst(sym.GetScope().ResolveFunction(name), arguments);
            }

            return base.VisitFunctionCallExpr(context);
        }
        #endregion
    }
}