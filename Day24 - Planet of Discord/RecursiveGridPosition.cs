namespace AdventOfCode.Year2019.Day24;

public record struct RecursiveGridPosition(int X, int Y, int Depth)
{
	public static (int, int, int) operator -(RecursiveGridPosition left, RecursiveGridPosition right)
	{
		return (left.X - right.X, left.Y - right.Y, left.Depth - right.Depth);
	}
}
