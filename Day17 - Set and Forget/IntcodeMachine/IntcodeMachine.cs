using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2019.Day17;

public partial class IntcodeMachine : ICloneable
{
	public long[] Program { get; }
	private readonly IntcodeMachineMemory _memory;
	private int _instructionPointer;
	private int _relativeBase;

	public event EventHandler<OutputInstructionExecutedEventArgs>? OutputInstructionExecuted;
	private void OnOutputInstructionExecuted(OutputInstructionExecutedEventArgs e) => OutputInstructionExecuted?.Invoke(this, e);

	public Queue<long>? Input => _instructionFactory.Input;

	public Stream OutputStream
	{
		get => _instructionFactory.OutputStream;
		set => _instructionFactory.OutputStream = value;
	}

	private readonly InstructionFactory _instructionFactory;

	private IntcodeMachine(long[] program, IntcodeMachineMemory memory, int instructionPointer, InstructionFactory instructionFactory, int relativeBase)
	{
		Program = new long[program.Length];
		program.CopyTo(Program, 0);
		_memory = memory;
		_instructionPointer = instructionPointer;
		_instructionFactory = instructionFactory;
		_relativeBase = relativeBase;
	}

	public IntcodeMachine(long[] program, IEnumerable<long>? inputValues = null, Stream? outputStream = null)
	{
		Program = new long[program.Length];
		program.CopyTo(Program, 0);
		_memory = new IntcodeMachineMemory(Program);
		_instructionPointer = 0;
		_instructionFactory = new InstructionFactory(inputValues, outputStream);
		_relativeBase = 0;
	}

	public IntcodeMachine(long[] program, Func<long> getInput, Stream? outputStream = null)
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

	private long? Step()
	{

		IntcodeMachineInstruction instruction = _instructionFactory.GetInstruction(_memory, _instructionPointer);
		long? output = null;
		if (instruction is OutputInstruction outputInstruction)
		{
			output = outputInstruction.GetOutputValue(_relativeBase);
			OnOutputInstructionExecuted(new OutputInstructionExecutedEventArgs(
				outputInstruction.GetOutputValue(_relativeBase),
				outputInstruction.OutputStream
			));
		}
		int move = instruction.Execute(ref _relativeBase);
		_instructionPointer += move;
		return output;
	}

	private void RunIndefinitely()
	{
		while (true)
		{
			Step();
		}
	}

	public long RunToFirstOutput()
	{
		long? output = Step();
		while (output is null)
		{
			output = Step();
		}
		return (long)output;
	}

	public IEnumerable<long> RunYieldingOutput()
	{
		bool finished = false;
		long output = 0;
		while (!finished)
		{
			try
			{
				output = RunToFirstOutput();
			}
			catch (IntcodeMachineHaltException)
			{
				finished = true;
			}
			yield return output;
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

	object ICloneable.Clone() => Clone();

	public IntcodeMachine Clone() => new(
			(long[])Program.Clone(),
			_memory.Clone(),
			_instructionPointer,
			_instructionFactory.Clone(),
			_relativeBase
		);
}
