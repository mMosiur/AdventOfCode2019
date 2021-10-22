using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
	public struct State : IEquatable<State>
	{
		public IReadOnlySet<INode> Positions { get; }
		public IReadOnlySet<KeyNode> Keys { get; }

		public State(IReadOnlySet<INode> positions, IReadOnlySet<KeyNode> keys)
		{
			Positions = positions;
			Keys = keys;
		}

		public State(IReadOnlySet<INode> positions)
			: this(positions, new HashSet<KeyNode>())
		{
		}

		public override int GetHashCode()
		{
			int seed = typeof(State).GetHashCode();
			int positionsHash = Positions.Aggregate(seed, (acc, next) => acc ^= next.GetHashCode());
			int keysHash = Keys.Aggregate(seed, (acc, next) => acc ^= next.GetHashCode());
			return HashCode.Combine(positionsHash, keysHash);
		}

		public bool Equals(State other)
		{
			return Positions.SetEquals(other.Positions) && Keys.SetEquals(other.Keys);
		}

		public override bool Equals(object? obj) => obj is State state && Equals(state);

		public static bool operator ==(State left, State right) => left.Equals(right);

		public static bool operator !=(State left, State right) => !(left == right);
	}
}
