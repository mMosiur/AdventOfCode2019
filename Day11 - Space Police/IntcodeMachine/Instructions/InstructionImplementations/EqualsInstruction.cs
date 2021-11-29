namespace Day11;

public partial class IntcodeMachine
{
	public class EqualsInstruction : IntcodeMachineInstruction
	{
		private readonly IntcodeMachineMemory _memory;
		private readonly InstructionParameter[] _parameters;

		public EqualsInstruction(IntcodeMachineMemory memory, int instructionPointer)
		{
			long instruction = memory[instructionPointer];
			const Opcode expectedOpcode = Opcode.Equals;
			Opcode opcode = GetOpcode(instruction);
			if (opcode != expectedOpcode)
			{
				throw new IntcodeMachineInvalidInstructionException($"Unexpected opcode {opcode}: expected {expectedOpcode} instead.");
			}
			_memory = memory;
			InstructionParameterMode[] parameterModes = InstructionParameter.GetParameterModes(instruction);
			_parameters = new InstructionParameter[3];
			for (int i = 0; i < 3; i++)
			{
				long value = _memory[instructionPointer + i + 1];
				_parameters[i] = new InstructionParameter(parameterModes[i], value);
			}
		}

		public override InstructionParameter[] Parameters => _parameters;

		public override Opcode Opcode => Opcode.Equals;

		public override int Execute(ref int relativeBase)
		{
			long param0value = Parameters[0].GetValue(_memory, relativeBase);
			long param1value = Parameters[1].GetValue(_memory, relativeBase);
			bool areParamsEqual = param0value == param1value;
			long operationResult = areParamsEqual ? 1 : 0;
			Parameters[2].SetValue(_memory, relativeBase, operationResult);
			return ParameterCount + 1;
		}
	}
}
