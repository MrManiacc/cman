namespace CMan.Ast.Type
{
    public class SystemType : IType
    {
        private readonly string _name;

        private SystemType(string name)
        {
            _name = name;
        }


        public string GetTypeName()
        {
            return _name;
        }

        //======================== Built in types ===========================
        public static readonly SystemType Boolean = new SystemType("boolean");
        public static readonly SystemType Int = new SystemType("int");
        public static readonly SystemType Char = new SystemType("char");
        public static readonly SystemType Byte = new SystemType("byte");
        public static readonly SystemType Short = new SystemType("short");
        public static readonly SystemType Long = new SystemType("long");
        public static readonly SystemType Float = new SystemType("float");
        public static readonly SystemType Double = new SystemType("double");
        public static readonly SystemType Void = new SystemType("void");
        public static readonly SystemType Null = new SystemType("null");
    }
}