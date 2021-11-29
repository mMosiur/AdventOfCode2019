using System;
using System.Runtime.Serialization;

namespace Day11;

public partial class IntcodeMachine
{
	[Serializable]
	internal class IntcodeMachineInvalidInstructionException : Exception
	{
		public IntcodeMachineInvalidInstructionException() { }
		public IntcodeMachineInvalidInstructionException(string message) : base(message) { }
		public IntcodeMachineInvalidInstructionException(string message, Exception inner) : base(message, inner) { }
		protected IntcodeMachineInvalidInstructionException(
			SerializationInfo info,
			StreamingContext context) : base(info, context) { }
	}
}
