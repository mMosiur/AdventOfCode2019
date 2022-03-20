using System.Collections;

namespace AdventOfCode.Year2019.Day24;

public class SimpleBugGridLayout : IEnumerable<GridSpot>
{
	private GridSpot[,] _grid;

	public static GridSpot SpotOutsideScope { get; } = GridSpot.Empty;

	public int Height => _grid.GetLength(0);
	public int Width => _grid.GetLength(1);

	public SimpleBugGridLayout(GridSpot[,] grid)
	{
		_grid = grid;
	}

	public ulong CalculateBiodiversityRating()
	{
		// Apparently the biodiversity rating is calculated
		// exactly the same as the chosen fingerprint method
		return CalculateFingerprint();
	}

	public SimpleBugGridLayout(int height, int width)
	{
		_grid = new GridSpot[height, width];
	}

	public GridSpot this[int x, int y]
	{
		get
		{
			if (x < 0 || x >= _grid.GetLength(0) || y < 0 || y >= _grid.GetLength(1))
			{
				return SpotOutsideScope;
			}
			return _grid[x, y];
		}

		set => _grid[x, y] = value;
	}

	public IEnumerable<GridSpot> NeighborsOf(int x, int y)
	{
		yield return this[x - 1, y];
		yield return this[x + 1, y];
		yield return this[x, y - 1];
		yield return this[x, y + 1];
	}

	public ulong CalculateFingerprint()
	{
		ulong high = Math.BigMul((ulong)_grid.GetLength(0), (ulong)_grid.GetLength(1), out ulong low);
		if (high > 0)
		{
			throw new InvalidOperationException("Grid too big - fingerprint overflow");
		}
		ulong fingerprint = 0;
		for (int y = 0; y < _grid.GetLength(1); y++)
		{
			for (int x = 0; x < _grid.GetLength(0); x++)
			{
				ulong bit = _grid[x, y] switch
				{
					GridSpot.Bug => 1,
					GridSpot.Empty => 0,
					_ => throw new InvalidOperationException("Unsupported grid spot for fingerprint calculation")
				};
				bit <<= y * _grid.GetLength(0) + x;
				fingerprint += bit;
			}
		}
		return fingerprint;
	}

	public IEnumerator<GridSpot> GetEnumerator() => _grid.Cast<GridSpot>().GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
