namespace Day02
{
	partial class IntcodeMachine
	{
		public class MultiplicationInstruction : Instruction
		{
			private int[] _memory;
			private int _instructionPointer;
			private int[] _parameters;
			public MultiplicationInstruction(int[] program, int instructionPointer)
			{
				if(program[instructionPointer] != (int)Opcode.Multiplication)
				{
					throw new IntcodeMachineInvalidInstructionException();
				}
				_memory = program;
				_instructionPointer = instructionPointer;
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
}
