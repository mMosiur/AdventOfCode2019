using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2019.Day07;

public partial class IntcodeMachine
{
	public int[] InitialMemory { get; }
	public int[] Memory { get; private set; }
	public int InstructionPointer { get; private set; }

	public IList<int> InputValues
	{
		get => _instructionFactory.InputValues;
		set => _instructionFactory.InputValues = value;
	}
	public Stream OutputStream
	{
		get => _instructionFactory.OutputStream;
		set => _instructionFactory.OutputStream = value;
	}

	private readonly InstructionFactory _instructionFactory;

	public IntcodeMachine(int[] program, int[]? inputValues = null, Stream? outputStream = null)
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

	public void ResetMachine()
	{
		ResetMemory();
		_instructionFactory.Reset();
	}

	public void ResetMachine(int noun, int verb)
	{
		ResetMemory(noun, verb);
		_instructionFactory.Reset();
	}

	public void Step()
	{

		Instruction instruction = _instructionFactory.GetInstruction(Memory, InstructionPointer);
		int move = instruction.Execute();
		InstructionPointer += move;
	}

	public bool Run(bool breakOnOutputWritten = false)
	{
		try
		{
			if (breakOnOutputWritten)
			{
				long startingPositionInOutputStream = OutputStream.Position;
				while (startingPositionInOutputStream == OutputStream.Position)
				{
					Step();
				}
				return false;
			}
			else
			{
				while (true)
				{
					Step();
				}
			}
		}
		catch (IntcodeMachineHaltException)
		{
			return true;
		}
	}
}
