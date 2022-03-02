using System;
using System.Runtime.Serialization;

namespace AdventOfCode.Year2019.Day07;

public partial class IntcodeMachine
{
	[Serializable]
	public class IntcodeMachineHaltException : Exception
	{
		public IntcodeMachineHaltException() { }
		public IntcodeMachineHaltException(string message) : base(message) { }
		public IntcodeMachineHaltException(string message, Exception inner) : base(message, inner) { }
		protected IntcodeMachineHaltException(
			SerializationInfo info,
			StreamingContext context) : base(info, context) { }
	}

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

	[Serializable]
	public class IntcodeMachineInvalidOperationModeException : Exception
	{
		public IntcodeMachineInvalidOperationModeException() { }
		public IntcodeMachineInvalidOperationModeException(string message) : base(message) { }
		public IntcodeMachineInvalidOperationModeException(string message, Exception inner) : base(message, inner) { }
		protected IntcodeMachineInvalidOperationModeException(
			SerializationInfo info,
			StreamingContext context) : base(info, context) { }
	}
}
