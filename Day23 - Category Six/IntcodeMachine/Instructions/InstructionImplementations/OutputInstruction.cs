namespace AdventOfCode.Year2019.Day23;

public partial class IntcodeMachine
{
	public class OutputInstruction : IntcodeMachineInstruction
	{
		private readonly InstructionParameter[] _parameters;
		private readonly IntcodeMachineMemory _memory;
		public Stream OutputStream { get; }


		public OutputInstruction(IntcodeMachineMemory memory, int instructionPointer, Stream outputStream)
		{
			long instruction = memory[instructionPointer];
			const Opcode expectedOpcode = Opcode.Output;
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
			OutputStream = outputStream;
		}

		public override InstructionParameter[] Parameters => _parameters;

		public override Opcode Opcode => Opcode.Output;

		public long GetOutputValue(int relativeBase) => Parameters[0].GetValue(_memory, relativeBase);

		public override int Execute(ref int relativeBase)
		{
			long outputValue = GetOutputValue(relativeBase);
			using StreamWriter writer = new(OutputStream, leaveOpen: true);
			writer.NewLine = "\n";
			writer.WriteLine(outputValue);
			return ParameterCount + 1;
		}
	}
}
