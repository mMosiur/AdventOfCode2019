using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day22;

/// <summary>
/// https://www.reddit.com/r/adventofcode/comments/ee0rqi/comment/fbnkaju/
/// </summary>
public class Day22Solver : DaySolver
{

	public Day22Solver(string inputFilePath) : base(inputFilePath)
	{
	}

	public override string SolvePart1()
	{
		const int NumberOfCards = 10007;
		const int CardToFind = 2019;

		var shuffler = new SimpleCardShuffler(NumberOfCards);
		var interpreter = new ShuffleInstructionInterpreter(InputLines);

		interpreter.ExecuteInstructionsOn(shuffler);

		long cardIndex = shuffler.GetCardIndex(CardToFind);

		return cardIndex.ToString();
	}

	public override string SolvePart2()
	{
		const long NumberOfCards = 119315717514047;
		const int CardToFind = 2020;
		const long NumberOfShuffles = 101741582076661;

		var shuffler = new SequenceFingerprintCardShuffler(NumberOfCards);
		var interpreter = new ShuffleInstructionInterpreter(InputLines);

		interpreter.ExecuteInstructionsOn(shuffler);
		long cardIndex = shuffler.GetCardIndex(CardToFind, NumberOfShuffles);

		return cardIndex.ToString();
	}
}
