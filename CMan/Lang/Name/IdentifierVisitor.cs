namespace CMan.Lang.Name {
    public class IdentifierVisitor : CmanParserBaseVisitor<Identifier> {
        public static IdentifierVisitor Instance { get; } = new IdentifierVisitor();

        static IdentifierVisitor() { }
        private IdentifierVisitor() { }

        public override Identifier VisitQualifiedName(CmanParser.QualifiedNameContext context) =>
            new Identifier(context.GetText());

        public override Identifier VisitFunctionName(CmanParser.FunctionNameContext context) =>
            new Identifier(context.GetText());

        public override Identifier VisitVariableName(CmanParser.VariableNameContext context) =>
            new Identifier(context.GetText());


        public override Identifier VisitModuleSignature(CmanParser.ModuleSignatureContext context) =>
            VisitModuleName(context.moduleName());

        public override Identifier VisitModuleName(CmanParser.ModuleNameContext context) =>
            new Identifier(context.GetText());

        public override Identifier VisitName(CmanParser.NameContext context) =>
            new Identifier(context.GetText());
    }
}