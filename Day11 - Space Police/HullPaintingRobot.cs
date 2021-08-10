using Day11.Geometry;

namespace Day11
{
	public class HullPaintingRobot
	{
		public Point Position { get; private set; }
		public Vector Direction { get; private set; }

		public IntcodeMachine Driver { get; }

		public void RotateClockwise()
		{
			Direction = new Vector
			{
				X = -Direction.Y,
				Y = Direction.X
			};
		}

		public void RotateCounterclockwise()
		{
			Direction = new Vector
			{
				X = Direction.Y,
				Y = -Direction.X
			};
		}

		public void GoForward()
		{
			Position += Direction;
		}

		public HullPaintingRobot(IntcodeMachine driver)
		{
			Position = new Point(0, 0);
			Direction = Vector.Up;
			Driver = driver;
		}
	}
}
