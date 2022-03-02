using System.Collections.Generic;

namespace AdventOfCode.Year2019.Day12;

public class Moon
{
	public Position3D Position { get; private set; }
	public Vector3D Velocity { get; private set; }

	public int TotalEnergy => PotentialEnergy * KineticEnergy;
	public int PotentialEnergy => Position.Magnitude;
	public int KineticEnergy => Velocity.Magnitude;

	public void ApplyGravity(Vector3D gravity)
	{
		Velocity += gravity;
	}

	public void ApplyVelocity()
	{
		Position += Velocity;
	}

	public Moon(Position3D position) : this(position, Vector3D.Zero) { }

	public Moon(Position3D position, Vector3D velocity)
	{
		Position = position;
		Velocity = velocity;
	}

	public Vector3D GravityTowards(Moon other)
	{
		Comparer<int> comparer = Comparer<int>.Default;
		return new Vector3D
		{
			X = comparer.Compare(other.Position.X, Position.X),
			Y = comparer.Compare(other.Position.Y, Position.Y),
			Z = comparer.Compare(other.Position.Z, Position.Z)
		};
	}
}
