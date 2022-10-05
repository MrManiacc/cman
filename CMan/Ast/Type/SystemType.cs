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
        public static readonly SystemType Boolean = new SystemType("Boolean");
        public static readonly SystemType Int = new SystemType("Int");
        public static readonly SystemType Char = new SystemType("Char");
        public static readonly SystemType Byte = new SystemType("Byte");
        public static readonly SystemType Short = new SystemType("Short");
        public static readonly SystemType Long = new SystemType("Long");
        public static readonly SystemType Float = new SystemType("Float");
        public static readonly SystemType Double = new SystemType("Double");
    }
}