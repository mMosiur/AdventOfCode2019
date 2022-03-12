using System;
using System.Linq;
namespace AdventOfCode.Year2019.Day18;

public static class ArrayExtensions
{
	public static T[,] ToRectangularArray<T>(this T[][] @this)
	{
		int len1 = @this.Length;
		if (len1 == 0)
		{
			throw new InvalidOperationException("This jagged array does not have rectangular shape.");
		}
		int len2 = @this[0].Length;
		if (len2 == 0 || !@this.All(row => row.Length == len2))
		{
			throw new InvalidOperationException("This jagged array does not have rectangular shape.");
		}
		T[,] result = new T[len1, len2];
		for (int i = 0; i < len1; i++)
		{
			for (int j = 0; j < len2; j++)
			{
				result[i, j] = @this[i][j];
			}
		}
		return result;
	}
}
