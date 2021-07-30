using System;
using System.IO;
using System.Linq;

namespace Day08
{
	class Program
	{
		const string INPUT_FILEPATH = "input.txt";
		const int IMAGE_WIDTH = 25;
		const int IMAGE_HEIGHT = 6;

		static void Main()
		{
			char[] encodedImage = File.ReadAllText(INPUT_FILEPATH).Trim().ToCharArray();
			SpaceImage image = new(encodedImage, IMAGE_WIDTH, IMAGE_HEIGHT);
			int min = int.MaxValue;
			int minIndex = -1;
			for (int i = 0; i < image.Layers.Count; i++)
			{
				int count = image.Layers[i].Count(c => c == '0');
				if (count < min)
				{
					min = count;
					minIndex = i;
				}
			}
			int ones = image.Layers[minIndex].Count(c => c == '1');
			int twos = image.Layers[minIndex].Count(c => c == '2');
			int part1 = ones * twos;
			Console.WriteLine($"Part 1: {part1}");

			Console.WriteLine("Part 2 message:");
			SpaceImageLayer visibleImage = image.GetVisibleImage();
			Console.WriteLine(visibleImage.GetImage());
		}
	}
}
