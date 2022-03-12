using System.Collections.Generic;

namespace AdventOfCode.Year2019.Day18;

public class VaultNode : INode
{
	public IDictionary<INode, int> Neighbors { get; }

	public Point Point { get; }

	public VaultNode(Point point)
	{
		Point = point;
		Neighbors = new Dictionary<INode, int>();
	}

	public override string ToString() => $"Vault at {Point}";
}
