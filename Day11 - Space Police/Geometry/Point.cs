using System;

namespace Day11.Geometry
{
	public struct Point
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static Point operator +(Point point, Vector vector)
		{
			return new Point
			{
				X = point.X + vector.X,
				Y = point.Y + vector.Y
			};
		}

		public override bool Equals(object? obj)
		{
			return obj is Point point
				&& X == point.X
				&& Y == point.Y;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
		}

		public static bool operator ==(Point left, Point right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Point left, Point right)
		{
			return !(left == right);
		}
	}
}
