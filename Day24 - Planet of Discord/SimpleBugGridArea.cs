namespace AdventOfCode.Year2019.Day24;

public class SimpleBugGridArea : IBugGridArea
{
	private SimpleBugGridLayout _layout;

	public int Minute { get; private set; }

	public int BugCount => _layout.Count();

	public ulong CalculateCurrentLayoutFingerprint() => _layout.CalculateFingerprint();
	public ulong CalculateCurrentLayoutBiodiversityRating() => _layout.CalculateBiodiversityRating();

	public SimpleBugGridArea(SimpleBugGridLayout layout)
	{
		_layout = layout;
		Minute = 0;
	}

	public static SimpleBugGridArea Parse(IList<string> inputLines)
	{
		int width = inputLines[0].Length;
		int height = inputLines.Count;
		if (!inputLines.All(l => l.Length == width))
		{
			throw new FormatException("All lines must be the same length");
		}
		var layout = new SimpleBugGridLayout(height, width);
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				layout[x, y] = inputLines[y][x] switch
				{
					'#' => GridSpot.Bug,
					'.' => GridSpot.Empty,
					_ => throw new FormatException($"Invalid character '{inputLines[y][x]}'")
				};
			}
		}
		return new SimpleBugGridArea(layout);
	}

	public void NextMinute()
	{
		var nextLayout = new SimpleBugGridLayout(_layout.Height, _layout.Width);
		for (int y = 0; y < _layout.Height; y++)
		{
			for (int x = 0; x < _layout.Width; x++)
			{
				int neighborBugCount = _layout.NeighborsOf(x, y).Count(s => s == GridSpot.Bug);
				nextLayout[x, y] = _layout[x, y] switch
				{
					GridSpot.Bug => neighborBugCount == 1 ? GridSpot.Bug : GridSpot.Empty,
					GridSpot.Empty => neighborBugCount == 1 || neighborBugCount == 2 ? GridSpot.Bug : GridSpot.Empty,
					_ => throw new InvalidOperationException($"Invalid grid spot {_layout[x, y]}")
				};
			}
		}
		Minute++;
		_layout = nextLayout;
	}
}
