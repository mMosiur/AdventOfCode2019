using System;
using System.Collections.Generic;
using System.IO;

namespace Day07
{
	internal partial class IntcodeMachine
	{
		public enum InstructionParameterMode
		{
			PositionMode = 0,
			ImmediateMode = 1
		}

		public class InstructionParameter
		{
			public InstructionParameterMode Mode { get; }
			public int RawValue { get; }

			public int GetValue(int[] memory)
			{
				return Mode switch
				{
					InstructionParameterMode.PositionMode => memory[RawValue],
					InstructionParameterMode.ImmediateMode => RawValue,
					_ => throw new IntcodeMachineInvalidOperationModeException()
				};
			}

			public InstructionParameter(InstructionParameterMode mode, int rawValue)
			{
				Mode = mode;
				RawValue = rawValue;
			}

			public static InstructionParameterMode[] GetParameterModes(int instruction)
			{
				instruction /= 100;
				InstructionParameterMode[] parameterModes = new InstructionParameterMode[3];
				for (int i = 0; i < 3; i++)
				{
					parameterModes[i] = (InstructionParameterMode)(instruction % 10);
					instruction /= 10;
				}
				return parameterModes;
			}
		}


		public class InstructionFactory
		{
			private int _inputValuesIndex;

			public IList<int> InputValues { get; set; }
			public Stream OutputStream { get; set; }

			public InstructionFactory(int[] inputValues = null, Stream outputStream = null)
			{
				InputValues = inputValues ?? Array.Empty<int>();
				_inputValuesIndex = 0;
				OutputStream = outputStream;
			}

			private int GetNextInputValue()
			{
				return _inputValuesIndex >= InputValues.Count ? throw new IndexOutOfRangeException() : InputValues[_inputValuesIndex++];
			}

			public void Reset()
			{
				_inputValuesIndex = 0;
			}

			public Instruction GetInstruction(int[] program, int instructionPointer)
			{
				Opcode opcode = GetOpcode(program[instructionPointer]);
				return opcode switch
				{
					Opcode.Addition => new AdditionInstruction(program, instructionPointer),
					Opcode.Multiplication => new MultiplicationInstruction(program, instructionPointer),
					Opcode.Input => new InputInstruction(program, instructionPointer, GetNextInputValue()),
					Opcode.Output => new OutputInstruction(program, instructionPointer, OutputStream),
					Opcode.JumpIfTrue => new JumpIfTrueInstruction(program, instructionPointer),
					Opcode.JumpIfFalse => new JumpIfFalseInstruction(program, instructionPointer),
					Opcode.LessThan => new LessThanInstruction(program, instructionPointer),
					Opcode.Equals => new EqualsInstruction(program, instructionPointer),
					Opcode.Halt => new HaltInstruction(program, instructionPointer),
					_ => throw new IntcodeMachineInvalidInstructionException($"Unknown opcode {opcode}.")
				};
			}
		}

		public abstract class Instruction
		{
			public abstract InstructionParameter[] Parameters { get; }
			public int ParameterCount => Parameters?.Length ?? 0;
			public abstract Opcode Opcode { get; }
			public abstract int Execute();
		}
	}
}
