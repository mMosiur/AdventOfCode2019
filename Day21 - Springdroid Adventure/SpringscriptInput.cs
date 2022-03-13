namespace AdventOfCode.Year2019.Day21;

public class SpringscriptInput
{
	private bool _fromFile;
	private readonly Queue<long> _inputBuffer = new();

	public bool EchoFileContent { get; set; }

	public SpringscriptInput(string springscriptFilepath, bool echoFileContent = false)
	{
		_fromFile = true;
		EchoFileContent = echoFileContent;
		string[] lines = File.ReadAllLines(springscriptFilepath);
		IEnumerable<string> meaningfulLines = lines
			.Select(line => line.Split('#')[0].Trim()) // Remove comments (everything after a '#')
			.Where(line => !string.IsNullOrWhiteSpace(line)); // Remove empty lines
		foreach (string line in meaningfulLines)
		{
			foreach (char c in line.Trim())
			{
				_inputBuffer.Enqueue(c);
			}
			_inputBuffer.Enqueue('\n');
		}
	}

	public SpringscriptInput()
	{
		_fromFile = false;
	}

	private long GetNextCharacterFromFile()
	{
		if (!_fromFile) throw new InvalidOperationException("Cannot get next character from file when not reading from file.");
		long character = _inputBuffer.Dequeue();
		if (EchoFileContent)
		{
			Console.Write((char)character);
		}
		return character;
	}

	private long GetNextCharacterFromConsole()
	{
		if (_fromFile) throw new InvalidOperationException("Cannot get next character from console when reading from file.");
		if (_inputBuffer.Count == 0)
		{
			string line = string.Empty;
			while (string.IsNullOrEmpty(line))
			{
				line = Console.ReadLine()!;
			}
			line.ReplaceLineEndings("\n");
			foreach (char c in line)
			{
				_inputBuffer.Enqueue((long)c);
			}
			if (!line.EndsWith('\n'))
			{
				_inputBuffer.Enqueue('\n');
			}
		}
		return _inputBuffer.Dequeue();
	}

	public long GetNextCharacter() => _fromFile ? GetNextCharacterFromFile() : GetNextCharacterFromConsole();
}
