using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
	class Program
	{
		static Droid GetDroidToOxygenGenerator(long[] program)
		{
			Queue<Droid> droidsToMove = new();
			droidsToMove.Enqueue(new Droid(program));
			ISet<Point> pointsVisited = new HashSet<Point>
			{
				droidsToMove.Single().Position
			};
			while (true)
			{
				Droid droid = droidsToMove.Dequeue();
				for (int i = 1; i <= 4; i++)
				{
					Droid droidClone = droid.Clone();
					float output = droidClone.Move(i);
					if (output == 2) return droidClone; // The droid clone has reached oxygen generator
					if (pointsVisited.Add(droidClone.Position))
					{
						droidsToMove.Enqueue(droidClone);
					}
				}
			}
		}

		/// <summary>
		/// Moves all droids in the provided queue and enqueues their resulting clones is applicable.
		/// Adds visited points along the way to pointsVisited.
		/// </summary>
		static void CycleDroids(Queue<Droid> queue, ISet<Point> pointsVisited)
		{
			int count = queue.Count;
			for (int i = 0; i < count; i++)
			{
				Droid d = queue.Dequeue();
				for (int m = 1; m <= 4; m++)
				{
					Droid droidClone = d.Clone();
					float output = droidClone.Move(m);
					if (output == 0) continue;
					if (pointsVisited.Add(droidClone.Position))
					{
						queue.Enqueue(droidClone);
					}
				}
			}
		}

		/// <summary>
		/// Propagates clones of given droid through map and counts how many generations it took to finish propagations.
		/// </summary>
		static int PropagateDroid(Droid droid)
		{
			Queue<Droid> droidsToMove = new();
			droidsToMove.Enqueue(droid);
			ISet<Point> pointsVisited = new HashSet<Point>
			{
				droidsToMove.Single().Position
			};
			int steps = 0;
			while (droidsToMove.Any())
			{
				CycleDroids(droidsToMove, pointsVisited);
				steps++;
			}
			return steps - 1;
		}

		static void Main()
		{
			long[] program = File.ReadAllText("input.txt").Split(',').Select(long.Parse).ToArray();
			Droid droid = GetDroidToOxygenGenerator(program);
			Console.WriteLine($"Part 1: {droid.StepsTaken}");
			int stepsToPropagate = PropagateDroid(droid);
			Console.WriteLine($"Part 2: {stepsToPropagate}");
		}
	}
}
