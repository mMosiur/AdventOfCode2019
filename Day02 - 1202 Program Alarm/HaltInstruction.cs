using System;

namespace Day02;

internal partial class IntcodeMachine
{
	internal class HaltInstruction : Instruction
	{
		public HaltInstruction(int[] program, int instructionPointer)
		{
			if (program[instructionPointer] != (int)Opcode.Halt)
			{
				throw new IntcodeMachineInvalidInstructionException();
			}
		}

		public override int[] Parameters => Array.Empty<int>();

		public override Opcode Opcode => Opcode.Halt;

		public override int Execute()
		{
			throw new IntcodeMachineHaltException();
		}
	}
}
