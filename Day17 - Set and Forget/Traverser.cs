using System;
using System.Collections.Generic;

namespace Day17;

public class Traverser
{
	public char[,] Map { get; }

	public int Y { get; private set; }
	public int X { get; private set; }
	public char Direction { get; private set; }

	void RotateLeft()
	{
		Direction = Direction switch
		{
			'^' => '<',
			'v' => '>',
			'<' => 'v',
			'>' => '^',
			_ => throw new InvalidOperationException()
		};
	}

	void RotateRight()
	{
		Direction = Direction switch
		{
			'^' => '>',
			'v' => '<',
			'<' => '^',
			'>' => 'v',
			_ => throw new InvalidOperationException()
		};
	}

	private void ApplyRotation(char rotation)
	{
		const char LeftRotationCommand = 'L';
		const char RightRotationCommand = 'R';
		switch (rotation)
		{
			case LeftRotationCommand:
				RotateLeft();
				break;
			case RightRotationCommand:
				RotateRight();
				break;
			default:
				throw new InvalidOperationException();
		}
	}

	private char? SuggestRotation()
	{
		const char LeftRotationCommand = 'L';
		const char RightRotationCommand = 'R';
		switch (Direction)
		{
			case '^':
				if (Map.TryGet(Y - 1, X) == '#')
				{
					return null;
				}
				else if (Map.TryGet(Y, X - 1) == '#')
				{
					return LeftRotationCommand;
				}
				else if (Map.TryGet(Y, X + 1) == '#')
				{
					return RightRotationCommand;
				}
				else throw new NoRotationAvailableException();
			case '>':
				if (Map.TryGet(Y, X + 1) == '#')
				{
					return null;
				}
				else if (Map.TryGet(Y - 1, X) == '#')
				{
					return LeftRotationCommand;
				}
				else if (Map.TryGet(Y + 1, X) == '#')
				{
					return RightRotationCommand;
				}
				else throw new NoRotationAvailableException();
			case 'v':
				if (Map.TryGet(Y + 1, X) == '#')
				{
					return null;
				}
				else if (Map.TryGet(Y, X + 1) == '#')
				{
					return LeftRotationCommand;
				}
				else if (Map.TryGet(Y, X - 1) == '#')
				{
					return RightRotationCommand;
				}
				else throw new NoRotationAvailableException();
			case '<':
				if (Map.TryGet(Y, X - 1) == '#')
				{
					return null;
				}
				else if (Map.TryGet(Y + 1, X) == '#')
				{
					return LeftRotationCommand;
				}
				else if (Map.TryGet(Y - 1, X) == '#')
				{
					return RightRotationCommand;
				}
				else throw new NoRotationAvailableException();
			default: throw new InvalidOperationException();
		}
	}

	private int GoForward()
	{
		int distance = 0;
		int xOffset = 0;
		int yOffset = 0;
		switch (Direction)
		{
			case '^':
				yOffset = -1;
				break;
			case '>':
				xOffset = +1;
				break;
			case 'v':
				yOffset = +1;
				break;
			case '<':
				xOffset = -1;
				break;
			default:
				throw new InvalidOperationException();
		}
		while (Map.TryGet(Y + yOffset, X + xOffset) == '#')
		{
			X += xOffset;
			Y += yOffset;
			distance++;
		}
		return distance;
	}

	public Traverser(char[,] map)
	{
		Map = map;
		(Y, X) = map.FindOnlyWhere(c => c == '^' || c == 'v' || c == '<' || c == '>');
		Direction = map[Y, X];
	}

	public IEnumerable<string> Traverse()
	{
		while (true)
		{
			char? r;
			try
			{
				r = SuggestRotation();
			}
			catch (NoRotationAvailableException)
			{
				yield break;
			}
			if (r is not null)
			{
				yield return $"{r}";
				ApplyRotation((char)r);
			}
			yield return GoForward().ToString();
		}
	}
}
