using CMan.Lang.Program.Atom.Function;
using CMan.Lang.Program.Atom.Module;
using CMan.Lang.Program.Atom.Statement;
using CMan.Lang.Scope;

namespace CMan.Lang.Program.Atom {
    public class AtomVisitor : CmanParserScopedVisitor<AtomAst> {
        public AtomVisitor(IScope scope) : base(scope) { }

        public override AtomAst VisitAtom(CmanParser.AtomContext context) {
            if (context.module() != null) {
                var visitor = new ModuleVisitor(Scope);
                var module = context.module().Accept(visitor);
                return new AtomAst(module);
            }

            if (context.function() != null) {
                var visitor = new FunctionVisitor(Scope);
                var function = context.function().Accept(visitor);
                return new AtomAst(function);
            }

            if (context.statement() != null) {
                var visitor = new StatementVisitor(Scope);
                var statement = context.statement().Accept(visitor);
                return new AtomAst(statement);
            }
            
            return base.VisitAtom(context);
        }
    }
}