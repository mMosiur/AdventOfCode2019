using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Day04;

public static class Extensions
{
	public static (T First, T Second) SplitIntoTwo<T>(this IEnumerable<T> source)
	{
		if (source is null)
		{
			throw new ArgumentNullException(nameof(source));
		}
		int count = source.Count();
		if (count != 2)
		{
			throw new ArgumentException($"Provided array had {count} elements instead of expected 2");
		}
		T[] array = source.ToArray();
		return (array[0], array[1]);
	}

	public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> source) => source.Pairwise(ValueTuple.Create);

	public static IEnumerable<TResult> Pairwise<T, TResult>(this IEnumerable<T> source, Func<T, T, TResult> selector)
	{
		if (selector is null)
		{
			throw new ArgumentNullException(nameof(selector));
		}

		using IEnumerator<T> it = source.GetEnumerator();
		if (!it.MoveNext())
		{
			yield break;
		}
		T previous = it.Current;
		while (it.MoveNext())
		{
			yield return selector(previous, it.Current);
			previous = it.Current;
		}
	}
}
