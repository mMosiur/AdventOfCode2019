using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Day12;

public class ArrayComparer<T> : IEqualityComparer<T[]>
{
	public bool Equals(T[]? array1, T[]? array2)
	{
		if (array1 is null)
		{
			throw new ArgumentNullException(nameof(array1));
		}
		if (array2 is null)
		{
			throw new ArgumentNullException(nameof(array2));
		}
		return Enumerable.SequenceEqual(array1, array2);
	}

	public int GetHashCode([DisallowNull] T[] array)
		=> array.Aggregate(HashCode.Combine(typeof(T)), HashCode.Combine);
}
