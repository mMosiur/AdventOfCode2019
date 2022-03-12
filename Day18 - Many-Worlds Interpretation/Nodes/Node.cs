using System.IO;

namespace AdventOfCode.Year2019.Day18;

public static class Node
{
	public static INode? FromChar(Point point, char symbol) => symbol switch
	{
		'#' => null,
		'.' => new PassageNode(point),
		'@' => new VaultNode(point),
		>= 'a' and <= 'z' => new KeyNode(point, symbol),
		>= 'A' and <= 'Z' => new DoorNode(point, symbol),
		_ => throw new InvalidDataException($"Unrecognized symbol '{symbol}' to create node from.")
	};
}
