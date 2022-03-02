using System;
using System.Linq;
using AdventOfCode.Abstractions;

namespace AdventOfCode.Year2019.Day08;

public class Day08Solver : DaySolver
{
	const int IMAGE_WIDTH = 25;
	const int IMAGE_HEIGHT = 6;

	private SpaceImage _image;

	public Day08Solver(string inputFilePath) : base(inputFilePath)
	{
		char[] encodedImage = Input.Trim().ToCharArray();
		_image = new(encodedImage, IMAGE_WIDTH, IMAGE_HEIGHT);
	}

	public override string SolvePart1()
	{
		int min = int.MaxValue;
		int minIndex = -1;
		for (int i = 0; i < _image.Layers.Count; i++)
		{
			int count = _image.Layers[i].Count(c => c == '0');
			if (count < min)
			{
				min = count;
				minIndex = i;
			}
		}
		int ones = _image.Layers[minIndex].Count(c => c == '1');
		int twos = _image.Layers[minIndex].Count(c => c == '2');
		int part1 = ones * twos;
		return part1.ToString();
	}

	public override string SolvePart2()
	{
		SpaceImageLayer visibleImage = _image.GetVisibleImage();
		return visibleImage.GetImage();
	}
}
