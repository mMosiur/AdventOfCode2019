using System;
using System.Collections;
using System.Collections.Generic;

namespace Day08
{
	public class SpaceImageLayer : IEnumerable<char>
	{
		private readonly char[,] _image;

		public char this[int rowIndex, int colIndex]
		{
			get => _image[rowIndex, colIndex];
			set => _image[rowIndex, colIndex] = value;
		}

		public SpaceImageLayer(Span<char> rawData, int imageWidth, int imageHeight)
		{
			if (rawData.Length != imageWidth * imageHeight)
			{
				throw new ArgumentException("Dimensions do not match provided data length");
			}
			_image = new char[imageWidth, imageHeight];
			for (int r = 0; r < imageHeight; r++)
			{
				for (int c = 0; c < imageWidth; c++)
				{
					int index = r * imageWidth + c;
					_image[c, r] = rawData[index];
				}
			}
		}

		public SpaceImageLayer(char[,] image)
		{
			_image = image;
		}

		public IEnumerator<char> GetEnumerator()
		{
			foreach (char c in _image)
			{
				yield return c;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public string GetImage()
		{
			string result = "";
			int imageWidth = _image.GetLength(0);
			int imageHeight = _image.GetLength(1);
			for (int r = 0; r < imageHeight; r++)
			{
				for (int c = 0; c < imageWidth; c++)
				{
					result += _image[c, r] switch
					{
						'0' => '░',
						'1' => '█',
						'2' => ' ',
						_ => throw new InvalidOperationException(),
					};
				}
				result = result[0..^1] + Environment.NewLine;
			}
			return result.Substring(0, result.Length - Environment.NewLine.Length);
		}
	}
}
