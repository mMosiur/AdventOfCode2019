namespace AdventOfCode.Year2019.Day02;

public partial class IntcodeMachine
{

	public int[] InitialMemory { get; }
	public int[] Memory { get; private set; }
	public int InstructionPointer { get; private set; }

	public IntcodeMachine(int[] program)
	{
		InitialMemory = new int[program.Length];
		program.CopyTo(InitialMemory, 0);
		Memory = new int[InitialMemory.Length];
		InitialMemory.CopyTo(Memory, 0);
		InstructionPointer = 0;
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
		Instruction instruction = Instruction.GetInstructionAt(Memory, InstructionPointer);
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
