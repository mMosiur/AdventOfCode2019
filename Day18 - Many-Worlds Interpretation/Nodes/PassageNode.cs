using System.Collections.Generic;

namespace Day18
{
	public class PassageNode : INode
	{
		public IDictionary<INode, int> Neighbors { get; }

		public Point Point { get; }

		public PassageNode(Point point)
		{
			Point = point;
			Neighbors = new Dictionary<INode, int>();
		}

		public override string ToString() => $"Passage at {Point}";
	}
}
