using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Misc;

namespace CMan.Lang.Type {
    public class SystemType : IType {
        private readonly string name;
        private static readonly List<SystemType> Primitives = new ArrayList<SystemType>();

        private SystemType(string name) {
            this.name = name;
            if (!Primitives.Contains(this))
                Primitives.Add(this);
        }


        public string GetTypeName() {
            return name;
        }

        public override string ToString() {
            return $"{nameof(name)}: {name}";
        }

        public override int GetHashCode() {
            return name.GetHashCode();
        }

        public override bool Equals(object obj) {
            return obj is SystemType type && type.GetTypeName().Equals(GetTypeName());
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
        public static readonly SystemType String = new SystemType("string");
        public static readonly SystemType Void = new SystemType("void");
        public static readonly SystemType Null = new SystemType("null");

        public static SystemType Get(string name) {
            foreach (var primitive in
                     Primitives.Where(primitive => primitive.name.ToLower().Equals(name.ToLower())))
                return primitive;
            return Null;
        }
    }
}