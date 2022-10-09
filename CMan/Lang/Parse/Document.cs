using System.Linq;

namespace CMan.Lang.Parse {
    public class Document {

        /// <summary>
        /// The relative path of this file corresponding to the workspace folder
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The name of this document
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The source code of this document that will be used during compilation
        /// </summary>
        public string Source { get; }

        public Document(string path, string source) {
            Path = path;
            Name = path.Contains(Workspace.PathSeparator) ? path.Split(Workspace.PathSeparator).Last() : path;
            Source = source;
        }

        public override string ToString() {
            return $"Path: {Path}, Source: {Source}";
        }
    }
}