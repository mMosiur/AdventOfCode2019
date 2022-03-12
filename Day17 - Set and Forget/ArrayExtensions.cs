using System;

namespace AdventOfCode.Year2019.Day17;

public static class ArrayExtensions
{
	public static (int X, int Y) FindOnlyWhere<T>(this T[,] @this, Predicate<T> selector)
	{
		bool found = false;
		int X = -1;
		int Y = -1;
		for (int i = 0; i < @this.GetLength(0); i++)
		{
			for (int j = 0; j < @this.GetLength(1); j++)
			{
				if (selector.Invoke(@this[i, j]))
				{
					if (found) throw new InvalidOperationException();
					X = i;
					Y = j;
					found = true;
				}
			}
		}
		return (X, Y);
	}

	public static char? TryGet(this char[,] @this, int i, int j)
	{
		if (i < 0 || i >= @this.GetLength(0)) return null;
		if (j < 0 || j >= @this.GetLength(1)) return null;
		return @this[i, j];
	}
}
