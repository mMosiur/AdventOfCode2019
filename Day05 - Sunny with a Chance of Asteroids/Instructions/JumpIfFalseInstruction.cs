namespace Day05
{
	partial class IntcodeMachine
	{
		public class JumpIfFalseInstruction : Instruction
		{
			private readonly int[] _memory;
			private readonly int _instructionPointer;
			private readonly InstructionParameter[] _parameters;

			public JumpIfFalseInstruction(int[] program, int instructionPointer)
			{
				_instructionPointer = instructionPointer;
				int instruction = program[instructionPointer];
				const Opcode expectedOpcode = Opcode.JumpIfFalse;
				Opcode opcode = GetOpcode(instruction);
				if (opcode != expectedOpcode)
				{
					throw new IntcodeMachineInvalidInstructionException($"Unexpected opcode {opcode}: expected {expectedOpcode} instead.");
				}
				_memory = program;
				InstructionParameterMode[] parameterModes = InstructionParameter.GetParameterModes(instruction);
				_parameters = new InstructionParameter[2];
				for (int i = 0; i < 2; i++)
				{
					int value = _memory[instructionPointer + i + 1];
					_parameters[i] = new InstructionParameter(parameterModes[i], value);
				}
			}

			public override InstructionParameter[] Parameters => _parameters;

			public override Opcode Opcode => Opcode.JumpIfFalse;

			public override int Execute()
			{
				if(Parameters[0].GetValue(_memory) == 0)
				{
					int offset = Parameters[1].GetValue(_memory) - _instructionPointer;
					return offset;
				}
				return ParameterCount + 1;
			}
		}
	}
}
