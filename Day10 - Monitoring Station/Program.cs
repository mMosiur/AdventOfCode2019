using System;
using System.IO;
using System.Linq;

using Day10;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
string[] lines = File.ReadAllLines(filepath);
Map map = Map.Parse(lines);

Console.Write("Part 1: ");
Asteroid asteroidWithMaxVisibleAsteroids = map.Asteroids.First();
int maxVisibleAsteroidCount = map.CountAsteroidsVisibleFrom(asteroidWithMaxVisibleAsteroids);
foreach (Asteroid asteroid in map.Asteroids.Skip(1))
{
	int count = map.CountAsteroidsVisibleFrom(asteroid);
	if (count > maxVisibleAsteroidCount)
	{
		maxVisibleAsteroidCount = count;
		asteroidWithMaxVisibleAsteroids = asteroid;
	}
}
Console.WriteLine(maxVisibleAsteroidCount);

Console.Write("Part 2: ");
Laser laser = new(map, asteroidWithMaxVisibleAsteroids);
Asteroid asteroid200 = laser.VaporizedAsteroids().ElementAt(199);
int result = asteroid200.Column * 100 + asteroid200.Row;
Console.WriteLine(result);
