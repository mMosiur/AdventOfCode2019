using System.Collections.Generic;
using System.Linq;

namespace Day12;

public class CelestialSystem
{
	public ICollection<Moon> Moons { get; }
	public CelestialSystem(IEnumerable<Moon> moons)
	{
		Moons = new List<Moon>(moons);
	}

	public int TotalEnergy => Moons.Sum(moon => moon.TotalEnergy);

	private void ApplyGravity()
	{
		foreach (Moon moon1 in Moons)
		{
			foreach (Moon moon2 in Moons)
			{
				if (ReferenceEquals(moon1, moon2))
				{
					continue;
				}
				Vector3D gravity = moon1.GravityTowards(moon2);
				moon1.ApplyGravity(gravity);
			}
		}
	}

	private void ApplyVelocity()
	{
		foreach (Moon moon in Moons)
		{
			moon.ApplyVelocity();
		}
	}

	public void Step()
	{
		ApplyGravity();
		ApplyVelocity();
	}

	public void Simulate(int steps)
	{
		for (int i = 0; i < steps; ++i)
		{
			Step();
		}
	}
}
