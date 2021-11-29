using System.Collections.Generic;
using System.IO;

namespace Day11;

public partial class IntcodeMachine
{
	public class InstructionFactory
	{
		public Queue<long> Input { get; }
		public Stream OutputStream { get; set; }

		public InstructionFactory(IEnumerable<long>? inputValues = null, Stream? outputStream = null)
		{
			Input = inputValues is null ? new Queue<long>() : new Queue<long>(inputValues);
			OutputStream = outputStream ?? Stream.Null;
		}

		private long GetNextInputValue()
		{
			return Input.Dequeue();
		}

		public IntcodeMachineInstruction GetInstruction(IntcodeMachineMemory memory, int instructionPointer)
		{
			Opcode opcode = GetOpcode(memory[instructionPointer]);
			return opcode switch
			{
				Opcode.Addition => new AdditionInstruction(memory, instructionPointer),
				Opcode.Multiplication => new MultiplicationInstruction(memory, instructionPointer),
				Opcode.Input => new InputInstruction(memory, instructionPointer, GetNextInputValue),
				Opcode.Output => new OutputInstruction(memory, instructionPointer, OutputStream),
				Opcode.JumpIfTrue => new JumpIfTrueInstruction(memory, instructionPointer),
				Opcode.JumpIfFalse => new JumpIfFalseInstruction(memory, instructionPointer),
				Opcode.LessThan => new LessThanInstruction(memory, instructionPointer),
				Opcode.Equals => new EqualsInstruction(memory, instructionPointer),
				Opcode.RelativeBaseOffset => new RelativeBaseOffsetInstruction(memory, instructionPointer),
				Opcode.Halt => new HaltInstruction(memory, instructionPointer),
				_ => throw new IntcodeMachineInvalidInstructionException($"Unknown opcode {opcode}.")
			};
		}
	}
}
