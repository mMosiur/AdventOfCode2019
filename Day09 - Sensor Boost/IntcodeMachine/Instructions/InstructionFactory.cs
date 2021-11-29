using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09;

public partial class IntcodeMachine
{
	public class InstructionFactory
	{
		private IEnumerator<long> _input;
		public IEnumerator<long> Input
		{
			get => _input;
			set => _input = value ?? Enumerable.Empty<long>().GetEnumerator();
		}
		public Stream OutputStream { get; set; }

		public InstructionFactory(IEnumerator<long>? inputValues = null, Stream? outputStream = null)
		{
			_input = inputValues ?? Enumerable.Empty<long>().GetEnumerator();
			OutputStream = outputStream ?? Stream.Null;
		}

		private long GetNextInputValue()
		{
			if (Input.MoveNext())
			{
				return Input.Current;
			}
			else
			{
				throw new IndexOutOfRangeException("Could not fetch next input value");
			}
		}

		public void Reset()
		{
			Input.Reset();
		}

		public IntcodeMachineInstruction GetInstruction(IntcodeMachineMemory memory, int instructionPointer)
		{
			Opcode opcode = GetOpcode(memory[instructionPointer]);
			return opcode switch
			{
				Opcode.Addition => new AdditionInstruction(memory, instructionPointer),
				Opcode.Multiplication => new MultiplicationInstruction(memory, instructionPointer),
				Opcode.Input => new InputInstruction(memory, instructionPointer, GetNextInputValue()),
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
