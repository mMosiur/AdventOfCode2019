using System;
using System.Runtime.Serialization;

namespace Day02;

internal partial class IntcodeMachine
{
	[Serializable]
	internal class IntcodeMachineHaltException : Exception
	{
		public IntcodeMachineHaltException() { }
		public IntcodeMachineHaltException(string message) : base(message) { }
		public IntcodeMachineHaltException(string message, Exception inner) : base(message, inner) { }
		protected IntcodeMachineHaltException(
			SerializationInfo info,
			StreamingContext context) : base(info, context) { }
	}

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
