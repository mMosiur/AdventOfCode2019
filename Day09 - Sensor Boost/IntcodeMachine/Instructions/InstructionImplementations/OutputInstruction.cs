using System.IO;

namespace Day09;

public partial class IntcodeMachine
{
	public class OutputInstruction : IntcodeMachineInstruction
	{
		private readonly InstructionParameter[] _parameters;
		private readonly IntcodeMachineMemory _memory;
		private readonly Stream _outputStream;

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
			_outputStream = outputStream;
		}

		public override InstructionParameter[] Parameters => _parameters;

		public override Opcode Opcode => Opcode.Output;

		public override int Execute(ref int relativeBase)
		{
			if (_outputStream is not null)
			{
				long param0value = Parameters[0].GetValue(_memory, relativeBase);
				using StreamWriter writer = new(_outputStream, leaveOpen: true);
				writer.WriteLine(param0value);
			}
			return ParameterCount + 1;
		}
	}
}
