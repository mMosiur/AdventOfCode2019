using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
	class Program
	{
		private const string INPUT_FILEPATH = "input.txt";

		static void Main()
		{
			IEnumerable<Moon> moons = File.ReadAllLines(INPUT_FILEPATH).Select(s => new Moon(Position3D.Parse(s)));

			Console.Write("Part 1: ");
			CelestialSystem system = new(moons);
			system.Simulate(1000);
			int totalEnergy = system.TotalEnergy;
			Console.WriteLine(totalEnergy);

			Console.Write("Part 2: ");
			system = new(moons);
			HashSet<int[]> xAxisStates = new(new ArrayComparer<int>());
			HashSet<int[]> yAxisStates = new(new ArrayComparer<int>());
			HashSet<int[]> zAxisStates = new(new ArrayComparer<int>());
			bool addedX = true;
			bool addedY = true;
			bool addedZ = true;
			while (addedX || addedY || addedZ)
			{
				system.Step();
				addedX = xAxisStates.Add(system.GetXAxisState());
				addedY = yAxisStates.Add(system.GetYAxisState());
				addedZ = zAxisStates.Add(system.GetZAxisState());
			}
			ulong result = Numerical.LCM(xAxisStates.Count, yAxisStates.Count, zAxisStates.Count);
			Console.WriteLine(result);
		}
	}
}
