using System;

namespace Day15;

public partial class IntcodeMachine
{
	public class HaltInstruction : IntcodeMachineInstruction
	{
		public HaltInstruction(IntcodeMachineMemory memory, int instructionPointer)
		{
			long instruction = memory[instructionPointer];
			const Opcode expectedOpcode = Opcode.Halt;
			Opcode opcode = GetOpcode(instruction);
			if (opcode != expectedOpcode)
			{
				throw new IntcodeMachineInvalidInstructionException($"Unexpected opcode {opcode}: expected {expectedOpcode} instead.");
			}
		}

		public override InstructionParameter[] Parameters => Array.Empty<InstructionParameter>();

		public override Opcode Opcode => Opcode.Halt;

		public override int Execute(ref int relativeBase)
		{
			throw new IntcodeMachineHaltException("Halt instruction has been executed.");
		}
	}
}
