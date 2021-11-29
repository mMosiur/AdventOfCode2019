namespace Day20;

internal static class ArrayExtensions
{
	public static T GetOrDefault<T>(this T[,] array, int index1, int index2) where T : struct
	{
		if (index1 < 0 || index1 >= array.GetLength(0) || index2 < 0 || index2 >= array.GetLength(1))
		{
			return default;
		}
		return array[index1, index2];
	}
}
