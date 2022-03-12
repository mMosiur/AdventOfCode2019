using System;
using System.IO;

namespace AdventOfCode.Year2019.Day19;

public class OutputInstructionExecutedEventArgs : EventArgs
{
	public long OutputValue { get; }
	public Stream OutputStream { get; }
	public OutputInstructionExecutedEventArgs(long outputValue, Stream outputStream)
	{
		OutputValue = outputValue;
		OutputStream = outputStream;
	}
}
