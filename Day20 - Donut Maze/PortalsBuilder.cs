using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20;

internal class PortalsBuilder
{
	private readonly Dictionary<string, List<(int X, int Y)>> _portalNodePositions = new();
	private readonly string _startLabel;
	private readonly string _endLabel;

	public PortalsBuilder(string startLabel, string endLabel)
	{
		_startLabel = startLabel;
		_endLabel = endLabel;
	}

	public void AddPortalNodePosition(string label, int x, int y)
	{
		if (!_portalNodePositions.TryGetValue(label, out List<(int X, int Y)>? nodes))
		{
			nodes = new List<(int X, int Y)>(2);
			_portalNodePositions.Add(label, nodes);
		}
		nodes.Add((x, y));
	}

	public (Node Start, Node End) Build(Node?[,] nodeMap)
	{
		// Retrieve start node
		if (!_portalNodePositions.TryGetValue(_startLabel, out var startNodes))
		{
			throw new InvalidOperationException("A start node was not found.");
		}
		if (startNodes.Count > 1)
		{
			throw new InvalidOperationException("Multiple start nodes found.");
		}
		(int startIndexX, int startIndexY) = startNodes.Single();
		Node start = nodeMap[startIndexX, startIndexY]
			?? throw new InvalidOperationException("The start node was not found in the given position.");
		_ = _portalNodePositions.Remove(_startLabel);
		// Retrieve end node
		if (!_portalNodePositions.TryGetValue(_endLabel, out var endNodes))
		{
			throw new InvalidOperationException("An end node was not found.");
		}
		if (endNodes.Count > 1)
		{
			throw new InvalidOperationException("Multiple end nodes found.");
		}
		(int endNodeX, int endNodeY) = endNodes.Single();
		Node end = nodeMap[endNodeX, endNodeY]
			?? throw new InvalidOperationException("The end node was not found in the given position.");
		_ = _portalNodePositions.Remove(_endLabel);
		// Connect portals
		foreach (List<(int X, int Y)> nodeIndexes in _portalNodePositions.Values)
		{
			if (nodeIndexes.Count != 2)
			{
				throw new InvalidOperationException("A portal should be between exactly two nodes");
			}
			(int x1, int y1) = nodeIndexes[0];
			(int x2, int y2) = nodeIndexes[1];
			Node node1 = nodeMap[x1, y1]
				?? throw new InvalidOperationException("A node was not found in the given position.");
			Node node2 = nodeMap[x2, y2]
				?? throw new InvalidOperationException("A node was not found in the given position.");
			int height = nodeMap.GetLength(0);
			int width = nodeMap.GetLength(1);
			(double xCenter, double yCenter) = (height / 2d, width / 2d);
			double dist1 = Math.Sqrt(SimpleMath.Squared(x1 - xCenter) + SimpleMath.Squared(y1 - yCenter));
			double dist2 = Math.Sqrt(SimpleMath.Squared(x2 - xCenter) + SimpleMath.Squared(y2 - yCenter));
			int step = Math.Sign(dist2 - dist1);
			node1.Neighbors.Add(node2, (LevelStep)step);
			node2.Neighbors.Add(node1, (LevelStep)(-step));
		}
		return (start, end);
	}
}
