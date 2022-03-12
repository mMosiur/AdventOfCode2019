using System;
using System.Linq;

namespace AdventOfCode.Year2019.Day15;

public class Droid : ICloneable
{
	private readonly IntcodeMachine _machine;
	public Point Position { get; private set; }
	public int StepsTaken { get; private set; }

	private Droid(IntcodeMachine machine, Point position, int stepsTaken)
	{
		_machine = machine;
		Position = position;
		StepsTaken = stepsTaken;
	}

	public Droid(long[] program)
	{
		_machine = new IntcodeMachine(
			program,
			Enumerable.Empty<long>(),
			null
		);
		Position = new Point(0, 0);
		StepsTaken = 0;
	}

	public long Move(int dir)
	{
		_machine.Input?.Enqueue(dir);
		Point target = Position;
		switch (dir)
		{
			case 1:
				target.Y--;
				break;
			case 2:
				target.Y++;
				break;
			case 3:
				target.X--;
				break;
			case 4:
				target.X++;
				break;
			default: throw new Exception("?");
		}
		long output = _machine.RunToFirstOutput();
		switch (output)
		{
			case 0:
				break;
			case 1:
				Position = target;
				break;
			case 2:
				Position = target;
				break;
			default: throw new Exception("?");
		}
		StepsTaken++;
		return output;
	}

	object ICloneable.Clone() => Clone();

	public Droid Clone() => new(_machine.Clone(), Position, StepsTaken);
}
