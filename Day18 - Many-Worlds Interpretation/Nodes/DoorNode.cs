using System;
using System.Collections.Generic;

namespace Day18;

public class DoorNode : INode
{
	private KeyNode? _matchingKeyNode;
	public KeyNode MatchingKeyNode
	{
		get => _matchingKeyNode ?? throw new InvalidOperationException($"Matching key node for {this} was not set.");
		set => _matchingKeyNode = value;
	}

	public IDictionary<INode, int> Neighbors { get; }

	public char Letter { get; }

	public Point Point { get; }

	public DoorNode(Point point, char letter)
	{
		Point = point;
		if (!char.IsUpper(letter))
		{
			throw new ArgumentException("Door letter should be an uppercase letter.", nameof(letter));
		}
		Neighbors = new Dictionary<INode, int>();
		Letter = letter;
	}

	public override string ToString() => $"Door '{Letter}' at {Point}";
}
