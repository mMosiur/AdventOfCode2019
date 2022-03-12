namespace AdventOfCode.Year2019.Day11.Geometry;

public record struct Vector(int X, int Y)
{
	public static Vector operator *(Vector vector, int scalar)
	{
		return new Vector
		{
			X = vector.X * scalar,
			Y = vector.Y * scalar
		};
	}

	public static Vector Up => new(0, -1);
	public static Vector Right => new(1, 0);
	public static Vector Down => new(0, 1);
	public static Vector Left => new(-1, 0);
}
