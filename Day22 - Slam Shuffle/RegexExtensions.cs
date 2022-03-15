using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2019.Day22;

public static class RegexExtensions
{
	public static bool TryMatch(this Regex regex, string input, [NotNullWhen(true)] out Match? match)
	{
		match = regex.Match(input);
		if (!match.Success)
		{
			match = default;
			return false;
		}
		return true;
	}
}
