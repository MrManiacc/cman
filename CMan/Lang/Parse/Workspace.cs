using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Antlr4.Runtime;
using CMan.Lang.Program;

namespace CMan.Lang.Parse {
    public class Workspace {
        public const char PathSeparator = '/';
        public const string DocumentExtension = "cm";
        public readonly string WorkingDirectory;
        private readonly Dictionary<string, Document> relativeDocuments = new Dictionary<string, Document>();
        private readonly Dictionary<string, ProgramAst> cachedPrograms = new Dictionary<string, ProgramAst>();

        public Workspace(string workingDirectory) {
            WorkingDirectory = workingDirectory;
            ResolveDocuments();
        }

        private void ResolveDocuments() {
            var documents = Directory.GetFiles(WorkingDirectory, $"*{DocumentExtension}", SearchOption.AllDirectories);
            foreach (var document in documents) {
                var length = WorkingDirectory.Length + 1;
                var source = File.ReadAllText(document);
                var relPath =
                    document.Substring(length, document.Length - length - 3)
                        .Replace("\\", PathSeparator.ToString()); //Remove .cm from end, 3 character
                relativeDocuments[relPath] = new Document(relPath, source);
            }
        }

        /// <summary>
        /// This will get the program with the given name or it will parse the program then cache it.
        /// </summary>
        /// <param name="name">The path name of the program to get/parse</param>
        /// <returns>The parsed program ast</returns>
        /// <exception cref="InvalidOperationException">thrown when unknown program is attempted to be retrieved.</exception>
        public ProgramAst GetProgram(string name) {
            if (cachedPrograms.ContainsKey(name))
                return cachedPrograms[name];
            if (!relativeDocuments.ContainsKey(name))
                throw new InvalidOperationException($"Couldn't find document with path {name}");
            var inputStream = new AntlrInputStream(relativeDocuments[name].Source);
            var lexer = new CmanLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new CmanParser(tokenStream);
            var visitor = new ProgramVisitor();
            var program = parser.program().Accept(visitor);
            cachedPrograms[name] = program;
            return program;
        }

        public override string ToString() {
            var lines = relativeDocuments.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
            return
                $"WorkingDirectory: {WorkingDirectory}, RelativeDocuments: {string.Join(Environment.NewLine, lines)}";
        }
    }
}