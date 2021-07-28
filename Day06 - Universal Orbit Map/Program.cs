using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
	class Program
    {
		const string INPUT_FILEPATH = "input.txt";

		static CelestialBody GetClosesIndirectOrbitedBody(CelestialBody body1, CelestialBody body2)
		{
			CelestialBody[] path = body1.GetPathToCenter().ToArray();
			foreach(CelestialBody b in body2.GetPathToCenter())
			{
				int index = Array.IndexOf(path, b);
				if(index >= 0)
				{
					return b;
				}
			}
			return null;
		}

		static int GetDistanceBetweenBodies(CelestialBody orbited, CelestialBody orbiting)
		{
			int distance = 0;
			CelestialBody current = orbiting.OrbitedBody;
			while(current is not null && !ReferenceEquals(orbited, current))
			{
				distance++;
				current = current.OrbitedBody;
			}
			return distance;
		}

		static void Main()
        {
			Dictionary<string, CelestialBody> bodies = new();
			foreach(string line in File.ReadLines(INPUT_FILEPATH))
			{
				(string orbitedBodyName, string orbitingBodyName) = line.Split(')').SplitIntoTwo();
				if(!bodies.TryGetValue(orbitedBodyName, out CelestialBody orbitedBody))
				{
					orbitedBody = new CelestialBody(orbitedBodyName);
					bodies.Add(orbitedBodyName, orbitedBody);
				}
				if(!bodies.TryGetValue(orbitingBodyName, out CelestialBody orbitingBody))
				{
					orbitingBody = new CelestialBody(orbitingBodyName);
					bodies.Add(orbitingBodyName, orbitingBody);
				}
				orbitingBody.OrbitedBody = orbitedBody;
			}
			int totalOrbits = bodies.Values.Sum(b => b.DistanceToCenter);
			Console.WriteLine($"Part 1: {totalOrbits}");

			CelestialBody youBody = bodies["YOU"];
			CelestialBody santaBody = bodies["SAN"];
			CelestialBody closestParent = GetClosesIndirectOrbitedBody(youBody, santaBody);
			int distance = GetDistanceBetweenBodies(closestParent, youBody) + GetDistanceBetweenBodies(closestParent, santaBody);
			Console.WriteLine($"Part 2: {distance}");
		}
    }
}
