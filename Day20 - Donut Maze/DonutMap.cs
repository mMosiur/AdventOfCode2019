using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20;

internal class DonutMap
{
	private const int DistanceBetweenNodes = 1;

	public Node Start { get; }
	public Node End { get; }
	public IReadOnlyCollection<Node> Nodes { get; }

	private DonutMap(Node start, Node end, IReadOnlyCollection<Node> nodes)
	{
		Start = start;
		End = end;
		Nodes = nodes;
	}

	public static DonutMap BuildFromCharMap(char[,] map)
	{
		const char PathNodeChar = '.';
		Node?[,] nodeMap = new Node?[map.GetLength(0), map.GetLength(1)];
		// Generate path nodes and find portal positions
		PortalsBuilder portalsBuilder = new(startLabel: "AA", endLabel: "ZZ");
		for (int x = 0; x < map.GetLength(0); x++)
		{
			for (int y = 0; y < map.GetLength(1); y++)
			{
				if (map[x, y] == PathNodeChar)
					nodeMap[x, y] = new Node();
				else if (!char.IsLetter(map[x, y]))
					continue;
				else if (char.IsLetter(map.GetOrDefault(x, y + 1)) && map.GetOrDefault(x, y - 1).Equals(PathNodeChar))
					portalsBuilder.AddPortalNodePosition($"{map[x, y]}{map[x, y + 1]}", x, y - 1);
				else if (char.IsLetter(map.GetOrDefault(x, y - 1)) && map.GetOrDefault(x, y + 1).Equals(PathNodeChar))
					portalsBuilder.AddPortalNodePosition($"{map[x, y - 1]}{map[x, y]}", x, y + 1);
				else if (char.IsLetter(map.GetOrDefault(x - 1, y)) && map.GetOrDefault(x + 1, y).Equals(PathNodeChar))
					portalsBuilder.AddPortalNodePosition($"{map[x - 1, y]}{map[x, y]}", x + 1, y);
				else if (char.IsLetter(map.GetOrDefault(x + 1, y)) && map.GetOrDefault(x - 1, y).Equals(PathNodeChar))
					portalsBuilder.AddPortalNodePosition($"{map[x, y]}{map[x + 1, y]}", x - 1, y);
				// else the letter is a further part of the two-letter portal label
			}
		}
		(Node start, Node end) = portalsBuilder.Build(nodeMap);
		// Connect horizontal neighbors
		for (int row = 0; row < nodeMap.GetLength(0); row++)
		{
			for (int col = 1; col < nodeMap.GetLength(1); col++)
			{
				Node? nodeUp = nodeMap[row, col - 1];
				Node? nodeDown = nodeMap[row, col];
				if (nodeUp is null || nodeDown is null) continue;
				nodeUp.Neighbors.Add(nodeDown, LevelStep.Same);
				nodeDown.Neighbors.Add(nodeUp, LevelStep.Same);
			}
		}
		// Connect vertical neighbors
		for (int col = 0; col < nodeMap.GetLength(1); col++)
		{
			for (int row = 1; row < nodeMap.GetLength(0); row++)
			{
				Node? nodeLeft = nodeMap[row - 1, col];
				Node? nodeRight = nodeMap[row, col];
				if (nodeLeft is null || nodeRight is null) continue;
				nodeLeft.Neighbors.Add(nodeRight, LevelStep.Same);
				nodeRight.Neighbors.Add(nodeLeft, LevelStep.Same);
			}
		}
		return new DonutMap(start, end, nodeMap.Cast<Node>().Where(n => n != null).ToList());
	}

	public int ShortestPathPortals()
	{
		const int startingDistance = 0;
		Queue<(Node Node, int Distance)> queue = new();
		queue.Enqueue((Start, startingDistance));
		Dictionary<Node, int> visited = new();
		while (queue.TryDequeue(out (Node Node, int Distance) current))
		{
			if (ReferenceEquals(current.Node, End))
				return current.Distance;
			foreach (Node neighborNode in current.Node.Neighbors.Keys)
			{
				int newDistance = current.Distance + DistanceBetweenNodes;
				if (visited.TryGetValue(neighborNode, out int previousDistance) && previousDistance <= newDistance)
					continue;
				queue.Enqueue((neighborNode, newDistance));
				visited[neighborNode] = newDistance;
			}
		}
		throw new InvalidOperationException("No path in 'portals' was found");
	}

	public int ShortestPathRecursiveSpaces()
	{
		const int startingDistance = 0;
		const int startingLevel = 0;
		Queue<(Node Node, int Distance, int Level)> queue = new();
		queue.Enqueue((Start, startingDistance, startingLevel));
		Dictionary<(Node Node, int Level), int /* Distance */> visited = new();
		while (queue.TryDequeue(out (Node Node, int Distance, int Level) current))
		{
			if (ReferenceEquals(current.Node, End) && current.Level == startingLevel)
				return current.Distance;
			foreach ((Node neighborNode, LevelStep step) in current.Node.Neighbors)
			{
				int newLevel = current.Level + (int)step;
				if (newLevel < startingLevel) continue; // Can't go out of the outer recursive Donut
				int newDistance = current.Distance + DistanceBetweenNodes;
				if (visited.TryGetValue((neighborNode, newLevel), out int previousDistance) && previousDistance <= newDistance)
					continue;
				queue.Enqueue((neighborNode, newDistance, newLevel));
				visited[(neighborNode, newLevel)] = newDistance;
			}
		}
		throw new InvalidOperationException("No path in 'recursive spaces' was found");
	}
}
