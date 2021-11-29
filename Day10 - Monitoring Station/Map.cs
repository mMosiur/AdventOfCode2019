using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day10;

public class Map : IEnumerable<MapSpot>
{
	private readonly MapSpot[,] _map;

	public Map(MapSpot[,] mapGrid)
	{
		_map = mapGrid ?? throw new ArgumentNullException(nameof(mapGrid));
	}

	public static Map Parse(string[] lines)
	{
		MapSpot[,] mapGrid = new MapSpot[lines[0].Length, lines.Length];
		for (int r = 0; r < lines.Length; r++)
		{
			for (int c = 0; c < lines[r].Length; c++)
			{
				MapSpotType type = (MapSpotType)lines[r][c];
				mapGrid[c, r] = MapSpot.NewMapSpot(type, c, r);
			}
		}
		return new Map(mapGrid);
	}

	public MapSpot this[int column, int row]
	{
		get => _map[column, row];
	}

	public int ColumnCount => _map.GetLength(0);
	public int RowCount => _map.GetLength(1);

	private IEnumerable<string> Lines
	{
		get
		{
			for (int r = 0; r < _map.GetLength(1); r++)
			{
				yield return string.Concat(Enumerable.Range(0, _map.GetLength(0)).Select(c => (char)_map[c, r].Type));
			}
		}
	}

	public override string ToString()
	{
		return string.Join(Environment.NewLine, Lines);
	}

	public IEnumerator<MapSpot> GetEnumerator()
	{
		return _map.Cast<MapSpot>().GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public IEnumerable<Asteroid> Asteroids => this.Where(spot => spot is Asteroid).Cast<Asteroid>();

	public int CountAsteroidsVisibleFrom(MapSpot mapSpot)
	{
		if (mapSpot is null)
		{
			throw new ArgumentNullException(nameof(mapSpot));
		}
		if (!ReferenceEquals(mapSpot, _map[mapSpot.Column, mapSpot.Row]))
		{
			throw new InvalidOperationException("Given mapSpot is not a part of given map.");
		}
		HashSet<double> directionsBlockedByAsteroids = new();
		foreach (Asteroid asteroid in Asteroids.Where(a => !ReferenceEquals(mapSpot, a)))
		{
			int x = asteroid.Column - mapSpot.Column;
			int y = asteroid.Row - mapSpot.Row;
			double direction = Math.Atan2(y, x);
			_ = directionsBlockedByAsteroids.Add(direction);
		}
		return directionsBlockedByAsteroids.Count;
	}

	public SortedDictionary<double, SortedList<double, Asteroid>> GetRadialMap(MapSpot centerSpot)
	{
		if (centerSpot is null)
		{
			throw new ArgumentNullException(nameof(centerSpot));
		}
		if (!ReferenceEquals(centerSpot, _map[centerSpot.Column, centerSpot.Row]))
		{
			throw new InvalidOperationException("Given center spot is not a part of given map.");
		}
		SortedDictionary<double, SortedList<double, Asteroid>> radialMap = new();
		foreach (Asteroid asteroid in Asteroids.Where(a => !ReferenceEquals(centerSpot, a)))
		{
			int x = asteroid.Column - centerSpot.Column;
			int y = asteroid.Row - centerSpot.Row;
			// Calculating the direction this way guarantees that rotating clockwise starting pointed directly up
			// direction values will be in ascending order from -PI to +PI.
			double direction = -Math.Atan2(x, y);
			double distance = Math.Sqrt((x * x) + (y * y));
			if (!radialMap.TryGetValue(direction, out SortedList<double, Asteroid>? asteroidsInDirection))
			{
				asteroidsInDirection = new SortedList<double, Asteroid>(1);
				radialMap.Add(direction, asteroidsInDirection);
			}
			asteroidsInDirection.Add(distance, asteroid);
		}
		return radialMap;
	}
}
