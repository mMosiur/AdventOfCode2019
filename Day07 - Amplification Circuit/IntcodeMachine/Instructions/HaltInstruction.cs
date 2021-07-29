using System;

namespace Day07
{
	internal partial class IntcodeMachine
	{
		public class HaltInstruction : Instruction
		{
			public HaltInstruction(int[] program, int instructionPointer)
			{
				int instruction = program[instructionPointer];
				const Opcode expectedOpcode = Opcode.Halt;
				Opcode opcode = GetOpcode(instruction);
				if (opcode != expectedOpcode)
				{
					throw new IntcodeMachineInvalidInstructionException($"Unexpected opcode {opcode}: expected {expectedOpcode} instead.");
				}
			}

			public override InstructionParameter[] Parameters => Array.Empty<InstructionParameter>();

			public override Opcode Opcode => Opcode.Halt;

			public override int Execute()
			{
				throw new IntcodeMachineHaltException("Halt instruction has been executed.");
			}
		}
	}
}
