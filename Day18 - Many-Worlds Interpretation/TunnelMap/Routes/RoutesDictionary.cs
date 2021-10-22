using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Day18
{
	public class RoutesDictionary : Dictionary<INode, IDictionary<INode, RouteDetails>>
	{
		private RoutesDictionary(IDictionary<INode, IDictionary<INode, RouteDetails>> routesDictionary)
			: base(routesDictionary)
		{
		}

		private static IDictionary<INode, RouteDetails> RoutesFrom(INode source)
		{
			PriorityQueue<RouteDetails, int> queue = new(routeDetails => routeDetails.Distance);
			queue.Enqueue(RouteDetails.StartRoute(source));
			ISet<INode> visited = new HashSet<INode>();
			IDictionary<INode, RouteDetails> routesFrom = new Dictionary<INode, RouteDetails>();
			while (queue.TryDequeue(out RouteDetails routeDetails))
			{
				if (visited.Contains(routeDetails.End))
				{
					if (!routesFrom.TryGetValue(routeDetails.End, out var tuple) || tuple.Distance <= routeDetails.Distance)
					{
						continue;
					}
				}
				visited.Add(routeDetails.End);
				if (routeDetails.End is KeyNode or DoorNode && routeDetails.Distance > 0)
				{
					routesFrom[routeDetails.End] = routeDetails;
				}
				foreach (INode neighbor in routeDetails.End.Neighbors.Keys)
				{
					queue.Enqueue(routeDetails.ExtendedTo(neighbor));
				}
			}
			return routesFrom;
		}

		public static RoutesDictionary Build(TunnelMap map)
		{
			IEnumerable<INode> nodes = map.Nodes.Where(n => n is KeyNode).Union(map.VaultNodes);
			return new RoutesDictionary(nodes.ToDictionary(
				keySelector: node => node,
				elementSelector: node => RoutesFrom(node)
			));
		}
	}
}
