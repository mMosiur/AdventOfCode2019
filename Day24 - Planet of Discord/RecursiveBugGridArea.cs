namespace AdventOfCode.Year2019.Day24;

public class RecursiveBugGridArea : IBugGridArea
{
	private RecursiveBugGridLayout _layout;

	public int Minute { get; private set; }

	public RecursiveBugGridArea(RecursiveBugGridLayout layout)
	{
		_layout = layout;
		Minute = 0;
	}

	public int BugCount => _layout.BugPositions.Count();

	public static RecursiveBugGridArea Parse(IList<string> inputLines)
	{
		int width = inputLines[0].Length;
		int height = inputLines.Count;
		if (!inputLines.All(l => l.Length == width))
		{
			throw new FormatException("All lines must be the same length.");
		}
		var grid = new GridSpot[height, width];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				grid[x, y] = inputLines[y][x] switch
				{
					'#' => GridSpot.Bug,
					'.' => GridSpot.Empty,
					_ => throw new FormatException($"Invalid character '{inputLines[y][x]}'")
				};
			}
		}
		return new RecursiveBugGridArea(new RecursiveBugGridLayout(grid));
	}

	public void NextMinute()
	{
		var newBugPositions = new HashSet<RecursiveGridPosition>();
		var emptySpots = new List<RecursiveGridPosition>();
		foreach (var position in _layout.BugPositions)
		{
			// A bug dies (becoming an empty space) unless there is exactly one bug adjacent to it.
			IEnumerable<RecursiveGridPosition> neighbors = _layout.Neighbors(position);
			emptySpots.AddRange(neighbors.Where(_layout.IsEmpty));
			int bugNeighborCount = neighbors.Count(_layout.IsBug);
			if (bugNeighborCount == 1)
			{
				newBugPositions.Add(position);
			}
		}
		foreach (var position in emptySpots)
		{
			// An empty space becomes infested with a bug if exactly one or two bugs are adjacent to it.
			IEnumerable<RecursiveGridPosition> neighbors = _layout.Neighbors(position);
			int bugNeighborCount = neighbors.Count(_layout.IsBug);
			if (bugNeighborCount == 1 || bugNeighborCount == 2)
			{
				newBugPositions.Add(position);
			}
		}
		_layout = new RecursiveBugGridLayout(newBugPositions, _layout.Width, _layout.Height);
		Minute++;
	}
}
