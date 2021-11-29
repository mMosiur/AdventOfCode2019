namespace Day02;

internal partial class IntcodeMachine
{
	internal class MultiplicationInstruction : Instruction
	{
		private readonly int[] _memory;
		private readonly int[] _parameters;

		public MultiplicationInstruction(int[] program, int instructionPointer)
		{
			if (program[instructionPointer] != (int)Opcode.Multiplication)
			{
				throw new IntcodeMachineInvalidInstructionException();
			}
			_memory = program;
			_parameters = new int[3];
			for (int i = 0; i < 3; i++)
			{
				_parameters[i] = program[instructionPointer + i + 1];
			}
		}

		public override int[] Parameters => _parameters;

		public override Opcode Opcode => Opcode.Multiplication;

		public override int Execute()
		{
			_memory[Parameters[2]] = _memory[Parameters[0]] * _memory[Parameters[1]];
			return ParameterCount + 1;
		}
	}
}
