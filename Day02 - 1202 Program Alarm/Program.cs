using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
	partial class IntcodeMachine
	{
		public enum Opcode
		{
			Addition = 1,
			Multiplication = 2,
			Halt = 99
		}

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

	class Program
	{
		const string INPUT_FILEPATH = "input.txt";
		const int INITIAL_NOUN = 12;
		const int INITIAL_VERB = 2;
		const int NOUN_VERB_TARGET = 19690720;

		static int GetNounVerbValue(int[] input, int target)
		{
			IntcodeMachine machine = new(input);
			for (int noun = 0; noun <= 99; noun++)
			{
				for (int verb = 0; verb <= 99; verb++)
				{
					machine.ResetMemory(noun, verb);
					machine.Run();
					if(machine.Memory[0] == target)
					{
						return 100 * noun + verb;
					}
				}
			}
			throw new Exception();
		}

		static void Main(string[] args)
		{
			int[] input = File.ReadAllText(INPUT_FILEPATH).Split(',').Select(s => int.Parse(s)).ToArray();

			Console.Write("Part 1: ");
			IntcodeMachine machine = new(input, INITIAL_NOUN, INITIAL_VERB);
			machine.Run();
			Console.WriteLine(machine.Memory[0]);

			Console.Write("Part 2: ");
			int result = GetNounVerbValue(input, NOUN_VERB_TARGET);
			System.Console.WriteLine(result);
		}
	}
}
