using System;
using System.IO;
using System.Linq;

using Day19;

const string DEFAULT_INPUT_FILEPATH = "input.txt";
const int AREA_WIDTH = 50;
const int AREA_HEIGHT = 50;
const int SHIP_WIDTH = 100;
const int SHIP_HEIGHT = 100;

string filepath = args.Length > 0 ? args[0] : DEFAULT_INPUT_FILEPATH;
long[] program = File.ReadAllText(filepath).Split(',').Select(long.Parse).ToArray();
DroneDispatcher dispatcher = new(program);

Console.Write("Part 1: ");
int part1 = dispatcher.CalculateTractorBeamArea(AREA_WIDTH, AREA_HEIGHT);
Console.WriteLine(part1);

Console.Write("Part 2: ");
Point p = dispatcher.FindPointWhereShipFits(SHIP_WIDTH, SHIP_HEIGHT);
int part2 = p.X * 10_000 + p.Y;
Console.WriteLine(part2);
