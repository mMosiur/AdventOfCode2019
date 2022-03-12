using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2019.Day10;

public class Laser
{
	private readonly Map _map;
	private readonly MapSpot _spot;

	public Laser(Map map, MapSpot spot)
	{
		_map = map ?? throw new System.ArgumentNullException(nameof(map));
		_spot = spot ?? throw new System.ArgumentNullException(nameof(spot));
	}

	public IEnumerable<Asteroid> VaporizedAsteroids(bool counterclockwise = false)
	{
		var radialMap = _map.GetRadialMap(_spot);
		int asteroidsLeft = _map.Asteroids.Count();
		int offset = 0;
		var laserMovement = counterclockwise ? radialMap.Values.Reverse() : radialMap.Values;
		while (asteroidsLeft > 0)
		{
			foreach (var list in laserMovement)
			{
				if (offset < list.Count)
				{
					yield return list.Values[offset];
					asteroidsLeft--;
				}
			}
			offset++;
		}
	}
}
