using System.Collections.Generic;
using System.Linq;
using CMan.Lang.Program.Atom.Function;
using CMan.Lang.Type;
using CMan.Lang.Util;

namespace CMan.Lang.Scope {
    public static class ScopeExtensions {
        public static FunctionAst ResolveFunction(this IScope scope, string name) {
            var resolved = scope.Resolve(name);
            if (resolved is FunctionAst ast) return ast;
            return null;
        }
    }
}