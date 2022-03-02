namespace AdventOfCode.Year2019.Day09;

public partial class IntcodeMachine
{
	public class JumpIfFalseInstruction : IntcodeMachineInstruction
	{
		private readonly IntcodeMachineMemory _memory;
		private readonly int _instructionPointer;
		private readonly InstructionParameter[] _parameters;

		public JumpIfFalseInstruction(IntcodeMachineMemory memory, int instructionPointer)
		{
			_instructionPointer = instructionPointer;
			long instruction = memory[instructionPointer];
			const Opcode expectedOpcode = Opcode.JumpIfFalse;
			Opcode opcode = GetOpcode(instruction);
			if (opcode != expectedOpcode)
			{
				throw new IntcodeMachineInvalidInstructionException($"Unexpected opcode {opcode}: expected {expectedOpcode} instead.");
			}
			_memory = memory;
			InstructionParameterMode[] parameterModes = InstructionParameter.GetParameterModes(instruction);
			_parameters = new InstructionParameter[2];
			for (int i = 0; i < 2; i++)
			{
				long value = _memory[instructionPointer + i + 1];
				_parameters[i] = new InstructionParameter(parameterModes[i], value);
			}
		}

		public override InstructionParameter[] Parameters => _parameters;

		public override Opcode Opcode => Opcode.JumpIfFalse;

		public override int Execute(ref int relativeBase)
		{
			long param0value = Parameters[0].GetValue(_memory, relativeBase);
			if (param0value == 0)
			{
				long param1value = Parameters[1].GetValue(_memory, relativeBase);
				int offset = System.Convert.ToInt32(param1value - _instructionPointer);
				return offset;
			}
			return ParameterCount + 1;
		}
	}
}
