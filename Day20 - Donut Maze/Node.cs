using System.Collections.Generic;

namespace Day20;

internal class Node
{
	public IDictionary<Node, LevelStep> Neighbors { get; }

	public Node()
	{
		Neighbors = new Dictionary<Node, LevelStep>();
	}
}
