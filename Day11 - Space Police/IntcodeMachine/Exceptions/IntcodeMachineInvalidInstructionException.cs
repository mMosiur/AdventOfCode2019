using System;
using System.Runtime.Serialization;

namespace AdventOfCode.Year2019.Day11;

public partial class IntcodeMachine
{
	[Serializable]
	public class IntcodeMachineInvalidInstructionException : Exception
	{
		public IntcodeMachineInvalidInstructionException() { }
		public IntcodeMachineInvalidInstructionException(string message) : base(message) { }
		public IntcodeMachineInvalidInstructionException(string message, Exception inner) : base(message, inner) { }
		protected IntcodeMachineInvalidInstructionException(
			SerializationInfo info,
			StreamingContext context) : base(info, context) { }
	}
}
