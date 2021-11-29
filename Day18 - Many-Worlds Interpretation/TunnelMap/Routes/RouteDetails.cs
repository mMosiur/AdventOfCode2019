using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18;

public struct RouteDetails
{
	public INode Start { get; }
	public INode End { get; }
	public int Distance { get; }
	public IList<INode> Route { get; }

	public RouteDetails(INode start, INode end, int distance, IList<INode> route)
	{
		Start = start;
		End = end;
		Distance = distance;
		Route = route;
	}

	public static RouteDetails StartRoute(INode node)
	{
		return new RouteDetails(node, node, 0, new List<INode>());
	}

	public RouteDetails ExtendedTo(INode newEnd)
	{
		if (!End.Neighbors.TryGetValue(newEnd, out int additionalDistance))
		{
			throw new InvalidOperationException();
		}
		IList<INode> newRoute = Route.ToList();
		if (!ReferenceEquals(Start, End))
		{
			newRoute.Add(End);
		}
		return new RouteDetails(Start, newEnd, Distance + additionalDistance, newRoute);
	}

	public bool IsPassable(IReadOnlySet<KeyNode> keys) => Route.All(
		routeNode => routeNode switch
		{
			KeyNode keyNode => keys.Contains(keyNode),
			DoorNode doorNode => keys.Contains(doorNode.MatchingKeyNode),
			_ => true
		}
	);
}
