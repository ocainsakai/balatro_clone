using System.Linq;
using System.Text.RegularExpressions;

public static class StringExtensions
{
	public static string NoWhiteSpace(this string str)
	{
		if (str == null) return string.Empty;
		return string.Concat(str.Where(c => !char.IsWhiteSpace(c)));
	}
	public static string AddWhiteSpace(this string str)
	{
		if (string.IsNullOrWhiteSpace(str)) return str;

		return Regex.Replace(str, "(?<!^)([A-Z])", " $1");
	}
}