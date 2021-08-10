using System;

namespace Day11.Geometry
{
	public struct Vector
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Vector(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static Vector operator *(Vector vector, int scalar)
		{
			return new Vector
			{
				X = vector.X * scalar,
				Y = vector.Y * scalar
			};
		}

		public static Vector Up => new(0, -1);
		public static Vector Right => new(1, 0);
		public static Vector Down => new(0, 1);
		public static Vector Left => new(-1, 0);

		public override bool Equals(object? obj)
		{
			return obj is Vector vector
				&& X == vector.X
				&& Y == vector.Y;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
		}

		public static bool operator ==(Vector left, Vector right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Vector left, Vector right)
		{
			return !(left == right);
		}
	}
}
