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

        protected bool Equals(UserType other) {
            return name == other.name;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserType)obj);
        }

        public override int GetHashCode() {
            return (name != null ? name.GetHashCode() : 0);
        }
    }
}