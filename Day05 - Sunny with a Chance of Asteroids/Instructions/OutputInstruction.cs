using System.IO;

namespace Day05
{
	partial class IntcodeMachine
	{
		internal class OutputInstruction : Instruction
		{
			private readonly InstructionParameter[] _parameters;
			private readonly int[] _memory;
			private readonly Stream _outputStream;

			public OutputInstruction(int[] program, int instructionPointer, Stream outputStream)
			{
				int instruction = program[instructionPointer];
				const Opcode expectedOpcode = Opcode.Output;
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
				_outputStream = outputStream;
			}

			public override InstructionParameter[] Parameters => _parameters;

			public override Opcode Opcode => Opcode.Output;

			public override int Execute()
			{
				using StreamWriter writer = new(_outputStream, leaveOpen: true);
				int output = _memory[Parameters[0].RawValue];
				writer.WriteLine(output);
				return ParameterCount + 1;
			}
		}
	}
}
