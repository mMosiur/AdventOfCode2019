using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day19
{
	public partial class IntcodeMachine
	{
		internal class InstructionFactory : ICloneable
		{
			private readonly Func<long>? _getInputValue;
			public Queue<long> Input { get; }
			public Stream OutputStream { get; set; }

			public InstructionFactory(IEnumerable<long>? inputValues, Stream? outputStream)
			{
				Input = inputValues is null ? new Queue<long>() : new Queue<long>(inputValues);
				_getInputValue = null;
				OutputStream = outputStream ?? Stream.Null;
			}

			public InstructionFactory(Func<long> getInputValue, Stream? outputStream)
			{
				Input = new Queue<long>();
				_getInputValue = getInputValue;
				OutputStream = outputStream ?? Stream.Null;
			}

			private long GetInputValue()
			{
				if (Input.Count > 0)
				{
					return Input.Dequeue();
				}
				if (_getInputValue is not null)
				{
					return _getInputValue.Invoke();
				}
				throw new InvalidOperationException(
					"The Input queue was empty and alternative input value retrieval function was not specified."
				);
			}

			public IntcodeMachineInstruction GetInstruction(IntcodeMachineMemory memory, int instructionPointer)
			{
				Opcode opcode = GetOpcode(memory[instructionPointer]);
				return opcode switch
				{
					Opcode.Addition => new AdditionInstruction(memory, instructionPointer),
					Opcode.Multiplication => new MultiplicationInstruction(memory, instructionPointer),
					Opcode.Input => new InputInstruction(memory, instructionPointer, GetInputValue),
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

			object ICloneable.Clone() => Clone();

			public InstructionFactory Clone()
			{
				if (_getInputValue is null)
				{
					return new InstructionFactory(
						Input.AsEnumerable(),
						OutputStream
					);
				}
				else
				{
					return new InstructionFactory(
						_getInputValue,
						OutputStream
					);
				}
			}
		}
	}
}
