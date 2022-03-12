using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Day17;

public static class ListExtensions
{
	public static int FindFirstSequenceIndex<T>(this IList<T> @this, IEnumerable<T> sequence, int indexStart = 0)
	{
		var pattern = sequence.ToArray();
		var source = new LinkedList<T>();
		for (int i = indexStart; i < @this.Count; i++)
		{
			source.AddLast(@this[i]);
			if (source.Count == pattern.Length)
			{
				if (source.SequenceEqual(pattern))
					return i - pattern.Length + 1;
				source.RemoveFirst();
			}
		}
		return -1;
	}

	public static void ReplaceSequencesWith<T>(this List<T> @this, IEnumerable<T> sequenceToReplace, T replacement)
	{
		int count = sequenceToReplace.Count();
		int index = FindFirstSequenceIndex(@this, sequenceToReplace);
		while (index >= 0)
		{
			@this.RemoveRange(index, count);
			@this.Insert(index, replacement);
			index = FindFirstSequenceIndex(@this, sequenceToReplace);
		}
	}

	public static void Replace<T>(this IList<T> @this, T valueToReplace, T replacement) where T : IEquatable<T>
	{
		for (int i = 0; i < @this.Count; i++)
		{
			if (@this[i].Equals(valueToReplace))
			{
				@this[i] = replacement;
			}
		}
	}

	public static int CountSubsequences<T>(this IEnumerable<T> @this, IEnumerable<T> subsequence)
	{
		int thisCount = @this.Count();
		int subsequenceCount = subsequence.Count();
		int sum = 0;
		for (int i = 0; i < thisCount; i++)
		{
			if (@this.Skip(i).Take(subsequenceCount).SequenceEqual(subsequence))
			{
				sum++;
				i += subsequenceCount - 1;
			}
		}
		return sum;
	}
}
