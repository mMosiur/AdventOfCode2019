using System.Collections.Generic;

namespace Day18;

public interface INode
{
	public IDictionary<INode, int> Neighbors { get; }
	public Point Point { get; }
}
