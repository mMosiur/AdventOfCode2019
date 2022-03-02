using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day06;

public class Day06Solver : DaySolver
{
	private readonly IReadOnlyDictionary<string, CelestialBody> _bodies;

	public Day06Solver(string inputFilePath) : base(inputFilePath)
	{
		_bodies = GenerateCelestialBodiesDictionary();
	}

	private CelestialBody GetClosestIndirectOrbitedBody(CelestialBody body1, CelestialBody body2)
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

	private int GetDistanceBetweenBodies(CelestialBody orbited, CelestialBody orbiting)
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

	private IReadOnlyDictionary<string, CelestialBody> GenerateCelestialBodiesDictionary()
	{
		Dictionary<string, CelestialBody> bodies = new();
		foreach (string line in InputLines)
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
		return bodies;
	}

	public override string SolvePart1()
	{
		int totalOrbits = _bodies.Values.Sum(b => b.DistanceToCenter);
		return totalOrbits.ToString();
	}

	public override string SolvePart2()
	{
		CelestialBody youBody = _bodies["YOU"];
		CelestialBody santaBody = _bodies["SAN"];
		CelestialBody closestParent = GetClosestIndirectOrbitedBody(youBody, santaBody);
		int distance = GetDistanceBetweenBodies(closestParent, youBody) + GetDistanceBetweenBodies(closestParent, santaBody);
		return distance.ToString();
	}
}
