using System;

using AdventOfCode.Year2019.Day08;

const string DEFAULT_INPUT_FILE = "input.txt";

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILE;
var solver = new Day08Solver(filepath);

Console.Write("Part 1: ");
string part1 = solver.SolvePart1();
Console.WriteLine(part1);

Console.WriteLine("Part 2 message: ");
string part2 = solver.SolvePart2();
Console.WriteLine(part2);
