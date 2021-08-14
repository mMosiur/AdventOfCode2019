using System.Collections.Generic;

namespace Day13
{
	public static class ScreenExtensions
	{
		public static IEnumerable<KeyValuePair<(int X, int Y), char>> Enumerate(this IntcodeMachineScreen screen)
		{
			for (int i = 0; i < screen.Frame.GetLength(0); i++)
			{
				for (int j = 0; j < screen.Frame.GetLength(1); j++)
				{
					yield return new KeyValuePair<(int X, int Y), char>((i, j), screen.Frame[i, j]);
				}
			}
		}
	}
}
