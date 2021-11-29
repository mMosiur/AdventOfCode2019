using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06;

internal class Program
{
	private const string DEFAULT_INPUT_FILEPATH = "input.txt";

	private static CelestialBody GetClosestIndirectOrbitedBody(CelestialBody body1, CelestialBody body2)
	{
		CelestialBody[] path = body1.GetPathToCenter().ToArray();
		foreach (CelestialBody b in body2.GetPathToCenter())
		{
			int index = Array.IndexOf(path, b);
			if (index >= 0)
			{
				return b;
			}
		}
		throw new InvalidDataException("There is no indirect orbited body between these two bodies.");
	}

	private static int GetDistanceBetweenBodies(CelestialBody orbited, CelestialBody orbiting)
	{
		int distance = 0;
		CelestialBody? current = orbiting.OrbitedBody;
		while (current is not null && !ReferenceEquals(orbited, current))
		{
			distance++;
			current = current.OrbitedBody;
		}
		return distance;
	}

	private static void Main(string[] args)
	{
		string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
		Dictionary<string, CelestialBody> bodies = new();
		foreach (string line in File.ReadLines(filepath))
		{
			(string orbitedBodyName, string orbitingBodyName) = line.Split(')').SplitIntoTwo();
			if (!bodies.TryGetValue(orbitedBodyName, out CelestialBody? orbitedBody))
			{
				orbitedBody = new CelestialBody(orbitedBodyName);
				bodies.Add(orbitedBodyName, orbitedBody);
			}
			if (!bodies.TryGetValue(orbitingBodyName, out CelestialBody? orbitingBody))
			{
				orbitingBody = new CelestialBody(orbitingBodyName);
				bodies.Add(orbitingBodyName, orbitingBody);
			}
			orbitingBody.OrbitedBody = orbitedBody;
		}
		Console.Write("Part 1: ");
		int totalOrbits = bodies.Values.Sum(b => b.DistanceToCenter);
		Console.WriteLine(totalOrbits);

		CelestialBody youBody = bodies["YOU"];
		CelestialBody santaBody = bodies["SAN"];
		CelestialBody closestParent = GetClosestIndirectOrbitedBody(youBody, santaBody);
		Console.Write("Part 2: ");
		int distance = GetDistanceBetweenBodies(closestParent, youBody) + GetDistanceBetweenBodies(closestParent, santaBody);
		Console.WriteLine(distance);
	}
}
