using System.Collections.Generic;

namespace AdventOfCode.Year2019.Day20;

public class Node
{
	public IDictionary<Node, LevelStep> Neighbors { get; }

	public Node()
	{
		Neighbors = new Dictionary<Node, LevelStep>();
	}
}
