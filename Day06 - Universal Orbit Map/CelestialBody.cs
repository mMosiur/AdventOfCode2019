namespace AdventOfCode.Year2019.Day06;

public class CelestialBody
{
	public string Name { get; set; }
	public CelestialBody? OrbitedBody { get; set; }
	private int _distanceToCenter;
	public int DistanceToCenter
	{
		get
		{
			if (_distanceToCenter < 0)
			{
				_distanceToCenter = OrbitedBody is null ? 0 : OrbitedBody.DistanceToCenter + 1;
			}
			return _distanceToCenter;
		}
	}
	public CelestialBody(string name)
	{
		Name = name;
		OrbitedBody = null;
		_distanceToCenter = -1;
	}
}
