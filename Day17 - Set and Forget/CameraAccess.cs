using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17;

public class AftScaffoldingControlAndInformationInterface
{
	private readonly long[] _program;
	public AftScaffoldingControlAndInformationInterface(long[] program)
	{
		_program = program;
	}

	private IList<IList<char>> GetJaggedCameraOutput()
	{
		IntcodeMachine machine = new(_program);
		IList<IList<char>> jaggedMap = new List<IList<char>>
			{
				new List<char>()
			};
		try
		{
			while (true)
			{
				long output = machine.RunToFirstOutput();
				if (output == 10)
				{
					jaggedMap.Add(new List<char>());
				}
				else
				{
					jaggedMap[^1].Add((char)output);
				}
			}
		}
		catch (IntcodeMachine.IntcodeMachineHaltException)
		{ }
		return jaggedMap;
	}

	private static char[,] JaggedListsToArray(IList<IList<char>> jaggedList)
	{
		int width = jaggedList[0].Count;
		if (!jaggedList.All(l => l.Count == width || l.Count == 0))
		{
			throw new Exception();
		}
		IEnumerable<IList<char>> nonEmpty = jaggedList.Where(l => l.Count > 0);
		int height = nonEmpty.Count();
		char[,] result = new char[height, width];
		foreach ((IList<char> l, int i) in nonEmpty.Select((l, i) => (l, i)))
		{
			for (int j = 0; j < width; j++)
			{
				result[i, j] = l[j];
			}
		}
		return result;
	}

	public char[,] GetCameraOutput() => JaggedListsToArray(GetJaggedCameraOutput());
}
