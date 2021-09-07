using System;

namespace Day14
{
	public struct Chemical : IEquatable<Chemical>, IComparable<Chemical>
	{
		public string Name { get; set; }

		public Chemical(string name)
		{
			Name = name;
		}

		public override string ToString() => Name;

		public override bool Equals(object? obj) => obj is Chemical chem && Equals(chem);

		public bool Equals(Chemical other) => Name == other.Name;

		public static bool operator ==(Chemical left, Chemical right) => left.Equals(right);

		public static bool operator !=(Chemical left, Chemical right) => !(left == right);

		public override int GetHashCode() => HashCode.Combine(typeof(Chemical), Name);

		public int CompareTo(Chemical other) => Name.CompareTo(other.Name);
	}
}
