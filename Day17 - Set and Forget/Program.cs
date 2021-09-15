using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day17
{
	class Program
	{
		static int GetAlignmentParametersSum(char[,] map)
		{
			int sum = 0;
			for (int i = 1; i < map.GetLength(0) - 1; i++)
			{
				for (int j = 1; j < map.GetLength(1) - 1; j++)
				{
					if (map[i, j] == '.' ||
						map[i - 1, j] == '.' ||
						map[i + 1, j] == '.' ||
						map[i, j - 1] == '.' ||
						map[i, j + 1] == '.'
					) continue;
					sum += i * j;
				}
			}
			return sum;
		}

		static IList<string> GetSubroutine(string compositeSubroutine)
		{
			const string Separator = ",";
			return compositeSubroutine.Split(Separator).ToList();
		}

		static IEnumerable<long> CompressToInput(IList<string> commands, bool continuousVideoFeed = false)
		{
			const string Separator = ",";
			const string NewLine = "\n";
			const string YesString = "y";
			const string NoString = "n";
			const string FunctionASymbol = "A";
			const string FunctionBSymbol = "B";
			const string FunctionCSymbol = "C";
			List<string> mainRoutine = new(commands);
			LinkedList<string> composite = new();
			ISet<string> compositeValues = new HashSet<string>();
			for (int i = 0; i < mainRoutine.Count; i++)
			{
				if (compositeValues.Contains(mainRoutine[i])) continue;
				composite.AddLast(mainRoutine[i]);
				int j = i + 1;
				while (mainRoutine.CountSubsequences(composite) > 1)
				{
					composite.AddLast(mainRoutine[j]);
					if (compositeValues.Contains(mainRoutine[j])) break;
					j++;
				}
				composite.RemoveLast();
				string replacement = string.Join(Separator, composite);
				mainRoutine.ReplaceSequencesWith(composite, replacement);
				compositeValues.Add(replacement);
				composite.Clear();
			}
			if (compositeValues.Count != 3)
			{
				throw new InvalidOperationException("Could not compress commands.");
			}
			string compositeValue = compositeValues.ElementAt(0);
			IList<string> functionA = GetSubroutine(compositeValue);
			mainRoutine.Replace(compositeValue, FunctionASymbol);
			compositeValue = compositeValues.ElementAt(1);
			IList<string> functionB = GetSubroutine(compositeValue);
			mainRoutine.Replace(compositeValue, FunctionBSymbol);
			compositeValue = compositeValues.ElementAt(2);
			IList<string> functionC = GetSubroutine(compositeValue);
			mainRoutine.Replace(compositeValue, FunctionCSymbol);

			StringBuilder builder = new();
			builder.Append(string.Join(Separator, mainRoutine) + NewLine);
			builder.Append(string.Join(Separator, functionA) + NewLine);
			builder.Append(string.Join(Separator, functionB) + NewLine);
			builder.Append(string.Join(Separator, functionC) + NewLine);
			builder.Append((continuousVideoFeed ? YesString : NoString) + NewLine);
			return builder.ToString().Select(c => (long)c);
		}

		static void Main()
		{
			long[] program = File.ReadAllText("input.txt").Split(',').Select(long.Parse).ToArray();
			// Part 1
			AftScaffoldingControlAndInformationInterface ascii = new(program);
			char[,] map = ascii.GetCameraOutput();
			int part1 = GetAlignmentParametersSum(map);
			Console.WriteLine($"Part 1: {part1}");
			// Part 2
			Traverser traverser = new(map);
			IList<string> commands = traverser.Traverse().ToList();
			IEnumerable<long> input = CompressToInput(commands);
			program[0] = 2;
			IntcodeMachine machine = new(program, input, null);
			long part2 = machine.RunYieldingOutput().Last();
			Console.WriteLine($"Part 2: {part2}");
		}
	}
}
