namespace Day13;

public enum Tile
{
	/// <summary>
	/// Empty tile - no game object appears in this tile.
	/// </summary>
	Empty = 0,

	/// <summary>
	/// Wall tile - walls are indestructible barriers.
	/// </summary>
	Wall = 1,

	/// <summary>
	/// Block tile - blocks can be broken by the ball.
	/// </summary>
	Block = 2,

	/// <summary>
	/// Horizontal paddle tile - the paddle is indestructible.
	/// </summary>
	HorizontalPaddle = 3,

	/// <summary>
	/// Ball tile - the ball moves diagonally and bounces off objects.
	/// </summary>
	Ball = 4
}
