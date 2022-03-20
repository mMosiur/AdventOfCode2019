namespace AdventOfCode.Year2019.Day24;
using Position = RecursiveGridPosition;

public class RecursiveBugGridLayout
{
	private HashSet<Position> _bugPositions;
	public int Height { get; }
	public int MidY => Height / 2;
	public int Width { get; }
	public int MidX => Width / 2;

	public RecursiveBugGridLayout(GridSpot[,] grid)
	{
		Height = grid.GetLength(0);
		Width = grid.GetLength(1);
		if (Height % 2 != 1 || Width % 2 != 1)
		{
			throw new InvalidDataException("Grid must have odd-sized sides.");
		}
		GridSpot midSpot = grid[Height / 2, Width / 2];
		if (midSpot == GridSpot.Bug)
		{
			throw new InvalidDataException("Recursive grid must have a center spot with no bugs.");
		}
		_bugPositions = new HashSet<Position>();
		for (int y = 0; y < Height; y++)
		{
			for (int x = 0; x < Width; x++)
			{
				if (grid[y, x] == GridSpot.Bug)
				{
					_bugPositions.Add(new Position(x, y, 0));
				}
			}
		}
	}

	public RecursiveBugGridLayout(IEnumerable<Position> bugPositions, int height, int width)
	{
		Height = height;
		Width = width;
		_bugPositions = bugPositions.ToHashSet();
	}

	public bool IsMidPosition(Position position)
	{
		return position.X == MidX && position.Y == MidY;
	}

	public bool IsEdgeNeighbor(Position position, out (int X, int Y) direction)
	{
		direction.X = 0;
		if (position.X == 0) direction.X = -1;
		else if (position.X == Width - 1) direction.X = 1;
		direction.Y = 0;
		if (position.Y == 0) direction.Y = -1;
		else if (position.Y == Height - 1) direction.Y = 1;
		bool isNeighbor = direction.X != 0 || direction.Y != 0;
		return isNeighbor;
	}

	public bool IsMidNeighbor(Position position, out (int X, int Y) direction)
	{
		Position midPosition = position with { X = MidX, Y = MidY };
		var (stepX, stepY, stepDepth) = midPosition - position;
		bool isNeighbor = stepX * stepY == 0 && Math.Abs(stepX + stepY) == 1;
		direction = (stepX, stepY);
		return isNeighbor;
	}

	public IEnumerable<Position> Neighbors(Position position)
	{
		if (position.Y - 1 >= 0)
		{
			Position neighbor = new Position(position.X, position.Y - 1, position.Depth);
			if (!IsMidPosition(neighbor))
			{
				yield return neighbor;
			}
		}
		if (position.X + 1 < Width)
		{

			Position neighbor = new Position(position.X + 1, position.Y, position.Depth);
			if (!IsMidPosition(neighbor))
			{
				yield return neighbor;
			}
		}
		if (position.Y + 1 < Height)
		{
			Position neighbor = new Position(position.X, position.Y + 1, position.Depth);
			if (!IsMidPosition(neighbor))
			{
				yield return neighbor;
			}
		}
		if (position.X - 1 >= 0)
		{
			Position neighbor = new Position(position.X - 1, position.Y, position.Depth);
			if (!IsMidPosition(neighbor))
			{
				yield return neighbor;
			}
		}

		if (IsEdgeNeighbor(position, out var directionToEdge))
		{
			(int dirX, int dirY) = directionToEdge;
			if (dirX * dirY == 0)
			{
				// Neighbors just one edge of the grid.
				yield return new Position(MidX + dirX, MidY + dirY, position.Depth - 1);
			}
			else
			{
				// Neighbors more than one (two exactly) edges of the grid
				yield return new Position(MidX, MidY + dirY, position.Depth - 1);
				yield return new Position(MidX + dirX, MidY, position.Depth - 1);
			}
		}

		if (IsMidNeighbor(position, out var directionToMid))
		{
			(int dirX, int dirY) = directionToMid;
			if (dirX == 1 && dirY == 0)
			{
				for (int i = 0; i < Height; i++)
				{
					yield return new Position(0, i, position.Depth + 1);
				}
			}
			else if (dirX == 0 && dirY == 1)
			{
				for (int i = 0; i < Width; i++)
				{
					yield return new Position(i, 0, position.Depth + 1);
				}
			}
			else if (dirX == -1 && dirY == 0)
			{
				for (int i = 0; i < Height; i++)
				{
					yield return new Position(Width - 1, i, position.Depth + 1);
				}
			}
			else if (dirX == 0 && dirY == -1)
			{
				for (int i = 0; i < Width; i++)
				{
					yield return new Position(i, Height - 1, position.Depth + 1);
				}
			}
			else throw new ApplicationException("Invalid direction to mid.");
			// yield return new Position(MidX + dirX, MidY + dirY, position.Depth + 1);
		}
	}

	public bool IsBug(Position position)
	{
		return _bugPositions.Contains(position);
	}

	public bool IsEmpty(Position position)
	{
		return !_bugPositions.Contains(position);
	}

	public IEnumerable<Position> BugPositions => _bugPositions.AsEnumerable();
}
