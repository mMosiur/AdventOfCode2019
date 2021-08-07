namespace Day09
{
	public partial class IntcodeMachine
	{
		public class RelativeBaseOffsetInstruction : IntcodeMachineInstruction
		{
			private readonly IntcodeMachineMemory _memory;
			private readonly InstructionParameter[] _parameters;

			public RelativeBaseOffsetInstruction(IntcodeMachineMemory memory, int instructionPointer)
			{
				long instruction = memory[instructionPointer];
				const Opcode expectedOpcode = Opcode.RelativeBaseOffset;
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
					long value = _memory[instructionPointer + i + 1];
					_parameters[i] = new InstructionParameter(parameterModes[i], value);
				}
			}

			public override InstructionParameter[] Parameters => _parameters;

			public override Opcode Opcode => Opcode.RelativeBaseOffset;

			public override int Execute(ref int relativeBase)
			{
				long param0value = Parameters[0].GetValue(_memory, relativeBase);
				int offset = System.Convert.ToInt32(param0value);
				relativeBase += offset;
				return ParameterCount + 1;
			}
		}
	}
}
