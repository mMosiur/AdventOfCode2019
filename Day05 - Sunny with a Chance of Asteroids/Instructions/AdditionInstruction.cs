namespace AdventOfCode.Year2019.Day05;

public partial class IntcodeMachine
{
	public class AdditionInstruction : Instruction
	{
		private readonly int[] _memory;
		private readonly InstructionParameter[] _parameters;
		public AdditionInstruction(int[] program, int instructionPointer)
		{
			int instruction = program[instructionPointer];
			const Opcode expectedOpcode = Opcode.Addition;
			Opcode opcode = GetOpcode(instruction);
			if (opcode != expectedOpcode)
			{
				throw new IntcodeMachineInvalidInstructionException($"Unexpected opcode {opcode}: expected {expectedOpcode} instead.");
			}
			_memory = program;
			InstructionParameterMode[] parameterModes = InstructionParameter.GetParameterModes(instruction);
			_parameters = new InstructionParameter[3];
			for (int i = 0; i < 3; i++)
			{
				int value = _memory[instructionPointer + i + 1];
				_parameters[i] = new InstructionParameter(parameterModes[i], value);
			}
		}

		public override InstructionParameter[] Parameters => _parameters;

		public override Opcode Opcode => Opcode.Addition;

		public override int Execute()
		{
			_memory[Parameters[2].RawValue] = Parameters[0].GetValue(_memory) + Parameters[1].GetValue(_memory);
			return ParameterCount + 1;
		}
	}
}
