using System;

namespace AdventOfCode.Year2019.Day17;

[Serializable]
public class NoRotationAvailableException : Exception
{
	public NoRotationAvailableException() { }
	public NoRotationAvailableException(string message) : base(message) { }
	public NoRotationAvailableException(string message, Exception inner) : base(message, inner) { }
	protected NoRotationAvailableException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
