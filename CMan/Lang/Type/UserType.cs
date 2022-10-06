namespace CMan.Lang.Type
{
    public class UserType : IType
    {
        private readonly string name;

        public UserType(string name)
        {
            this.name = name;
        }
        
        public string GetTypeName()
        {
            return name;
        }
    }
}