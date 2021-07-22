using System;

namespace Day03
{
	public struct Vector2D
	{
		public int X { get; init; }
		public int Y { get; init; }

		public Vector2D(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static Vector2D ParsePathStep(string step)
		{
			if (string.IsNullOrWhiteSpace(step))
			{
				throw new FormatException("Input string was not in a correct format.");
			}
			char direction = step[0];
			int distance = int.Parse(step.AsSpan(1));
			return direction switch
			{
				'U' => new Vector2D(0, distance),
				'R' => new Vector2D(distance, 0),
				'D' => new Vector2D(0, -distance),
				'L' => new Vector2D(-distance, 0),
				_ => throw new FormatException("Input string was not in a correct format.")
			};
		}
	}
}
