using System;
using System.Globalization;

namespace ObjLoader.Loader.Common
{
    public static class StringExtensions
    {
        public static float ParseInvariantFloat(this string floatString)
        {
            return float.Parse(floatString, CultureInfo.InvariantCulture.NumberFormat);
        }

        public static int ParseInvariantInt(this string intString)
        {
            return int.Parse(intString, CultureInfo.InvariantCulture.NumberFormat);
        }

        public static bool EqualsInvariantCultureIgnoreCase(this string str, string s)
        {
            return str.Equals(s, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        //non-.NET 4.0 workaround method
        public static bool IsNullOrWhiteSpace(this string str)
        {
            //The following lines are a workaround for .NET versions less that 4.0
            //This replaces the IsNullOrWhiteSpace method
            bool isWhiteSpace = true;
            for (int i = 0; i < str.Length; ++i)
            {
                isWhiteSpace = (isWhiteSpace && char.IsWhiteSpace(str, i));
                if (!isWhiteSpace)
                    break;
            }
            bool isNullOrWhiteSpace = (isWhiteSpace || str == null || str.Length == 0);
            return isNullOrWhiteSpace;
        }
    }
}