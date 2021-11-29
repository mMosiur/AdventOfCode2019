namespace Day07;

public partial class IntcodeMachine
{
	public class InputInstruction : Instruction
	{
		private readonly InstructionParameter[] _parameters;
		private readonly int[] _memory;
		private readonly int _input;

		public InputInstruction(int[] program, int instructionPointer, int input)
		{
			int instruction = program[instructionPointer];
			const Opcode expectedOpcode = Opcode.Input;
			Opcode opcode = GetOpcode(instruction);
			if (opcode != expectedOpcode)
			{
				throw new IntcodeMachineInvalidInstructionException($"Unexpected opcode {opcode}: expected {expectedOpcode} instead.");
			}
			_memory = program;
			InstructionParameterMode[] parameterModes = InstructionParameter.GetParameterModes(instruction);
			_parameters = new InstructionParameter[1];
			for (int i = 0; i < 1; i++)
			{
				int value = program[instructionPointer + i + 1];
				_parameters[i] = new InstructionParameter(parameterModes[i], value);
			}
			_input = input;
		}

		public override InstructionParameter[] Parameters => _parameters;

		public override Opcode Opcode => Opcode.Input;

		public override int Execute()
		{
			_memory[Parameters[0].RawValue] = _input;
			return ParameterCount + 1;
		}
	}
}
