using System;
using System.Runtime.Serialization;

namespace AdventOfCode.Year2019.Day13;

public partial class IntcodeMachine
{
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
