using System.Text.RegularExpressions;

namespace AdventOfCode.Year2019.Day22;

public class ShuffleInstructionInterpreter
{
	private static readonly Regex _dealIntoNewStackRegex = new Regex(@"deal into new stack", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	private static readonly Regex _cutNRegex = new Regex(@"cut (-?\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
	private static readonly Regex _dealWithIncrementNRegex = new Regex(@"deal with increment (\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

	private readonly string[] _shuffleInstructions;

	public ShuffleInstructionInterpreter(IEnumerable<string> shuffleInstructions)
	{
		ArgumentNullException.ThrowIfNull(shuffleInstructions);
		_shuffleInstructions = shuffleInstructions.ToArray();
	}

	public void ExecuteInstructionsOn(ICardShuffler shuffler)
	{
		IEnumerable<string> instructions = _shuffleInstructions
			.Where(line => !string.IsNullOrWhiteSpace(line))
			.Select(line => line.Trim());
		foreach (string instruction in instructions)
		{
			Match? match = null;
			if (_dealIntoNewStackRegex.TryMatch(instruction, out match))
			{
				shuffler.DealIntoNewStack();
			}
			else if (_cutNRegex.TryMatch(instruction, out match))
			{
				int n = int.Parse(match.Groups[1].ValueSpan);
				shuffler.CutN(n);
			}
			else if (_dealWithIncrementNRegex.TryMatch(instruction, out match))
			{
				int n = int.Parse(match.Groups[1].ValueSpan);
				shuffler.DealWithIncrementN(n);
			}
			else
			{
				throw new InvalidOperationException($"Unknown instruction: {instruction}");
			}
		}
	}
}
