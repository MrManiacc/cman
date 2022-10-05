namespace CMan.Ast.Type
{
    public class ClassType : IType
    {
        private readonly string _name;

        public ClassType(string name)
        {
            _name = name;
        }
        
        public string GetTypeName()
        {
            return _name;
        }
    }
}