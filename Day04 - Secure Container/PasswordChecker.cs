using System;
using System.Collections.Generic;
using System.Linq;

namespace Day04;

public class PasswordChecker
{
	public int RangeStart { get; }
	public int RangeEnd { get; }

	public PasswordChecker(int rangeStart, int rangeEnd)
	{
		// It is a six-digit number.
		RangeStart = Math.Max(rangeStart, 100000);
		RangeEnd = Math.Min(rangeEnd, 999999);
	}

	// Two adjacent digits are the same (like 22 in 122345).
	public static bool HasSameAdjacentDigits(int number)
	{
		return number.ToString().Pairwise().Any(pair => pair.Item1 == pair.Item2);
	}

	// The two adjacent matching digits are not part of a larger group of matching digits.
	public static bool HasExactlyTwoAdjacentDigits(int number)
	{
		string numberString = number.ToString();
		char lastCharacter = numberString[0];
		int previousStreak = 1;
		foreach (char c in numberString.Skip(1))
		{
			if (c == lastCharacter)
			{
				previousStreak++;
			}
			else if (previousStreak == 2)
			{
				return true;
			}
			else
			{
				lastCharacter = c;
				previousStreak = 1;
			}
		}
		return previousStreak == 2;
	}

	// Going from left to right, the digits never decrease;
	public static bool IsInNonDecreasingOrder(int number)
	{
		return number.ToString().Pairwise().All(pair => pair.Item1 <= pair.Item2);
	}

	private static bool MatchesAllNonRangeRules(int number, bool strictAdjacentRule)
	{
		bool adjacent = strictAdjacentRule ? HasExactlyTwoAdjacentDigits(number) : HasSameAdjacentDigits(number);
		return adjacent && IsInNonDecreasingOrder(number);
	}

	public IEnumerable<int> AllMatchingPassword(bool strictAdjacentRule)
	{
		int count = RangeEnd - RangeStart + 1;
		return Enumerable.Range(RangeStart, count).Where(p => MatchesAllNonRangeRules(p, strictAdjacentRule));
	}

}
