namespace AdventOfCode.Year2019.Day09;

public partial class IntcodeMachine
{
	public class InputInstruction : IntcodeMachineInstruction
	{
		private readonly InstructionParameter[] _parameters;
		private readonly IntcodeMachineMemory _memory;
		private readonly long _input;

		public InputInstruction(IntcodeMachineMemory memory, int instructionPointer, long input)
		{
			long instruction = memory[instructionPointer];
			const Opcode expectedOpcode = Opcode.Input;
			Opcode opcode = GetOpcode(instruction);
			if (opcode != expectedOpcode)
			{
				throw new IntcodeMachineInvalidInstructionException($"Unexpected opcode {opcode}: expected {expectedOpcode} instead.");
			}
			_memory = memory;
			InstructionParameterMode[] parameterModes = InstructionParameter.GetParameterModes(instruction);
			_parameters = new InstructionParameter[1];
			for (int i = 0; i < 1; i++)
			{
				long value = memory[instructionPointer + i + 1];
				_parameters[i] = new InstructionParameter(parameterModes[i], value);
			}
			_input = input;
		}

		public override InstructionParameter[] Parameters => _parameters;

		public override Opcode Opcode => Opcode.Input;

		public override int Execute(ref int relativeBase)
		{
			Parameters[0].SetValue(_memory, relativeBase, _input);
			return ParameterCount + 1;
		}
	}
}
