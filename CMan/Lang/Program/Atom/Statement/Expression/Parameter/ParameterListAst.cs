using System.Collections.Generic;

namespace CMan.Lang.Program.Atom.Statement.Expression.Parameter {
    public class ParameterListAst {
        public ParameterListAst(List<ParameterAst> parameters) {
            Parameters = parameters;
        }

        public List<ParameterAst> Parameters { get; }

        public override string ToString() {
            return $"Parameters: {string.Join(", ", Parameters)}";
        }
    }
}