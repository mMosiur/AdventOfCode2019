using System.Collections.Generic;
using System.IO;

namespace Day09;

public partial class IntcodeMachine
{
	public long[] Program { get; }
	public IntcodeMachineMemory Memory { get; private set; }
	public int InstructionPointer { get; private set; }
	private int _relativeBase;

	public IEnumerator<long> Input
	{
		set => _instructionFactory.Input = value;
	}

	public IEnumerable<long> InputValues
	{
		set => Input = value.GetEnumerator();
	}

	public Stream OutputStream
	{
		set => _instructionFactory.OutputStream = value;
	}

	private readonly InstructionFactory _instructionFactory;

	public IntcodeMachine(long[] program, IEnumerator<long>? inputValues = null, Stream? outputStream = null)
	{
		Program = new long[program.Length];
		program.CopyTo(Program, 0);
		Memory = new IntcodeMachineMemory(program);
		InstructionPointer = 0;
		_instructionFactory = new InstructionFactory(inputValues, outputStream);
		_relativeBase = 0;
	}

	public IntcodeMachine(long[] program, int noun, int verb) : this(program)
	{
		Memory[1] = noun;
		Memory[2] = verb;
	}

	public void ResetMemory()
	{
		Memory.ResetWith(Program);
		InstructionPointer = 0;
		_relativeBase = 0;
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

		IntcodeMachineInstruction instruction = _instructionFactory.GetInstruction(Memory, InstructionPointer);
		int move = instruction.Execute(ref _relativeBase);
		InstructionPointer += move;
	}

	private void RunIndefinitely()
	{
		while (true)
		{
			Step();
		}
	}

	private void RunToFirstOutput()
	{
		Stream outputStream = _instructionFactory.OutputStream;
		long startingPositionInOutputStream = outputStream.Position;
		while (startingPositionInOutputStream == outputStream.Position)
		{
			Step();
		}
	}

	public bool Run(bool breakOnOutputWritten = false)
	{
		try
		{
			if (breakOnOutputWritten)
			{
				RunToFirstOutput();
			}
			else
			{
				RunIndefinitely();
			}
			return false;
		}
		catch (IntcodeMachineHaltException)
		{
			return true;
		}
	}
}
