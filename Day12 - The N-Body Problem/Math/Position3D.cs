using System;

namespace Day12;

public record struct Position3D(int X, int Y, int Z)
{
	public int Magnitude => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

	public static Position3D operator +(Position3D position, Vector3D vector)
	{
		return new Position3D
		{
			X = position.X + vector.X,
			Y = position.Y + vector.Y,
			Z = position.Z + vector.Z,
		};
	}

	public static Position3D Parse(string s)
	{
		int length = s.Length;
		s = s.TrimStart('<');
		if (s.Length != length - 1)
		{
			throw new FormatException();
		}
		length = s.Length;
		s = s.TrimEnd('>');
		if (s.Length != length - 1)
		{
			throw new FormatException();
		}
		string[] parts = s.Split(',', StringSplitOptions.TrimEntries);
		if (parts.Length != 3)
		{
			throw new FormatException();
		}
		return new Position3D
		{
			X = ParseAssignmentValue("x", parts[0]),
			Y = ParseAssignmentValue("y", parts[1]),
			Z = ParseAssignmentValue("z", parts[2])
		};
	}

	private static int ParseAssignmentValue(string name, string s)
	{
		string[] parts = s.Split('=', StringSplitOptions.TrimEntries);
		if (parts.Length != 2 || parts[0] != name)
		{
			throw new FormatException();
		}
		return int.Parse(parts[1]);
	}
}
