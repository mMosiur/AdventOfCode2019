namespace Day13
{
	public static class TileExtensions
	{
		public static char ToChar(this Tile tile) => tile switch
		{
			Tile.Empty => ' ',
			Tile.Wall => '█',
			Tile.Block => '■',
			Tile.HorizontalPaddle => '―',
			Tile.Ball => '○',
			_ => '�'
		};
	}
}
