using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day10;

public class Day10Solver : DaySolver
{
	private readonly Map _map;

	private readonly Lazy<(Asteroid Asteroid, int Count)> _maxVisibleAsteroids;
	public (Asteroid Asteroid, int Count) MaxVisibleAsteroids => _maxVisibleAsteroids.Value;

	public Day10Solver(string inputFilePath) : base(inputFilePath)
	{
		_map = Map.Parse(InputLines.ToArray());
		_maxVisibleAsteroids = new Lazy<(Asteroid, int)>(FindMaxVisibleAsteroids);
	}

	private (Asteroid, int) FindMaxVisibleAsteroids()
	{
		Asteroid asteroidWithMaxVisibleAsteroids = _map.Asteroids.First();
		int maxVisibleAsteroidCount = _map.CountAsteroidsVisibleFrom(asteroidWithMaxVisibleAsteroids);
		foreach (Asteroid asteroid in _map.Asteroids.Skip(1))
		{
			int count = _map.CountAsteroidsVisibleFrom(asteroid);
			if (count > maxVisibleAsteroidCount)
			{
				maxVisibleAsteroidCount = count;
				asteroidWithMaxVisibleAsteroids = asteroid;
			}
		}
		return (asteroidWithMaxVisibleAsteroids, maxVisibleAsteroidCount);
	}

	public override string SolvePart1()
	{
		return MaxVisibleAsteroids.Count.ToString();
	}

	public override string SolvePart2()
	{
		Laser laser = new(_map, MaxVisibleAsteroids.Asteroid);
		Asteroid asteroid200 = laser.VaporizedAsteroids().ElementAt(199);
		int result = asteroid200.Column * 100 + asteroid200.Row;
		return result.ToString();
	}
}
