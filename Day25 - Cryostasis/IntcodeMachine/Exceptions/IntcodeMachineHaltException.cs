using System.Runtime.Serialization;

namespace AdventOfCode.Year2019.Day25;

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
}
