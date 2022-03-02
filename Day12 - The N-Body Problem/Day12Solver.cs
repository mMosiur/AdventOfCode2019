using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day12;

public class Day12Solver : DaySolver
{
	private IEnumerable<Moon> moons;

	public Day12Solver(string inputFilePath) : base(inputFilePath)
	{
		moons = InputLines.Select(s => new Moon(Position3D.Parse(s)));
	}

	public override string SolvePart1()
	{
		CelestialSystem system = new(moons);
		system.Simulate(1000);
		int totalEnergy = system.TotalEnergy;
		return totalEnergy.ToString();
	}

	public override string SolvePart2()
	{
		CelestialSystem system = new(moons);
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
		ulong result = Numerical.LCM(
			(ulong)xAxisStates.Count,
			(ulong)yAxisStates.Count,
			(ulong)zAxisStates.Count
		);
		return result.ToString();
	}
}
