namespace AdventOfCode.Year2019.Day07;

public partial class IntcodeMachine
{
	public class LessThanInstruction : Instruction
	{
		private readonly int[] _memory;
		private readonly InstructionParameter[] _parameters;

		public LessThanInstruction(int[] program, int instructionPointer)
		{
			int instruction = program[instructionPointer];
			const Opcode expectedOpcode = Opcode.LessThan;
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

		public override Opcode Opcode => Opcode.LessThan;

		public override int Execute()
		{
			_memory[Parameters[2].RawValue] = Parameters[0].GetValue(_memory) < Parameters[1].GetValue(_memory) ? 1 : 0;
			return ParameterCount + 1;
		}
	}
}
