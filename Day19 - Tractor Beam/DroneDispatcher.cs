using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19;

public class DroneDispatcher
{
	private readonly IntcodeMachine _machine;
	private readonly IDictionary<Point, bool> _cache;

	public bool this[int X, int Y] => this[new Point(X, Y)];

	public bool this[Point point]
	{
		get
		{
			if (!_cache.TryGetValue(point, out bool value))
			{
				value = DispatchTo(point);
				_cache.Add(point, value);
			}
			return value;
		}
	}

	private bool DispatchTo(Point p) => DispatchTo(p.X, p.Y);

	private bool DispatchTo(int x, int y)
	{
		if (x < 0) throw new ArgumentOutOfRangeException(nameof(x), "X position must be non-negative.");
		if (y < 0) throw new ArgumentOutOfRangeException(nameof(y), "Y position must be non-negative.");
		_machine.Reset();
		_machine.Input.Clear();
		_machine.Input.Enqueue(x);
		_machine.Input.Enqueue(y);
		long output = _machine.RunYieldingOutput().First();
		return output != 0;
	}

	public DroneDispatcher(long[] program)
	{
		_machine = new IntcodeMachine(program);
		_cache = new Dictionary<Point, bool>();
	}

	public int CalculateTractorBeamArea(int maxDistanceX, int maxDistanceY)
	{
		int area = 0;
		for (int x = 0; x < maxDistanceX; x++)
		{
			for (int y = 0; y < maxDistanceY; y++)
			{
				area += this[x, y] ? 1 : 0;
			}
		}
		return area;
	}

	private Point NextRowStart(Point point, int maxWidthToCheck = 100, int maxHeightToCheck = 3)
	{
		for (int newX = point.X + 1; newX <= point.X + maxHeightToCheck; newX++)
		{
			for (int newY = point.Y; newY < point.Y + maxWidthToCheck; newY++)
			{
				if (this[newX, newY])
				{
					return new Point(newX, newY);
				}
			}
		}
		throw new Exception($"Next row beginning not found from point {point} in rectangle {maxWidthToCheck}x{maxHeightToCheck}.");
	}

	private bool ShipFitsInTractorBeam(Point bottomLeftPoint, int shipWidth, int shipHeight)
	{
		return this[bottomLeftPoint.Moved(xOffset: -shipHeight + 1, yOffset: shipWidth - 1)]
			&& this[bottomLeftPoint.Moved(xOffset: -shipHeight + 1)]
			&& this[bottomLeftPoint.Moved(yOffset: shipWidth - 1)];
	}

	public Point FindPointWhereShipFits(int shipWidth, int shipHeight)
	{
		Point point = new(0, 0);
		while (point.X < shipHeight)
		{
			point = NextRowStart(point);
		}
		while (true)
		{
			if (ShipFitsInTractorBeam(point, shipWidth, shipHeight))
			{
				return point.Moved(xOffset: -shipHeight + 1);
			}
			point = NextRowStart(point);
		}
	}
}
