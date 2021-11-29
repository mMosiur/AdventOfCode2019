using System;
using System.IO;
using System.Linq;

using Day09;

const string DEFAULT_INPUT_FILEPATH = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
long[] program = File.ReadAllText(filepath).Split(',').Select(long.Parse).ToArray();
IntcodeMachine machine = new(program);
machine.OutputStream = Console.OpenStandardOutput();

// Part 1
Console.Write("Part 1: ");
machine.InputValues = new long[] { 1 };
machine.Run();

machine.ResetMachine();

// Part 2
Console.Write("Part 2: ");
machine.InputValues = new long[] { 2 };
machine.Run();
