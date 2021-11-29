namespace Day11;

public partial class IntcodeMachine
{

	internal abstract class IntcodeMachineInstruction
	{
		public abstract InstructionParameter[] Parameters { get; }
		public int ParameterCount => Parameters?.Length ?? 0;
		public abstract Opcode Opcode { get; }
		public abstract int Execute(ref int relativeBase);
	}
}
