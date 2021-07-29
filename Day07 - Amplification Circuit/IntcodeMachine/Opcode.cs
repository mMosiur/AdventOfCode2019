namespace Day07
{
	internal partial class IntcodeMachine
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
			Halt = 99
		}

		private static Opcode GetOpcode(int instruction) => (Opcode)(instruction % 100);
	}
}
