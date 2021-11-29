using System;
using System.Collections.Generic;

namespace Day03;

public static class Extensions
{
	public static (T First, T Second) SplitIntoTwo<T>(this T[] array)
	{
		if (array is null)
		{
			throw new ArgumentNullException(nameof(array));
		}
		if (array.Length != 2)
		{
			throw new ArgumentException("Provided array did not have exactly two parts");
		}
		return (array[0], array[1]);
	}

	public static IEnumerable<TResult> Pairwise<T, TResult>(this IEnumerable<T> source, Func<T?, T?, TResult> selector)
	{
		T? previous = default;
		using IEnumerator<T> it = source.GetEnumerator();
		if (it.MoveNext())
		{
			previous = it.Current;
		}
		while (it.MoveNext())
		{
			yield return selector(previous, it.Current);
			previous = it.Current;
		}
	}

	public static IEnumerable<Point> ToPointPath(this IEnumerable<Vector2D> source, Point startingPoint)
	{
		Point point = startingPoint;
		yield return point;
		foreach (Vector2D vector in source)
		{
			point += vector;
			yield return point;
		}
	}

	public static IEnumerable<ManhattanEdge> EdgesInPath(this IEnumerable<Point> source)
	{
		return source.Pairwise((p1, p2) => new ManhattanEdge(p1, p2));
	}
}
