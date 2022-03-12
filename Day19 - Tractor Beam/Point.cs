namespace AdventOfCode.Year2019.Day19;

public record struct Point(int X, int Y)
{
	public Point Moved(int xOffset = 0, int yOffset = 0) => new(X + xOffset, Y + yOffset);
}
