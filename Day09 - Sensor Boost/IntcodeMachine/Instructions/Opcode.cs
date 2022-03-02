namespace AdventOfCode.Year2019.Day09;

public partial class IntcodeMachine
{
	public enum Opcode
	{
		Addition = 1,
		Multiplication = 2,
		Input = 3,
		Output = 4,
		JumpIfTrue = 5,
		JumpIfFalse = 6,
		LessThan = 7,
		Equals = 8,
		RelativeBaseOffset = 9,
		Halt = 99
	}

	private static Opcode GetOpcode(long instruction) => (Opcode)(instruction % 100);
}
