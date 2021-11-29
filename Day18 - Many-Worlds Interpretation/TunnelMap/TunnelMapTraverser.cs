using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Day18;

public class TunnelMapRobots
{
	private readonly TunnelMap _map;
	private readonly Lazy<RoutesDictionary> _routes;
	private readonly IReadOnlySet<KeyNode> _allKeys;

	public RoutesDictionary Routes => _routes.Value;

	public TunnelMapRobots(TunnelMap map)
	{
		_map = map;
		_allKeys = _map.Nodes.OfType<KeyNode>().ToHashSet();
		_routes = new Lazy<RoutesDictionary>(() => RoutesDictionary.Build(_map));
	}

	private IDictionary<State, int> MoveRobots(IDictionary<State, int> currentRobots)
	{
		IDictionary<State, int> nextStateDistances = new Dictionary<State, int>();
		foreach ((State currentState, int currentDistance) in currentRobots)
		{
			foreach (KeyNode missingKeyNode in _allKeys.Except(currentState.Keys))
			{
				foreach (INode position in currentState.Positions)
				{
					if (!Routes[position].TryGetValue(missingKeyNode, out RouteDetails routeDetails)
						|| !routeDetails.IsPassable(currentState.Keys))
					{
						continue;
					}
					int newDistance = currentDistance + routeDetails.Distance;
					IReadOnlySet<KeyNode> newKeys = currentState.Keys.Append(missingKeyNode).ToHashSet();
					ISet<INode> newLocations = currentState.Positions.ToHashSet();
					newLocations.Remove(position);
					newLocations.Add(missingKeyNode);
					State newState = new((IReadOnlySet<INode>)newLocations, newKeys);
					if (!nextStateDistances.TryGetValue(newState, out int value) || newDistance < value)
					{
						nextStateDistances[newState] = newDistance;
					}
				}
			}
		}
		return nextStateDistances;
	}

	public int ShortestRouteToAllKeys()
	{
		State initialState = new(_map.VaultNodes.Cast<INode>().ToHashSet());
		IDictionary<State, int> currentRobots = new Dictionary<State, int> { { initialState, 0 } };
		IDictionary<State, int> nextRobots = MoveRobots(currentRobots);
		while (nextRobots.Count > 0)
		{
			currentRobots = nextRobots;
			nextRobots = MoveRobots(currentRobots);
		}
		return currentRobots.Values.Min();
	}
}
