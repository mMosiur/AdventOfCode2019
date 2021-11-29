using System;
using System.Collections.Generic;

namespace Day16;

public static class EnumerableExtensions
{
	public static IEnumerable<TElement> RepeatSequence<TElement>(this IEnumerable<TElement> sequence, int repetitions)
	{
		if (repetitions < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(repetitions));
		}
		for (int i = 0; i < repetitions; i++)
		{
			foreach (TElement el in sequence)
			{
				yield return el;
			}
		}
	}
}
