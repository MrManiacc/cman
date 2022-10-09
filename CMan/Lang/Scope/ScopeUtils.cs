using System.Collections.Generic;
using System.Text;
using CMan.Lang.Name;

namespace CMan.Lang.Scope {
    public static class ScopeUtils {
        /// <summary>
        /// This will take all of the scopes and turn them into a string that will optionally be reversed to
        /// show the closest scope last. The separator is a char that is used split the scopes up and can be changed  
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="separator"></param>
        /// <param name="reversed"></param>
        /// <returns></returns>
        public static string JoinScopeNames(this List<IScope> scopes, char separator = Identifier.Separator,
            bool reversed = true) {
            if (reversed) scopes.Reverse();
            if (scopes.Count == 0) return "";
            var sb = new StringBuilder(scopes[0].GetName());
            for (var i = 1; i < scopes.Count; i++) {
                var scope = scopes[i];
                sb.Append(separator)
                    .Append(scope.GetName());
            }

            return sb.ToString();
        }
    }
}