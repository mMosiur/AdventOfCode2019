namespace Day02;

internal partial class IntcodeMachine
{
	internal abstract class Instruction
	{
		public static Instruction GetInstructionAt(int[] program, int instructionPointer)
		{
			Opcode opcode = (Opcode)program[instructionPointer];
			return opcode switch
			{
				Opcode.Addition => new AdditionInstruction(program, instructionPointer),
				Opcode.Multiplication => new MultiplicationInstruction(program, instructionPointer),
				Opcode.Halt => new HaltInstruction(program, instructionPointer),
				_ => throw new IntcodeMachineInvalidInstructionException()
			};
		}

		public abstract int[] Parameters { get; }
		public int ParameterCount => Parameters?.Length ?? 0;
		public abstract Opcode Opcode { get; }
		public abstract int Execute();
	}
}
