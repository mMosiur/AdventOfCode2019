using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day24;

public class Day24Solver : DaySolver
{

	public Day24Solver(string inputFilePath) : base(inputFilePath)
	{
	}

	public override string SolvePart1()
	{
		var area = SimpleBugGridArea.Parse(InputLines.ToArray());
		HashSet<ulong> fingerprints = new()
		{
			area.CalculateCurrentLayoutFingerprint()
		};
		while (true)
		{
			area.NextMinute();
			ulong fingerprint = area.CalculateCurrentLayoutFingerprint();
			if (fingerprints.Contains(fingerprint))
			{
				ulong biodiversityRating = area.CalculateCurrentLayoutBiodiversityRating();
				return biodiversityRating.ToString();
			}
			fingerprints.Add(fingerprint);
		}
	}

	public override string SolvePart2()
	{
		const int NumberOfMinutes = 200;
		var area = RecursiveBugGridArea.Parse(InputLines.ToArray());
		for (int i = 0; i < NumberOfMinutes; i++)
		{
			area.NextMinute();
		}
		return area.BugCount.ToString();
	}
}
