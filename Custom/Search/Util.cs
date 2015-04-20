using System.Globalization;

namespace SitefinityWebApp.Custom.Search
{
    public static class Util
    {
        public static string GetSafeString(object o)
        {
            if (o == null)
            {
                return null;
            }
            return o.ToString();
        }

        public static string ToTitleCase(this string s)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
        }
    }
}
