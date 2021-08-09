using System;

namespace Day10
{
	public abstract class MapSpot
	{
		public static MapSpot NewMapSpot(MapSpotType type, int column, int row)
		{
			return type switch
			{
				MapSpotType.Empty => new EmptySpot { Column = column, Row = row },
				MapSpotType.Asteroid => new Asteroid { Column = column, Row = row },
				_ => throw new InvalidOperationException($"Unknown {nameof(MapSpotType)}: {type}")
			};
		}
		public abstract MapSpotType Type { get; }

		public int Row { get; init; }
		public int Column { get; init; }
	}
}
