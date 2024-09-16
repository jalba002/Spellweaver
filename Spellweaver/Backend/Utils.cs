using System.Collections;
using System.Text.RegularExpressions;

namespace Spellweaver.Backend
{
    public static class Utils
    {
        public static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[()]", "", RegexOptions.CultureInvariant);
        }

        public static string ToOrdinal(int number)
        {
            if (number < 0) return number.ToString();
            long rem = number % 100;
            if (rem >= 11 && rem <= 13) return number + "th";

            switch (number % 10)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }
        public static IList? CreateList(Type? myType)
        {
            if(myType is null) return null;
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }
    }
}
