using System;
using System.Linq;
using CMan.Lang.Program.Atom;

namespace CMan.Lang.Program {
    public class ProgramVisitor : CmanParserScopedVisitor<ProgramAst> {
        public override ProgramAst VisitProgram(CmanParser.ProgramContext context) {
            var program = new ProgramAst();
            var atomVisitor = new AtomVisitor(program);
            program.Atoms = context.atom()
                .Select(x => x.Accept(atomVisitor)).ToList();
            return program;
        }
    }
}