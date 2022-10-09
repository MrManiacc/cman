using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMan.Lang.Util {
    public static class ListUtil {
        public static TC AddAll<T, TC>(this TC collection, IEnumerable<T> values)
            where TC : ICollection<T> {
            foreach (var value in values) collection.Add(value);
            return collection;
        }

        public static List<T> InstancesOf<T>(this IEnumerable<object> values) {
            var type = typeof(T);
            return values.Where(value => type.IsInstanceOfType(value)).Cast<T>().ToList();
        }

        // public static List<T> Filter<T>(this IEnumerable<object> values, Func<>) {
        //     
        // }

        public static bool IsEmpty<T>(this IEnumerable<T> values) => !values.Any();

        public static string JoinToString<T>(this IEnumerable<T> values, string separator = ", ") {
            var listType = values.GetType();
            var valueType = typeof(T);
            var sb = new StringBuilder($"{listType.Name}<{valueType.Name}>: (");
            var enumerable = values.ToList();
            if (enumerable.IsEmpty()) return sb.Append(")").ToString();
            sb.Append(enumerable[0]);
            for (var i = 1; i < enumerable.Count; i++) {
                sb.Append(separator)
                    .Append(enumerable[i]);
            }

            return sb.Append(")").ToString();
        }

        static ListUtil() { }
    }
}