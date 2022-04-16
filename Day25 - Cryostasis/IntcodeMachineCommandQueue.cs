namespace AdventOfCode.Year2019.Day25;

public class IntcodeMachineCommandQueue
{
	private Queue<long> _inputQueue = new();

	public void EnqueueCommandsFrom(string filename)
	{
		IEnumerable<string> commands = File.ReadLines(filename)
			.Where(line => !string.IsNullOrWhiteSpace(line)) // Ignore empty lines
			.Select(line => line.Trim()) // Trim lines
			.Where(line => !line.StartsWith("#")); // Ignore comments
		EnqueueCommands(commands);
	}

	public void EnqueueCommands(IEnumerable<string> commands)
	{
		foreach (string command in commands)
		{
			EnqueueCommand(command);
		}
	}

	public void EnqueueCommand(string command)
	{
		foreach (char c in command)
		{
			_inputQueue.Enqueue((long)c);
		}
		_inputQueue.Enqueue((long)'\n');
	}

	public long GetInput()
	{
		if (_inputQueue.Count == 0)
		{
			string line = Console.ReadLine() ?? "";
			while (string.IsNullOrWhiteSpace(line))
			{
				line = Console.ReadLine() ?? "";
			}
			foreach (char c in line)
			{
				_inputQueue.Enqueue((long)c);
			}
			_inputQueue.Enqueue((long)'\n');
		}
		return _inputQueue.Dequeue();
	}
}
