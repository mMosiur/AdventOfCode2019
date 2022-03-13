using System.Text;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day21;

public class Day21Solver : DaySolver
{
	private readonly long[] _program;
	private readonly bool _debugToConsole;

	public Day21Solver(string inputFilePath, bool debugToConsole = false) : base(inputFilePath)
	{
		_debugToConsole = debugToConsole;
		_program = Input
			.Trim().Split(',', StringSplitOptions.TrimEntries)
			.Select(long.Parse).ToArray();
	}

	public void DebugSpringdroid(SpringscriptInput springscriptinput)
	{
		AsciiOutputStream outputStream = new(Console.OpenStandardOutput());
		IntcodeMachine machine = new IntcodeMachine(
			_program,
			springscriptinput.GetNextCharacter,
			outputStream
			);
		machine.Run();
	}

	public long CheckHullDamageWithSpringdroid(SpringscriptInput springscriptInput)
	{
		MemoryStream outputStream = new MemoryStream();
		IntcodeMachine machine = new IntcodeMachine(
			_program,
			springscriptInput.GetNextCharacter,
			outputStream
			);
		machine.Run();
		string lastOutput = Encoding.ASCII
			.GetString(outputStream.ToArray())
			.Split('\n', StringSplitOptions.RemoveEmptyEntries)
			.Last();
		long hullDamage = long.Parse(lastOutput);
		return hullDamage;
	}

	public override string SolvePart1()
	{
		var input = new SpringscriptInput("springscript1.txt");
		if (_debugToConsole)
		{
			Console.WriteLine("Debug mode (no method return value, only direct console output):");
			input.EchoFileContent = true;
			DebugSpringdroid(input);
			return string.Empty;
		}
		long hullDamage = CheckHullDamageWithSpringdroid(input);
		return hullDamage.ToString();
	}

	public override string SolvePart2()
	{
		var input = new SpringscriptInput("springscript2.txt");
		if (_debugToConsole)
		{
			Console.WriteLine("Debug mode (no method return value, only direct console output):");
			input.EchoFileContent = true;
			DebugSpringdroid(input);
			return string.Empty;
		}
		long hullDamage = CheckHullDamageWithSpringdroid(input);
		return hullDamage.ToString();
	}
}
