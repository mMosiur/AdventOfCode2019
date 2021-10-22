using System.Collections.Generic;
using System.Linq;

namespace Day18
{
	public class TunnelMap
	{
		public ICollection<INode> Nodes { get; private set; }

		public ICollection<VaultNode> VaultNodes { get; }

		private static void RemoveNodeFromGraph(INode node)
		{
			IList<INode> neighbors = node.Neighbors.Keys.ToList();
			for (int i = 0; i < neighbors.Count; i++)
			{
				INode neighbor1 = neighbors[i];
				int distance1 = node.Neighbors[neighbor1];
				neighbor1.Neighbors.Remove(node);
				for (int j = i + 1; j < neighbors.Count; j++)
				{
					INode neighbor2 = neighbors[j];
					int distance2 = node.Neighbors[neighbor2];
					neighbor2.Neighbors.Remove(node);
					int newDistance = distance1 + distance2;
					if (neighbor1.Neighbors.ContainsKey(neighbor2))
					{
						if (newDistance < neighbor1.Neighbors[neighbor2])
						{
							neighbor1.Neighbors[neighbor2] = newDistance;
						}
					}
					else
					{
						neighbor1.Neighbors.Add(neighbor2, newDistance);
					}
					if (neighbor2.Neighbors.ContainsKey(neighbor1))
					{
						if (newDistance < neighbor2.Neighbors[neighbor1])
						{
							neighbor2.Neighbors[neighbor1] = newDistance;
						}
					}
					else
					{
						neighbor2.Neighbors.Add(neighbor1, newDistance);
					}
				}
			}
		}

		private static ICollection<INode> CompressNodesOnce(ICollection<INode> nodes, int maximumNumberOfNeighbors = int.MaxValue)
		{
			IList<INode> compressed = new List<INode>();
			foreach (INode node in nodes)
			{
				if (node is PassageNode && node.Neighbors.Count <= maximumNumberOfNeighbors)
				{
					RemoveNodeFromGraph(node);
				}
				else
				{
					compressed.Add(node);
				}
			}
			return compressed;
		}

		private static ICollection<INode> CompressPathNodesOnce(ICollection<INode> nodes)
		{
			return CompressNodesOnce(nodes, 2);
		}

		private static ICollection<INode> CompressNodes(ICollection<INode> nodes)
		{
			// First compress path nodes until there is no more compression happening
			// Compressing path nodes is much faster speeds compression up
			ICollection<INode> compressed = CompressPathNodesOnce(nodes);
			while (compressed.Count < nodes.Count)
			{
				nodes = compressed;
				compressed = CompressPathNodesOnce(nodes);
			}
			// Second compress the remaining hub nodes until there is nothing more to compress
			compressed = CompressNodesOnce(nodes);
			while (compressed.Count < nodes.Count)
			{
				nodes = compressed;
				compressed = CompressNodesOnce(nodes);
			}
			return compressed;
		}

		public static TunnelMap BuildFrom(char[,] charMap, bool compress = false)
		{
			INode?[,] mapNodes = new INode?[charMap.GetLength(0), charMap.GetLength(1)];
			for (int i = 0; i < mapNodes.GetLength(0); i++)
			{
				for (int j = 0; j < mapNodes.GetLength(1); j++)
				{
					Point p = new(i, j);
					mapNodes[i, j] = Node.FromChar(p, charMap[i, j]);
				}
			}
			ICollection<VaultNode> vaultNodes = mapNodes.OfType<VaultNode>().ToList();
			// Connect horizontal neighbors
			for (int i = 0; i < mapNodes.GetLength(0); i++)
			{
				for (int j = 1; j < mapNodes.GetLength(1); j++)
				{
					INode? nodeUp = mapNodes[i, j - 1];
					INode? nodeDown = mapNodes[i, j];
					if (nodeUp is null || nodeDown is null) continue;
					nodeUp.Neighbors.Add(nodeDown, 1);
					nodeDown.Neighbors.Add(nodeUp, 1);
				}
			}
			// Connect vertical neighbors
			for (int j = 0; j < mapNodes.GetLength(1); j++)
			{
				for (int i = 1; i < mapNodes.GetLength(0); i++)
				{
					INode? nodeLeft = mapNodes[i - 1, j];
					INode? nodeRight = mapNodes[i, j];
					if (nodeLeft is null || nodeRight is null) continue;
					nodeLeft.Neighbors.Add(nodeRight, 1);
					nodeRight.Neighbors.Add(nodeLeft, 1);
				}
			}
			ICollection<INode> nodes = mapNodes.Cast<INode?>().Where(n => n is not null).Cast<INode>().ToList();
			IDictionary<char, KeyNode> keyNodes = mapNodes.OfType<KeyNode>().ToDictionary(n => n.Letter);
			foreach (DoorNode doorNode in mapNodes.OfType<DoorNode>())
			{
				doorNode.MatchingKeyNode = keyNodes[char.ToLower(doorNode.Letter)];
			}
			TunnelMap map = new(nodes, vaultNodes);
			if (compress)
			{
				map.Compress();
			}
			return map;
		}

		private void Compress()
		{
			Nodes = CompressNodes(Nodes);
			foreach (VaultNode vaultNode in VaultNodes)
			{
				RemoveNodeFromGraph(vaultNode);
				Nodes.Remove(vaultNode);
			}
		}

		private TunnelMap(ICollection<INode> nodes, ICollection<VaultNode> vaultNodes)
		{
			Nodes = nodes;
			VaultNodes = vaultNodes;
		}
	}
}
