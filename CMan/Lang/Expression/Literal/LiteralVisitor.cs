using System.Linq;
using CMan.Lang.Type;

namespace CMan.Lang.Expression.Literal
{
    public class LiteralVisitor : CmanParserBaseVisitor<LiteralAst>
    {
        public override LiteralAst VisitLiteralExpr(CmanParser.LiteralExprContext context)
        {
            var value = context.value().GetText();
            var type = ResolveType(value);
            if (Equals(type, SystemType.Char) || Equals(type, SystemType.String))
                value = value.Substring(1, value.Length - 2);
            else if (!value.StartsWith("0x") && !char.IsDigit(value.Last()) && !Equals(type, SystemType.Boolean))
                value = value.Substring(0, value.Length - 1);
            return new LiteralAst(type, value);
        }

        /// <summary>
        /// Resolves the type based upon the literal string value
        /// </summary>
        /// <param name="value"></param>
        /// <returns>The resolved primitive type</returns>
        private static IType ResolveType(string value)
        {
            var array = value.ToCharArray();
            if (array.Length == 0) return SystemType.Void;
            if (value.StartsWith("'") && value.EndsWith("'")) return SystemType.Char;
            if (value.StartsWith("\"") && value.EndsWith("\"")) return SystemType.String;
            switch (value)
            {
                case "true":
                case "false":
                    return SystemType.Boolean;
                case "null":
                    return SystemType.Null;
            }

            var isNumber = char.IsDigit(array[0]);
            var isDecimal = isNumber && value.Contains(".");
            var isHex = value.StartsWith("0x");
            if (isHex) return SystemType.Int;
            switch (isDecimal)
            {
                case true when value.EndsWith("d"): return SystemType.Double;
                case true: return SystemType.Float;
            }

            if (!isNumber) return SystemType.Void;
            if (value.EndsWith("b")) return SystemType.Byte;
            if (value.EndsWith("l")) return SystemType.Long;
            return value.EndsWith("s") ? SystemType.Short : SystemType.Int;
        }
    }
}