using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2019.Day18;

public class KeyNode : INode
{
	public IDictionary<INode, int> Neighbors { get; }

	public char Letter { get; }

	public Point Point { get; }

	public KeyNode(Point point, char letter)
	{
		Point = point;
		if (!char.IsLower(letter))
		{
			throw new ArgumentException("Key letter should be a lowercase letter.", nameof(letter));
		}
		Neighbors = new Dictionary<INode, int>();
		Letter = letter;
	}

	public override string ToString() => $"Key '{Letter}' at {Point}";
}
