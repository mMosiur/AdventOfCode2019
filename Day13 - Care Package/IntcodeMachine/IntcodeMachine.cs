using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2019.Day13;

public partial class IntcodeMachine
{
	public long[] Program { get; }
	private readonly IntcodeMachineMemory _memory;
	private int _instructionPointer;
	private int _relativeBase;

	public Queue<long>? Input => _instructionFactory.Input;

	public Stream OutputStream
	{
		get => _instructionFactory.OutputStream;
		set => _instructionFactory.OutputStream = value;
	}

	private readonly InstructionFactory _instructionFactory;

	public IntcodeMachine(long[] program, IEnumerable<long>? inputValues = null, Stream? outputStream = null)
	{
		Program = new long[program.Length];
		program.CopyTo(Program, 0);
		_memory = new IntcodeMachineMemory(Program);
		_instructionPointer = 0;
		_instructionFactory = new InstructionFactory(inputValues, outputStream);
		_relativeBase = 0;
	}

	public IntcodeMachine(long[] program, System.Func<long> getInput, Stream? outputStream = null)
	{
		Program = new long[program.Length];
		program.CopyTo(Program, 0);
		_memory = new IntcodeMachineMemory(Program);
		_instructionPointer = 0;
		_instructionFactory = new InstructionFactory(getInput, outputStream);
		_relativeBase = 0;
	}

	public IntcodeMachine(long[] program, int noun, int verb) : this(program)
	{
		_memory[1] = noun;
		_memory[2] = verb;
	}

	public void Reset()
	{
		_memory.ResetWith(Program);
		_instructionPointer = 0;
		_relativeBase = 0;
		_instructionFactory.Input?.Clear();
		using StreamReader reader = new(_instructionFactory.OutputStream);
		_ = reader.ReadToEnd();
	}

	public void Reset(int noun, int verb)
	{
		Reset();
		_memory[1] = noun;
		_memory[2] = verb;
	}

	private void Step()
	{

		IntcodeMachineInstruction instruction = _instructionFactory.GetInstruction(_memory, _instructionPointer);
		int move = instruction.Execute(ref _relativeBase);
		_instructionPointer += move;
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
