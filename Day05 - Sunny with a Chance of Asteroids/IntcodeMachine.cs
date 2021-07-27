using System.IO;

namespace Day05
{
	partial class IntcodeMachine
	{
		public int[] InitialMemory { get; }
		public int[] Memory { get; private set; }
		public int InstructionPointer { get; private set; }

		private readonly InstructionFactory _instructionFactory;

		public IntcodeMachine(int[] program, int[] inputValues = null, Stream outputStream = null)
		{
			InitialMemory = new int[program.Length];
			program.CopyTo(InitialMemory, 0);
			Memory = new int[InitialMemory.Length];
			InitialMemory.CopyTo(Memory, 0);
			InstructionPointer = 0;
			_instructionFactory = new InstructionFactory(inputValues, outputStream);
		}

		public IntcodeMachine(int[] program, int noun, int verb) : this(program)
		{
			Memory[1] = noun;
			Memory[2] = verb;
		}

		public void ResetMemory()
		{
			InitialMemory.CopyTo(Memory, 0);
			InstructionPointer = 0;
		}

		public void ResetMemory(int noun, int verb)
		{
			ResetMemory();
			Memory[1] = noun;
			Memory[2] = verb;
		}

		public void Step()
		{

			Instruction instruction = _instructionFactory.GetInstruction(Memory, InstructionPointer);
			int move = instruction.Execute();
			InstructionPointer += move;
		}

		public void Run()
		{
			try
			{
				while (true)
				{
					Step();
				}
			}
			catch (IntcodeMachineHaltException)
			{ }
		}
	}
}
