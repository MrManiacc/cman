namespace CMan.Lang.Type
{
    public interface IType
    {
        /// <summary>
        /// The raw name of this type. I.E: string, int, etc.
        /// </summary>
        string GetTypeName();
        
    }
}