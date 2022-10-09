using System.Collections.Generic;
using System.Linq;

namespace CMan.Lang.Name {
    public class Identifier {
        public const char Separator = '.';
        public const char All = '*';

        public string QualifiedName { get; private set; }

        public string Name { get; private set; }

        public List<string> Path { get; private set; }

        public bool IsAll { get; private set; }

        public bool IsQualified { get; private set; }


        public Identifier(string fullName) =>
            Build(fullName);


        private void Build(string nameIn) {
            //If we don't have a separator char this isn't a qualified identifier
            if (!nameIn.Contains(Separator)) {
                IsQualified = false;
                IsAll = false;
                Name = nameIn;
                Path = new List<string> { Name };
                QualifiedName = Name;
                return;
            }

            IsAll = nameIn.EndsWith(All.ToString());
            //When IsAll is true, we want to remove the .* at the end of qualifiedName
            QualifiedName = IsAll ? nameIn.Substring(0, nameIn.Length - 2) : nameIn;

            //Split based upon separator character then store
            Path = QualifiedName.Split(Separator).ToList();
            Name = Path.Last();
        }

        public override string ToString() {
            return $"QualifiedName: {QualifiedName}";
        }
    }
}