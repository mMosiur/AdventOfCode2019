namespace Day05;

public partial class IntcodeMachine
{
	public class EqualsInstruction : Instruction
	{
		private readonly int[] _memory;
		private readonly InstructionParameter[] _parameters;

		public EqualsInstruction(int[] program, int instructionPointer)
		{
			int instruction = program[instructionPointer];
			const Opcode expectedOpcode = Opcode.Equals;
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

		public override Opcode Opcode => Opcode.Equals;

		public override int Execute()
		{
			if (Parameters[0].GetValue(_memory) == Parameters[1].GetValue(_memory))
			{
				_memory[Parameters[2].RawValue] = 1;
			}
			else
			{
				_memory[Parameters[2].RawValue] = 0;
			}
			return ParameterCount + 1;
		}
	}
}
