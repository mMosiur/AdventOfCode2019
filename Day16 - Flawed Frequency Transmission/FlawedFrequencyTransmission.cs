using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16;

public static class FlawedFrequencyTransmission
{
	private static IEnumerable<short> NonShiftedInfinitePattern(int elementRepetitions)
	{
		if (elementRepetitions <= 0)
		{
			if (elementRepetitions == 0) yield break;
			else throw new ArgumentOutOfRangeException(nameof(elementRepetitions));
		}
		short[] basePattern = new short[] { 0, 1, 0, -1 };
		while (true)
		{
			foreach (short el in basePattern)
			{
				for (int i = 0; i < elementRepetitions; i++)
				{
					yield return el;
				}
			}
		}
	}

	private static IEnumerable<short> InfinitePattern(int elementRepetitions) => NonShiftedInfinitePattern(elementRepetitions).Skip(1);

	public static IEnumerable<short> GetPattern(int elementRepetitions, int elementCount)
	{
		if (elementRepetitions < 1) throw new ArgumentOutOfRangeException(nameof(elementRepetitions));
		if (elementCount < 0) throw new ArgumentOutOfRangeException(nameof(elementCount));
		return InfinitePattern(elementRepetitions).Take(elementCount);
	}

	public static IList<short> CalculateFullPhase(IList<short> input)
	{
		return input.Select(
			(el, i) =>
			{
				IEnumerable<short> pattern = GetPattern(i + 1, input.Count);
				return Numerical.GetLastDigit(Enumerable.Zip(input, pattern, (f, s) => f * s).Sum());
			}
		).ToList();
	}

	public static IList<short> CalculateFullPhases(IList<short> input, int phaseCount)
	{
		IList<short> result = input;
		for (int i = 0; i < phaseCount; i++)
		{
			result = CalculateFullPhase(result);
		}
		return result;
	}

	public static IList<short> CalculatePartiallyPhase(IList<short> input)
	{
		long partialSum = input.Select(n => (long)n).Sum();
		return input.Select(
			el =>
			{
				short newEl = Numerical.GetLastDigit(partialSum);
				partialSum -= el;
				return newEl;
			}
		).ToList();
	}

	public static IList<short> CalculatePartiallyPhases(IList<short> input, int phaseCount)
	{
		IList<short> result = input;
		for (int i = 0; i < phaseCount; i++)
		{
			result = CalculatePartiallyPhase(result);
		}
		return result;
	}
}
