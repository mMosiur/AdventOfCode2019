namespace Day06
{
	public class CelestialBody
	{
		public string Name { get; set; }
		public CelestialBody OrbitedBody { get; set; }
		private int _distanceToCenter;
		public int DistanceToCenter
		{
			get
			{
				if(_distanceToCenter < 0)
				{
					if(OrbitedBody is null)
					{
						_distanceToCenter = 0;
					}
					else
					{
						_distanceToCenter = OrbitedBody.DistanceToCenter + 1;
					}
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
}
