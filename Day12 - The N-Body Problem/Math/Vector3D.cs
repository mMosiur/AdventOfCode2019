using System;

namespace Day12
{
	public struct Vector3D
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }

		public int Magnitude => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

		public static Vector3D Zero => new() { X = 0, Y = 0, Z = 0 };

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
}
