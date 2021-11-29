using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08;

public class SpaceImage
{
	public IList<SpaceImageLayer> Layers { get; }

	public int ImageWidth { get; }
	public int ImageHeight { get; }

	public SpaceImage(char[] rawData, int imageWidth, int imageHeight)
	{
		ImageWidth = imageWidth;
		ImageHeight = imageHeight;
		int layerSize = imageWidth * imageHeight;
		int layerCount = rawData.Length / layerSize;
		Layers = Enumerable.Range(0, layerCount)
			.Select(l => l * layerSize)
			.Select(l => new SpaceImageLayer(rawData.AsSpan(l, layerSize), imageWidth, imageHeight))
			.ToList();
	}

	public SpaceImageLayer GetVisibleImage()
	{
		char[,] image = new char[ImageWidth, ImageHeight];
		for (int r = 0; r < ImageHeight; r++)
		{
			for (int c = 0; c < ImageWidth; c++)
			{
				foreach (SpaceImageLayer layer in Layers)
				{
					if (layer[c, r] != '2')
					{
						image[c, r] = layer[c, r];
						break;
					}
				}
			}
		}
		return new SpaceImageLayer(image);
	}
}
