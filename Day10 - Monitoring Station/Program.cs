using System;
using System.IO;
using System.Linq;

namespace Day10
{

	class Program
	{
		const string INPUT_FILEPATH = "input.txt";

		static void Main()
		{
			string[] lines = File.ReadAllLines(INPUT_FILEPATH);
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
		}
	}
}
