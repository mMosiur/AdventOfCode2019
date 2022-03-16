namespace AdventOfCode.Year2019.Day23;

public partial class IntcodeMachine
{
	public class InstructionFactory : ICloneable
	{
		private readonly Func<long> _getInputValue;
		public Queue<long>? Input { get; }
		public Stream OutputStream { get; set; }

		public InstructionFactory(IEnumerable<long>? inputValues, Stream? outputStream)
		{
			Input = inputValues is null ? new Queue<long>() : new Queue<long>(inputValues);
			_getInputValue = GetInputValueFromQueue;
			OutputStream = outputStream ?? Stream.Null;
		}

		public InstructionFactory(Func<long> getInputValue, Stream? outputStream)
		{
			Input = null;
			_getInputValue = getInputValue;
			OutputStream = outputStream ?? Stream.Null;
		}

		private long GetInputValueFromQueue()
		{
			try
			{
				return Input?.Dequeue()
					?? throw new InvalidOperationException("InstructionFactory was initialized with custom GetInput function");
			}
			catch (InvalidOperationException e)
			{
				throw new InvalidOperationException("There was no available next input value.", e);
			}
		}

		private long GetInputValue() => _getInputValue.Invoke();

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
			if (Input is null)
			{
				return new InstructionFactory(
					_getInputValue,
					OutputStream
				);
			}
			else
			{
				return new InstructionFactory(
					Input.AsEnumerable(),
					OutputStream
				);
			}
		}
	}
}
