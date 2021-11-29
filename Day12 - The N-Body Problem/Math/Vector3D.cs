using System;

namespace Day12;

public record struct Vector3D(int X, int Y, int Z)
{
	public int Magnitude => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

	public static Vector3D Zero => new(0, 0, 0);

	public static Vector3D operator +(Vector3D vec1, Vector3D vec2)
	{
		return new Vector3D
		{
			X = vec1.X + vec2.X,
			Y = vec1.Y + vec2.Y,
			Z = vec1.Z + vec2.Z,
		};
	}
}
