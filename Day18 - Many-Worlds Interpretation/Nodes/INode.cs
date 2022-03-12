using System.Collections.Generic;

namespace AdventOfCode.Year2019.Day18;

public interface INode
{
	public IDictionary<INode, int> Neighbors { get; }
	public Point Point { get; }
}
