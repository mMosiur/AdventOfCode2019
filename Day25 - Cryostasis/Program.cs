using AdventOfCode.Year2019.Day25;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

try
{
	string filepath = args.Length switch
	{
		0 => DEFAULT_INPUT_FILEPATH,
		1 => args[0],
		_ => throw new ApplicationException(
			$"Program was called with too many arguments. Proper usage: \"dotnet run [<input filepath>]\"."
		)
	};

	var solver = new Day25Solver(filepath);

	Console.Write("Part 1: ");
	string part1 = solver.SolvePart1();
	Console.WriteLine(part1);
}
catch (FileNotFoundException e)
{
	ConsoleColor previousColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine(e.Message);
	Console.ForegroundColor = previousColor;
	Environment.Exit(1);
}
catch (ApplicationException e)
{
	ConsoleColor previousColor = Console.ForegroundColor;
	Console.ForegroundColor = ConsoleColor.Red;
	Console.Error.WriteLine($"Error: {e.Message}");
	Console.ForegroundColor = previousColor;
	Environment.Exit(1);
}
