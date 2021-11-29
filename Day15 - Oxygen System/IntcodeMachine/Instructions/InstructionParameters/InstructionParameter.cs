namespace Day15;

public partial class IntcodeMachine
{
	public class InstructionParameter
	{
		public InstructionParameterMode Mode { get; }
		public long RawValue { get; }

		public long GetValue(IntcodeMachineMemory memory, int relativeBase)
		{
			return Mode switch
			{
				InstructionParameterMode.PositionMode => memory[RawValue],
				InstructionParameterMode.ImmediateMode => RawValue,
				InstructionParameterMode.RelativeMode => memory[relativeBase + RawValue],
				_ => throw new IntcodeMachineInvalidOperationModeException("Unknown InstructionParameterMode.")
			};
		}

		public void SetValue(IntcodeMachineMemory memory, int relativeBase, long value)
		{
			switch (Mode)
			{
				case InstructionParameterMode.PositionMode:
					memory[RawValue] = value;
					break;
				case InstructionParameterMode.ImmediateMode:
					throw new IntcodeMachineInvalidOperationModeException("Cannot set value of a parameter in the immediate mode.");
				case InstructionParameterMode.RelativeMode:
					memory[relativeBase + RawValue] = value;
					break;
				default:
					throw new IntcodeMachineInvalidOperationModeException("Unknown InstructionParameterMode.");
			}
		}

		public InstructionParameter(InstructionParameterMode mode, long rawValue)
		{
			Mode = mode;
			RawValue = rawValue;
		}

		public static InstructionParameterMode[] GetParameterModes(long instruction)
		{
			instruction /= 100;
			InstructionParameterMode[] parameterModes = new InstructionParameterMode[3];
			for (int i = 0; i < 3; i++)
			{
				parameterModes[i] = (InstructionParameterMode)(instruction % 10);
				instruction /= 10;
			}
			return parameterModes;
		}
	}
}
