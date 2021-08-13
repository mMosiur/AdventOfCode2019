using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Day12
{
	public class ArrayComparer<T> : IEqualityComparer<T[]>
	{
		public bool Equals(T[]? x, T[]? y)
		{
			if (x is null)
			{
				throw new ArgumentNullException(nameof(x));
			}
			if (y is null)
			{
				throw new ArgumentNullException(nameof(y));
			}
			return Enumerable.SequenceEqual(x, y);
		}

		public int GetHashCode([DisallowNull] T[] obj)
		{
			int hash = HashCode.Combine(typeof(T));
			foreach (T el in obj)
			{
				hash = HashCode.Combine(hash, el);
			}
			return hash;
		}
	}
}
