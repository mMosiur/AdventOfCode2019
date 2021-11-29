using System;
using System.Collections.Generic;

namespace Day06;

public static class ExtensionMethods
{
	public static (T First, T Second) SplitIntoTwo<T>(this T[] array)
	{
		if (array is null)
		{
			throw new ArgumentNullException(nameof(array));
		}
		const int expectedLength = 2;
		if (array.Length != expectedLength)
		{
			throw new ArgumentException($"Provided array had {array.Length} elements instead of expected {expectedLength}");
		}
		return (array[0], array[1]);
	}

	public static IEnumerable<CelestialBody> GetPathToCenter(this CelestialBody body)
	{
		CelestialBody? current = body;
		while (current is not null)
		{
			yield return current;
			current = current.OrbitedBody;
		}
	}
}
