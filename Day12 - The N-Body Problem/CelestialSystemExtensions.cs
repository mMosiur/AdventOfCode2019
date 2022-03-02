using System;
using System.Linq;

namespace AdventOfCode.Year2019.Day12;

public static class CelestialSystemExtensions
{
	public static int[] GetXAxisState(this CelestialSystem system)
	{
		return system.Moons
			.Select(m => HashCode.Combine(m.Position.X, m.Velocity.X))
			.ToArray();
	}

	public static int[] GetYAxisState(this CelestialSystem system)
	{
		return system.Moons
			.Select(m => HashCode.Combine(m.Position.Y, m.Velocity.Y))
			.ToArray();
	}

	public static int[] GetZAxisState(this CelestialSystem system)
	{
		return system.Moons
			.Select(m => HashCode.Combine(m.Position.Z, m.Velocity.Z))
			.ToArray();
	}
}
